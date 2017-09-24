using System;
using System.Data.SqlClient;

namespace ProjetoIntegrado.Model
{
    using BaseDeDados;
    using View;

    public partial class EnderecoModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO endereco
	                            ( cep,  cidade,  uf,  bairro,  logradouro,  numero,  complemento,  ativo)
                            OUTPUT inserted.id_endereco
                            VALUES
	                            (@cep, @cidade, @uf, @bairro, @logradouro, @numero, @complemento, @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("cep", cep);
                Conexao.Cmd.Parameters.AddWithValue("cidade", cidade);
                Conexao.Cmd.Parameters.AddWithValue("uf", uf);
                Conexao.Cmd.Parameters.AddWithValue("bairro", bairro);
                Conexao.Cmd.Parameters.AddWithValue("logradouro", logradouro);
                Conexao.Cmd.Parameters.AddWithValue("numero", numero);
                Conexao.Cmd.Parameters.AddWithValue("complemento", complemento);
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
                var cmd = @"UPDATE endereco SET
	                            cep			= @cep,
	                            cidade		= @cidade,
	                            uf			= @uf,
	                            bairro		= @bairro,
	                            logradouro	= @logradouro,
	                            numero		= @numero,
	                            complemento	= @complemento,
	                            ativo		= @ativo
                            WHERE
	                            id_endereco	= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("cep", cep);
                Conexao.Cmd.Parameters.AddWithValue("cidade", cidade);
                Conexao.Cmd.Parameters.AddWithValue("uf", uf);
                Conexao.Cmd.Parameters.AddWithValue("bairro", bairro);
                Conexao.Cmd.Parameters.AddWithValue("logradouro", logradouro);
                Conexao.Cmd.Parameters.AddWithValue("numero", numero);
                Conexao.Cmd.Parameters.AddWithValue("complemento", complemento);
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
	                            cep,
	                            cidade,
	                            uf, 
	                            bairro,
	                            logradouro,
	                            numero,
	                            complemento,
	                            ativo
                            FROM	
	                            endereco
                            WHERE
	                            id_endereco = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    cep         = Conexao.Leitor["cep"].ToString();
                    cidade      = Conexao.Leitor["cidade"].ToString();
                    uf          = Conexao.Leitor["uf"].ToString();
                    bairro      = Conexao.Leitor["bairro"].ToString();
                    logradouro  = Conexao.Leitor["logradouro"].ToString();
                    numero      = Conexao.Leitor["numero"].ToString();
                    complemento = Conexao.Leitor["complemento"].ToString();
                    ativo       = bool.Parse(Conexao.Leitor["ativo"].ToString());
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
    }
}
