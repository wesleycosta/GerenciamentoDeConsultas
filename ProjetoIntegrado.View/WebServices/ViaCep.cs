using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjetoIntegrado.View.WebServices
{
    using Model;

    public class ViaCep
    {
        private static string GetUrl(string cep) => $"https://viacep.com.br/ws/{cep}/json";

        public async Task<EnderecoModel> BuscarCep(string cep)
        {
            var requisicao = new Requisicao();
            var respJson = await requisicao.MetodoGetAsync(GetUrl(cep));

            if (respJson != string.Empty)
            {
                var dicionario = JsonConvert.DeserializeObject<Dictionary<string, string>>(respJson);

                if (!dicionario.ContainsKey("erro"))
                    return await Task.Run(() => ConveterToEnderecoModel(dicionario));
            }

            return null;
        }

        private EnderecoModel ConveterToEnderecoModel(Dictionary<string, string> dicionario)
        {
            return new EnderecoModel
            {
                cep = dicionario["cep"].ToUpper(),
                cidade = dicionario["localidade"].ToUpper(),
                uf = dicionario["uf"].ToUpper(),
                bairro = dicionario["bairro"].ToUpper(),
                logradouro = dicionario["logradouro"].ToUpper(),
                complemento = dicionario["complemento"].ToUpper()
            };
        }
    }
}
