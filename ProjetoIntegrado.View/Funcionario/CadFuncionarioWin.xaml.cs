using System.Threading.Tasks;
using ProjetoIntegrado.Model;
using ProjetoIntegrado.View.WebServices;

namespace ProjetoIntegrado.View.Funcionario
{
    public partial class CadFuncionarioWin
    {
        internal bool cadastrou;
        private FuncionarioModel funcionario;

        public CadFuncionarioWin()
        {
            InitializeComponent();

            tbCep.LostFocus += (o, a) => MantemBuscaCep();
        }

        public CadFuncionarioWin(FuncionarioModel funcionario)
        {
            this.funcionario = funcionario;
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

        private async void MantemBuscaCep()
        {
            var viaCep = new ViaCep();
            var end = await viaCep.BuscarCep(tbCep.Text);

            if (end != null)
                CarregarEndereco(end);
        }
    }
}
