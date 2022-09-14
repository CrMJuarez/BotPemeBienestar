using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System.Data.SqlClient;



namespace BL
{
    class Datos
    {

        //public static HttpWebResponse HttpPost(String url, String referer, String userAgent, ref CookieCollection cookies, String postData, out WebHeaderCollection headers, WebProxy proxy)
        //{
        //    try
        //    {
        //        HttpWebRequest http = WebRequest.Create(url) as HttpWebRequest;
        //        http.Proxy = proxy;
        //        http.AllowAutoRedirect = true;
        //        http.Method = "POST";
        //        http.ContentType = "application/x-www-form-urlencoded";
        //        http.UserAgent = userAgent;
        //        http.CookieContainer = new CookieContainer();
        //        http.CookieContainer.Add(cookies);
        //        http.Referer = referer;
        //        byte[] dataBytes = UTF8Encoding.UTF8.GetBytes(postData);
        //        http.ContentLength = dataBytes.Length;
        //        using (Stream postStream = http.GetRequestStream())
        //        {
        //            postStream.Write(dataBytes, 0, dataBytes.Length);
        //        }
        //        HttpWebResponse httpResponse = http.GetResponse() as HttpWebResponse;
        //        headers = http.Headers;
        //        cookies.Add(httpResponse.Cookies);

        //        return httpResponse;
        //    }
        //    catch { }
        //    headers = null;

        //    return null;
        //}
        public static void ExtraerDatos()

        {
            //Este codigo se utiliza para mantener la sesion del portal dinamico

            //HttpWebRequest http = WebRequest.Create("https://portal.gsi.com.mx:8443/portal_desa/MonitorServicios.do") as HttpWebRequest;
            //http.KeepAlive = true;
            //http.Method = "POST";
            //http.ContentType = "application/x-www-form-urlencoded";
            //string postData = string.Format("txtUsuario={0}&txtPassword={1}", "MONHDRS03", "123");
            ////string data = string.Format("txtUsuario={0}&txtPassword={1}", "MONHDRS03", "123");
            //byte[] dataBytes = UTF8Encoding.UTF8.GetBytes(postData);
            //http.ContentLength = dataBytes.Length;
            //using (Stream postStream = http.GetRequestStream())
            //{
            //    postStream.Write(dataBytes, 0, dataBytes.Length);
            //}
            //HttpWebResponse httpResponse = http.GetResponse() as HttpWebResponse;
            //// Probably want to inspect the http.Headers here first
            //http = WebRequest.Create("https://portal.gsi.com.mx:8443/portal_desa/MonitorServicios.do") as HttpWebRequest;
            //http.CookieContainer = new CookieContainer();
            //http.CookieContainer.Add(httpResponse.Cookies);
            //HttpWebResponse httpResponse2 = http.GetResponse() as HttpWebResponse;
            
            //Codigo para instanciar el Ml donde estan todos mis atributos
            ML.DatosPortal datosPortal = new ML.DatosPortal();
            //Codigo para hacer la conexion a la base de datos con SQL.Client y a su vez se instancia la clase DL
            using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
            {
                //se crea una variable de tipo List para guardar los datos del documento html
                List<string> datosextract = new List<string>();

                //se instancia la clase html 
                HtmlWeb web = new HtmlWeb();

                //se utiliza para poder cargar el documento html de la pagina web y guardarlo en la variable documento
                HtmlDocument documento = web.Load("https://datosprueba01.000webhostapp.com/DatosPreba02/Datos.html");

                //Codigo para extraer los datos de los nodos del documento html es decir solo los datos que necesitamos
                                                               //se separa por etiqueta, clase, nombre de clase
                var datos = documento.DocumentNode.SelectNodes("//span[not(contains(@class, 'styleTableRow'))]");
                foreach (var inf in datos)
                {

                    datosextract.Add(inf.InnerText);
                    Console.WriteLine(datosextract);
                      
                }


                //Codigo para llamar el Stored procedure para guardar la informacion en la base de datos
                string query = "DatosAdd";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = context;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter[] collection = new SqlParameter[12];
                collection[0] = new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar);
                collection[0].Value = datosPortal.IdFolioDeServicio;

            }


        }
    }
}





//foreach (var Nodo in documento.DocumentNode.CssSelect(""))
//{
//    var NodoDatos = Nodo.SelectSingleNode("//tr[@class='styleTableRow']");
//    datos.Add(NodoDatos.InnerHtml);

//}









//List<string> datos = new List<string>();


//HtmlWeb web = new HtmlWeb();


//HtmlDocument documento = web.Load("https://datosprueba01.000webhostapp.com/DatosPreba02/Datos.html");

//foreach (var Nodo in documento.DocumentNode.CssSelect(""))

//{
//    var NodoDatos = Nodo.SelectSingleNode("//tr[@class='styleTableRow']");
//    datos.Add(NodoDatos.InnerHtml);

//}








//string [] caracter = { "<span>", "</span>","</td>","<td align",
//    "=","<div>","</div>","<input>","</input>","<input type","</a>","center",
//    ">","<div style","<a href"};
//var split = String.Join("<span>", datos.ToArray());
//string[] text = split.Split(caracter, System.StringSplitOptions.RemoveEmptyEntries);
//System.Console.WriteLine($"{text.Length} substrings in text:");

//foreach (var word in text)
//{
//    System.Console.WriteLine(word);
//}
//string split = Datos.ToString();
//List<string> list = new List<string>();
//list = split.Split(',').ToList();

