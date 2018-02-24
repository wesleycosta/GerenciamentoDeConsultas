using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    using BaseDeDados;
    using System.Data.SqlTypes;

    public partial class CaixaModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO caixa
	                            (funcionario_abertura,
	                             valor_inicial,
	                             data_abertura,
                                 caixa_aberto,
	                             ativo)
                            OUTPUT inserted.id_caixa
                            VALUES
	                            (@funcionario_abertura,
	                             @valor_inicial,
	                             @data_abertura,
                                 @caixa_aberto,
	                             @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("funcionario_abertura", funcionarioAbertura.id);
                Conexao.Cmd.Parameters.AddWithValue("valor_inicial", valorInicial);
                Conexao.Cmd.Parameters.AddWithValue("data_abertura", dtAbertura);
                Conexao.Cmd.Parameters.AddWithValue("caixa_aberto", "V");
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
                var cmd = @"UPDATE caixa SET
	                            funcionario_abertura	= @funcionario_abertura,
	                            funcionario_fechamento	= @funcionario_fechamento,
	                            valor_inicial			= @valor_inicial,
	                            data_abertura			= @data_abertura,
	                            data_fechamento			= @data_fechamento,
	                            ativo					= @ativo
                            WHERE
	                            id_caixa = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);
                Conexao.Cmd.Parameters.AddWithValue("funcionario_abertura", funcionarioAbertura.id);
                Conexao.Cmd.Parameters.AddWithValue("valor_inicial", valorInicial);
                Conexao.Cmd.Parameters.AddWithValue("data_abertura", dtAbertura);
                Conexao.Cmd.Parameters.AddWithValue("ativo", ativo);

                if (funcionarioFechamento != null && (dtFechamento != null || dtFechamento != DateTime.MinValue))
                {
                    Conexao.Cmd.Parameters.AddWithValue("funcionario_fechamento", funcionarioFechamento?.id);
                    Conexao.Cmd.Parameters.AddWithValue("data_fechamento", dtFechamento);
                }
                else
                {
                    Conexao.Cmd.Parameters.AddWithValue("funcionario_fechamento", SqlInt32.Null);
                    Conexao.Cmd.Parameters.AddWithValue("data_fechamento", SqlDateTime.Null);
                }

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
                var cmd = @"SELECT TOP 1
                                id_caixa,
                                funcionario_abertura,
	                            valor_inicial,
	                            data_abertura,
	                            ativo
                            FROM
	                            caixa
                            WHERE 
	                            caixa_aberto = 'V'";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    id = int.Parse(Conexao.Leitor["id_caixa"].ToString());

                    funcionarioAbertura = new FuncionarioModel
                    {
                        id = int.Parse(Conexao.Leitor["funcionario_abertura"].ToString())
                    };

                    valorInicial = decimal.Parse(Conexao.Leitor["valor_inicial"].ToString());
                    dtAbertura = DateTime.Parse(Conexao.Leitor["data_abertura"].ToString());
                    ativo = bool.Parse(Conexao.Leitor["ativo"].ToString());
                    caixaAberto = true;
                }
                else
                    caixaAberto = false;
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
            }
            finally
            {
                Conexao.FecharConexao();
            }

            if (caixaAberto)
                funcionarioAbertura.Carregar();
        }

        #endregion

        public List<CaixaSaidaModel> CarregarSaidas() =>
            CaixaSaidaModel.CarregarSaidas(id);
    }
}