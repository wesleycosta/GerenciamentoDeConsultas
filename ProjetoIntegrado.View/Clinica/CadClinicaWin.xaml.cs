using System.Windows;
using System.Windows.Input;
using System.Drawing;
using System.Threading.Tasks;
using MahApps.Metro.Controls;

namespace ProjetoIntegrado.View.Clinica
{
    using Model;
    using ViewUtil;
    using Funcoes;
    using Model.Estado;

    public partial class CadClinicaWin : MetroWindow
    {
        #region  PROPRIEDADES E CTOR

        public bool cadastrou { get; private set; }

        private ClinicaModel clinica;
        private readonly bool cadastrar;
        private Bitmap logo;

        public CadClinicaWin(bool cadastrar = false)
        {
            InitializeComponent();
            Iniciar();

            this.cadastrar = cadastrar;

            if (!cadastrar)
                CarregarDados();
        }

        private void Iniciar()
        {
            EventosLogo();

            cbUf.ItemsSource = EstadoModel.Siglas;

            Loaded += (o, a) => tbRazaoSocial.Focus();
        }

        #endregion

        #region CARREGAR

        private async void CarregarDados()
        {
            clinica = new ClinicaModel();
            await clinica.CarregarAsync();

            // DADOS EMPRESA
            tbRazaoSocial.Text = clinica.razaoSocial;
            tbNomeFantasia.Text = clinica.nomeFantasia;
            tbIE.Text = clinica.ie;
            tbCNPJ.Text = clinica.cnpj;
            logo = clinica.logo;

            // CONTADO EMPRESA
            tbDddCel.Text = clinica.dddCel.Trim();
            tbCelular.Text = clinica.celular;
            tbDddTel.Text = clinica.dddTel.Trim();
            tbTelefone.Text = clinica.telefone;
            tbEmail.Text = clinica.email;
            tbSite.Text = clinica.site;

            CarregarEndereco(clinica.endereco);
            CarregaLogo();
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

        private async void CarregaLogo()
        {
            await Task.Run(() => Dispatcher.Invoke(() =>
                                 {
                                     if (logo != null)
                                         imageLogo.BitmapToImageSource(logo);
                                     else
                                         imageLogo.Source = null;
                                 }));
        }

        #endregion

        #region MANTEM CLINICA

        #region TO MODEL

        private EnderecoModel ToModelEndereco() =>
          new EnderecoModel
          {
              id = clinica?.endereco?.id ?? 0,
              cep = Mascara.Remover(tbCep.Text),
              cidade = tbCidade.Text,
              uf = cbUf.Text,
              bairro = tbBairro.Text,
              logradouro = tbLogradouro.Text,
              numero = tbNumero.Text,
              complemento = tbComplemento.Text
          };

        private ClinicaModel ToModel() =>
            new ClinicaModel
            {
                id = clinica?.id ?? 0,
                razaoSocial = tbRazaoSocial.Text,
                nomeFantasia = tbNomeFantasia.Text,
                ie = Mascara.Remover(tbIE.Text),
                cnpj = Mascara.Remover(tbCNPJ.Text),
                logo = logo,

                dddCel = tbDddCel.Text.Trim(),
                celular = tbCelular.Text,
                dddTel = tbDddTel.Text.Trim(),
                telefone = tbTelefone.Text,
                email = tbEmail.Text,
                site = tbSite.Text,

                endereco = ToModelEndereco()
            };

        #endregion

        private void MantemClinica()
        {
            clinica = ToModel();

            if (cadastrar)
                clinica.Cadastrar();
            else
                clinica.Atualizar();
        }

        #endregion

        #region EVENTOS

        #region EVENTOS IMAGEM LOGO

        private void BuscarImg()
        {
            AbrirArquivo abrir = new AbrirArquivo();
            var selecionou = abrir.AbrirImagem();

            if (selecionou)
            {
                logo = new Bitmap(abrir.caminho);

                if (logo.Width > Config.TamanhoImg || logo.Height > Config.TamanhoImg)
                    logo.ResizeImage(Config.TamanhoImg, Config.TamanhoImg * logo.Height / logo.Width);

                CarregaLogo();
            }
        }

        private void EventosLogo()
        {
            btnBuscar.Click += (o, a) => BuscarImg();

            btnRemover.Click += async (o, a) =>
            {
                logo = null;
                CarregaLogo();
            };
        }

        #endregion

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            MantemClinica();
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
