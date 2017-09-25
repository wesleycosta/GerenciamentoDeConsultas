using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    using View;
    using BaseDeDados;

    public partial class CargoModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO cargo
                                ( descricao, ativo)
                            OUTPUT inserted.id_cargo
                            VALUES
                                (@descricao, @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("descricao", descricao);
                Conexao.Cmd.Parameters.AddWithValue("ativo", ativo);

                id = (int)Conexao.Cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
            }
            finally
            {
                Conexao.FecharConexao();
            }
        }

        public void Atualizar()
        {
            try
            {
                var cmd = @"UPDATE cargo SET
	                            descricao = @descricao,
	                            ativo	  = @ativo
                            WHERE
	                            id_cargo  = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("descricao", descricao);
                Conexao.Cmd.Parameters.AddWithValue("ativo", ativo);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
            }
            finally
            {
                Conexao.FecharConexao();
            }
        }

        public void Remover()
        {
            ativo = false;
            Atualizar();
        }

        public void Carregar()
        {
            try
            {
                var cmd = @"SELECT
	                            descricao,
	                            ativo
                            FROM
	                            cargo
                            WHERE
	                            id_cargo = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    descricao = Conexao.Leitor["descricao"].ToString();
                    ativo = bool.Parse(Conexao.Leitor["ativo"].ToString());
                }
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
            }
            finally
            {
                Conexao.FecharConexao();
            }
        }

        #endregion

        #region CARREGAR LISTA

        public static List<CargoModel> Pesquisar(string pesquisa)
        {
            var lista = new List<CargoModel>();

            try
            {
                var cmd = $@"SELECT 
	                            id_cargo,
	                            descricao,
	                            ativo
                             FROM
	                            cargo
                             WHERE
	                            ativo = 1
	                            AND
	                            descricao LIKE @pesquisa
                            ORDER BY
                                id_cargo";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("pesquisa", $"%{pesquisa}%");
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                    lista.Add(new CargoModel
                    {
                        id = int.Parse(Conexao.Leitor["id_cargo"].ToString()),
                        descricao = Conexao.Leitor["descricao"].ToString(),
                        ativo = bool.Parse(Conexao.Leitor["ativo"].ToString())
                    });
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
            }
            finally
            {
                Conexao.FecharConexao();
            }

            return lista;
        }

        public static List<CargoModel> CarregarTodos()
        {
            return Pesquisar("");
        }

        #endregion
    }
}