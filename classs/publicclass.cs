using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace com.ruijie.EVEMarket.Jimmy.classs
{
    class publicclass
    {
        public string HttpGet(string url)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public string FormatXml(string sUnformattedXml)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sUnformattedXml);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = System.Xml.Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }
            return sb.ToString();
        }

        public string XMLmade(string url, string styl, string pace)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(url);
            //string Res= " ";
            XmlElement root = null;
            root = doc.DocumentElement;
            XmlNodeList listNodes = null;
            listNodes = root.SelectNodes("/cevemarket/" + styl + "/" + pace);
            String res = "";
            foreach (XmlNode node in listNodes)
            {
                res += node.InnerText + "\n";
            }
            return res;
        }

    }
}
