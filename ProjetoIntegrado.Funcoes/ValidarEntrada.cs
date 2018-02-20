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
    }
}
