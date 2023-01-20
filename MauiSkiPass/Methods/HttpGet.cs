using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSkiPass.Methods
{
    public static class HttpGet
    {
        public static string Send(string URL)//, string data)
        {
            try
            {
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    };
                HttpClient Clnt = new HttpClient(handler);
                HttpResponseMessage Resp = new HttpResponseMessage();
                //   System.Net.Http.StringContent Cont = new StringContent(data);
                //   Cont.Headers.ContentLength = data.Length;
                //  Cont.Headers.ContentType.MediaType = "application/json";
                Resp = Clnt.GetAsync(URL).Result;
                string Rsp = Resp.Content.ReadAsStringAsync().Result;
                return Rsp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
