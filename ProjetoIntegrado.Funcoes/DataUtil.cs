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

        public static DateTime Converter(string valor)
        {
            return DateTime.Now;
        }

        public static DateTime GetPrimeiroDia(DateTime dt) =>
            new DateTime(dt.Year, dt.Month, 1);

        public static DateTime GetUltimoDia(DateTime dt) =>
            new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));

        public static DateTime GetPrimeiroDiaDesseMes() =>
            GetPrimeiroDia(DateTime.Now);

        public static DateTime GetUltimoDiaDesseMes() =>
            GetUltimoDia(DateTime.Now);

    }
}
