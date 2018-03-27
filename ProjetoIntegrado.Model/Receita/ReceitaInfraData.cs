using System;
using System.Data.SqlClient;

namespace ProjetoIntegrado.Model
{
    using BaseDeDados;

    public partial class ReceitaModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO receita	
	                            (id_consulta, olho_esquerdo, olho_direito, ativo)
                            OUTPUT inserted.id_receita
                            VALUES
	                            (@id_consulta, @olho_esquerdo, @olho_direito, @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id_consulta", idConsulta);
                Conexao.Cmd.Parameters.AddWithValue("olho_esquerdo", olhoEsquerdo?.id);
                Conexao.Cmd.Parameters.AddWithValue("olho_direito", olhoDireito?.id);
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
                var cmd = @"UPDATE receita SET
	                            id_consulta			= @id_consulta,
	                            olho_esquerdo		= @olho_esquerdo,
	                            olho_direito		= @olho_direito,
	                            ativo				= @ativo
                            WHERE
	                            id_receita			= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Cmd.Parameters.AddWithValue("id_consulta", idConsulta);
                Conexao.Cmd.Parameters.AddWithValue("olho_esquerdo", olhoEsquerdo?.id);
                Conexao.Cmd.Parameters.AddWithValue("olho_direito", olhoDireito?.id);
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
	                            id_receita,
	                            id_consulta,
	                            olho_esquerdo,
	                            olho_direito,
	                            ativo
                            FROM	
	                            receita
                            WHERE
	                            id_consulta = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", idConsulta);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    id = int.Parse(Conexao.Leitor["id_receita"].ToString());
                    idConsulta= int.Parse(Conexao.Leitor["id_consulta"].ToString());


                    olhoDireito = new DiagnosticoModel
                    {
                        id = int.Parse(Conexao.Leitor["olho_direito"].ToString())
                    };

                    olhoEsquerdo = new DiagnosticoModel
                    {
                        id = int.Parse(Conexao.Leitor["olho_esquerdo"].ToString())
                    };


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

            olhoEsquerdo?.Carregar();
            olhoDireito?.Carregar();
        }

        #endregion

        public void CadastrarComDiagnostico()
        {
            olhoDireito?.Cadastrar();
            olhoEsquerdo?.Cadastrar();

            Cadastrar();
        }

        public void AtualizarComDiagnostico()
        {
            olhoDireito?.Atualizar();
            olhoEsquerdo?.Atualizar();

            Atualizar();
        }
    }
}
