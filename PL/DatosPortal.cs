using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
namespace PL

{

    public class DatosPortal
    {
        //    void Page_Load(Object sender, EventArgs e)
        //    {

        //        Button1.Click += new EventHandler(this.Btn_Click);
        //    }

        //    void Btn_Click(Object sender,
        //                           EventArgs e)
        //    {

        //        Button clickedButton = (Button)sender;

        //        clickedButton.Enabled = false;

        //    }


        public static void ExtraerDatos()

        {
            //instancia del navegador en segundo plano
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://portal.gsi.com.mx:8443/portal_desa/Logout.do");
            //se mandan las credenciales 
            var Input = driver.FindElement(By.Id("txtUsuario"));
            Input.SendKeys("MONHDRS03");

            var Input1 = driver.FindElement(By.Id("txtPassword"));
            Input1.SendKeys("123");
            //se hace input al boton de login
            var Input2 = driver.FindElement(By.Name("imgLogin"));
            Input2.Submit();
            
            
            //se pone un tiempo de espera para que cargue los elementos
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //se hace uso de switchto para esoger el frame por nombre
            driver.SwitchTo().Frame("fraEncabezado");
            //se pone ActiveElement(); para que mantenga cargado lo que esta dentro del frame y asi nos muestre el contenido en html
            driver.SwitchTo().ActiveElement();

            //se selecciona el combo con la opcion Monitor de Solicitudes TV para pasar a la siguente pagina con el evento click
            driver.FindElement(By.XPath("//select[@id='" + "cboMenus" + "']/option[contains(.,'" + "Monitor de Solicitudes TV" + "')]")).Click();

            //A diferencia del active aqui si necesitamos el contenido por default para que podamos encontrar ahoara el frame Principal
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame("fraPrincipal");
            //una vez que encontro el fraPrincipal ahora si que nos traiga el contenido activo dentro de el
            driver.SwitchTo().ActiveElement();
            
            //seleccionamos el boton para que cargue lso datos en la tabla con un evento click
            driver.FindElement(By.Id("btnFilFecha")).Click();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //guardamos el contenido del html generado en una variable tipo string
            string pagesrc = driver.PageSource;

            //creamos la instancia de la libreria web client
            WebClient webClient1 = new WebClient();
            //declaramos una variable tippo doc que toma por valor htmldocument
            var doc = new HtmlDocument();

            //se carga el contenido de la variable pagesrc y lo convierte a htmldocument
            doc.LoadHtml(pagesrc);

            //se encarga de encontrar dentro del documento la tabla que nos interesa 
            var myTable = doc.DocumentNode
                 .Descendants("div")
                 .Where(t => t.Attributes["id"].Value == "tablaJson")
                 .FirstOrDefault();

            //se hace un foreach para que selecciones los datos de la tabla
            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table[1]"))
            {
                //segundo foreach para que divida el documento por div
                foreach (HtmlNode row in table.SelectNodes("//div"))
                {
                    //  tercer foreach para dividir el documento pot tr
                    foreach (HtmlNode cell in row.SelectNodes("//tr"))
                    {
                        //trae los td de forma decendiente y en un arreglo
                        var tds = cell.Descendants("td").ToArray(); // trae todos los td

                        if (tds.Count() == 19)
                        {
                            var Valor = "Id";

                            if (tds[0].InnerHtml.Equals(Valor))
                            {
                                continue;
                            }
                        }

                        if (tds.Count() == 19)
                        {
                            //guarda los datos en las variables correspondientes para agregar a la base de datos

                            ML.DatosPortal datosPortal = new ML.DatosPortal();

                            datosPortal.Prioridad = tds[1].InnerText.ToString();
                            datosPortal.TipoServicio = tds[2].InnerText.ToString();
                            datosPortal.SucursalConsignatario = tds[3].InnerText.ToString();
                            datosPortal.FechaCaptura = tds[4].InnerText.ToString();
                            datosPortal.FechaRealizarServicio = tds[5].InnerText.ToString();
                            datosPortal.IdFolioDeServicio = tds[6].InnerText.ToString();
                            datosPortal.OrdenServicio = tds[7].InnerText.ToString();
                            char[] chars = { ' ' };
                            string Imp = tds[8].InnerText;
                            string Import = Imp.Trim(chars);
                            datosPortal.Importe = decimal.Parse(Import.ToString());
                            datosPortal.Divisa = tds[9].InnerText.ToString();
                            datosPortal.Te = tds[10].InnerText.ToString();
                            datosPortal.HoraEnvio = tds[11].InnerText.ToString();
                            datosPortal.Actualización = tds[12].InnerText.ToString();
                            datosPortal.Estatus = tds[13].InnerText.ToString();
                            //condiciones para que viaje entre metodos add,update,getbyid
                            if (datosPortal.IdFolioDeServicio == null)
                            {
                                Console.WriteLine("No existe formato valido de folio de servicio");
                            }
                            else
                            {
                                ML.Result result = BL.DatosPortal.GetById(datosPortal.IdFolioDeServicio);
                                if (result.Correct)
                                {
                                    BL.DatosPortal.Update(datosPortal);
                                    Console.WriteLine("Se modificaron los datos");
                                }
                                else
                                {
                                    BL.DatosPortal.Add(datosPortal);
                                    Console.WriteLine("Se registraron los datos");
                                }
                            }
                        }
                    }
                    break;
                }
                break;
            }

            ////Input3.Submit();


            ////var step2 = (driver.FindElements(By.XPath("//*[contains(@id, 'dropdown-intervaltype')]")));
            ////select.select_by_value("Previous Year")
























            ////  driver.Navigate().GoToUrl("https://portal.gsi.com.mx:8443/portal_desa/Login.do");




            ////WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            ////wait.Until(SeleniumExtras.WaitHelpers.Expect
            ////edConditions.ElementIsVisible(By.ClassName("combo")));

            ////            var Input3 = driver.FindElement(By.Id("cboMenus"));


            ////            Input3.Submit();











            //// driver.FindElement(By.Id("btnFillFecha"));







            //// var Password = ConfigurationManager.AppSettings["txtPassword"];

            ////string formParams = string.Format("txtUsuario={0}&txtPassword={1}", "MONHDRS03", "123");
            ////string cookieHeader;

            ////WebRequest request = WebRequest.Create("https://portal.gsi.com.mx:8443/portal_desa/Logout.do");
            ////request.ContentType = "text/plain";
            ////request.Method = "POST";
            ////byte[] bytes = Encoding.ASCII.GetBytes(formParams);
            ////request.ContentLength = bytes.Length;
            ////using (Stream os = request.GetRequestStream())
            ////{
            ////    os.Write(bytes, 0, bytes.Length);
            ////}
            ////WebResponse response = request.GetResponse();
            ////cookieHeader = response.Headers["Set-Cookie"];

            ////WebRequest getRequest = WebRequest.Create("https://portal.gsi.com.mx:8443/portal_desa/Inicio.do?pUsuClave=15142&pUsuRolClave=41&pUsuNombre=MONITOREO%20HIDROSINA%2003&pUsuBanClave=1&pUsuSubClave=19102&pUsuSucClave=%202&pUsuTraClave=62&pNueUsu=&pEmpClave=&pEmpDesc=&claveTraslado=%201&pUsuRegClave=&pUsuMultiBanco=1&pUsuIdSucursalBovirBpf=");
            ////getRequest.Method = "GET";
            ////getRequest.Headers.Add("Cookie", cookieHeader);
            ////WebResponse getResponse = getRequest.GetResponse();




            ////WebRequest getRequest2 = WebRequest.Create("https://portal.gsi.com.mx:8443/portal_desa/MonitorServicios.do?monitorRefresh=&pPage=&pUsuRolClave=41&hdnObjectValues=&hdnOperation=F&hdnResultado=&hdnSucursal=0&hdnBanco=0&hdnEstatus=0&hdnTipoFecha=A+Realizar+Servicio&hdnTipoDeposito=&hdnRegion=0&hdnTipoOrden=0&hdnFolio=&hdnFechaDel=22%2F09%2F2022&hdnFechaAl=22%2F09%2F2022&hdnBusqueda=&pUsuMultiBanco=1&horaEnvio=&hndNomenclatura=Todos&pUsuClave=15142&pUsuRolClave=41&pUsuBanClave=1&pUsuSubClave=19102&pUsuNombre=&pUsuSucClave=+2&pUsuTraClave=62&pEmpClave=&pEmpDesc=");
            ////getRequest.Method = "POST";
            ////getRequest.Headers.Add("Cookie", cookieHeader);
            ////WebResponse getResponse2 = getRequest.GetResponse();

            ///////////////////////////////


            ////WebBrowser webBrowser = new WebBrowser();
            ////webBrowser.Navigate("https://portal.gsi.com.mx:8443/portal_desa/MonitorServicios.do?carga=1&pUsuClave=15142&pUsuRolClave=41&pUsuBanClave=1&pUsuSubClave=19102&pUsuSucClave=%202&pUsuTraClave=62&pEmpClave=&pEmpDesc=&pUsuMultiBanco=1&pUsuIdSucursalBovirBpf=&claveTraslado=%201");
            ////webBrowser.Document.GetElementById("btnFilFecha").InvokeMember("click");







            ////var doc = new HtmlDocument();
            ////var request = (HttpWebRequest)WebRequest.Create(url);
            ////request.Method = "GET";
            ////using (var response = (HttpWebResponse)request.GetResponse())
            ////{
            ////    using (var stream = response.GetResponseStream())
            ////    {
            ////        doc.Load(stream);
            ////    }
            ////}
            ////var table = doc.GetElementbyId("tblThreads");



            //////////////////////////
            //WebClient webClient = new WebClient();

            //string page = webClient.DownloadString("https://datosprueba01.000webhostapp.com/DatosPreba02/Datos.html");
            ////// "https://portal.gsi.com.mx:8443/portal_desa/MonitorServicios.do?monitorRefresh=&pPage=&pUsuRolClave=41&hdnObjectValues=&hdnOperation=F&hdnResultado=&hdnSucursal=0&hdnBanco=0&hdnEstatus=0&hdnTipoFecha=A+Realizar+Servicio&hdnTipoDeposito=&hdnRegion=0&hdnTipoOrden=0&hdnFolio=&hdnFechaDel=22%2F09%2F2022&hdnFechaAl=22%2F09%2F2022&hdnBusqueda=&pUsuMultiBanco=1&horaEnvio=&hndNomenclatura=Todos&pUsuClave=15142&pUsuRolClave=41&pUsuBanClave=1&pUsuSubClave=19102&pUsuNombre=&pUsuSucClave=+2&pUsuTraClave=62&pEmpClave=&pEmpDesc="
            //////monitorRefresh=&pPage=&pUsuRolClave=41&hdnObjectValues=&hdnOperation=F&hdnResultado=&hdnSucursal=0&hdnBanco=0&hdnEstatus=0&hdnTipoFecha=A+Realizar+Servicio&hdnTipoDeposito=&hdnRegion=0&hdnTipoOrden=0&hdnFolio=&hdnFechaDel=22%2F09%2F2022&hdnFechaAl=22%2F09%2F2022&hdnBusqueda=&pUsuMultiBanco=1&horaEnvio=&hndNomenclatura=Todos&pUsuClave=15142&pUsuRolClave=41&pUsuBanClave=1&pUsuSubClave=19102&pUsuNombre=&pUsuSucClave=+2&pUsuTraClave=62&pEmpClave=&pEmpDesc=

            //////https://portal.gsi.com.mx:8443/portal_desa/MonitorServicios.do
            //////carga el documento html del portal y lo guarda en doc
            //var doc = new HtmlDocument();

            //doc.LoadHtml(page);

            ////var databutton = doc.GetElementbyId("btnFilFecha");

            ////var databutton = doc.GetElementbyId("btnFilFecha");
            ////databutton.




            ////var el = doc.do.GetElementById("email");
            ////if (el != null)
            ////    el.InvokeMember("click");




            //////lee la tabla que encontro en el documento por el id de la tabla y lo muestra por div
            ////var myTable = doc.DocumentNode
            ////     .Descendants("div")
            ////     .Where(t => t.Attributes["id"].Value == "tablaJson")
            ////     .FirstOrDefault();

            //////se hace un foreach para que selecciones los datos de la tabla
            ////foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table[1]"))
            ////{
            ////    //segundo foreach para que divida el documento por div
            ////    foreach (HtmlNode row in table.SelectNodes("//div"))
            ////    {
            ////        //  tercer foreach para dividir el documento pot tr
            ////        foreach (HtmlNode cell in row.SelectNodes("//tr"))
            ////        {
            ////            //trae los td de forma decendiente y en un arreglo
            ////            var tds = cell.Descendants("td").ToArray(); // trae todos los td

            ////            if (tds.Count() == 19)
            ////            {
            ////                var Valor = "Id";

            ////                if (tds[0].InnerHtml.Equals(Valor))
            ////                {
            ////                    continue;
            ////                }
            ////            }

            ////            if (tds.Count() == 19)
            ////            {
            ////                //guarda los datos en las variables correspondientes para agregar a la base de datos

            ////                ML.DatosPortal datosPortal = new ML.DatosPortal();

            ////                datosPortal.Prioridad = tds[1].InnerText.ToString();
            ////                datosPortal.TipoServicio = tds[2].InnerText.ToString();
            ////                datosPortal.SucursalConsignatario = tds[3].InnerText.ToString();
            ////                datosPortal.FechaCaptura = tds[4].InnerText.ToString();
            ////                datosPortal.FechaRealizarServicio = tds[5].InnerText.ToString();
            ////                datosPortal.IdFolioDeServicio = tds[6].InnerText.ToString();
            ////                datosPortal.OrdenServicio = tds[7].InnerText.ToString();
            ////                char[] chars = { ' ' };
            ////                string Imp = tds[8].InnerText;
            ////                string Import = Imp.Trim(chars);
            ////                datosPortal.Importe = decimal.Parse(Import.ToString());
            ////                datosPortal.Divisa = tds[9].InnerText.ToString();
            ////                datosPortal.Te = tds[10].InnerText.ToString();
            ////                datosPortal.HoraEnvio = tds[11].InnerText.ToString();
            ////                datosPortal.Actualización = tds[12].InnerText.ToString();
            ////                datosPortal.Estatus = tds[13].InnerText.ToString();

            ////                if (datosPortal.IdFolioDeServicio == null)
            ////                {
            ////                    Console.WriteLine("No existe formato valido de folio de servicio");
            ////                }
            ////                else
            ////                {
            ////                    ML.Result result = BL.DatosPortal.GetById(datosPortal.IdFolioDeServicio);
            ////                    if (result.Correct)
            ////                    {
            ////                        BL.DatosPortal.Update(datosPortal);
            ////                        Console.WriteLine("Se modificaron los datos");
            ////                    }
            ////                    else
            ////                    {
            ////                        BL.DatosPortal.Add(datosPortal);
            ////                        Console.WriteLine("Se registraron los datos");
            ////                    }
            ////                }
            ////            }
            ////        }
            ////        break;
            ////    }
            ////    break;
            ////}
        }
    }
}