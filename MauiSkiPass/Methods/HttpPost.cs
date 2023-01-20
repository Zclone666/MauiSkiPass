using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSkiPass.Methods
{
    public static class HttpPost
    {
        public static string Send(string URL, string data)
        {
            try
            {
                HttpClient Clnt = new HttpClient();
                HttpResponseMessage Resp = new HttpResponseMessage();
                System.Net.Http.StringContent Cont = new StringContent(data);
                Cont.Headers.ContentLength = data.Length;
                Cont.Headers.ContentType.MediaType = "application/json";
                Resp = Clnt.PostAsync(URL, Cont).Result;
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
