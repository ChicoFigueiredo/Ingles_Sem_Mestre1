﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
//https://api.datamarket.azure.com/Bing/MicrosoftTranslator/v1/Translate?To='pt'&From='en'&Text='bread'
namespace Ingles_Sem_Mestre
{
    class Captura_Traducao_BING
    {
        private WebClientX web = new WebClientX();
        private String _last_navigation = "";
        const String site = "https://api.datamarket.azure.com/Bing/MicrosoftTranslator/v1/Translate";
        const String site_req = "To='pt'&From='en'&Text='";
        const String user = "fran.fig@gmail.com";
        const String pwd = "anQnvgVTDjgNz0MDIqonWSmn3y73GGcZ+QcXt/iIjpY";

        public Captura_Traducao_BING()
        {
            web.Encoding = Encoding.UTF8;
            //_last_navigation = web.DownloadString("http://upodn.com/");
        }

        public string Get_Traducao(string texto_a_fonetizar)
        {
            //Regex R = new Regex("[<]textarea name=\"txtTranscription\".*?[|](.*?)[|]",RegexOptions.Multiline);
            //Regex R = new Regex("[|](.{1,})[|]", RegexOptions.Multiline);
            string param = site_req + Uri.EscapeUriString(texto_a_fonetizar) + "'";
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(user + ":" + pwd));
            web.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            _last_navigation = web.DownloadString(site + "?" + param);//.UploadString(site, "POST", param );

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(_last_navigation);

            String response = doc.SelectNodes("//*[name()='d:Text']/text()").Item(0).Value.Trim(); //WebUtility.HtmlDecode(R.Matches(_last_navigation)[0].Groups[1].Value.Trim());


            return response;
        }
    }
}