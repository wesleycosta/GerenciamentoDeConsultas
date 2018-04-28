using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

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
	                             tipo_de_consulta,
                                 observacao,
                                 retorno) 
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
	                             @tipo_de_consulta,
                                 @observacao,
                                 @retorno)";

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
                Conexao.Cmd.Parameters.AddWithValue("tipo_de_consulta", tipoDeConsulta);
                Conexao.Cmd.Parameters.AddWithValue("observacao", observacao);
                Conexao.Cmd.Parameters.AddWithValue("retorno", retorno);

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
	                            tipo_de_consulta	    = @tipo_de_consulta,
                                retorno                 = @retorno,
                                observacao              = @observacao,
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
                Conexao.Cmd.Parameters.AddWithValue("tipo_de_consulta", tipoDeConsulta);
                Conexao.Cmd.Parameters.AddWithValue("retorno", retorno);
                Conexao.Cmd.Parameters.AddWithValue("ativo", ativo);
                Conexao.Cmd.Parameters.AddWithValue("observacao", observacao);

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
	                            tipo_de_consulta,
                                retorno,
                                observacao,
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


                    convenio = new ConvenioModel
                    {
                        id = Conexao.Leitor["id_convenio"] != DBNull.Value ?
                                int.Parse(Conexao.Leitor["id_convenio"].ToString()) : 1
                    };

                    numeroProcedimento = Conexao.Leitor["numero_procedimento"].ToString();
                    formaDeAtentimento = (FormaDeAtendimento)Enum.Parse(typeof(FormaDeAtendimento), Conexao.Leitor["forma_de_atendimento"].ToString());
                    data = DateTime.Parse(Conexao.Leitor["data"].ToString());
                    horario = TimeSpan.Parse(Conexao.Leitor["horario"].ToString());
                    valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
                    statusPagamento = (StatusPagamento)Enum.Parse(typeof(StatusPagamento), Conexao.Leitor["status_pagamento"].ToString());
                    tipoDeConsulta = (TipoDeConsulta)Enum.Parse(typeof(TipoDeConsulta), Conexao.Leitor["tipo_de_consulta"].ToString());
                    retorno = bool.Parse(Conexao.Leitor["retorno"].ToString());
                    ativo = bool.Parse(Conexao.Leitor["ativo"].ToString());
                    observacao = Conexao.Leitor["observacao"].ToString();
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

        public void CarregarPagamentos() =>
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
                                id_consulta,
	                            id_medico,
	                            id_cliente,
	                            ISNULL(id_convenio, 0) AS id_convenio,
	                            numero_procedimento,
	                            forma_de_atendimento,
	                            data,
                                horario,
	                            valor,
	                            status_pagamento, 
	                            tipo_de_consulta,
                                retorno,
                                observacao,
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
                    obj.id = int.Parse(Conexao.Leitor["id_consulta"].ToString());

                    obj.medico = new FuncionarioModel
                    {
                        id = int.Parse(Conexao.Leitor["id_medico"].ToString())
                    };

                    obj.cliente = new ClienteModel
                    {
                        id = int.Parse(Conexao.Leitor["id_cliente"].ToString())
                    };

                    obj.convenio = new ConvenioModel
                    {
                        id = Conexao.Leitor["id_convenio"] != DBNull.Value ?
                                int.Parse(Conexao.Leitor["id_convenio"].ToString()) : 1
                    };

                    obj.numeroProcedimento = Conexao.Leitor["numero_procedimento"].ToString();
                    obj.formaDeAtentimento = (FormaDeAtendimento)Enum.Parse(typeof(FormaDeAtendimento), Conexao.Leitor["forma_de_atendimento"].ToString());
                    obj.data = DateTime.Parse(Conexao.Leitor["data"].ToString());
                    obj.horario = TimeSpan.Parse(Conexao.Leitor["horario"].ToString());
                    obj.valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
                    obj.statusPagamento = (StatusPagamento)Enum.Parse(typeof(StatusPagamento), Conexao.Leitor["status_pagamento"].ToString());
                    obj.tipoDeConsulta = (TipoDeConsulta)Enum.Parse(typeof(TipoDeConsulta), Conexao.Leitor["tipo_de_consulta"].ToString());
                    obj.ativo = bool.Parse(Conexao.Leitor["retorno"].ToString());
                    obj.ativo = bool.Parse(Conexao.Leitor["ativo"].ToString());
                    obj.observacao = Conexao.Leitor["observacao"].ToString();

                    obj.cirgurgia = new CirurgiaModel
                    {
                        idConsulta = obj.id
                    };

                    obj.receita = new ReceitaModel
                    {
                        idConsulta = obj.id
                    };

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

            lista.ForEach(x => x.convenio?.Carregar());
            lista.ForEach(x => x.medico.Carregar());
            lista.ForEach(x => x.cirgurgia?.Carregar());

            lista.ForEach(x => x.receita.Carregar());
            lista.ForEach(x => x.cirgurgia.Carregar());

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
	                            C.tipo_de_consulta,
                                C.retorno,
                                C.observacao,
	                            C.ativo
                            FROM
	                            consulta C
                            INNER JOIN pagamento P 
	                            ON P.id_consulta = C.id_consulta
                            WHERE
	                            C.ativo    =  1
	                            AND
	                            P.id_caixa = @id
                            ORDER BY
                                C.data DESC, C.horario DESC";

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

                    obj.convenio = new ConvenioModel
                    {
                        id = Conexao.Leitor["id_convenio"] != DBNull.Value ?
                                int.Parse(Conexao.Leitor["id_convenio"].ToString()) : 1
                    };

                    obj.numeroProcedimento = Conexao.Leitor["numero_procedimento"].ToString();
                    obj.formaDeAtentimento = (FormaDeAtendimento)Enum.Parse(typeof(FormaDeAtendimento), Conexao.Leitor["forma_de_atendimento"].ToString());
                    obj.data = DateTime.Parse(Conexao.Leitor["data"].ToString());
                    obj.horario = TimeSpan.Parse(Conexao.Leitor["horario"].ToString());
                    obj.valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
                    obj.statusPagamento = (StatusPagamento)Enum.Parse(typeof(StatusPagamento), Conexao.Leitor["status_pagamento"].ToString());
                    obj.tipoDeConsulta = (TipoDeConsulta)Enum.Parse(typeof(TipoDeConsulta), Conexao.Leitor["tipo_de_consulta"].ToString());
                    obj.retorno = bool.Parse(Conexao.Leitor["retorno"].ToString());
                    obj.ativo = bool.Parse(Conexao.Leitor["ativo"].ToString());
                    obj.observacao = Conexao.Leitor["observacao"].ToString();

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

            lista.ForEach(x => x.cliente?.Carregar());
            lista.ForEach(x => x.convenio?.Carregar());
            lista.ForEach(x => x.medico?.Carregar());
            lista.ForEach(x => x.CarregarPagamentos());

            lista.ForEach(x => x.receita?.Carregar());
            lista.ForEach(x => x.cirgurgia?.Carregar());

            return lista;
        }

        public static List<ConsultaModel> Pesquisar(FiltroPessoa filtro, string pesquisa, FormaDeAtendimento atendimento, DateTime dtInicial, DateTime dtFinal, FuncionarioModel medico, ConvenioModel convenio)
        {
            var lista = new List<ConsultaModel>();

            try
            {
                var cmd = $@"SELECT
                                C.id_consulta,
	                            C.id_medico,
	                            C.id_cliente,
	                            ISNULL(C.id_convenio, 0) AS id_convenio,
	                            C.numero_procedimento,
	                            C.forma_de_atendimento,
	                            C.data,
                                C.horario,
	                            C.valor,
	                            C.status_pagamento, 
	                            C.tipo_de_consulta,
                                C.retorno,
                                C.observacao,
	                            C.ativo
                            FROM
	                            consulta    C
                            INNER JOIN cliente CLI
                                ON CLI.id_cliente = C.id_cliente
                            WHERE
	                            C.ativo	= 1
                                AND
	                            CLI.{filtro} LIKE @pesquisa
                                AND
                                C.data    BETWEEN @data_inicial AND @data_final ";

                if (atendimento != FormaDeAtendimento.Todos)
                    cmd += " AND C.forma_de_atendimento = @forma_de_atendimento ";

                if (medico != null)
                    cmd += " AND C.id_medico = @id_medico ";

                if (convenio != null)
                    cmd += " AND C.id_convenio = @id_convenio ";

                cmd += " ORDER BY C.data, C.horario";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("pesquisa", $"%{pesquisa}%");
                Conexao.Cmd.Parameters.AddWithValue("data_inicial", dtInicial.Date);
                Conexao.Cmd.Parameters.AddWithValue("data_final", dtFinal.Date);

                if (cmd.Contains("@forma_de_atendimento"))
                    Conexao.Cmd.Parameters.AddWithValue("forma_de_atendimento", atendimento);

                if (cmd.Contains("@id_medico"))
                    Conexao.Cmd.Parameters.AddWithValue("id_medico", medico?.id);

                if (cmd.Contains("@id_convenio"))
                    Conexao.Cmd.Parameters.AddWithValue("id_convenio", convenio?.id);

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

                    obj.convenio = new ConvenioModel
                    {
                        id = Conexao.Leitor["id_convenio"] != DBNull.Value ?
                                int.Parse(Conexao.Leitor["id_convenio"].ToString()) : 1
                    };

                    obj.numeroProcedimento = Conexao.Leitor["numero_procedimento"].ToString();
                    obj.formaDeAtentimento = (FormaDeAtendimento)Enum.Parse(typeof(FormaDeAtendimento), Conexao.Leitor["forma_de_atendimento"].ToString());
                    obj.data = DateTime.Parse(Conexao.Leitor["data"].ToString());
                    obj.horario = TimeSpan.Parse(Conexao.Leitor["horario"].ToString());
                    obj.valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
                    obj.statusPagamento = (StatusPagamento)Enum.Parse(typeof(StatusPagamento), Conexao.Leitor["status_pagamento"].ToString());
                    obj.tipoDeConsulta = (TipoDeConsulta)Enum.Parse(typeof(TipoDeConsulta), Conexao.Leitor["tipo_de_consulta"].ToString());
                    obj.retorno = bool.Parse(Conexao.Leitor["retorno"].ToString());
                    obj.ativo = bool.Parse(Conexao.Leitor["ativo"].ToString());
                    obj.observacao = Conexao.Leitor["observacao"].ToString();

                    obj.cirgurgia = new CirurgiaModel
                    {
                        idConsulta = obj.id
                    };

                    obj.receita = new ReceitaModel
                    {
                        idConsulta = obj.id
                    };

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

            lista.ForEach(x => x.convenio?.Carregar());
            lista.ForEach(x => x.medico.Carregar());
            lista.ForEach(x => x.cliente.Carregar());

            lista.ForEach(x => x.receita.Carregar());
            lista.ForEach(x => x.cirgurgia.Carregar());

            return lista;
        }

        public static List<ConsultaModel> Procedimentos(ConvenioModel convenio, FiltroPessoa filtro, string pesquisa)
        {
            var lista = new List<ConsultaModel>();

            try
            {
                var cmd = $@"SELECT
	                            C.id_consulta,
                                C.id_medico,
	                            C.id_cliente,
	                            ISNULL(C.id_convenio, 0) AS id_convenio,
	                            C.numero_procedimento,
	                            C.forma_de_atendimento,
	                            C.data,
                                C.horario,
	                            C.valor,
	                            C.status_pagamento, 
	                            C.tipo_de_consulta,
                                C.retorno,
                                C.observacao,
	                            C.ativo
                            FROM
	                            consulta    C
                            INNER JOIN cliente CLI
                                ON CLI.id_cliente = C.id_cliente
                            WHERE
	                            C.ativo	            = 1
                                AND
	                            CLI.{filtro} LIKE @pesquisa
                                AND
                                C.status_pagamento     = 1
	                            AND
                                C.forma_de_atendimento = 1 ";

                if (convenio != null)
                    cmd += " AND C.id_convenio = @id_convenio ";

                cmd += " ORDER BY C.data, C.horario";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("pesquisa", $"%{pesquisa}%");

                if (cmd.Contains("@id_convenio"))
                    Conexao.Cmd.Parameters.AddWithValue("id_convenio", convenio?.id);

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

                    obj.convenio = new ConvenioModel
                    {
                        id = Conexao.Leitor["id_convenio"] != DBNull.Value ?
                                int.Parse(Conexao.Leitor["id_convenio"].ToString()) : 1
                    };

                    obj.numeroProcedimento = Conexao.Leitor["numero_procedimento"].ToString();
                    obj.formaDeAtentimento = (FormaDeAtendimento)Enum.Parse(typeof(FormaDeAtendimento), Conexao.Leitor["forma_de_atendimento"].ToString());
                    obj.data = DateTime.Parse(Conexao.Leitor["data"].ToString());
                    obj.horario = TimeSpan.Parse(Conexao.Leitor["horario"].ToString());
                    obj.valor = decimal.Parse(Conexao.Leitor["valor"].ToString());
                    obj.statusPagamento = (StatusPagamento)Enum.Parse(typeof(StatusPagamento), Conexao.Leitor["status_pagamento"].ToString());
                    obj.tipoDeConsulta = (TipoDeConsulta)Enum.Parse(typeof(TipoDeConsulta), Conexao.Leitor["tipo_de_consulta"].ToString());
                    obj.retorno = bool.Parse(Conexao.Leitor["retorno"].ToString());
                    obj.ativo = bool.Parse(Conexao.Leitor["ativo"].ToString());
                    obj.observacao = Conexao.Leitor["observacao"].ToString();

                    obj.cirgurgia = new CirurgiaModel
                    {
                        idConsulta = obj.id
                    };

                    obj.receita = new ReceitaModel
                    {
                        idConsulta = obj.id
                    };

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

            lista.ForEach(x => x.convenio?.Carregar());
            lista.ForEach(x => x.medico.Carregar());
            lista.ForEach(x => x.cliente.Carregar());

            lista.ForEach(x => x.receita.Carregar());
            lista.ForEach(x => x.cirgurgia.Carregar());

            return lista;
        }


        #endregion

        public void ReceberPagamento()
        {
            statusPagamento = StatusPagamento.Recebido;

            var p = new PagamentoModel
            {
                idConsulta = id,
                caixa = new CaixaModel { id = 0 },
                formaDePagamento = new FormaDePagamentoModel { id = 6 },
                data = DateTime.Now,
                qtdParcelas = 1,
                valor = valor,
                ativo = true
            };

            p.Cadastrar();
            Atualizar();
        }

        public double CalcularDebito()
        {
            var pagamento = TotalPagamento();
            return (double)valor - pagamento;
        }

        public double TotalPagamento()
        {
            CarregarPagamentos();
            return listaDePagamentos.Sum(x => (double)x?.valor);
        }

        public static bool ExisteConsulta(DateTime data, TimeSpan horario, int id = 0)
        {
            var existe = true;

            try
            {
                var cmd = @"SELECT
                                id_consulta
                            FROM
	                            consulta
                            WHERE
	                            ativo	     = 1
                                AND
                                data         = @data
                                AND
                                horario      = @horario
                                AND
                                id_consulta != @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("data", data);
                Conexao.Cmd.Parameters.AddWithValue("horario", horario);
                Conexao.Cmd.Parameters.AddWithValue("id", id);
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                existe = Conexao.Leitor.Read();
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
            }
            finally
            {
                Conexao.FecharConexao();
            }

            return existe;
        }

        public void Cancelar(TipoDeCancelamento tipo)
        {
            try
            {
                var cmd = @"UPDATE consulta SET
                                tipo_de_cancelamento    = @tipo_de_cancelamento,
                                ativo                   = @ativo
                            WHERE
	                            id_consulta				= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);
                Conexao.Cmd.Parameters.AddWithValue("tipo_de_cancelamento", tipo == TipoDeCancelamento.NaoCompareceu);
                Conexao.Cmd.Parameters.AddWithValue("ativo", false);

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
    }
}
