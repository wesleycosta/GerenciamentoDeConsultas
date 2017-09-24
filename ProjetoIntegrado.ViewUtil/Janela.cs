namespace ProjetoIntegrado.ViewUtil
{
    using System.Windows;

    public class Janela
    {
        public static double GetHeight() =>
            SystemParameters.WorkArea.Height;

        public static double GetWidth() =>
            SystemParameters.WorkArea.Width;
    }
}
