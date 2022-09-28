using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PL
{
    public class Program
    {


        public static void Main(string[] args)
        {
            
            DatosPortal.ExtraerDatos();           
            Timer timer = new Timer(10000);//10000
            //timer.AutoReset = true;

            timer.Elapsed += EventoElapsed;
            timer.Start();
            //DatosPortal.ExtraerDatos();
            //DatosPortal.ExtraerDatos();
            while (true) ;
            //Console.ReadKey();
        }
        private static void EventoElapsed(object sender, ElapsedEventArgs e)
        {
            DatosPortal.ExtraerDatos();

        }
        //private static Task HandleTimer()
        //{
        //    Console.WriteLine("\nHandler not implemented...");
        //    throw new NotImplementedException();
        //}
    }
}
