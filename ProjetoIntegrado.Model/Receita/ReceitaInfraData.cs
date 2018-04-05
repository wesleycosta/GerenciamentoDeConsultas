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
	                            (id_consulta, olho_esquerdo_longe, olho_direito_longe, olho_esquerdo_perto, olho_direito_perto, ativo)
                            OUTPUT inserted.id_receita
                            VALUES
	                            (@id_consulta, @olho_esquerdo_longe, @olho_direito_longe, @olho_esquerdo_perto, @olho_direito_perto, @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id_consulta", idConsulta);
                Conexao.Cmd.Parameters.AddWithValue("olho_esquerdo_longe", olhoEsquerdoLonge?.id);
                Conexao.Cmd.Parameters.AddWithValue("olho_direito_longe", olhoDireitoLonge?.id);
                Conexao.Cmd.Parameters.AddWithValue("olho_direito_perto", olhoDireitoPerto?.id);
                Conexao.Cmd.Parameters.AddWithValue("olho_esquerdo_perto", olhoEsquerdoPerto?.id);
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
	                            id_consulta			    = @id_consulta,
	                            olho_esquerdo_longe		= @olho_esquerdo_longe,
	                            olho_direito_longe		= @olho_direito_longe,
	                            olho_esquerdo_perto		= @olho_esquerdo_perto,
	                            olho_direito_perto		= @olho_direito_perto,
                                ativo				    = @ativo
                            WHERE
	                            id_receita			= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Cmd.Parameters.AddWithValue("id_consulta", idConsulta);
                Conexao.Cmd.Parameters.AddWithValue("olho_esquerdo_longe", olhoEsquerdoLonge?.id);
                Conexao.Cmd.Parameters.AddWithValue("olho_direito_longe", olhoDireitoLonge?.id);
                Conexao.Cmd.Parameters.AddWithValue("olho_esquerdo_perto", olhoEsquerdoPerto?.id);
                Conexao.Cmd.Parameters.AddWithValue("olho_direito_perto", olhoDireitoPerto?.id);
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
	                            olho_esquerdo_longe,
	                            olho_direito_longe,
	                            olho_esquerdo_perto,
	                            olho_direito_perto,
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

                    olhoDireitoLonge = new DiagnosticoModel
                    {
                        id = int.Parse(Conexao.Leitor["olho_direito_longe"].ToString())
                    };

                    olhoDireitoPerto = new DiagnosticoModel
                    {
                        id = int.Parse(Conexao.Leitor["olho_direito_perto"].ToString())
                    };

                    olhoEsquerdoLonge = new DiagnosticoModel
                    {
                        id = int.Parse(Conexao.Leitor["olho_esquerdo_longe"].ToString())
                    };

                    olhoEsquerdoPerto = new DiagnosticoModel
                    {
                        id = int.Parse(Conexao.Leitor["olho_esquerdo_perto"].ToString())
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

            olhoEsquerdoLonge?.Carregar();
            olhoDireitoLonge?.Carregar();

            olhoEsquerdoPerto?.Carregar();
            olhoDireitoPerto?.Carregar();
        }

        #endregion

        public void CadastrarComDiagnostico()
        {
            olhoDireitoLonge?.Cadastrar();
            olhoEsquerdoLonge?.Cadastrar();

            olhoDireitoPerto?.Cadastrar();
            olhoEsquerdoPerto?.Cadastrar();

            Cadastrar();
        }

        public void AtualizarComDiagnostico()
        {
            olhoDireitoLonge?.Atualizar();
            olhoEsquerdoLonge?.Atualizar();

            olhoDireitoPerto?.Atualizar();
            olhoEsquerdoPerto?.Atualizar();

            Atualizar();
        }
    }
}
