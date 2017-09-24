using System.Diagnostics;

namespace ProjetoIntegrado.Funcoes
{
    public static class Processo
    {
        public static void MatarProcessoSistema()
        {
            var nomeSis = "ProjetoIntegrado";

            var processes = Process.GetProcessesByName(nomeSis);
            foreach (var p in processes)
                p.Kill();
        }
    }
}
