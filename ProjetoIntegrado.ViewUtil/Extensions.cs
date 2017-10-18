using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;


namespace ProjetoIntegrado.ViewUtil
{
    public static class Extensions
    {
        public static void SelecionarPrimeiraLinha(this ListView lvw)
        {
            if (lvw.Items.Count > 0)
            {
                lvw.Focus();
                lvw.SelectedItem = lvw.Items[0];
            }
        }

        public static void CarregarPagina(this Window janelaPrincipal, Frame frame, Page pagina, double descontar = 0)
        {
            frame.Width = Janela.GetWidth() - descontar;
            frame.Height = Janela.GetHeight();

            frame.Navigate(pagina);
        }

        public static void BitmapToImageSource(this Image image, Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();

                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                image.Source = bitmapimage;
            }
        }

        public static void SetColor(this Control ctrl, string hex)
        {
            var bc = new BrushConverter();

            ctrl.Background = (System.Windows.Media.Brush)bc.ConvertFrom(hex);
        }
    }
}
