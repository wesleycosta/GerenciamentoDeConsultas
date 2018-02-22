using MahApps.Metro.Controls;
using ProjetoIntegrado.Mensagens;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjetoIntegrado.Funcoes
{
    public static class ValidarEntrada
    {
        #region VALIDAR KEY PRESS

        // EVENTO KEY PRESS PARA VALIDAR NUMERO REAIS
        public static void Real_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Tab)
            {
                // 0 = 48 ||| 9 = 57
                int ascii = KeyInterop.VirtualKeyFromKey(e.Key);
                var tb = sender as TextBox;

                if (e.Key == Key.OemComma)
                {
                    if (tb.Text.Contains(",") || tb.Text.Length == 0)
                        e.Handled = true;
                }
                else if ((ascii < 48 || ascii > 57) && e.Key != Key.Back) // VALDIDA APENAS VALORES NUMERICOS (0 ATÉ 9)
                    e.Handled = true;
            }
        }

        // EVENTO KEY PRESS PARA VALIDAR NUMERO NATURAIS
        public static void Naturais_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Tab)
            {
                int ascii = KeyInterop.VirtualKeyFromKey(e.Key);

                if ((ascii < 48 || ascii > 57) && e.Key != Key.Back)
                    e.Handled = true;
            }
        }

        // EVENTO KEY PRESS PARA VALIDA LETRA DE 'A' ATÉ 'Z' COM '_'
        public static void LetrasA_Z_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Tab)
            {
                int ascii = KeyInterop.VirtualKeyFromKey(e.Key);

                if (e.Key != Key.Back)
                {
                    // VALIDA CAMPO CAIXA BAIXA
                    if (ascii >= 97 && ascii <= 122)
                        ascii -= 32;

                    if ((ascii < 65 || ascii > 90) && ascii != '_')
                        e.Handled = true;
                }
            }
        }

        #endregion

        #region VALIDAR EMAIL

        public static bool ValidaEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return false;

            var EmailAddress =
                new Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$");
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

        #region Valida CPF

        public static bool ValidaCPF(string cpf)
        {

            if (cpf.Trim() == string.Empty) return false;

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
    }
}
