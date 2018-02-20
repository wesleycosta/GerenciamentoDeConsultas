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
    public static class ValidarCampos
    {
        public static MetroWindow JanelaPrincipal { get; set; }

        public static bool Validar(MetroWindow janela)
        {
            Mbox.JanelaPrincipal = JanelaPrincipal;
            var TbOk = ValidarTextBox(ValidaUtil.FindVisualChildren<TextBox>(janela));

            if (!TbOk) return false;

            return true;
        }

        private static bool ValidarTextBox(IEnumerable<TextBox> lTextBoxs)
        {
            foreach (TextBox tb in lTextBoxs)
            {
                if (tb?.Tag?.ToString() == "*")
                    if (string.IsNullOrEmpty(tb.Text.Trim()))
                    {
                        Mbox.CampoInvalido(tb.Uid);
                        tb.Focus();

                        return false;
                    }
            }

            return true;
        }
    }
}
