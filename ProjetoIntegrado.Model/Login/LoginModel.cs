namespace ProjetoIntegrado.Model
{
    public class LoginModel
    {
        public bool Autenticar(string usuario, string senha)
        {
            var id = FuncionarioModel.Autenticar(usuario, senha);

            if (id > 0)
                IniciarUsuario(id);

            return id > 0;
        }

        private void IniciarUsuario(int idFuncionario)
        {
            var funcionario = new FuncionarioModel
            {
                id = idFuncionario
            };

            funcionario.Carregar();
            Sessao.funcionario = funcionario;
            Sessao.CarregarUltimoCaixa();
        }

    }
}
