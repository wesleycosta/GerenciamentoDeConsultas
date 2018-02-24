using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ProjetoIntegrado.Mensagens
{
    public static class Mbox
    {
        #region MBOX DE AFIRMAÇÃOE E CONFIRMAÇÃO

        public static MetroWindow JanelaPrincipal { get; set; }

        public static void Afirmacao(string titulo, string texto) =>
            JanelaPrincipal.ShowModalMessageExternal(titulo, texto);

        private static MessageDialogResult Pergunta(string titulo, string texto)
        {
            var config = new MetroDialogSettings
            {
                AffirmativeButtonText = "SIM",
                NegativeButtonText = "NÃO"
            };

            return JanelaPrincipal.ShowModalMessageExternal(titulo, texto, MessageDialogStyle.AffirmativeAndNegative, config);
        }

        #endregion

        public static void SelecioneUmaLinhaDaTabela() =>
            Afirmacao("Aviso", "Por favor, selecione uma linha da tabela.");

        public static MessageDialogResult DesejaExcluir() =>
            Pergunta("Aviso", "Tem certeza que deseja remover esse registro?");

        public static MessageDialogResult DesejaSair() =>
            Pergunta("Aviso", "Tem certeza que deseja sair?");

        public static void Excecao(string msg) =>
            Afirmacao("ERRO", msg);

        public static void CampoInvalido(string campo) =>
                     Afirmacao("Aviso", $"Por favor, preencha o campo {campo}.");
    }
}
