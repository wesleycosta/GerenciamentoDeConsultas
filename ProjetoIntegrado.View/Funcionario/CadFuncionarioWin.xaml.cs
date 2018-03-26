using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace ProjetoIntegrado.View.Funcionario
{
    using Model;
    using Model.Estado;
    using Funcoes;
    using Mensagens;

    public partial class CadFuncionarioWin
    {
        #region PROPRIEDADES E CTOR

        public bool cadastrou { get; set; }
        public bool AlterouOftal { get; set; }

        private List<CargoModel> cargos;
        private FuncionarioModel funcionario;
        private bool cadastrar;

        public CadFuncionarioWin()
        {
            InitializeComponent();
            Inicializar();
            lbCodigo.Visibility = lbCodigoText.Visibility = Visibility.Hidden;

            Title = "NOVO FUNCIONÁRIO";
            cadastrar = true;
        }

        public CadFuncionarioWin(FuncionarioModel funcionario)
        {
            this.funcionario = funcionario;

            InitializeComponent();
            Inicializar();

            Title = "EDITAR FUNCIONÁRIO";
            CarregarDados();
        }

        private bool ValidarSenha()
        {
            if (tbSenha.Password == string.Empty && cadastrar)
            {
                Mbox.CampoInvalido("Senha");
                tbSenha.Focus();
                return false;
            }

            if (tbConfirmarSenha.Password == string.Empty && cadastrar)
            {
                Mbox.CampoInvalido("Confimar Senha");
                tbConfirmarSenha.Focus();
                return false;
            }

            if (tbSenha.Password != string.Empty)
                if (tbSenha.Password != tbConfirmarSenha.Password)
                {
                    Mbox.Afirmacao("Aviso", "A senhas informadas são diferentes!");
                    tbSenha.Clear();
                    tbConfirmarSenha.Clear();
                    tbSenha.Focus();
                    return false;
                }

            return true;
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
            tbSalario.KeyDown += ValidarEntrada.Real_KeyPress;
            tbNumero.KeyDown += ValidarEntrada.Naturais_KeyPress;
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

            tbDddCel.Text = funcionario.dddCel.Trim();
            tbCelular.Text = funcionario.celular;
            tbDddTel.Text = funcionario.dddTel.Trim();
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

                dddCel = tbDddCel.Text.Trim(),
                celular = tbCelular.Text,
                dddTel = tbDddTel.Text.Trim(),
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

            if (cadastrar)
                funcionario.Cadastrar();
            else
                funcionario.Atualizar();
        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
                if (ValidarSenha())
                {
                    MantemFuncionario();
                    cadastrou = true;
                    AlterouOftal = cbCargo.SelectedIndex == 0;
                    Close();
                }
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
