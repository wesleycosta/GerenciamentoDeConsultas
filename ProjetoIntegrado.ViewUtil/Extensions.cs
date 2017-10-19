using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

        public static void ResizeImage(this Bitmap image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            image = destImage;
        }

        public static void SetColor(this Control ctrl, string hex)
        {
            var bc = new BrushConverter();

            ctrl.Background = (System.Windows.Media.Brush)bc.ConvertFrom(hex);
        }
    }
}
