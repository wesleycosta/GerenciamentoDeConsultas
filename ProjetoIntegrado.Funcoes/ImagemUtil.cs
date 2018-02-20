using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ProjetoIntegrado.Funcoes
{
    public static class ImagemUtil
    {
        public static byte[] ImageParaByte(Image foto)
        {
            if (foto == null) return null;

            using (var stream = new MemoryStream())
            {
                foto.Save(stream, ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);
                var bArray = new byte[stream.Length];
                stream.Read(bArray, 0, Convert.ToInt32(stream.Length));
                return bArray;
            }
        }

        public static Bitmap ByteParaImage(object img)
        {
            Bitmap foto = null;

            if (img != null)
                if (img != DBNull.Value)
                    try
                    {
                        var Vetor = (byte[])img;
                        var fs = new MemoryStream(Vetor);
                        foto = new Bitmap(fs);
                        fs.Flush();
                        fs.Close();
                    }
                    catch (Exception ex)
                    {

                    }

            return foto;
        }
    }
}
