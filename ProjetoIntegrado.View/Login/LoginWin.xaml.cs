using System.Linq;
using System.Windows;
using System.Windows.Input;
using ProjetoIntegrado.Funcoes;
using ProjetoIntegrado.ViewUtil;

namespace ProjetoIntegrado.View.Login
{
    using Model;

    public partial class LoginWin
    {
        public LoginWin(Window splashInicial)
        {
            InitializeComponent();
            splashInicial.Close();
            imgLogin.BitmapToImageSource(Icons.eyeglasses);

            Loaded += (o, a) => CarregarUsuarios();
        }

        #region AUTENTICAR

        private void CarregarUsuarios()
        {
            var usuarios = FuncionarioModel.CarregarTodos().Select(x => x.usuario).Where(x => x != string.Empty).ToList();
            cbUsuario.Items.Clear();

            cbUsuario.Items.Add("ADMINISTRADOR");
            usuarios.ForEach(x => cbUsuario.Items.Add(x));
        }

        private void CriarTelaPrincipal()
        {
            var frmPrincipal = new Principal.JanelaPrincipalWin(this);
            frmPrincipal.ShowDialog();
        }

        private bool Validar() =>
            cbUsuario.Text != string.Empty && tbSenha.Password != string.Empty;

        private void RealizarLogin()
        {
            if (Validar())
            {
                var login = new LoginModel();
                var autenticou = login.Autenticar(cbUsuario.Text, tbSenha.Password);

                if (autenticou)
                    CriarTelaPrincipal();
                else
                    lbInvalido.Visibility = Visibility.Visible;
            }
            else
                lbInvalido.Visibility = Visibility.Visible;
        }

        #endregion

        #region  EVENTOS

        private void BtnEntrar_OnClick(object sender, RoutedEventArgs e)
        {
            RealizarLogin();
        }

        private void Login_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Enter)
                RealizarLogin();
        }

        #endregion
    }
}
