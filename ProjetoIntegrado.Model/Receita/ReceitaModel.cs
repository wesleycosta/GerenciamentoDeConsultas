using System;

namespace ProjetoIntegrado.Model
{
    public partial class ReceitaModel
    {
        public int id { get; set; }
        public int idConsulta { get; set; }
        public DiagnosticoModel olhoDireito { get; set; }
        public DiagnosticoModel olhoEsquerdo { get; set; }
        public bool ativo { get; set; } = true;
    }
}
