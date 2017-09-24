using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrado.View.WebServices
{
    using Model;
    using Funcoes;

    public class ViaCep
    {
        private static string GetUrl(string cep) => $"https://viacep.com.br/ws/{cep}/json";

        public async Task<EnderecoModel> BuscarCep(string cep)
        {
            var requisicao = new Requisicao();
            var respJson = requisicao.MetodoGet(GetUrl(Marcara.Remover(cep)));

            if (respJson != string.Empty)
            {
                var dicionario = JsonConvert.DeserializeObject<Dictionary<string, string>>(respJson);

                if (!dicionario.ContainsKey("erro"))
                    return ConveterToEnderecoModel(dicionario);
            }

            return null;
        }

        private EnderecoModel ConveterToEnderecoModel(Dictionary<string, string> dicionario) =>
            new EnderecoModel
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
