using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    using BaseDeDados;

    public partial class CaixaSaidaModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO caixa_saida
	                            (id_caixa, descricao, valor, ativo)
                            OUTPUT inserted.id_caixa
                            VALUES
	                            (@id_caixa, @descricao, @valor, @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id_caixa", idCaixa);
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
                var cmd = @"UPDATE caixa_saida SET
	                            descricao		= @descricao,
	                            valor			= @valor,
                                ativo           = @ativo
                            WHERE
	                            id_caixa_saida	= @id";

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
                var cmd = @"SElECT 
	                            id_caixa,
	                            descricao,
	                            valor,
	                            data,
	                            ativo
                            FROM 
	                            caixa_saida
                            WHERE
	                            id_caixa_saida	 = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    idCaixa = int.Parse(Conexao.Leitor["id_caixa"].ToString());
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

        public static List<CaixaSaidaModel> CarregarSaidas(int idCaixa)
        {
            var lista = new List<CaixaSaidaModel>();

            try
            {
                var cmd = $@"SElECT TOP 100
                                id_caixa_saida,
	                            id_caixa,
	                            descricao,
	                            valor,
	                            data,
	                            ativo
                            FROM 
	                            caixa_saida
                            WHERE
	                            id_caixa	 = @id
                                AND
                                ativo        = 1";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", idCaixa);
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                    lista.Add(new CaixaSaidaModel
                    {
                        id = int.Parse(Conexao.Leitor["id_caixa_saida"].ToString()),
                        idCaixa = int.Parse(Conexao.Leitor["id_caixa"].ToString()),
                        descricao = Conexao.Leitor["descricao"].ToString(),
                        valor = decimal.Parse(Conexao.Leitor["valor"].ToString()),
                        data = DateTime.Parse(Conexao.Leitor["data"].ToString()),
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

        #endregion

    }
}
