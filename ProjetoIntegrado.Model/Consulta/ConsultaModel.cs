using System;
using System.Collections.Generic;

namespace ProjetoIntegrado.Model
{
    public partial class ConsultaModel
    {
        public int id { get; set; }
        public FuncionarioModel medico { get; set; }
        public ClienteModel cliente { get; set; }
        public ConvenioModel convenio { get; set; }
        public string numeroProcedimento { get; set; }
        public FormaDeAtendimento formaDeAtentimento { get; set; }
        public DateTime data { get; set; }
        public TimeSpan horario { get; set; }
        public decimal valor { get; set; }
        public StatusPagamento statusPagamento { get; set; }
        public TipoDeConsulta tipoDeConsulta { get; set; }
        public bool retorno { get; set; }
        public bool ativo { get; set; } = true;

        public List<PagamentoModel> listaDePagamentos { get; set; } = new List<PagamentoModel>();

        #region BINDING

        public string cancelamento
        {
            get
            {
                if (tipoDeConsulta == TipoDeConsulta.Cancelado)
                    return "Cancelado";
                else if (tipoDeConsulta == TipoDeConsulta.NaoCompareceu)
                    return "Não Compareceu";
                else
                    return "";
            }
        }

        public string horarioFormatado => horario.ToString(@"hh\:mm");

        public string formaDeAtentimentoFormatado
        {
            get
            {
                if (formaDeAtentimento == FormaDeAtendimento.Convenio)
                    return "Convênio";

                return "Particular";
            }
        }

        public string retornoFormatado => retorno ? "SIM" : "NÃO";

        public string cpfFormatado => cliente.cpf;

        public string celularFormatado => $"{cliente.dddCel} {cliente.celular}";

        public string telefoneFormatado => $"{cliente.dddTel} {cliente.telefone}";

        #endregion
    }
}
