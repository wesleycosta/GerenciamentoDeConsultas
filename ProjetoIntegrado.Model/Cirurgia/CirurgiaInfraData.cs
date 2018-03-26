using System;
using System.Data.SqlClient;

namespace ProjetoIntegrado.Model
{
    using BaseDeDados;

    public partial class CirurgiaModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO cirurgia
	                            (id_consulta, local, valor_medico, ativo)
                            OUTPUT cirurgia.id_cirurgia
	                            (@id_consulta, @local, @valor_medico, @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id_consulta", idConsulta);
                Conexao.Cmd.Parameters.AddWithValue("local", local);
                Conexao.Cmd.Parameters.AddWithValue("valor_medico", valor);
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
                var cmd = @"UPDATE cirurgia SET
	                            id_consulta			= @id_consulta,
	                            local				= @local,
	                            valor_medico		= @valor_medico,
	                            ativo				= @ativo
                            WHERE
	                            id_cirurgia			= @id";


                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);
                Conexao.Cmd.Parameters.AddWithValue("id_consulta", idConsulta);
                Conexao.Cmd.Parameters.AddWithValue("local", local);
                Conexao.Cmd.Parameters.AddWithValue("valor_medico", valor);
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
	                            id_cirurgia,
	                            local,
	                            valor_medico,
	                            ativo
                            FROM
	                            cirurgia
                            WHERE
	                            id_consulta  = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", idConsulta);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    id = int.Parse(Conexao.Leitor["id_cirurgia"].ToString());
                    local = Conexao.Leitor["local"].ToString();
                    valor = decimal.Parse(Conexao.Leitor["valor_medico"].ToString());
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
    }
}
