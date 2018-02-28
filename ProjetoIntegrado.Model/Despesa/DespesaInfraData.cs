using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    using BaseDeDados;

    public partial class DespesaModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO despesa
	                            (descricao, valor, data)
                            OUTPUT inserted.id_despesa
                            VALUES
	                            (@descricao, @valor, @data)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("descricao", descricao);
                Conexao.Cmd.Parameters.AddWithValue("valor", valor);
                Conexao.Cmd.Parameters.AddWithValue("data", data);

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
                var cmd = @"UPDATE despesa SET
	                            descricao			= @descricao,
	                            valor				= @valor,
	                            data				= @data,
	                            ativo				= @ativo
                            WHERE
	                            id_despesa			= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);
                Conexao.Cmd.Parameters.AddWithValue("descricao", descricao);
                Conexao.Cmd.Parameters.AddWithValue("valor", valor);
                Conexao.Cmd.Parameters.AddWithValue("data", data);
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
	                            data,
	                            ativo
                            FROM
	                            despesa
                            WHERE
	                            id_despesa	= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    descricao = Conexao.Leitor["descricao"].ToString();
                    valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
                    data = DateTime.Parse(Conexao.Leitor["data"].ToString());
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

        public static List<DespesaModel> Pesquisar(string pesquisa, DateTime dtInicial, DateTime dtFinal)
        {
            var lista = new List<DespesaModel>();

            try
            {
                var cmd = $@"SELECT  
	                            id_despesa,
	                            descricao,
	                            valor,
	                            data,
	                            ativo
                            FROM 
	                            despesa
                            WHERE
	                            ativo		=  1
	                            AND
	                            data BETWEEN @data_inicial AND @data_final
                                AND
                                descricao LIKE @pesquisa";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("data_inicial", dtInicial.Date);
                Conexao.Cmd.Parameters.AddWithValue("data_final", dtFinal.Date);
                Conexao.Cmd.Parameters.AddWithValue("pesquisa", $"%{pesquisa}%");
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                    lista.Add(new DespesaModel
                    {
                        id = int.Parse(Conexao.Leitor["id_despesa"].ToString()),
                        descricao = Conexao.Leitor["descricao"].ToString(),
                        valor = decimal.Parse(Conexao.Leitor["valor"].ToString()),
                        data = DateTime.Parse(Conexao.Leitor["data"].ToString()),
                        ativo = true
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

        public static List<string> CarregarDiferentes()
        {
            var lista = new List<string>();

            try
            {
                var cmd = $@"SELECT  
                                DISTINCT descricao
                            FROM 
	                            despesa
                            WHERE
	                            ativo		=  1";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                    lista.Add(Conexao.Leitor["descricao"].ToString());

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

        #endregion
    }
}
