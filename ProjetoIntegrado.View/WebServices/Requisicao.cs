using System;
using System.Net;
using RestSharp;
using System.Threading.Tasks;

namespace ProjetoIntegrado.View.WebServices
{
    using Mensagens;

    public class Requisicao
    {
        public string MetodoGet(string url)
        {
            try
            {
                var cliente = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var resposta = cliente.Execute(request);

                if (resposta.StatusCode == HttpStatusCode.OK)
                    return resposta.Content;
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
            }

            return string.Empty;
        }

        public async Task<string> MetodoGetAsync(string url) => await Task.Run(() => MetodoGet(url));
    }
}
