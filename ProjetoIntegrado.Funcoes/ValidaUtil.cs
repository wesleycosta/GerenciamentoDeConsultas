using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace ProjetoIntegrado.Funcoes
{
    internal static class ValidaUtil
    {
        #region EMAIL
        public static bool ValidaEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return false;

            var EmailAddress = new Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$");
            var match = EmailAddress.Match(emailAddress);

            return match.Success;
        }

        #endregion

        #region CNPJ

        public static bool ValidarCNPJ(string cnpj)
        {
            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj;
            string digito;
            string verifica;
            int soma;
            int resto;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace("/", "").Replace(".", "").Replace("-", "");

            if (cnpj.Length == 14)
            {
                verifica = cnpj.Substring(12);
                tempCnpj = cnpj.Substring(0, 12);
                soma = 0;

                for (var i = 0; i < 12; i++)
                    soma = soma + int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

                resto = soma % 11;
                resto = resto < 2 ? 0 : 11 - resto;
                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;

                for (var i = 0; i < 13; i++)
                    soma = soma + int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

                resto = soma % 11;
                resto = resto < 2 ? 0 : 11 - resto;
                digito = digito + resto;

                if (digito != verifica)
                    return false;

                for (var i = 0; i < 10; i++)
                    if (cnpj == new string(char.Parse(i.ToString()), 14))
                        return false;
            }
            else
                return false;

            return true;
        }

        #endregion

        #region  CPF

        public static bool ValidaCPF(string cpf)
        {
            cpf = cpf.Replace("-", "").Replace(".", "").Trim();
            if (cpf.Length != 11)
                return false;

            for (var i = 0; i < 10; i++)
                if (cpf == new string(char.Parse(i.ToString()), 11))
                    return false;

            var codVerificador = cpf.Substring(9);
            var primeiroDigito = GetPrimeiroCodigoVerificador(cpf);
            var segundoDigito = GetSegundoDigito(cpf, primeiroDigito);

            if (primeiroDigito + segundoDigito != codVerificador)
                return false;

            return true;
        }

        private static string GetPrimeiroCodigoVerificador(string cpf)
        {
            var cpfSemCodVerificador = cpf.Substring(0, 9);
            int soma = 0, resto = 0;

            for (var i = 0; i < 9; i++)
                soma = soma + int.Parse(cpfSemCodVerificador[i].ToString()) * (10 - i);

            resto = soma % 11;

            return (resto < 2 ? 0 : 11 - resto).ToString();
        }

        private static string GetSegundoDigito(string cpf, string primeiroDigito)
        {
            int soma = 0, resto = 0;
            var segundoDigito = "";
            var cpfSemCodVerificador = cpf.Substring(0, 9);

            cpfSemCodVerificador += primeiroDigito;

            for (var i = 0; i < 10; i++)
                soma = soma + int.Parse(cpfSemCodVerificador[i].ToString()) * (11 - i);

            resto = soma % 11;
            return segundoDigito + (resto < 2 ? 0 : 11 - resto);
        }

        #endregion

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
