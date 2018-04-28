using System;
using System.Data.SqlClient;

namespace ProjetoIntegrado.Model
{
    using BaseDeDados;

    public partial class DiagnosticoModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO diagnostico
	                            (esferico, cilindro, adicao, eixo, ativo)
                            OUTPUT inserted.id_diagnostico
                            VALUES
	                            (@esferico, @cilindro, @adicao, @eixo, @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("esferico", esferico);
                Conexao.Cmd.Parameters.AddWithValue("cilindro", cilindro);
                Conexao.Cmd.Parameters.AddWithValue("adicao", adicao);
                Conexao.Cmd.Parameters.AddWithValue("eixo", eixo);
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
                var cmd = @"UPDATE diagnostico SET
	                            esferico			= @esferico,
	                            cilindro			= @cilindro,
	                            adicao				= @adicao,
	                            eixo				= @eixo,
	                            ativo				= @ativo
                            WHERE
	                            id_diagnostico		= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Cmd.Parameters.AddWithValue("esferico", esferico);
                Conexao.Cmd.Parameters.AddWithValue("cilindro", cilindro);
                Conexao.Cmd.Parameters.AddWithValue("adicao", adicao);
                Conexao.Cmd.Parameters.AddWithValue("eixo", eixo);
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
	                            esferico,
	                            cilindro,
	                            adicao,
	                            eixo,
	                            ativo
                            FROM
	                            diagnostico
                            WHERE
	                            id_diagnostico	 = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    esferico = decimal.Parse(Conexao.Leitor["esferico"].ToString());
                    cilindro = decimal.Parse(Conexao.Leitor["cilindro"].ToString());
                    adicao = decimal.Parse(Conexao.Leitor["adicao"].ToString());
                    eixo = decimal.Parse(Conexao.Leitor["eixo"].ToString());

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
