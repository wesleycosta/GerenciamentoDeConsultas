namespace ProjetoIntegrado.Model
{
    public static class Sessao
    {
        public static FuncionarioModel funcionario { get; set; }
        public static ClinicaModel clinica { get; set; }
        public static CaixaModel caixa { get; set; }


        public static void CarregarUltimoCaixa()
        {
            var c = new CaixaModel();
            c.Carregar();

            caixa = c;
        }
    }
}
