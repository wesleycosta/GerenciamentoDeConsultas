using System;
using System.Data.SqlClient;

namespace ProjetoIntegrado.Funcoes
{
    public static class DataUtil
    {
        public static void Converter(this DateTime dt, SqlDataReader leitor, string index)
        {
            if (leitor[index] != DBNull.Value)
                dt = DateTime.Parse(leitor[index].ToString());
        }

        public static DateTime Converter(SqlDataReader leitor, string index)
        {
            DateTime dt = new DateTime();

            if (leitor[index] != DBNull.Value)
                dt = DateTime.Parse(leitor[index].ToString());

            return dt;
        }
    }
}
