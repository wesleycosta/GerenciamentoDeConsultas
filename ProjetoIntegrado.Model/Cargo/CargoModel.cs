namespace ProjetoIntegrado.Model
{
    public partial class CargoModel
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public bool ativo { get; set; } = true;


        public bool IsOftalmologista => id == 1;
        public bool IsRecepcionista => id == 2;
        public bool IsGerente => id == 3;
    }
}
