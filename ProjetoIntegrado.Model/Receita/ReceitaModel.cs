using System;

namespace ProjetoIntegrado.Model
{
    public partial class ReceitaModel
    {
        public int id { get; set; }
        public int idConsulta { get; set; }
        public DiagnosticoModel olhoDireitoLonge { get; set; }
        public DiagnosticoModel olhoEsquerdoLonge { get; set; }
        public DiagnosticoModel olhoDireitoPerto { get; set; }
        public DiagnosticoModel olhoEsquerdoPerto { get; set; }
        public bool ativo { get; set; } = true;
    }
}
