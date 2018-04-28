using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    using BaseDeDados;
    using View;

    public partial class FormaDePagamentoModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO forma_de_pagamento
                                ( descricao, ativo)
                            OUTPUT inserted.id_forma_pagamento
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
                var cmd = @"UPDATE forma_de_pagamento SET
	                            descricao             = @descricao,
	                            ativo	              = @ativo
                            WHERE
	                            id_forma_pagamento = @id";

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
	                            forma_de_pagamento
                            WHERE
	                            id_forma_pagamento = @id";

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

        public static List<FormaDePagamentoModel> Pesquisar(string pesquisa)
        {
            var lista = new List<FormaDePagamentoModel>();

            try
            {
                var cmd = $@"SELECT TOP 50
	                            id_forma_pagamento,
	                            descricao,
	                            ativo
                             FROM
	                            forma_de_pagamento
                             WHERE
	                            ativo = 1
	                            AND
	                            descricao LIKE @pesquisa
                            ORDER BY
                                id_forma_pagamento";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("pesquisa", $"%{pesquisa}%");
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                    lista.Add(new FormaDePagamentoModel
                    {
                        id = int.Parse(Conexao.Leitor["id_forma_pagamento"].ToString()),
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

        public static List<FormaDePagamentoModel> CarregarTodos()
        {
            return Pesquisar("");
        }

        #endregion
    }
}
