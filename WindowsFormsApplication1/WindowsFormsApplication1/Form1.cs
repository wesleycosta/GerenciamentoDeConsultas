using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebEye.Controls.WinForms.WebCameraControl;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private List<WebCameraId> listaCameras;
        private WebCameraControl webCameraControl;

        public Form1()
        {
            InitializeComponent();
        }

        #region INICIAR

        private void InitializeComboBox()
        {
            listaCameras = webCameraControl.GetVideoCaptureDevices().ToList();
        }

        private void IniciarCapturacao()
        {
            webCameraControl.StartCapture(listaCameras[cbWebCam.SelectedIndex]);
        }

        private void Iniciar()
        {
          
        }

        private void WebCamWin_OnLoaded(object sender, RoutedEventArgs e)
        {
            SplashScreenControle.Mostrar();
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
    }
}
