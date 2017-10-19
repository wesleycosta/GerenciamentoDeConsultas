using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using System.Drawing;

namespace ProjetoIntegrado.View.Clinica
{
    using ViewUtil;

    public partial class CadClinicaWin : MetroWindow
    {
        private Bitmap logo;

        public CadClinicaWin()
        {
            InitializeComponent();
            Iniciar();
        }

        private void Iniciar()
        {
            EventosLogo();
        }

        private void CarregaLogo()
        {
            imageLogo.BitmapToImageSource(logo);
        }

        #region EVENTOS

        #region EVENTOS IMAGEM LOGO

        private void BuscarImg()
        {
            AbrirArquivo abrir = new AbrirArquivo();
            var selecionou = abrir.AbrirImagem();

            if (selecionou)
            {
                logo = new Bitmap(abrir.caminho);
                CarregaLogo();
            }
        }

        private void EventosLogo()
        {
            btnBuscar.Click += (o, a) => BuscarImg();
            btnRemover.Click += (o, a) => imageLogo.Source = null;
        }

        #endregion

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            //MantemFuncionario();
            //cadastrou = true;
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
