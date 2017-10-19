using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Threading;
using WebEye.Controls.Wpf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrado.View.WebCam
{
    public partial class WebCamWin
    {
        public bool Capturou;
        public Bitmap imgCapturada;
        private List<WebCameraId> listaCameras;
        private bool iniciou;

        public WebCamWin()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        #region INICIAR

        private void InitializeComboBox()
        {
            listaCameras = webCameraControl.GetVideoCaptureDevices().ToList();
            cbWebCam.ItemsSource = listaCameras.Select(x => x.Name);

            if (cbWebCam.Items.Count > 0)
                cbWebCam.SelectedIndex = 0;

            cbWebCam.Visibility = listaCameras.Count > 1 ? Visibility.Visible : Visibility.Hidden;
        }

        private async void IniciarCapturacao()
        {
            await Task.Run(() => Dispatcher.Invoke(() =>
                            webCameraControl.StartCapture(listaCameras[cbWebCam.SelectedIndex])));
        }

        private void Iniciar()
        {
            var timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 500),
                IsEnabled = true
            };

            timer.Tick += (o, args) =>
            {
                IniciarCapturacao();
                iniciou = true;

                timer.IsEnabled = false;
                timer.Stop();

                SplashScreen.Fechar();
                webCameraControl.Visibility = Visibility.Visible;
            };

            timer.Start();
        }

        private void WebCamWin_OnLoaded(object sender, RoutedEventArgs e)
        {
            SplashScreen.Mostrar();
            Iniciar();
        }

        #endregion

        #region EVENTOS

        // CHANGED COMBO CAMERAS
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (iniciou)
                IniciarCapturacao();
        }

        // BTN CAPTURAR
        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            imgCapturada = webCameraControl.GetCurrentImage();
            Capturou = true;
            Close();
        }

        // BTN CANCELAR
        private void OnStopButtonClick(object sender, RoutedEventArgs e)
        {
            webCameraControl.StopCapture();
            Close();
        }

        private void WebCamWin_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
