using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoIntegrado.Model
{
    public partial class CaixaModel
    {
        public int id { get; set; }

        public FuncionarioModel funcionarioAbertura { get; set; }

        public FuncionarioModel funcionarioFechamento { get; set; }

        public decimal valorInicial { get; set; }

        public DateTime dtAbertura { get; set; }

        public DateTime dtFechamento { get; set; }

        public bool caixaAberto { get; set; }

        public bool ativo { get; set; }
    }
}
