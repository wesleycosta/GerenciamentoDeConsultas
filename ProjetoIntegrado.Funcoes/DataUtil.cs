using System;
using System.Data.SqlClient;

namespace ProjetoIntegrado.Funcoes
{
    public static class DataUtil
    {
        public static DateTime Converter(SqlDataReader leitor, string index)
        {
            DateTime dt = new DateTime();

            if (leitor[index] != DBNull.Value)
                dt = DateTime.Parse(leitor[index].ToString());

            return dt;
        }

        public static DateTime Converter(string valor)
        {
            var data = DateTime.Now;
            var ok = DateTime.TryParse(valor, out data);

            return data;
        }

        public static DateTime GetPrimeiroDia(DateTime dt) =>
            new DateTime(dt.Year, dt.Month, 1);

        public static DateTime GetUltimoDia(DateTime dt) =>
            new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));

        public static DateTime GetPrimeiroDiaDesseMes() =>
            GetPrimeiroDia(DateTime.Now);

        public static DateTime GetUltimoDiaDesseMes() =>
            GetUltimoDia(DateTime.Now);

        public static bool ValidarData(string text)
        {
            if (string.IsNullOrEmpty(text.Trim()))
                return false;

            try
            {
                var x = DateTime.Parse(text);

                if (x.Year < 1800 || x.Year > 2050)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool ValidarHorario(string text)
        {
            if (!string.IsNullOrEmpty(text.Trim()))
                try
                {
                    var tm = DateTime.Now.TimeOfDay;

                    return TimeSpan.TryParse(text, out tm);
                }
                catch (Exception)
                {
                    return false;
                }

            return true;
        }
    }
}
