using ProjetoIntegrado.Model;

namespace ProjetoIntegrado.View.Clientes
{
    /// <summary>
    /// Interaction logic for CadClienteWin.xaml
    /// </summary>
    public partial class CadClienteWin
    {
        internal bool cadastrou;
        private ClienteModel cliente;
        private FuncionarioModel funcionario;

        public CadClienteWin()
        {
            InitializeComponent();
        }

        public CadClienteWin(ClienteModel cliente)
        {
            this.cliente = cliente;
        }
    }
}
