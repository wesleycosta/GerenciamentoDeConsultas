using System;
using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Clientes
{
    using Model;
    using Model.Estado;
    using Funcoes;

    public partial class CadClienteWin
    {
        public bool cadastrou { get; set; }

        private ClienteModel cliente;
        private bool cadastrar;

        public CadClienteWin()
        {
            InitializeComponent();
            lbCodigo.Visibility = lbCodigoText.Visibility = Visibility.Hidden;
            paginaHistorico.Visibility = Visibility.Hidden;
            tabControl.RemoveFromSource(paginaHistorico);

            Inicializar();
            Title = "NOVO PACIENTE";
            cadastrar = true;
        }

        public CadClienteWin(ClienteModel cliente)
        {
            this.cliente = cliente;

            InitializeComponent();
            Inicializar();

            Title = "EDITAR PACIENTE";
            CarregarDados();
        }

        #region INICIALIZAR

        private void Inicializar()
        {
            cbUf.ItemsSource = EstadoModel.Siglas;
            AdicionarEventos();
            Loaded += (o, a) => tbNome.Focus();
        }

        private void AdicionarEventos()
        {
            tbNumero.KeyDown += ValidarEntrada.Naturais_KeyPress;
        }

        #endregion

        #region  CARREGAR DADOS

        private void CarregarDados()
        {
            lbCodigo.Content = cliente.id.ToString("D3");
            tbNome.Text = cliente.nome;
            tbCpf.Text = cliente.cpf;
            cbGenero.SelectedIndex = cliente.genero == Genero.Masculino ? 0 : 1;

            tbDataNascimento.Text = cliente.dataDeNascimento.ToString("d");

            tbDddCel.Text = cliente.dddCel.Trim();
            tbCelular.Text = cliente.celular;
            tbDddTel.Text = cliente.dddTel.Trim();
            tbTelefone.Text = cliente.telefone;
            tbEmail.Text = cliente.email;

            CarregarEndereco(cliente.endereco);

            var historico = cliente.Historio(); 
            lvwHistorico.ItemsSource = historico;
            lbTotalRegistro.Content = historico.Count.ToString("D3");
        }

        private void CarregarEndereco(EnderecoModel endereco)
        {
            tbCep.Text = endereco.cep;
            tbCidade.Text = endereco.cidade;
            cbUf.SelectedItem = endereco.uf;
            tbBairro.Text = endereco.bairro;
            tbLogradouro.Text = endereco.logradouro;
            tbNumero.Text = endereco.numero;
            tbComplemento.Text = endereco.complemento;
        }

        #endregion

        #region MANTEM PACIENTE

        #region  TO MODEL

        private EnderecoModel ToModelEndereco() =>
            new EnderecoModel
            {
                id = cliente?.endereco?.id ?? 0,
                cep = Mascara.Remover(tbCep.Text),
                cidade = tbCidade.Text,
                uf = cbUf.Text,
                bairro = tbBairro.Text,
                logradouro = tbLogradouro.Text,
                numero = tbNumero.Text,
                complemento = tbComplemento.Text
            };

        private ClienteModel ToModel() =>
            new ClienteModel
            {
                id = cliente?.id ?? 0,
                nome = tbNome.Text,
                cpf = Mascara.Remover(tbCpf.Text),
                genero = cbGenero.SelectedIndex == 0 ? Genero.Masculino : Genero.Feminino,
                dataDeNascimento = DataUtil.Converter(tbDataNascimento.Text),

                dddCel = tbDddCel.Text.Trim(),
                celular = tbCelular.Text,
                dddTel = tbDddTel.Text.Trim(),
                telefone = tbTelefone.Text,
                email = tbEmail.Text,

                endereco = ToModelEndereco()
            };

        #endregion

        private void MantemCliente()
        {
            cliente = ToModel();

            if (cadastrar)
                cliente.Cadastrar();
            else
                cliente.Atualizar();
        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
            {
                MantemCliente();
                cadastrou = true;
                Close();
            }
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e) => Close();

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
