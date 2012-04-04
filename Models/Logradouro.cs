using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace BuscaCep.Models
{
    public class Logradouro
    {


        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Cep { get; set; }


        public Logradouro RecuperaCep(string cep)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://m.correios.com.br/movel/buscaCepConfirma.do?cepEntrada=" + cep + " &tipoCep=&cepTemp=&metodo=buscarCep");

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes("");
            request.ContentLength = bytes.Length;

            using (Stream os = request.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
                os.Close();
            }

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                if (resp == null) return null;

                using (Stream sr = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(sr, Encoding.Default);
                    return GetString(reader.ReadToEnd().Trim());
                }
            }
        }


        private Logradouro GetString(string html)
        {

            Regex r = new Regex(@"<span(.*?)class=""resposta(.*?)"">(.*?)</span>");
            MatchCollection mc = r.Matches(html.Replace("\t", " ").Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("   ", " ").Replace("  ", " ").Replace(":", ""));

            html = String.Empty;

            Logradouro log = new Logradouro();



            for (int i = 0; i < mc.Count; i++)
            {

                Match m = mc[i];

                switch (i)
                {
                    case 1:
                        log.Endereco = m.Groups[3].Value.Trim();
                        break;
                    case 3:
                        log.Bairro = m.Groups[3].Value.Trim();
                        break;
                    case 5:
                        log.Localidade = m.Groups[3].Value.Trim();
                        break;
                    case 7:
                        log.Cep = m.Groups[3].Value.Trim();
                        break;
                }

            }
            return log;
        }
    }
}

