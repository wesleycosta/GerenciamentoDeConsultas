namespace ProjetoIntegrado.Model
{
    public partial class CirurgiaModel
    {
        public int id { get; set; }
        public int idConsulta { get; set; }
        public string local { get; set; }
        public decimal valor { get; set; }
        public bool ativo { get; set; } = true;
    }
}
