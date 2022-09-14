using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System.Configuration;
using System.Collections.Specialized;

namespace BL
{
    class Program
    {
        static void Main(string[] args)
        {
            //string sAttr;
            //sAttr = ConfigurationManager.AppSettings.Get("txUsuario");
            //Console.WriteLine("The value of User is " + sAttr);
            //NameValueCollection sAll;
            //sAll = ConfigurationManager.AppSettings;
            //foreach (string s in sAll.AllKeys)
            //    Console.WriteLine("Key: " + s + " Value: " + sAll.Get(s));
            //Console.ReadLine();
            BL.Datos.ExtraerDatos();
    
            Console.ReadKey();
        }
        
    }
}

