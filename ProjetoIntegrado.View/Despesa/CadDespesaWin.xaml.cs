using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Despesa
{
    using Model;
    using Funcoes;
    using System;
    using System.Windows.Controls;

    public partial class CadDespesaWin
    {
        #region  PROPRIEDADES E CTOR
        public bool cadastrou { get; private set; }

        private readonly DespesaModel despesa;
        private readonly bool cadastrar;

        public CadDespesaWin()
        {
            InitializeComponent();
            Iniciar();

            Title = "NOVA DESPESA";
            cadastrar = true;
        }

        public CadDespesaWin(DespesaModel despesa)
        {
            InitializeComponent();
            Iniciar();
            this.despesa = despesa;

            Title = "EDITAR DESPESA";
            CarregarDados();
        }

        #endregion

        #region CARREGAR E INICIAR

        private void Iniciar()
        {
            tbValor.KeyDown += ValidarEntrada.Real_KeyPress;
            tbData.SelectedDate = DateTime.Now;

            Loaded += (o, a) =>
            {
                DespesaModel.CarregarDiferentes().ForEach(x => cbDescricao.Items.Add(x));
                cbDescricao.Focus();
            };
        }

        private void CarregarDados()
        {
            cbDescricao.Text = despesa?.descricao;
            tbValor.Text = despesa?.valor.ToString("n");
            tbData.SelectedDate = despesa?.data;
        }

        #endregion

        #region MANTEM DESPESA

        private DespesaModel ToModel() =>
            new DespesaModel
            {
                id = despesa?.id ?? 0,
                descricao = cbDescricao.Text,
                valor = decimal.Parse(tbValor.Text),
                data = tbData.SelectedDate.Value
            };

        private void MantemDespesa()
        {
            var despesa = ToModel();

            if (cadastrar)
                despesa.Cadastrar();
            else
                despesa.Atualizar();
        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
            {
                MantemDespesa();
                cadastrou = true;
                Close();
            }
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e) =>
            Close();

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void cboTest_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ComboBox cbo = sender as ComboBox;

            if (cbo != null)
            {
                TextBox txt = cbo.Template.FindName("PART_EditableTextBox", cbo) as TextBox;

                if (txt != null)
                {
                    txt.CharacterCasing = CharacterCasing.Upper;
                }
            }
        }

        #endregion
    }
}
