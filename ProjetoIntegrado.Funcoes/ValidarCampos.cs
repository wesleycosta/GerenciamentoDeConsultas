using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace ProjetoIntegrado.Funcoes
{
    using Mensagens;

    public static class ValidarCampos
    {
        public static MetroWindow JanelaPrincipal { get; set; }

        public static bool Validar(MetroWindow janela)
        {
            Mbox.JanelaPrincipal = JanelaPrincipal;
            var TbOk = ValidarTextBox(ValidaUtil.FindVisualChildren<TextBox>(janela));

            if (!TbOk)
                return false;

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

        //private static bool ValidarCpf(IEnumerable<MaskedTextBox> lTextBoxs)
        //{
        //    foreach (MaskedTextBox tb in lTextBoxs)
        //    {
        //        if (tb?.Tag?.ToString() == "cpf*")
        //            if (ValidarEntrada.ValidaCPF(Mascara.Remover(tb.Text)))
        //            {
        //                Mbox.CampoInvalido(tb.Uid);
        //                tb.Focus();

        //                return false;
        //            }
        //    }

        //    return true;
        //}

        //private static bool ValidarData(IEnumerable<TextBox> lTextBoxs)
        //{
        //    foreach (TextBox tb in lTextBoxs)
        //    {
        //        if (tb?.Tag?.ToString() == "data*")
        //            if (string.IsNullOrEmpty(tb.Text.Trim()))
        //            {
        //                Mbox.CampoInvalido(tb.Uid);
        //                tb.Focus();

        //                return false;
        //            }
        //    }

        //    return true;
        //}

    }
}
