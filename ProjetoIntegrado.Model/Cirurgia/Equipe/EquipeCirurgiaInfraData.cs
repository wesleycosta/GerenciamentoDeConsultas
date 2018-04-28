using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    using BaseDeDados;

    public partial class EquipeCirurgiaModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO equipe_cirurgia
	                            (id_cirurgia, id_funcionario, funcao, ativo)
                            OUTPUT inserted.id_equipe_cirurgia
                            VALUES
	                            (@id_cirurgia, @id_funcionario, @funcao, @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id_cirurgia", cirurgia?.id);
                Conexao.Cmd.Parameters.AddWithValue("id_funcionario", funcionario?.id);
                Conexao.Cmd.Parameters.AddWithValue("funcao", funcao);
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
                var cmd = @"UPDATE equipe_cirurgia SET
	                            id_cirurgia			= @id_cirurgia,
	                            id_funcionario		= @id_funcionario,
                                funcao              = @funcao,
	                            ativo				= @ativo
                            WHERE
	                            id_equipe_cirurgia	= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);
                Conexao.Cmd.Parameters.AddWithValue("id_cirurgia", cirurgia?.id);
                Conexao.Cmd.Parameters.AddWithValue("id_funcionario", funcionario?.id);
                Conexao.Cmd.Parameters.AddWithValue("funcao", funcao);
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
                                id_equipe_cirurgia,
	                            id_cirurgia,
	                            id_funcionario,
                                funcao,
	                            ativo
                            FROM
	                            equipe_cirurgia
                            WHERE
	                            id_equipe_cirurgia = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    id = int.Parse(Conexao.Leitor["	id_equipe_cirurgia"].ToString());

                    cirurgia = new CirurgiaModel
                    {
                        id = int.Parse(Conexao.Leitor["id_cirurgia"].ToString())
                    };

                    funcionario = new FuncionarioModel
                    {
                        id = int.Parse(Conexao.Leitor["id_funcionario"].ToString())
                    };

                    funcao = Conexao.Leitor["funcao"].ToString();
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

            cirurgia?.Carregar();
            funcionario?.Carregar();
        }

        #endregion

        #region CARREGAR LISTA

        public List<EquipeCirurgiaModel> CarregarPorIdConsulta(int idConsulta)
        {
            var lista = new List<EquipeCirurgiaModel>();

            try
            {
                var cmd = $@"SELECT
	                            E.id_equipe_cirurgia,
	                            E.id_cirurgia,
	                            E.id_funcionario,
                                E.funcao,
	                            E.ativo
                            FROM
	                            equipe_cirurgia E
                            INNER JOIN cirurgia ON
	                            cirurgia.id_cirurgia = E.id_cirurgia
                            INNER JOIN consulta ON
	                            consulta.id_consulta = cirurgia.id_consulta
                            WHERE
	                            consulta.id_consulta = @id
                                AND
                                E.ativo = 1";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", idConsulta);
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                    lista.Add(new EquipeCirurgiaModel
                    {
                        id = int.Parse(Conexao.Leitor["id_equipe_cirurgia"].ToString()),

                        cirurgia = new CirurgiaModel
                        {
                            id = int.Parse(Conexao.Leitor["id_cirurgia"].ToString())
                        },

                        funcionario = new FuncionarioModel
                        {
                            id = int.Parse(Conexao.Leitor["id_funcionario"].ToString())
                        },

                        funcao = Conexao.Leitor["funcao"].ToString(),
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

            lista.ForEach(x => x?.cirurgia?.Carregar());
            lista.ForEach(x => x?.funcionario?.Carregar());

            return lista;
        }

        #endregion
    }
}
