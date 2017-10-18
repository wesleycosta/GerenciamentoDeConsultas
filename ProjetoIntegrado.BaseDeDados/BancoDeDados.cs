
using System;

namespace ProjetoIntegrado.BaseDeDados
{
    using View;
    using System.Data.SqlClient;

    public static class BancoDeDados
    {
        #region CRIAÇÃO DA BASE DE DADOS (BANCO E TABELAS)
        public static void CriarBaseDeDados()
        {
            CriarBanco();
            CriarTabelas();
        }

        private static void CriarBanco()
        {
            var cmd = $@"CREATE DATABASE {Conexao.Configuracao.banco}";

            Conexao.ExecutarMaster(cmd);
        }

        private static void CriarTabelas()
        {
            foreach (var tabela in ResourceBanco.Banco.Split(';'))
                Conexao.Executar(tabela);
        }

        #endregion

        public static bool ExisteBaseDeDados()
        {
            var existe = false;

            try
            {
                var cmd = @"SELECT
                                COUNT(*)
                            FROM 
                                sys.databases
                            WHERE
                                name = @nome_banco";

                Conexao.AbrirConexaoMaster();
                Conexao.Cmd = new SqlCommand(cmd, Conexao.ConexaoMaster);
                Conexao.Cmd.Parameters.AddWithValue("nome_banco", Conexao.Configuracao.banco);

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
                Conexao.FecharConexaoMaster();
            }

            return existe;
        }
    }
}
