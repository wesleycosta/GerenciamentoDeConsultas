using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;


namespace ProjetoIntegrado.View.Consultas.Material
{
    using Model;
    using Funcoes;

    public partial class CadMaterialCirurgiaWin
    {
        #region PROPRIEDADES E CTOR

        public bool cadastrou { get; private set; }
        private readonly CirurgiaModel cirurgia;
        private List<MaterialModel> materiais = new List<MaterialModel>();

        public CadMaterialCirurgiaWin(CirurgiaModel cirurgia)
        {
            this.cirurgia = cirurgia;
            InitializeComponent();
            Iniciar();
        }

        #endregion

        #region CARREGAR E INICIAR

        private void Iniciar()
        {
            tbQtdParcelas.KeyDown += ValidarEntrada.Naturais_KeyPress;
            CarregarMateriais();
            Loaded += (o, a) => cbMaterial.Focus();
        }

        private void CarregarMateriais()
        {
            materiais = MaterialModel.CarregarTodos();
            cbMaterial.Items.Clear();
            materiais.ForEach(item => cbMaterial.Items.Add(item.descricao));

            if (materiais.Count > 0)
                cbMaterial.SelectedIndex = 0;
        }

        private void CarregarPreco()
        {
            if (cbMaterial.SelectedIndex >= 0)
            {
                var material = materiais[cbMaterial.SelectedIndex];
                int quantidade = 0;

                int.TryParse(tbQtdParcelas.Text, out quantidade);

                tbValorUnitario.Text = $"{material.valor:n}";
                tbValor.Text = $"{material.valor * quantidade:n}";
            }
        }

        #endregion

        #region MANTEM MATERIAL

        private MaterialCirurgiaModel ToModel() =>
            new MaterialCirurgiaModel
            {
                cirurgia = cirurgia,
                material = materiais[cbMaterial.SelectedIndex],
                quantidade = int.Parse(tbQtdParcelas.Text),
                valorUnitario = materiais[cbMaterial.SelectedIndex].valor,
                ativo = true,
            };

        private void MantemMaterial()
        {
            var equipe = ToModel();
            equipe.Cadastrar();
        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
            {
                MantemMaterial();
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

        private void cbMaterial_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbMaterial.SelectedIndex >= 0)
                CarregarPreco();
        }

        private void tbQtdParcelas_KeyUp(object sender, KeyEventArgs e) =>
            CarregarPreco();

        #endregion
    }
}
