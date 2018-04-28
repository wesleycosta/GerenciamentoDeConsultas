using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    using BaseDeDados;

    public partial class MaterialModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO material	
	                            (descricao, valor, ativo)
                            OUTPUT inserted.id_material
                            VALUES
	                            (@descricao, @valor, @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("descricao", descricao);
                Conexao.Cmd.Parameters.AddWithValue("valor", valor);
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
                var cmd = @"UPDATE material SET
	                            descricao	= @descricao,
	                            valor	    = @valor,
	                            ativo		= @ativo
                            WHERE
	                            id_material = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);
                Conexao.Cmd.Parameters.AddWithValue("descricao", descricao);
                Conexao.Cmd.Parameters.AddWithValue("valor", valor);
                Conexao.Cmd.Parameters.AddWithValue("ativo", ativo);

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
                                valor,
	                            ativo
                            FROM
	                            material
                            WHERE
	                            id_material = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    descricao = Conexao.Leitor["descricao"].ToString();
                    valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
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

        public static List<MaterialModel> Pesquisar(string pesquisa)
        {
            var lista = new List<MaterialModel>();

            try
            {
                var cmd = $@"SELECT TOP 50
	                            id_material,
	                            descricao,
                                valor,
	                            ativo
                            FROM
	                            material
                             WHERE
	                            ativo = 1
	                            AND
	                            descricao LIKE @pesquisa
                            ORDER BY
                                id_material";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("pesquisa", $"%{pesquisa}%");
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                    lista.Add(new MaterialModel
                    {
                        id = int.Parse(Conexao.Leitor["id_material"].ToString()),
                        descricao = Conexao.Leitor["descricao"].ToString(),
                        valor = decimal.Parse(Conexao.Leitor["valor"].ToString()),
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

        public static List<MaterialModel> CarregarTodos() =>
            Pesquisar("");

        #endregion
    }
}
