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

            //try
            //{
            //    var cmd = @"INSERT INTO consulta
            //                 (id_medico,
            //                  id_cliente, 
            //                  id_convenio, 
            //                  numero_procedimento, 
            //                  forma_de_atendimento, 
            //                  data, 
            //                  horario, 
            //                  valor, 
            //                  status_pagamento, 
            //                  tipo_de_cancelamento) 
            //                OUTPUT inserted.id_consulta
            //                VALUES
            //                 (@id_medico,
            //                  @id_cliente, 
            //                  @id_convenio, 
            //                  @numero_procedimento, 
            //                  @forma_de_atendimento, 
            //                  @data, 
            //                  @horario, 
            //                  @valor, 
            //                  @status_pagamento, 
            //                  @tipo_de_cancelamento)";

            //    Conexao.AbrirConexao();
            //    Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

            //    Conexao.Cmd.Parameters.AddWithValue("id_medico", medico.id);
            //    Conexao.Cmd.Parameters.AddWithValue("id_cliente", cliente.id);
            //    Conexao.Cmd.Parameters.AddWithValue("id_convenio", convenio?.id ?? SqlInt32.Null);
            //    Conexao.Cmd.Parameters.AddWithValue("numero_procedimento", numeroProcedimento);
            //    Conexao.Cmd.Parameters.AddWithValue("forma_de_atendimento", formaDeAtentimento);
            //    Conexao.Cmd.Parameters.AddWithValue("data", data);
            //    Conexao.Cmd.Parameters.AddWithValue("horario", horario);
            //    Conexao.Cmd.Parameters.AddWithValue("valor", valor);
            //    Conexao.Cmd.Parameters.AddWithValue("status_pagamento", statusPagamento);
            //    Conexao.Cmd.Parameters.AddWithValue("tipo_de_cancelamento", tipoDeCancelamento);

            //    id = (int)Conexao.Cmd.ExecuteScalar();
            //}
            //catch (Exception ex)
            //{
            //    Excecao.Mostrar(ex);
            //}
            //finally
            //{
            //    Conexao.FecharConexao();
            //}
        }

        public void Atualizar()
        {
            //try
            //{
            //    var cmd = @"UPDATE consulta SET
            //                 id_medico				= @id_medico,
            //                 id_cliente				= @id_cliente,
            //                 id_convenio				= @id_convenio,
            //                 numero_procedimento		= @numero_procedimento,
            //                 forma_de_atendimento	= @forma_de_atendimento,
            //                 data					= @data,
            //                 horario					= @horario,
            //                 valor					= @valor,
            //                 status_pagamento		= @status_pagamento,
            //                 tipo_de_cancelamento	= @tipo_de_cancelamento,
            //                 ativo					= @ativo
            //                WHERE
            //                 id_consulta				= @id";

            //    Conexao.AbrirConexao();
            //    Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

            //    Conexao.Cmd.Parameters.AddWithValue("id", id);
            //    Conexao.Cmd.Parameters.AddWithValue("id_medico", medico.id);
            //    Conexao.Cmd.Parameters.AddWithValue("id_cliente", cliente.id);
            //    Conexao.Cmd.Parameters.AddWithValue("id_convenio", convenio?.id ?? SqlInt32.Null);
            //    Conexao.Cmd.Parameters.AddWithValue("numero_procedimento", numeroProcedimento);
            //    Conexao.Cmd.Parameters.AddWithValue("forma_de_atendimento", formaDeAtentimento);
            //    Conexao.Cmd.Parameters.AddWithValue("data", data);
            //    Conexao.Cmd.Parameters.AddWithValue("horario", horario);
            //    Conexao.Cmd.Parameters.AddWithValue("valor", valor);
            //    Conexao.Cmd.Parameters.AddWithValue("status_pagamento", statusPagamento);
            //    Conexao.Cmd.Parameters.AddWithValue("tipo_de_cancelamento", tipoDeCancelamento);
            //    Conexao.Cmd.Parameters.AddWithValue("ativo", ativo);

            //    Conexao.Cmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    Excecao.Mostrar(ex);
            //}
            //finally
            //{
            //    Conexao.FecharConexao();

            //}
        }

        public void Carregar()
        {
            //    try
            //    {
            //        var cmd = @"SELECT
            //                     id_medico,
            //                     id_cliente,
            //                     ISNULL(id_convenio, 0) AS id_convenio,
            //                     numero_procedimento,
            //                     forma_de_atendimento,
            //                     data,
            //                        horario,
            //                     valor,
            //                     status_pagamento, 
            //                     tipo_de_cancelamento,
            //                     ativo
            //                    FROM
            //                     consulta
            //                    WHERE
            //                     id_consulta	= @id";

            //        Conexao.AbrirConexao();
            //        Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
            //        Conexao.Cmd.Parameters.AddWithValue("id", id);

            //        Conexao.Leitor = Conexao.Cmd.ExecuteReader();

            //        if (Conexao.Leitor.Read())
            //        {
            //            this.id = id;
            //            medico = new FuncionarioModel
            //            {
            //                id = int.Parse(Conexao.Leitor["id_medico"].ToString())
            //            };

            //            cliente = new ClienteModel
            //            {
            //                id = int.Parse(Conexao.Leitor["id_cliente"].ToString())
            //            };

            //            if (int.Parse(Conexao.Leitor["id_convenio"].ToString()) > 0)
            //                convenio = new ConvenioModel
            //                {
            //                    id = int.Parse(Conexao.Leitor["id_cliente"].ToString())
            //                };

            //            numeroProcedimento = Conexao.Leitor["numero_procedimento"].ToString();
            //            formaDeAtentimento = (FormaDeAtendimento)Enum.Parse(typeof(FormaDeAtendimento), Conexao.Leitor["forma_de_atendimento"].ToString());
            //            data = DateTime.Parse(Conexao.Leitor["data"].ToString());
            //            horario = TimeSpan.Parse(Conexao.Leitor["horario"].ToString());
            //            valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
            //            statusPagamento = (StatusPagamento)Enum.Parse(typeof(StatusPagamento), Conexao.Leitor["status_pagamento"].ToString());
            //            tipoDeCancelamento = (TipoDeCancelamento)Enum.Parse(typeof(TipoDeCancelamento), Conexao.Leitor["tipo_de_cancelamento"].ToString());
            //            ativo = bool.Parse(Conexao.Leitor["ativo"].ToString());
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Excecao.Mostrar(ex);
            //    }
            //    finally
            //    {
            //        Conexao.FecharConexao();
            //    }
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
