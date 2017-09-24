using System;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoIntegrado.BaseDeDados
{
    using View;

    public static class Conexao
    {
        #region PROPRIEDADES E INICIAR

        public static SqlConnection ConexaoSQL { get; private set; }
        public static SqlConnection ConexaoMaster { get; private set; }
        public static SqlCommand Cmd { get; set; }
        public static SqlDataReader Leitor { get; set; }
        public static ConfiguracaoArquivo Configuracao { get; private set; }

        public static bool Iniciar()
        {
            Configuracao = ConfiguracaoArquivo.Carregar();
            ConfigurarStringDeConexao();

            return Configuracao.banco != null && Configuracao.servidor != null;
        }

        #endregion

        #region CONFIGURA STRING DE CONEXAO

        private static void ConfigurarStringDeConexao()
        {
            if (Configuracao.IsLocalhost)
            {
                ConexaoSQL = new SqlConnection(GetStrDeConexaoLocal(Configuracao.banco));
                ConexaoMaster = new SqlConnection(GetStrDeConexaoLocal("master"));
            }
            else
            {
                ConexaoSQL = new SqlConnection(GetStrDeConexaoRede(Configuracao.banco));
                ConexaoMaster = new SqlConnection(GetStrDeConexaoRede("master"));
            }
        }

        private static string GetStrDeConexaoRede(string nomeBanco) =>
              $@"Data Source={GetDataSource()}; Network Library=DBMSSOCN;
               Initial Catalog={nomeBanco}; User ID={Configuracao.usuario}; Password={Configuracao.senha};";

        private static string GetStrDeConexaoLocal(string nomeBanco) =>
              $@"Data Source={GetDataSource()};Initial Catalog={nomeBanco};Integrated Security=SSPI;
                User ID={Configuracao.usuario};Password={Configuracao.senha};";


        private static string GetDataSource()
        {
            if (Configuracao.IsLocalhost)
                return @".\SQLEXPRESS";

            return $"{Configuracao.servidor},{Configuracao.porta}";
        }

        #endregion

        #region ABRIR E FECHAR CONEXAO

        #region PROJETO

        public static void AbrirConexao()
        {
            AbrirConexao(ConexaoSQL);
        }

        public static void AbrirConexaoMaster()
        {
            AbrirConexao(ConexaoMaster);
        }

        #endregion

        #region MASTER

        public static void FecharConexao()
        {
            FecharConexao(ConexaoSQL);
        }

        public static void FecharConexaoMaster()
        {
            FecharConexao(ConexaoMaster);
        }

        #endregion

        private static void AbrirConexao(SqlConnection con)
        {
            if (ConexaoSQL.State != ConnectionState.Closed)
                FecharConexao(con);

            con.Open();
        }

        private static void FecharConexao(SqlConnection con)
        {
            con.Close();
        }

        #endregion

        #region EXECUTAR

        public static void Executar(string cmd)
        {
            try
            {
                AbrirConexao();
                Cmd = new SqlCommand(cmd, ConexaoSQL);
                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
            }
            finally
            {
                FecharConexao();
            }
        }

        public static void ExecutarMaster(string cmd)
        {
            try
            {
                AbrirConexaoMaster();
                Cmd = new SqlCommand(cmd, ConexaoMaster);
                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
            }
            finally
            {
                FecharConexaoMaster();
            }
        }

        #endregion
    }
}