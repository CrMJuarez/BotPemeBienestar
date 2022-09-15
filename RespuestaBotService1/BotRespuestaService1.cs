using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RespuestaBotService1
{
    public partial class BotRespuestaService1 : ServiceBase
    {
        public BotRespuestaService1()
        {
            InitializeComponent();
            eventoSistema = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("BotRespuestaService1"))
            {
                System.Diagnostics.EventLog.CreateEventSource("BotRespuestaService1", "Application");
            }
            eventoSistema.Source = "BotRespuestaService1";
            eventoSistema.Log = "Application";
        }
        protected override void OnStart(string[] args)
        {
            eventoSistema.WriteEntry("Se ha iniciado el servicio de respuesta (BotRespuestaService1).");
        }

        protected override void OnStop()
        {
            eventoSistema.WriteEntry("Se ha detenido el servicio de respuesta (BotRespuestaService1).");
        }
    }
}
