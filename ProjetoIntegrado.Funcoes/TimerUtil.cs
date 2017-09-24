using System;
using System.Windows.Threading;

namespace ProjetoIntegrado.Funcoes
{
    public class TimerUtil
    {
        public DispatcherTimer timer { get; set; }

        private TimeSpan tempo;
        private EventHandler eventoTick;

        public TimerUtil(TimeSpan tempo, EventHandler eventoTick)
        {
            this.tempo = tempo;
            this.eventoTick = eventoTick;

            Configurar();
        }

        private void Configurar()
        {
            timer = new DispatcherTimer
            {
                Interval = tempo,
                IsEnabled = true
            };

            timer.Tick += eventoTick;
        }

        public void Iniciar()
        {
            timer.IsEnabled = true;
            timer.Start();
        }

        public void Parar()
        {
            timer.IsEnabled = false;
            timer.Stop();
        }
    }
}
