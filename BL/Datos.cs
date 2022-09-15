using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data.SqlClient;

namespace BL
{
   public class Datos
    {
        public static void ExtraerDatos()
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


        {
            ML.Result result = new ML.Result();
            try
            {
                //Este codigo se utiliza para mantener la sesion del portal dinamico
                //IWebDriver driver = new ChromeDriver(Environment.CurrentDirectory); //Start a new instance of chrome
                //HttpWebRequest http = WebRequest.Create("https://portal.gsi.com.mx:8443/portal_desa/Logout.do") as HttpWebRequest;
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

                //http = WebRequest.Create("https://portal.gsi.com.mx:8443/portal_desa/Login.do") as HttpWebRequest;
                //http.CookieContainer = new CookieContainer();
                //http.CookieContainer.Add(httpResponse.Cookies);
                ////driver.Navigate().GoToUrl("https://portal.gsi.com.mx:8443/portal_desa/MonitorServicios.do");
                //HttpWebResponse httpResponse2 = http.GetResponse() as HttpWebResponse;

                /////////////////

                ////IWebElement element = driver.FindElement(By.Id("frmSubmit")); //find a button on the page
                ////((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
                ////driver.FindElement(element).Click(); //Click the button

                //Codigo para instanciar el Ml donde estan todos mis atributos


                ////Codigo para hacer la conexion a la base de datos con SQL.Client y a su vez se instancia la clase DL
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
                    Console.WriteLine(inf.InnerText.ToString());
                        //Console.WriteLine(datosextract.ToString());
                    }

                    ML.DatosPortal datosPortal = new ML.DatosPortal();
                //Codigo para llamar el Stored procedure para guardar la informacion en la base de datos
                string query = "DatosAdd";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = context;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter[] collection = new SqlParameter[13];

                collection[0] = new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar);
                collection[0].Value = datosPortal.IdFolioDeServicio;

                collection[1] = new SqlParameter("@IdFolioDeServicio", System.Data.SqlDbType.VarChar);
                collection[1].Value = datosPortal.Prioridad;

                collection[2] = new SqlParameter("@TipoServicio", System.Data.SqlDbType.VarChar);
                collection[2].Value = datosPortal.TipoServicio;

                collection[3] = new SqlParameter("@SucursalConsignatario", System.Data.SqlDbType.VarChar);
                collection[3].Value = datosPortal.SucursalConsignatario;

                collection[4] = new SqlParameter("@FechaCaptura", System.Data.SqlDbType.VarChar);
                collection[4].Value = datosPortal.FechaCaptura;

                collection[5] = new SqlParameter("@FechaRealizarServicio", System.Data.SqlDbType.VarChar);
                collection[5].Value = datosPortal.FechaRealizarServicio;

                collection[6] = new SqlParameter("@OrdenServicio", System.Data.SqlDbType.VarChar);
                collection[6].Value = datosPortal.OrdenServicio;

                collection[7] = new SqlParameter("@Importe", System.Data.SqlDbType.Decimal);
                collection[7].Value = datosPortal.Importe;

                collection[8] = new SqlParameter("@Divisa", System.Data.SqlDbType.VarChar);
                collection[8].Value = datosPortal.Divisa;

                collection[9] = new SqlParameter("@Ter", System.Data.SqlDbType.VarChar);
                collection[9].Value = datosPortal.Ter;

                collection[10] = new SqlParameter("@HoraEnvio", System.Data.SqlDbType.VarChar);
                collection[10].Value = datosPortal.HoraEnvio;

                collection[11] = new SqlParameter("@Actualización", System.Data.SqlDbType.VarChar);
                collection[11].Value = datosPortal.Actualización;

                collection[12] = new SqlParameter("@Estatus", System.Data.SqlDbType.VarChar);
                collection[12].Value = datosPortal.Estatus;

                cmd.Parameters.AddRange(collection);
                cmd.Connection.Open();

                int RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }

            }

        }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

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

