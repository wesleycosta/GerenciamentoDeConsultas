using System.IO;
using System.Xml.Serialization;

namespace ProjetoIntegrado.BaseDeDados
{
    [XmlRoot("configuration")]
    public class ConfiguracaoArquivo
    {
        [XmlElement("banco")]
        public string banco { get; set; }

        [XmlElement("servidor")]
        public string servidor { get; set; }

        public int porta { get; }

        public string usuario { get; }
        public string senha { get; }

        public bool IsLocalhost => servidor?.ToLower() == "localhost";

        public ConfiguracaoArquivo()
        {
            usuario = "sa";
            senha = "123";
        }

        public static ConfiguracaoArquivo Carregar()
        {
            ConfiguracaoArquivo config = null;

            if (File.Exists("App.Config"))
            {
                var serializer = new XmlSerializer(typeof(ConfiguracaoArquivo));
                using (TextReader reader = new StringReader(File.ReadAllText("App.config")))
                {
                    config = (ConfiguracaoArquivo)serializer.Deserialize(reader);
                }
            }

            return config;
        }
    }
}
