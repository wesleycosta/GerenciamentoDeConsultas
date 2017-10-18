using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
	using BaseDeDados;
	using Funcoes;
	using View;

	public partial class FuncionarioModel : ICadastro
	{
		#region ICADASTRO

		public void Cadastrar()
		{
			try
			{
				endereco.Cadastrar();

				var cmd = @"INSERT INTO funcionario
	                            (id_endereco, 
	                             id_cargo, 
	                             nome, 
	                             genero, 
	                             cpf, 
	                             data_de_nascimento, 
	                             ddd_cel, 
	                             celular, 
	                             ddd_tel, 
	                             telefone, 
	                             email, 
	                             salario, 
	                             data_de_admissao, 
	                             usuario, 
	                             senha, 
	                             ativo)
                            OUTPUT inserted.id_funcionario
                            VALUES
	                            (@id_endereco, 
	                             @id_cargo, 
	                             @nome, 
	                             @genero, 
	                             @cpf, 
	                             @data_de_nascimento, 
	                             @ddd_cel, 
	                             @celular, 
	                             @ddd_tel, 
	                             @telefone, 
	                             @email, 
	                             @salario, 
	                             @data_de_admissao, 
	                             @usuario, 
	                             @senha, 
	                             @ativo)";

				Conexao.AbrirConexao();
				Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

				Conexao.Cmd.Parameters.AddWithValue("id_endereco", endereco.id);
				Conexao.Cmd.Parameters.AddWithValue("id_cargo", cargo.id);
				Conexao.Cmd.Parameters.AddWithValue("nome", nome);
				Conexao.Cmd.Parameters.AddWithValue("genero", genero);
				Conexao.Cmd.Parameters.AddWithValue("cpf", cpf);
				Conexao.Cmd.Parameters.AddWithValue("data_de_nascimento", dataDeNascimento);
				Conexao.Cmd.Parameters.AddWithValue("ddd_cel", dddCel);
				Conexao.Cmd.Parameters.AddWithValue("celular", celular);
				Conexao.Cmd.Parameters.AddWithValue("ddd_tel", dddTel);
				Conexao.Cmd.Parameters.AddWithValue("telefone", telefone);
				Conexao.Cmd.Parameters.AddWithValue("email", email);
				Conexao.Cmd.Parameters.AddWithValue("salario", salario);
				Conexao.Cmd.Parameters.AddWithValue("data_de_admissao", dataDeAdmissao);
				Conexao.Cmd.Parameters.AddWithValue("usuario", usuario);
				Conexao.Cmd.Parameters.AddWithValue("senha", senhaMd5());
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
				var cmd = @"UPDATE funcionario SET
	                            id_cargo			= @id_cargo,
	                            nome				= @nome,
	                            cpf					= @cpf,
	                            genero				= @genero,
	                            data_de_nascimento  = @data_de_nascimento,
	                            ddd_cel				= @ddd_cel,
	                            celular				= @celular,
	                            ddd_tel				= @ddd_tel,
	                            telefone			= @telefone,
	                            email				= @email,
	                            salario				= @salario,
	                            data_de_admissao	= @data_de_admissao,
	                            usuario				= @usuario,
	                            senha				= @senha,
	                            ativo				= @ativo
                            WHERE					  
	                            id_funcionario		= @id";

				Conexao.AbrirConexao();
				Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

				Conexao.Cmd.Parameters.AddWithValue("id", id);
				Conexao.Cmd.Parameters.AddWithValue("id_cargo", cargo.id);
				Conexao.Cmd.Parameters.AddWithValue("nome", nome);
				Conexao.Cmd.Parameters.AddWithValue("cpf", cpf);
				Conexao.Cmd.Parameters.AddWithValue("genero", genero);
				Conexao.Cmd.Parameters.AddWithValue("data_de_nascimento", dataDeNascimento);
				Conexao.Cmd.Parameters.AddWithValue("ddd_cel", dddCel);
				Conexao.Cmd.Parameters.AddWithValue("celular", celular);
				Conexao.Cmd.Parameters.AddWithValue("ddd_tel", dddTel);
				Conexao.Cmd.Parameters.AddWithValue("telefone", telefone);
				Conexao.Cmd.Parameters.AddWithValue("email", email);
				Conexao.Cmd.Parameters.AddWithValue("salario", salario);
				Conexao.Cmd.Parameters.AddWithValue("data_de_admissao", dataDeAdmissao);
				Conexao.Cmd.Parameters.AddWithValue("usuario", usuario);
				Conexao.Cmd.Parameters.AddWithValue("senha", senhaMd5());
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
			ativo = endereco.ativo = false;
			endereco.Atualizar();
			Atualizar();
		}

		public void Carregar()
		{
			try
			{
				var cmd = @"SELECT
	                            ISNULL(id_endereco, 0) AS id_endereco,
	                            id_cargo,
	                            nome, 
	                            genero,
	                            cpf,
	                            data_de_nascimento,
	                            ddd_cel,
	                            celular,
	                            ddd_tel,
	                            telefone,
	                            email,
	                            ISNULL(salario, 0) AS salario,
	                            data_de_admissao,
	                            usuario,
	                            senha,
	                            ativo
                            FROM
	                            funcionario
                            WHERE
	                            id_funcionario	= @id";

				Conexao.AbrirConexao();
				Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
				Conexao.Cmd.Parameters.AddWithValue("id", id);

				Conexao.Leitor = Conexao.Cmd.ExecuteReader();

				if (Conexao.Leitor.Read())
				{
					nome = Conexao.Leitor["nome"].ToString();
					cpf = Conexao.Leitor["cpf"].ToString();
					genero = (Genero)Enum.Parse(typeof(Genero), Conexao.Leitor["genero"].ToString());

					dddCel = Conexao.Leitor["ddd_cel"].ToString();
					celular = Conexao.Leitor["celular"].ToString();
					dddTel = Conexao.Leitor["ddd_tel"].ToString();
					telefone = Conexao.Leitor["telefone"].ToString();
					email = Conexao.Leitor["email"].ToString();

					salario = decimal.Parse(Conexao.Leitor["salario"].ToString());
					usuario = Conexao.Leitor["usuario"].ToString();
					senhaHash = Conexao.Leitor["senha"].ToString();

					dataDeNascimento.Converter(Conexao.Leitor, "data_de_nascimento");
					dataDeAdmissao.Converter(Conexao.Leitor, "data_de_admissao");

					endereco = new EnderecoModel
					{
						id = int.Parse(Conexao.Leitor["id_endereco"].ToString())
					};

					cargo = new CargoModel
					{
						id = int.Parse(Conexao.Leitor["id_cargo"].ToString())
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
			cargo?.Carregar();
		}

		#endregion

		#region CARREGAR LISTA

		public static List<FuncionarioModel> Pesquisar(FiltroPessoa filtro, string pesquisa)
		{
			var lista = new List<FuncionarioModel>();

			try
			{
				var cmd = $@"SELECT TOP 50
                                id_funcionario,
                                id_endereco,
	                            id_cargo,
	                            nome, 
	                            genero,
	                            cpf,
	                            data_de_nascimento,
	                            ddd_cel,
	                            celular,
	                            ddd_tel,
	                            telefone,
	                            email,
	                            salario,
	                            data_de_admissao,
	                            usuario,
	                            senha,
	                            ativo
                            FROM
	                            funcionario
                            WHERE
	                            ativo		    = 1
								AND
								id_funcionario != 1
	                            AND
	                            {filtro} LIKE @pesquisa
                            ORDER BY
                                id_funcionario";

				Conexao.AbrirConexao();
				Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);
				Conexao.Cmd.Parameters.AddWithValue("pesquisa", $"%{pesquisa}%");
				Conexao.Leitor = Conexao.Cmd.ExecuteReader();

				while (Conexao.Leitor.Read())
					lista.Add(new FuncionarioModel
					{
						id = int.Parse(Conexao.Leitor["id_funcionario"].ToString()),
						nome = Conexao.Leitor["nome"].ToString(),
						cpf = Conexao.Leitor["cpf"].ToString(),
						genero = (Genero)Enum.Parse(typeof(Genero), Conexao.Leitor["genero"].ToString()),

						dddCel = Conexao.Leitor["ddd_cel"].ToString(),
						celular = Conexao.Leitor["celular"].ToString(),
						dddTel = Conexao.Leitor["ddd_tel"].ToString(),
						telefone = Conexao.Leitor["telefone"].ToString(),
						email = Conexao.Leitor["email"].ToString(),

						salario = decimal.Parse(Conexao.Leitor["salario"].ToString()),
						usuario = Conexao.Leitor["usuario"].ToString(),
						senhaHash = Conexao.Leitor["senha"].ToString(),

						dataDeNascimento = DataUtil.Converter(Conexao.Leitor, "data_de_nascimento"),
						dataDeAdmissao = DataUtil.Converter(Conexao.Leitor, "data_de_admissao"),

						endereco = new EnderecoModel
						{
							id = int.Parse(Conexao.Leitor["id_endereco"].ToString())
						},

						cargo = new CargoModel
						{
							id = int.Parse(Conexao.Leitor["id_cargo"].ToString())
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
			{
				lista.ForEach(x => x.cargo.Carregar());
				lista.ForEach(x => x.endereco.Carregar());
			}

			return lista;
		}

		public static List<FuncionarioModel> CarregarTodos()
		{
			return Pesquisar(FiltroPessoa.nome, "");
		}

		#endregion

		#region LOGIN

		public static int Autenticar(string usuario, string senha)
		{
			var id = 0;
			senha = MD5.Criptografar(senha);

			try
			{
				var cmd = @"SELECT
								id_funcionario
							FROM
								funcionario
							WHERE
								usuario		= @usuario
								AND
								senha		= @senha
								AND
								ativo		= 1";

				Conexao.AbrirConexao();
				Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoSQL);

				Conexao.Cmd.Parameters.AddWithValue("usuario", usuario);
				Conexao.Cmd.Parameters.AddWithValue("senha", senha);
				Conexao.Leitor = Conexao.Cmd.ExecuteReader();

				if (Conexao.Leitor.Read())
					id = int.Parse(Conexao.Leitor["id_funcionario"].ToString());
			}
			catch (Exception ex)
			{
				Excecao.Mostrar(ex);
			}
			finally
			{
				Conexao.FecharConexao();
			}

			return id;
		}

		#endregion
	}
}
