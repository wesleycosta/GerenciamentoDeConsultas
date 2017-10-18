using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace ProjetoIntegrado.View.Funcionario
{
    using Model;
    using Model.Estado;
    using Funcoes;
    using WebServices;

    public partial class CadFuncionarioWin 
    {
        #region PROPRIEDADES E CTOR

        public bool cadastrou;
        private List<CargoModel> cargos;
        private FuncionarioModel funcionario;

        public CadFuncionarioWin()
        {
            InitializeComponent();
            Inicializar();
            lbCodigo.Visibility = lbCodigoText.Visibility = Visibility.Hidden;

            Title = "NOVO FUNCIONÁRIO";
            cadastrou = true;
        }

        public CadFuncionarioWin(FuncionarioModel funcionario)
        {
            this.funcionario = funcionario;

            InitializeComponent();
            Inicializar();

            Title = "EDITAR FUNCIONÁRIO";
            CarregarDados();
        }

        private void Inicializar()
        {
            cargos = CargoModel.CarregarTodos();

            cbCargo.ItemsSource = cargos;
            cbUf.ItemsSource = EstadoModel.Siglas;

            AdicionarEventos();

            Loaded += (o, a) => tbNome.Focus();
        }

        private void AdicionarEventos()
        {
            tbCep.LostFocus += (o, a) => MantemBuscaCep();
            tbSalario.KeyDown += Validar.Real_KeyPress;
            tbNumero.KeyDown += Validar.Naturais_KeyPress;
        }

        #endregion

        #region  MANTEM CEP

        private async void MantemBuscaCep()
        {
            var cep = Mascara.Remover(tbCep.Text);

            if (cep != string.Empty)
            {
                var viaCep = new ViaCep();
                var end = await viaCep.BuscarCep(cep);

                if (end != null)
                    CarregarEndereco(end);
            }
        }

        #endregion

        #region  CARREGAR DADOS

        private void CarregarDados()
        {
            lbCodigo.Content = funcionario.id.ToString("D3");
            tbNome.Text = funcionario.nome;
            tbCpf.Text = funcionario.cpf;
            cbGenero.SelectedIndex = funcionario.genero == Genero.Masculino ? 0 : 1;

            tbDataNascimento.Text = funcionario.dataDeNascimento.ToString("d");
            cbCargo.SelectedIndex = cargos.FindIndex(x => x.id == funcionario.cargo.id);
            tbDataAdminissao.Text = funcionario.dataDeAdmissao.ToString("d");
            tbSalario.Text = funcionario.salario.ToString("n");

            tbDddCel.Text = funcionario.dddCel;
            tbCelular.Text = funcionario.celular;
            tbDddTel.Text = funcionario.dddTel;
            tbTelefone.Text = funcionario.telefone;
            tbEmail.Text = funcionario.email;

            tbUsuario.Text = funcionario.usuario;

            CarregarEndereco(funcionario.endereco);
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

        #region MANTEM FUNCIONÁRIO

        #region  TO MODEL

        private EnderecoModel ToModelEndereco() =>
            new EnderecoModel
            {
                id = funcionario?.endereco?.id ?? 0,
                cep = Mascara.Remover(tbCep.Text),
                cidade = tbCidade.Text,
                uf = cbUf.Text,
                bairro = tbBairro.Text,
                logradouro = tbLogradouro.Text,
                numero = tbNumero.Text,
                complemento = tbComplemento.Text
            };

        private FuncionarioModel ToModel() =>
            new FuncionarioModel
            {
                id = funcionario?.id ?? 0,
                nome = tbNome.Text,
                cpf = Mascara.Remover(tbCpf.Text),
                genero = cbGenero.SelectedIndex == 0 ? Genero.Masculino : Genero.Feminino,
                dataDeNascimento = DataUtil.Converter(tbDataNascimento.Text),

                cargo = cargos[cbCargo.SelectedIndex],
                dataDeAdmissao = DataUtil.Converter(tbDataAdminissao.Text),
                salario = tbSalario.Text != string.Empty ? decimal.Parse(tbSalario.Text) : 0,

                dddCel = tbDddCel.Text,
                celular = tbCelular.Text,
                dddTel = tbDddTel.Text,
                telefone = tbTelefone.Text,
                email = tbEmail.Text,

                endereco = ToModelEndereco(),

                usuario = tbUsuario.Text,
                senha = tbSenha.Password,
                senhaHash = funcionario?.senhaHash
            };

        #endregion

        private void MantemFuncionario()
        {
            funcionario = ToModel();

            if (cadastrou)
                funcionario.Cadastrar();
            else
                funcionario.Atualizar();
        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            MantemFuncionario();
            cadastrou = true;
            Close();
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
