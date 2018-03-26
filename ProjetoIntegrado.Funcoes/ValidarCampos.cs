using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace ProjetoIntegrado.Funcoes
{
    using Mensagens;
    using System;

    public static class ValidarCampos
    {
        public static MetroWindow JanelaPrincipal { get; set; }

        public static bool Validar(MetroWindow janela)
        {
            Mbox.JanelaPrincipal = JanelaPrincipal;

            var TbOk = ValidarTextBox(ValidaUtil.FindVisualChildren<TextBox>(janela));
            if (!TbOk) return false;

            var TbPass = ValidarPassword(ValidaUtil.FindVisualChildren<PasswordBox>(janela));
            if (!TbPass) return false;

            var TbCpf = ValidarCpf(ValidaUtil.FindVisualChildren<MaskedTextBox>(janela));
            if (!TbCpf) return false;

            var TbCnpj = ValidarCnpj(ValidaUtil.FindVisualChildren<MaskedTextBox>(janela));
            if (!TbCnpj) return false;

            var TbData = ValidarData(ValidaUtil.FindVisualChildren<MaskedTextBox>(janela));
            if (!TbData) return false;

            var tbHorario = ValidarHoras(ValidaUtil.FindVisualChildren<MaskedTextBox>(janela));
            if (!tbHorario) return false;

            var tbCombo = ValidarComboBox(ValidaUtil.FindVisualChildren<ComboBox>(janela));
            if (!tbCombo) return false;

            var tbDate = ValidarDatePicker(ValidaUtil.FindVisualChildren<DatePicker>(janela));
            if (!tbDate) return false;

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

        private static bool ValidarPassword(IEnumerable<PasswordBox> lTextBoxs)
        {
            foreach (PasswordBox tb in lTextBoxs)
            {
                if (tb?.Tag?.ToString() == "*")
                    if (string.IsNullOrEmpty(tb.Password.Trim()))
                    {
                        Mbox.CampoInvalido(tb.Uid);
                        tb.Focus();

                        return false;
                    }
            }

            return true;
        }

        private static bool ValidarCpf(IEnumerable<MaskedTextBox> lTextBoxs)
        {
            foreach (MaskedTextBox tb in lTextBoxs)
            {
                if (tb?.Tag?.ToString() == "cpf*")
                    if (string.IsNullOrEmpty(tb.Text.Trim()))
                    {
                        Mbox.CampoInvalido(tb.Uid);
                        tb.Focus();

                        return false;
                    }
                    else if (!ValidarEntrada.ValidaCPF(Mascara.Remover(tb.Text)))
                    {
                        Mbox.Afirmacao("Aviso", "O número do CPF informado é inválido!");
                        tb.Focus();
                        return false;
                    }
            }

            return true;
        }

        private static bool ValidarCnpj(IEnumerable<MaskedTextBox> lTextBoxs)
        {
            foreach (MaskedTextBox tb in lTextBoxs)
            {
                if (tb?.Tag?.ToString() == "cnpj*")
                    if (string.IsNullOrEmpty(tb.Text.Trim()))
                    {
                        Mbox.CampoInvalido(tb.Uid);
                        tb.Focus();

                        return false;
                    }
                    else if (!ValidarEntrada.ValidarCNPJ(Mascara.Remover(tb.Text)))
                    {
                        Mbox.Afirmacao("Aviso", "O número do CNPJ informado é inválido!");
                        tb.Focus();
                        return false;
                    }
            }

            return true;
        }

        private static bool ValidarData(IEnumerable<MaskedTextBox> lTextBoxs)
        {
            foreach (TextBox tb in lTextBoxs)
            {
                if (tb?.Tag?.ToString() == "data*")
                    if (!DataUtil.ValidarData(tb.Text))
                    {
                        Mbox.CampoInvalido(tb.Uid);
                        tb.Focus();

                        return false;
                    }
            }

            return true;
        }

        private static bool ValidarComboBox(IEnumerable<ComboBox> lCombo)
        {
            foreach (ComboBox cb in lCombo)
            {
                if (cb?.Tag?.ToString() == "*")
                    if (string.IsNullOrEmpty(cb.Text.Trim()))
                    {
                        Mbox.CampoInvalido(cb.Uid);
                        cb.Focus();

                        return false;
                    }
            }

            return true;
        }

        private static bool ValidarDatePicker(IEnumerable<DatePicker> lDates)
        {
            foreach (DatePicker dp in lDates)
            {
                if (dp?.Tag?.ToString() == "*")
                    if (!DataUtil.ValidarData(dp.Text))
                    {
                        Mbox.CampoInvalido(dp.Uid);
                        dp.SelectedDate = DateTime.Now;
                        dp.Focus();

                        return false;
                    }
            }

            return true;
        }

        private static bool ValidarHoras(IEnumerable<MaskedTextBox> lTextBoxs)
        {
            foreach (TextBox tb in lTextBoxs)
            {
                if (tb?.Tag?.ToString() == "horas*")
                    if (!DataUtil.ValidarHorario(tb.Text))
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
