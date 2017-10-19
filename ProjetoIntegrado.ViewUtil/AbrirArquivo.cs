using System.Windows.Forms;

namespace ProjetoIntegrado.ViewUtil
{
    public class AbrirArquivo
    {
        public string filtro { private get; set; }
        public string caminho { get; private set; }

        public bool Abrir()
        {
            OpenFileDialog op = new OpenFileDialog
            {
                Filter = filtro
            };

            var opcao = op.ShowDialog();

            if (opcao == DialogResult.OK)
                caminho = op.FileName;

            return opcao == DialogResult.OK;
        }

        public bool AbrirImagem()
        {
            filtro = "Imagens(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            return Abrir();
        }
    }
}
