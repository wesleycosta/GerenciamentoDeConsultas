using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProjetoIntegrado.View.WebServices
{
    using Model;

    public class ViaCep
    {
        private static string GetUrl(string cep) => $"https://viacep.com.br/ws/{cep}/json";

        public async Task<EnderecoModel> BuscarCep(string cep)
        {
            var endereco = new EnderecoModel();
            var requisicao = new Requisicao();
            var respostaJson = await requisicao.MetodoGetAsync(GetUrl(cep));

            if (respostaJson != string.Empty)
            {
                await Task.Run(() =>
                {
                    respostaJson = respostaJson.ToUpper();
                    endereco = JsonConvert.DeserializeObject<EnderecoModel>(respostaJson);
                    endereco.cep = cep;
                });

                return endereco;
            }

            return null;
        }
    }
}
