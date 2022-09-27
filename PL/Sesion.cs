using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Configuration;
using System.Web.UI;
namespace PL
{
    public class Sesion
    {
        //private static Sesion instance;
        //private Sesion()
        //{

        //}

        //private static readonly object locker = new object();
        //public static Sesion GetInstance()
        //{
        //    lock (locker)
        //    {
        //        if (instance == null)
        //        {
        //            instance = new Sesion();
        //        }
        //        return instance;
        //    }
        //}
        //    /// <summary>
        //    /// Obtener datos del servidor a través de Get
        //    /// </summary>
        //    /// <param name = "url"> dirección de solicitud </param>
        //    /// <param name = "timeout"> Periodo de tiempo de espera </param>
        //    /// <param name = "statusCode"> Código de respuesta personalizado </param>
        //    /// <param name = "message"> información de respuesta </param>
        //    /// <returns></returns>
        //    public string GetRequest(string url, int timeout, out int statusCode, out string message)
        //    {
        //        string getUrl = VerifyUrl(url);
        //        if (getUrl.StartsWith("https://portal.gsi.com.mx:8443/portal_desa/Login.do"))
        //        {
        //            ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidate;
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        //        }
        //        try
        //        {
        //            Encoding encoding = Encoding.UTF8;
        //            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(getUrl));
        //            // Cancela el proxy para evitar un acceso lento por primera vez
        //            webRequest.Proxy = null;
        //            webRequest.KeepAlive = true;
        //            webRequest.Method = "Get";
        //            webRequest.Timeout = timeout;
        //            SetHeaderValue(webRequest.Headers, "Accept", "application/json");
        //            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
        //            StreamReader sr = new StreamReader(response.GetResponseStream(), encoding);
        //            string result = sr.ReadToEnd();
        //            statusCode = (int)response.StatusCode;
        //            message = "";
        //            return result;
        //        }
        //        catch (Exception ex)
        //        {
        //            statusCode = -1;
        //            message = ex.Message;
        //            return string.Empty;
        //            throw;
        //        }
        //    }

        //    /// <summary>
        //    /// Inicie una solicitud de registro al servidor a través de Post.
        //    /// </summary>
        //    /// <param name="url"></param>
        //    /// <param name="timeout"></param>
        //    /// <param name="parameters"></param>
        //    /// <param name="statusCode"></param>
        //    /// <param name="message"></param>
        //    /// <returns></returns>
        //    public string PostRequestRegister(string url, int timeout, IDictionary<string, string> parameters, out int statusCode, out string message)
        //    {
        //        string getUrl = VerifyUrl(url);
        //        if (getUrl.StartsWith("https://https://portal.gsi.com.mx:8443/portal_desa/Login.do"))
        //        {
        //            ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidate;
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        //        }
        //        try
        //        {
        //            Encoding encoding = Encoding.UTF8;
        //            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(getUrl));
        //            webRequest.Proxy = null;
        //            webRequest.KeepAlive = true;
        //            webRequest.Method = "Post";
        //            webRequest.Timeout = timeout;
        //            SetHeaderValue(webRequest.Headers, "Content-Type", "appliction/x-www-form-urlencoded");
        //            SetHeaderValue(webRequest.Headers, "Accept", "application/json");
        //            if (parameters != null && parameters.Count != 0)
        //            {
        //                StringBuilder buffer = new StringBuilder();
        //                bool first = true;
        //                foreach (string key in parameters.Keys)
        //                {
        //                    if (!first)
        //                    {
        //                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
        //                    }
        //                    else
        //                    {
        //                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
        //                        first = false;
        //                    }
        //                }
        //                byte[] json = Encoding.UTF8.GetBytes(buffer.ToString());
        //                // Escribir flujo de solicitud
        //                using (Stream stream = webRequest.GetRequestStream())
        //                {
        //                    stream.Write(json, 0, json.Length);
        //                }
        //            }

        //            HttpWebResponse response = (webRequest.GetResponse() as HttpWebResponse);
        //            statusCode = (int)response.StatusCode;
        //            string result;
        //            using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
        //            {
        //                result = sr.ReadToEnd();
        //            }
        //            message = "";
        //            return result;
        //        }
        //        catch (Exception ex)
        //        {
        //            statusCode = -1;
        //            message = ex.Message;
        //            return string.Empty;
        //        }
        //    }

