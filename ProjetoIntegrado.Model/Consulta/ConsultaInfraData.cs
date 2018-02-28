using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    using Funcoes;
    using BaseDeDados;

    public partial class ConsultaModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {

            try
            {
                var cmd = @"INSERT INTO consulta
	                            (id_medico,
	                             id_cliente, 
	                             id_convenio, 
	                             numero_procedimento, 
	                             forma_de_atendimento, 
	                             data, 
	                             horario, 
	                             valor, 
	                             status_pagamento, 
	                             tipo_de_cancelamento) 
                            OUTPUT inserted.id_consulta
                            VALUES
	                            (@id_medico,
	                             @id_cliente, 
	                             @id_convenio, 
	                             @numero_procedimento, 
	                             @forma_de_atendimento, 
	                             @data, 
	                             @horario, 
	                             @valor, 
	                             @status_pagamento, 
	                             @tipo_de_cancelamento)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id_medico", medico.id);
                Conexao.Cmd.Parameters.AddWithValue("id_cliente", cliente.id);
                Conexao.Cmd.Parameters.AddWithValue("id_convenio", convenio?.id ?? SqlInt32.Null);
                Conexao.Cmd.Parameters.AddWithValue("numero_procedimento", numeroProcedimento);
                Conexao.Cmd.Parameters.AddWithValue("forma_de_atendimento", formaDeAtentimento);
                Conexao.Cmd.Parameters.AddWithValue("data", data);
                Conexao.Cmd.Parameters.AddWithValue("horario", horario);
                Conexao.Cmd.Parameters.AddWithValue("valor", valor);
                Conexao.Cmd.Parameters.AddWithValue("status_pagamento", statusPagamento);
                Conexao.Cmd.Parameters.AddWithValue("tipo_de_cancelamento", tipoDeCancelamento);

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
                var cmd = @"UPDATE consulta SET
	                            id_medico				= @id_medico,
	                            id_cliente				= @id_cliente,
	                            id_convenio				= @id_convenio,
	                            numero_procedimento		= @numero_procedimento,
	                            forma_de_atendimento	= @forma_de_atendimento,
	                            data					= @data,
	                            horario					= @horario,
	                            valor					= @valor,
	                            status_pagamento		= @status_pagamento,
	                            tipo_de_cancelamento	= @tipo_de_cancelamento,
	                            ativo					= @ativo
                            WHERE
	                            id_consulta				= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);
                Conexao.Cmd.Parameters.AddWithValue("id_medico", medico.id);
                Conexao.Cmd.Parameters.AddWithValue("id_cliente", cliente.id);
                Conexao.Cmd.Parameters.AddWithValue("id_convenio", convenio?.id ?? SqlInt32.Null);
                Conexao.Cmd.Parameters.AddWithValue("numero_procedimento", numeroProcedimento);
                Conexao.Cmd.Parameters.AddWithValue("forma_de_atendimento", formaDeAtentimento);
                Conexao.Cmd.Parameters.AddWithValue("data", data);
                Conexao.Cmd.Parameters.AddWithValue("horario", horario);
                Conexao.Cmd.Parameters.AddWithValue("valor", valor);
                Conexao.Cmd.Parameters.AddWithValue("status_pagamento", statusPagamento);
                Conexao.Cmd.Parameters.AddWithValue("tipo_de_cancelamento", tipoDeCancelamento);
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
	                            id_medico,
	                            id_cliente,
	                            ISNULL(id_convenio, 0) AS id_convenio,
	                            numero_procedimento,
	                            forma_de_atendimento,
	                            data,
                                horario,
	                            valor,
	                            status_pagamento, 
	                            tipo_de_cancelamento,
	                            ativo
                            FROM
	                            consulta
                            WHERE
	                            id_consulta	= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    this.id = id;
                    medico = new FuncionarioModel
                    {
                        id = int.Parse(Conexao.Leitor["id_medico"].ToString())
                    };

                    cliente = new ClienteModel
                    {
                        id = int.Parse(Conexao.Leitor["id_cliente"].ToString())
                    };

                    if (int.Parse(Conexao.Leitor["id_convenio"].ToString()) > 0)
                        convenio = new ConvenioModel
                        {
                            id = int.Parse(Conexao.Leitor["id_cliente"].ToString())
                        };

                    numeroProcedimento = Conexao.Leitor["numero_procedimento"].ToString();
                    formaDeAtentimento = (FormaDeAtendimento)Enum.Parse(typeof(FormaDeAtendimento), Conexao.Leitor["forma_de_atendimento"].ToString());
                    data = DateTime.Parse(Conexao.Leitor["data"].ToString());
                    horario = TimeSpan.Parse(Conexao.Leitor["horario"].ToString());
                    valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
                    statusPagamento = (StatusPagamento)Enum.Parse(typeof(StatusPagamento), Conexao.Leitor["status_pagamento"].ToString());
                    tipoDeCancelamento = (TipoDeCancelamento)Enum.Parse(typeof(TipoDeCancelamento), Conexao.Leitor["tipo_de_cancelamento"].ToString());
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

            CarregarPagamentos();
        }

        private void CarregarPagamentos() =>
            listaDePagamentos = PagamentoModel.CarregarConsulta(id);

        public void Remover()
        {
            ativo = false;
            Atualizar();
        }

        #endregion

        #region CARREGAR DADOS

        public static List<ConsultaModel> Historio(int idCliente)
        {
            var lista = new List<ConsultaModel>();

            try
            {
                var cmd = @"SELECT
	                            id_medico,
	                            id_cliente,
	                            ISNULL(id_convenio, 0) AS id_convenio,
	                            numero_procedimento,
	                            forma_de_atendimento,
	                            data,
                                horario,
	                            valor,
	                            status_pagamento, 
	                            tipo_de_cancelamento,
	                            ativo
                            FROM
	                            consulta
                            WHERE
	                            id_cliente	= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", idCliente);
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                {
                    var obj = new ConsultaModel();

                    obj.medico = new FuncionarioModel
                    {
                        id = int.Parse(Conexao.Leitor["id_medico"].ToString())
                    };

                    obj.cliente = new ClienteModel
                    {
                        id = int.Parse(Conexao.Leitor["id_cliente"].ToString())
                    };

                    if (int.Parse(Conexao.Leitor["id_convenio"].ToString()) > 0)
                        obj.convenio = new ConvenioModel
                        {
                            id = int.Parse(Conexao.Leitor["id_cliente"].ToString())
                        };

                    obj.numeroProcedimento = Conexao.Leitor["numero_procedimento"].ToString();
                    obj.formaDeAtentimento = (FormaDeAtendimento)Enum.Parse(typeof(FormaDeAtendimento), Conexao.Leitor["forma_de_atendimento"].ToString());
                    obj.data = DateTime.Parse(Conexao.Leitor["data"].ToString());
                    obj.horario = TimeSpan.Parse(Conexao.Leitor["horario"].ToString());
                    obj.valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
                    obj.statusPagamento = (StatusPagamento)Enum.Parse(typeof(StatusPagamento), Conexao.Leitor["status_pagamento"].ToString());
                    obj.tipoDeCancelamento = (TipoDeCancelamento)Enum.Parse(typeof(TipoDeCancelamento), Conexao.Leitor["tipo_de_cancelamento"].ToString());
                    obj.ativo = bool.Parse(Conexao.Leitor["ativo"].ToString());

                    lista.Add(obj);
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

            lista.ForEach(x => x.convenio.Carregar());
            lista.ForEach(x => x.medico.Carregar());

            return lista;
        }

        public static List<ConsultaModel> CarregarEntradaCaixa(int idCaixa)
        {
            var lista = new List<ConsultaModel>();

            try
            {
                var cmd = @"SELECT DISTINCT
	                            C.id_consulta,
                                C.id_cliente,
	                            C.id_medico,
	                            C.id_convenio,
	                            C.numero_procedimento,
	                            C.forma_de_atendimento,
	                            C.data,
                                C.horario,
	                            C.valor,
	                            C.status_pagamento,
	                            C.tipo_de_cancelamento,
	                            C.ativo
                            FROM
	                            consulta C
                            INNER JOIN pagamento P 
	                            ON P.id_consulta = C.id_consulta
                            WHERE
	                            C.ativo    =  1
	                            AND
	                            P.id_caixa = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", idCaixa);
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                {
                    var obj = new ConsultaModel();

                    obj.id = int.Parse(Conexao.Leitor["id_consulta"].ToString());

                    obj.medico = new FuncionarioModel
                    {
                        id = int.Parse(Conexao.Leitor["id_medico"].ToString())
                    };

                    obj.cliente = new ClienteModel
                    {
                        id = int.Parse(Conexao.Leitor["id_cliente"].ToString())
                    };

                    if (int.Parse(Conexao.Leitor["id_convenio"].ToString()) > 0)
                        obj.convenio = new ConvenioModel
                        {
                            id = int.Parse(Conexao.Leitor["id_cliente"].ToString())
                        };

                    obj.numeroProcedimento = Conexao.Leitor["numero_procedimento"].ToString();
                    obj.formaDeAtentimento = (FormaDeAtendimento)Enum.Parse(typeof(FormaDeAtendimento), Conexao.Leitor["forma_de_atendimento"].ToString());
                    obj.data = DateTime.Parse(Conexao.Leitor["data"].ToString());
                    obj.horario = TimeSpan.Parse(Conexao.Leitor["horario"].ToString());
                    obj.valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
                    obj.statusPagamento = (StatusPagamento)Enum.Parse(typeof(StatusPagamento), Conexao.Leitor["status_pagamento"].ToString());
                    obj.tipoDeCancelamento = (TipoDeCancelamento)Enum.Parse(typeof(TipoDeCancelamento), Conexao.Leitor["tipo_de_cancelamento"].ToString());
                    obj.ativo = bool.Parse(Conexao.Leitor["ativo"].ToString());

                    lista.Add(obj);
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

            lista.ForEach(x => x.cliente.Carregar());
            lista.ForEach(x => x.convenio.Carregar());
            lista.ForEach(x => x.medico.Carregar());
            lista.ForEach(x => x.CarregarPagamentos());

            return lista;
        }

        #endregion
    }
}
