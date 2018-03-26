using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    using Funcoes;
    using BaseDeDados;

    public partial class PagamentoModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {

            try
            {
                var cmd = @"INSERT INTO pagamento
	                            ( id_caixa,  id_consulta,  id_forma_pagamento,  data,  valor,  qtd_parcela,  ativo)
                            OUTPUT inserted.id_pagamento
                            VALUES
	                            (@id_caixa, @id_consulta, @id_forma_pagamento, @data, @valor, @qtd_parcela, @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id_consulta", idConsulta);
                Conexao.Cmd.Parameters.AddWithValue("id_caixa", caixa?.id);
                Conexao.Cmd.Parameters.AddWithValue("id_forma_pagamento", formaDePagamento?.id);
                Conexao.Cmd.Parameters.AddWithValue("data", data);
                Conexao.Cmd.Parameters.AddWithValue("valor", valor);
                Conexao.Cmd.Parameters.AddWithValue("qtd_parcela", qtdParcelas);
                Conexao.Cmd.Parameters.AddWithValue("ativo", true);

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
                var cmd = @"UPDATE pagamento SET
	                            id_caixa				= @id_caixa,
	                            id_consulta				= @id_consulta,
	                            id_forma_pagamento		= @id_forma_pagamento,
	                            data					= @data,
	                            valor					= @valor,
	                            qtd_parcela				= @qtd_parcela,
	                            ativo					= @ativo
                            WHERE
	                            id_pagamento			= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);
                Conexao.Cmd.Parameters.AddWithValue("id_consulta", idConsulta);
                Conexao.Cmd.Parameters.AddWithValue("id_caixa", caixa?.id);
                Conexao.Cmd.Parameters.AddWithValue("id_forma_pagamento", formaDePagamento?.id);
                Conexao.Cmd.Parameters.AddWithValue("data", data);
                Conexao.Cmd.Parameters.AddWithValue("valor", valor);
                Conexao.Cmd.Parameters.AddWithValue("qtd_parcela", qtdParcelas);
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

        public void Carregar()
        {
            try
            {
                var cmd = @"SELECT
	                            id_caixa,
	                            id_consulta,
	                            id_forma_pagamento,
	                            data,
	                            valor,
	                            qtd_parcela,
	                            ativo
                            FROM
	                            pagamento
                            WHERE
	                            id_pagamento	= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    idConsulta = int.Parse(Conexao.Leitor["id_consulta"].ToString());
                    caixa = new CaixaModel { id = int.Parse(Conexao.Leitor["id_caixa"].ToString()) };
                    formaDePagamento = new FormaDePagamentoModel { id = int.Parse(Conexao.Leitor["id_forma_pagamento"].ToString()) };
                    data = DateTime.Parse(Conexao.Leitor["data"].ToString());
                    valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
                    qtdParcelas = int.Parse(Conexao.Leitor["qtd_parcela"].ToString());
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

        public void Remover()
        {
            ativo = false;
            Atualizar();
        }

        #endregion

        public static List<PagamentoModel> CarregarConsulta(int idConsulta)
        {
            var lista = new List<PagamentoModel>();

            try
            {
                var cmd = @"SELECT 
	                            id_pagamento,
	                            id_caixa,
	                            id_forma_pagamento,
	                            data, 
	                            valor,
	                            qtd_parcela, 
	                            ativo
                            FROM 
	                            pagamento
                            WHERE
	                            id_consulta = @id
	                            AND
	                            ativo = 1";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", idConsulta);
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                    lista.Add(new PagamentoModel
                    {
                        id = int.Parse(Conexao.Leitor["id_pagamento"].ToString()),
                        idConsulta = idConsulta,

                        caixa = new CaixaModel
                        {
                            id = int.Parse(Conexao.Leitor["id_caixa"].ToString())
                        },
                        formaDePagamento = new FormaDePagamentoModel
                        {
                            id = int.Parse(Conexao.Leitor["id_forma_pagamento"].ToString())
                        },
                        data = DateTime.Parse(Conexao.Leitor["data"].ToString()),
                        valor = decimal.Parse(Conexao.Leitor["valor"].ToString()),
                        qtdParcelas = int.Parse(Conexao.Leitor["qtd_parcela"].ToString()),
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

            lista.ForEach(x => x.formaDePagamento.Carregar());

            return lista;
        }
    }
}
