using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ProjetoIntegrado.Model
{
    using View;
    using BaseDeDados;
    using Funcoes;
    using System.Threading.Tasks;

    public partial class ClinicaModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                endereco.Cadastrar();

                var cmd = @"INSERT INTO clinica
	                               (id_endereco,
                                    razao_social,
	                                nome_fantasia,
	                                cnpj,
	                                ie,
	                                ddd_tel,
	                                telefone,
	                                ddd_cel,
	                                celular,
	                                email,
	                                logo_clinica,
	                                site)
                           OUTPUT inserted.id_clinica
                           VALUES
	                               (@id_endereco,
                                    @razao_social,
	                                @nome_fantasia,
	                                @cnpj,
	                                @ie,
	                                @ddd_tel,
	                                @telefone,
	                                @ddd_cel,
	                                @celular,
	                                @email,
	                                @logo_clinica,
	                                @site)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id_endereco", endereco.id);
                Conexao.Cmd.Parameters.AddWithValue("razao_social", razaoSocial);
                Conexao.Cmd.Parameters.AddWithValue("nome_fantasia", nomeFantasia);
                Conexao.Cmd.Parameters.AddWithValue("cnpj", cnpj);
                Conexao.Cmd.Parameters.AddWithValue("ie", ie);

                Conexao.Cmd.Parameters.AddWithValue("ddd_tel", dddTel);
                Conexao.Cmd.Parameters.AddWithValue("telefone", telefone);
                Conexao.Cmd.Parameters.AddWithValue("ddd_cel", dddCel);
                Conexao.Cmd.Parameters.AddWithValue("celular", celular);
                Conexao.Cmd.Parameters.AddWithValue("email", email);
                Conexao.Cmd.Parameters.AddWithValue("site", site);

                Conexao.Cmd.Parameters.AddWithValue("logo_clinica", logo != null ? ImagemUtil.ImageParaByte(logo) : SqlBinary.Null);

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
                endereco.Atualizar();

                var cmd = @"UPDATE clinica SET
								id_endereco		= @id_endereco,
								razao_social	= @razao_social,
								nome_fantasia	= @nome_fantasia,
								cnpj			= @cnpj,
								ie				= @ie,
								ddd_tel			= @ddd_tel,
								telefone		= @telefone,
								ddd_cel			= @ddd_cel,
								celular			= @celular,
								email			= @email,
								logo_clinica	= @logo_clinica,
								site			= @site";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id_endereco", endereco.id);
                Conexao.Cmd.Parameters.AddWithValue("razao_social", razaoSocial);
                Conexao.Cmd.Parameters.AddWithValue("nome_fantasia", nomeFantasia);
                Conexao.Cmd.Parameters.AddWithValue("cnpj", cnpj);
                Conexao.Cmd.Parameters.AddWithValue("ie", ie);

                Conexao.Cmd.Parameters.AddWithValue("ddd_tel", dddTel);
                Conexao.Cmd.Parameters.AddWithValue("telefone", telefone);
                Conexao.Cmd.Parameters.AddWithValue("ddd_cel", dddCel);
                Conexao.Cmd.Parameters.AddWithValue("celular", celular);
                Conexao.Cmd.Parameters.AddWithValue("email", email);
                Conexao.Cmd.Parameters.AddWithValue("site", site);

                Conexao.Cmd.Parameters.AddWithValue("logo_clinica", logo != null ? ImagemUtil.ImageParaByte(logo) : SqlBinary.Null);

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

        public void Remover() { throw new NotImplementedException(); }

        public void Carregar()
        {
            try
            {
                var cmd = @"SELECT TOP 1
								id_clinica,
								id_endereco,
								razao_social,
								nome_fantasia,
								cnpj,
								ie,
								ddd_tel,
								telefone,
								ddd_cel,
								celular,
								email,
								logo_clinica,
								site
							FROM
								clinica";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    id = int.Parse(Conexao.Leitor["id_clinica"].ToString());
                    razaoSocial = Conexao.Leitor["razao_social"].ToString();
                    nomeFantasia = Conexao.Leitor["nome_fantasia"].ToString();
                    cnpj = Conexao.Leitor["cnpj"].ToString();
                    ie = Conexao.Leitor["ie"].ToString();

                    dddTel = Conexao.Leitor["ddd_tel"].ToString();
                    telefone = Conexao.Leitor["telefone"].ToString();
                    dddCel = Conexao.Leitor["ddd_cel"].ToString();
                    celular = Conexao.Leitor["celular"].ToString();
                    email = Conexao.Leitor["email"].ToString();
                    site = Conexao.Leitor["site"].ToString();

                    endereco = new EnderecoModel
                    {
                        id = int.Parse(Conexao.Leitor["id_endereco"].ToString())
                    };

                    if (Conexao.Leitor["logo_clinica"] != DBNull.Value)
                        logo = ImagemUtil.ByteParaImage(Conexao.Leitor["logo_clinica"]);
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

            endereco.Carregar();
        }

        public Task CarregarAsync() => Task.Run(() => Carregar());

        #endregion

        public static bool ExisteCadastro()
        {
            var existe = false;

            try
            {
                var cmd = @"SELECT COUNT(*) FROM clinica";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                    existe = int.Parse(Conexao.Leitor[0].ToString()) > 0;
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
    }
}