        //    /// <summary>
        //    /// Publicar solicitud para iniciar sesión (los parámetros de solicitud aquí son diferentes a los del momento del registro)
        //    /// </summary>
        //    /// <param name="url"></param>
        //    /// <param name="timeout"></param>
        //    /// <param name="parameters"></param>
        //    /// <param name="statusCode"></param>
        //    /// <param name="message"></param>
        //    /// <returns></returns>
        //    public string PostRequestLogin(string url, int timeout, IDictionary<string, string> parameters, out int statusCode, out string message)
        //    {
        //        string getUrl = VerifyUrl(url);
        //        if (getUrl.StartsWith("https://https://portal.gsi.com.mx:8443/portal_desa/Login.do"))
        //        {
        //            ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidate;
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        //        }
        //        try
        //        {
        //            Encoding encoding = Encoding.UTF8;
        //            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(getUrl));
        //            webRequest.Proxy = null;
        //            webRequest.Timeout = timeout;
        //            webRequest.KeepAlive = true;
        //            webRequest.Method = "Post";
        //            SetHeaderValue(webRequest.Headers, "Content-Type", "application/json");
        //            if (parameters != null && parameters.Count != 0)
        //            {
        //                StringBuilder buffer = new StringBuilder();
        //                bool first = true;
        //                foreach (string key in parameters.Keys)
        //                {
        //                    if (!first)
        //                    {
        //                        buffer.AppendFormat(",\"" + "{0}" + "\":\"" + "{1}" + "\"}}", key, parameters[key]);
        //                    }
        //                    else
        //                    {
        //                        buffer.AppendFormat("{{" + "\"" + "{0}" + "\":\"" + "{1}" + "\"", key, parameters[key]);
        //                    }
        //                }
        //                byte[] json = Encoding.UTF8.GetBytes(buffer.ToString());
        //                // Escribir flujo de solicitud
        //                using (Stream stream = webRequest.GetRequestStream())
        //                {
        //                    stream.Write(json, 0, json.Length);
        //                }
        //            }
        //            HttpWebResponse response = (webRequest.GetResponse() as HttpWebResponse);
        //            statusCode = (int)response.StatusCode;
        //            string result;
        //            using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
        //            {
        //                result = sr.ReadToEnd();
        //            }
        //            message = "";
        //            return result;
        //        }
        //        catch (Exception ex)
        //        {
        //            statusCode = -1;
        //            message = ex.Message;
        //            return string.Empty;
        //        }
        //    }
        //    /// <summary>
        //    /// Agregar manualmente el encabezado de la solicitud
        //    /// </summary>
        //    /// <param name="headers"></param>
        //    /// <param name="name"></param>
        //    /// <param name="value"></param>
        //    private void SetHeaderValue(WebHeaderCollection headers, string name, string value)
        //    {
        //        var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        //        if (property != null)
        //        {
        //            var collection = property.GetValue(headers, null) as System.Collections.Specialized.NameValueCollection;
        //            collection[name] = value;
        //        }
        //    }

        //    public string VerifyUrl(string url)
        //    {
        //        if (string.IsNullOrEmpty(url))
        //        {
        //            //anzar una nueva excepción("¡La URL no puede estar vacía!");
        //        }
        //        if (url.StartsWith("https://portal.gsi.com.mx:8443/portal_desa/Login.do", StringComparison.CurrentCultureIgnoreCase) || url.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            return url;
        //        }
        //        else
        //        {
        //            // lanzar una nueva excepción("¡Ingrese la URL correcta!");
        //            return string.Format("https://portal.gsi.com.mx:8443/portal_desa/Login.do", url);
        //        }
        //    }

        //    // Confía en cualquier certificado
        //    public static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        //    {
        //        return true;
        //    }

        //    /// <summary>
        //    /// Verificar conexión
        //    /// </summary>
        //    /// <param name="url"></param>
        //    /// <returns></returns>
        //    public bool IsWebRequest(string url)
        //    {
        //        if (url.StartsWith("https://portal.gsi.com.mx:8443/portal_desa/Login.do"))
        //        {
        //            ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidate;
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        //        }
        //        try
        //        {
        //            WebRequest webRequest = WebRequest.Create(url);
        //            webRequest.Proxy = null;
        //            webRequest.Timeout = 20000;
        //            WebResponse webResponse = webRequest.GetResponse();
        //            webResponse.Close();
        //            return true;
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }
        //}  
        public static void Session()

        {
            //var Password = ConfigurationManager.AppSettings["txtPassword"];

            string formParams = string.Format("txtUsuario={0}&txtPassword={1}", "MONHDRS03", "123");
            string cookieHeader;

            WebRequest request = WebRequest.Create("https://portal.gsi.com.mx:8443/portal_desa/Logout.do");
            request.ContentType = "text/plain";
            request.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(formParams);
            request.ContentLength = bytes.Length;
            using (Stream os = request.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }
            WebResponse response = request.GetResponse();
            cookieHeader = response.Headers["Set-Cookie"];

            WebRequest getRequest = WebRequest.Create("https://portal.gsi.com.mx:8443/portal_desa/Inicio.do?pUsuClave=15142&pUsuRolClave=41&pUsuNombre=MONITOREO%20HIDROSINA%2003&pUsuBanClave=1&pUsuSubClave=19102&pUsuSucClave=%202&pUsuTraClave=62&pNueUsu=&pEmpClave=&pEmpDesc=&claveTraslado=%201&pUsuRegClave=&pUsuMultiBanco=1&pUsuIdSucursalBovirBpf=");
            getRequest.Method = "GET";
            getRequest.Headers.Add("Cookie", cookieHeader);
            WebResponse getResponse = getRequest.GetResponse();

            WebRequest getRequest2 = WebRequest.Create("https://portal.gsi.com.mx:8443/portal_desa/MonitorServicios.do?monitorRefresh=&pPage=&pUsuRolClave=41&hdnObjectValues=&hdnOperation=F&hdnResultado=&hdnSucursal=0&hdnBanco=0&hdnEstatus=0&hdnTipoFecha=A+Realizar+Servicio&hdnTipoDeposito=&hdnRegion=0&hdnTipoOrden=0&hdnFolio=&hdnFechaDel=22%2F09%2F2022&hdnFechaAl=22%2F09%2F2022&hdnBusqueda=&pUsuMultiBanco=1&horaEnvio=&hndNomenclatura=Todos&pUsuClave=15142&pUsuRolClave=41&pUsuBanClave=1&pUsuSubClave=19102&pUsuNombre=&pUsuSucClave=+2&pUsuTraClave=62&pEmpClave=&pEmpDesc=");
            getRequest.Method = "POST";
            getRequest.Headers.Add("Cookie", cookieHeader);
            WebResponse getResponse2 = getRequest.GetResponse();



        }
    }
}
