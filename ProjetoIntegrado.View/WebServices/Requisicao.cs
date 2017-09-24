using System;
using System.Net;
using RestSharp;

namespace ProjetoIntegrado.View.WebServices
{
    public class Requisicao
    {
        public string MetodoGet(string url)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var resposta = client.Execute(request);

                if (resposta.StatusCode == HttpStatusCode.OK)
                    return resposta.Content;
            }
            catch (Exception ex)
            {

            }

            return string.Empty;
        }
    }
}
