using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntegrado.ConsoleTeste
{
    using BaseDeDados;
    using Model;

    class Program
    {
        static void Main(string[] args)
        {
            Conexao.Iniciar();
            CargoModel cargo = new CargoModel { id = 1 };

            FuncionarioModel funcionario = new FuncionarioModel
            {
                nome = "ADMIN",
                cargo = cargo,
                cpf = "123",
                genero = Genero.Masculino,
                dataDeNascimento = new DateTime(1996, 10, 28),
                dataDeAdmissao = DateTime.Now,
                dddTel = "12",
                telefone = "1233",
                celular = "",
                dddCel = "",
                email = "",
                salario = 1000,
                senha = "ADMIN123",
                usuario = "ADMIN",

                endereco = new EnderecoModel
                {
                    cep = "123566",
                    cidade = "CAMPOS DO JORDAO",
                    uf = "SP",
                    bairro = "VILA BRITANIA",
                    logradouro = "AV. PRINCIPAL",
                    complemento = "CASA 1",
                    numero = "123"
                }
            };

            funcionario.Cadastrar();

            Console.ReadKey();
        }
    }
}
