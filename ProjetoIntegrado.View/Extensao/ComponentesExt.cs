using System.Windows.Controls;

namespace ProjetoIntegrado.View
{
    public static class ComponentesExt
    {
        public static void SelecionarPrimeiraLinha(this ListView lvw)
        {
            if (lvw.Items.Count > 0)
            {
                lvw.Focus();
                lvw.SelectedItem = lvw.Items[0];
            }
        }
    }
}
