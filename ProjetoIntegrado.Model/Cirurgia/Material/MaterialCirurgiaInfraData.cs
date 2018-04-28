using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    using BaseDeDados;

    public partial class MaterialCirurgiaModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {
            try
            {
                var cmd = @"INSERT INTO material_cirurgia
	                            (id_cirurgia, id_material, quantidade, valor_unitario, ativo)
                            OUTPUT inserted.id_material_cirurgia
                            VALUES
	                            (@id_cirurgia, @id_material, @quantidade, @valor_unitario, @ativo)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id_cirurgia", cirurgia?.id);
                Conexao.Cmd.Parameters.AddWithValue("id_material", material?.id);
                Conexao.Cmd.Parameters.AddWithValue("quantidade", quantidade);
                Conexao.Cmd.Parameters.AddWithValue("valor_unitario", valorUnitario);
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
                var cmd = @"UPDATE material_cirurgia SET
	                            id_cirurgia			= @id_cirurgia,
	                            id_material			= @id_material,
	                            quantidade			= @quantidade,
	                            valor_unitario		= @valor_unitario,
	                            ativo				= @ativo
                            WHERE
	                            id_material_cirurgia = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);
                Conexao.Cmd.Parameters.AddWithValue("id_cirurgia", cirurgia?.id);
                Conexao.Cmd.Parameters.AddWithValue("id_material", material?.id);
                Conexao.Cmd.Parameters.AddWithValue("quantidade", quantidade);
                Conexao.Cmd.Parameters.AddWithValue("valor_unitario", valorUnitario);
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
	                            id_material_cirurgia,
	                            id_cirurgia,
	                            id_material,
	                            quantidade,
	                            valor_unitario,
	                            ativo
                            FROM
	                            material_cirurgia
                            WHERE
	                            id_material_cirurgia = @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    id = int.Parse(Conexao.Leitor["	id_material_cirurgia"].ToString());

                    cirurgia = new CirurgiaModel
                    {
                        id = int.Parse(Conexao.Leitor["id_cirurgia"].ToString())
                    };

                    material = new MaterialModel
                    {
                        id = int.Parse(Conexao.Leitor["id_material"].ToString())
                    };

                    quantidade = int.Parse(Conexao.Leitor["quantidade"].ToString());
                    valorUnitario = decimal.Parse(Conexao.Leitor["valor_unitario"].ToString());
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
        }

        #endregion

        #region CARREGAR LISTA

        public List<MaterialCirurgiaModel> CarregarPorIdConsulta(int idConsulta)
        {
            var lista = new List<MaterialCirurgiaModel>();

            try
            {
                var cmd = $@"SELECT
	                            M.id_material_cirurgia,
	                            M.id_cirurgia,
	                            M.id_material,
	                            M.quantidade,
	                            M.valor_unitario,
	                            M.ativo
                            FROM
	                            material_cirurgia M
                            INNER JOIN cirurgia ON
	                            M.id_cirurgia		 = cirurgia.id_cirurgia
                            INNER JOIN consulta ON
	                            consulta.id_consulta = cirurgia.id_consulta
                            WHERE
	                            consulta.id_consulta = @id
	                            AND
	                            M.ativo = 1";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", idConsulta);
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                    lista.Add(new MaterialCirurgiaModel
                    {
                        id = int.Parse(Conexao.Leitor["id_material_cirurgia"].ToString()),

                        cirurgia = new CirurgiaModel
                        {
                            id = int.Parse(Conexao.Leitor["id_cirurgia"].ToString())
                        },

                        material = new MaterialModel
                        {
                            id = int.Parse(Conexao.Leitor["id_material"].ToString())
                        },

                        quantidade = int.Parse(Conexao.Leitor["quantidade"].ToString()),
                        valorUnitario = decimal.Parse(Conexao.Leitor["valor_unitario"].ToString()),
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

            lista.ForEach(x => x?.cirurgia.Carregar());
            lista.ForEach(x => x?.material.Carregar());

            return lista;
        }

        #endregion
    }
}
