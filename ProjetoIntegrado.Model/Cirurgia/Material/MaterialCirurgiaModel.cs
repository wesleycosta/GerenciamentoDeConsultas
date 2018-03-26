namespace ProjetoIntegrado.Model
{
    public partial class MaterialCirurgiaModel
    {
        public int id { get; set; }
        public CirurgiaModel cirurgia { get; set; }
        public MaterialModel material { get; set; }
        public int quantidade { get; set; }
        public decimal valorUnitario { get; set; }
        public bool ativo { get; set; }

        public decimal valorTotal => valorUnitario * quantidade;
    }
}
