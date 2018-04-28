using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    using Funcoes;
    using BaseDeDados;

    public partial class ClienteModel : ICadastro
    {
        #region ICADASTRO

        public void Cadastrar()
        {

            try
            {
                endereco.Cadastrar();

                var cmd = @"INSERT INTO cliente
	                            (id_endereco, nome, genero, cpf, data_de_nascimento, ddd_cel, celular, ddd_tel, telefone, email)
                            OUTPUT inserted.id_cliente
                            VALUES
	                            (@id_endereco, @nome, @genero, @cpf, @data_de_nascimento, @ddd_cel, @celular, @ddd_tel, @telefone, @email)";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id_endereco", endereco.id);
                Conexao.Cmd.Parameters.AddWithValue("nome", nome);
                Conexao.Cmd.Parameters.AddWithValue("genero", genero);
                Conexao.Cmd.Parameters.AddWithValue("cpf", cpf);
                Conexao.Cmd.Parameters.AddWithValue("data_de_nascimento", dataDeNascimento);
                Conexao.Cmd.Parameters.AddWithValue("ddd_cel", dddCel);
                Conexao.Cmd.Parameters.AddWithValue("celular", celular);
                Conexao.Cmd.Parameters.AddWithValue("ddd_tel", dddTel);
                Conexao.Cmd.Parameters.AddWithValue("telefone", telefone);
                Conexao.Cmd.Parameters.AddWithValue("email", email);

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

                var cmd = @"UPDATE cliente SET
	                            id_endereco				= @id_endereco,
	                            nome					= @nome,
	                            genero					= @genero,
	                            cpf						= @cpf,
	                            data_de_nascimento		= @data_de_nascimento,
	                            ddd_cel					= @ddd_cel,
	                            celular					= @celular,
	                            ddd_tel					= @ddd_tel,
	                            telefone				= @telefone,
	                            email					= @email,
	                            ativo					= @ativo
                            WHERE
	                            id_cliente				= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

                Conexao.Cmd.Parameters.AddWithValue("id", id);
                Conexao.Cmd.Parameters.AddWithValue("id_endereco", endereco.id);
                Conexao.Cmd.Parameters.AddWithValue("nome", nome);
                Conexao.Cmd.Parameters.AddWithValue("genero", genero);
                Conexao.Cmd.Parameters.AddWithValue("cpf", cpf);
                Conexao.Cmd.Parameters.AddWithValue("data_de_nascimento", dataDeNascimento);
                Conexao.Cmd.Parameters.AddWithValue("ddd_cel", dddCel);
                Conexao.Cmd.Parameters.AddWithValue("celular", celular);
                Conexao.Cmd.Parameters.AddWithValue("ddd_tel", dddTel);
                Conexao.Cmd.Parameters.AddWithValue("telefone", telefone);
                Conexao.Cmd.Parameters.AddWithValue("email", email);
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

        public void Carregar()
        {
            try
            {
                var cmd = @"SELECT
	                            id_endereco,
	                            nome,
	                            genero,
	                            cpf,
	                            data_de_nascimento,
	                            ddd_cel,
	                            celular,
	                            ddd_tel,
	                            telefone,
	                            email,
                                ativo
                            FROM
	                            cliente
                            WHERE
	                            id_cliente	= @id";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("id", id);

                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                if (Conexao.Leitor.Read())
                {
                    this.id = id;
                    nome = Conexao.Leitor["nome"].ToString();
                    cpf = Conexao.Leitor["cpf"].ToString();
                    genero = (Genero)Enum.Parse(typeof(Genero), Conexao.Leitor["genero"].ToString());

                    dataDeNascimento = DataUtil.Converter(Conexao.Leitor, "data_de_nascimento");

                    dddCel = Conexao.Leitor["ddd_cel"].ToString();
                    celular = Conexao.Leitor["celular"].ToString();
                    dddTel = Conexao.Leitor["ddd_tel"].ToString();
                    telefone = Conexao.Leitor["telefone"].ToString();
                    email = Conexao.Leitor["email"].ToString();

                    endereco = new EnderecoModel
                    {
                        id = int.Parse(Conexao.Leitor["id_endereco"].ToString())
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

            endereco?.Carregar();
        }

        public void Remover()
        {
            ativo = endereco.ativo = false;
            Atualizar();
        }

        #endregion

        #region CARREGAR LISTA

        public static List<ClienteModel> Pesquisar(FiltroPessoa filtro, string pesquisa)
        {
            var lista = new List<ClienteModel>();

            try
            {
                var cmd = $@"SELECT TOP 50
                                id_cliente,
                                id_endereco,
	                            nome, 
	                            genero,
	                            cpf,
	                            data_de_nascimento,
	                            ddd_cel,
	                            celular,
	                            ddd_tel,
	                            telefone,
	                            email,
	                            ativo
                            FROM
	                            cliente
                            WHERE
	                            ativo		    = 1
	                            AND
	                            {filtro} LIKE @pesquisa
                            ORDER BY
                                id_cliente DESC";

                Conexao.AbrirConexao();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
                Conexao.Cmd.Parameters.AddWithValue("pesquisa", $"%{pesquisa}%");
                Conexao.Leitor = Conexao.Cmd.ExecuteReader();

                while (Conexao.Leitor.Read())
                    lista.Add(new ClienteModel
                    {
                        id = int.Parse(Conexao.Leitor["id_cliente"].ToString()),
                        nome = Conexao.Leitor["nome"].ToString(),
                        cpf = Conexao.Leitor["cpf"].ToString(),
                        genero = (Genero)Enum.Parse(typeof(Genero), Conexao.Leitor["genero"].ToString()),

                        dddCel = Conexao.Leitor["ddd_cel"].ToString(),
                        celular = Conexao.Leitor["celular"].ToString(),
                        dddTel = Conexao.Leitor["ddd_tel"].ToString(),
                        telefone = Conexao.Leitor["telefone"].ToString(),
                        email = Conexao.Leitor["email"].ToString(),
                        dataDeNascimento = DataUtil.Converter(Conexao.Leitor, "data_de_nascimento"),

                        endereco = new EnderecoModel
                        {
                            id = int.Parse(Conexao.Leitor["id_endereco"].ToString())
                        },

                        ativo = true
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

            if (lista.Count > 0)
                lista.ForEach(x => x.endereco.Carregar());

            return lista;
        }

        public static List<ClienteModel> CarregarTodos() =>
            Pesquisar(FiltroPessoa.nome, "");

        public List<ConsultaModel> Historio() =>
            ConsultaModel.Historio(id);

        #endregion
    }
}
