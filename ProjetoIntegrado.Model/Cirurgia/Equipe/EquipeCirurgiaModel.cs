namespace ProjetoIntegrado.Model
{
    public partial class EquipeCirurgiaModel
    {
        public int id { get; set; }
        public CirurgiaModel cirurgia { get; set; }
        public FuncionarioModel funcionario { get; set; }
        public bool ativo { get; set; } = true;
    }
}
