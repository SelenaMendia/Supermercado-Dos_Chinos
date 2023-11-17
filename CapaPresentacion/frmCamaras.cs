using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using AForge.Video.DirectShow;
using AForge.Video;



namespace CapaPresentacion
{
    public partial class frmCamaras : Form
    {


        private bool Dipositivos;

        private FilterInfoCollection MiDispositivos;

        private VideoCaptureDevice MiWebCam;
        public frmCamaras()
        {
            InitializeComponent();
        }

        private void frmCamaras_Load(object sender, EventArgs e)
        {
            CargaDispositivos();
        }

        public void CargaDispositivos()
        {
            MiDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (MiDispositivos.Count > 0)
            {
                Dipositivos = true;

                foreach (FilterInfo dispositivo in MiDispositivos)
                {
                    comboBox1.Items.Add(dispositivo.Name); // Agrega el nombre del dispositivo
                }

                comboBox1.SelectedIndex = 0; // Selecciona el primer dispositivo por defecto
            }
            else
            {
                Dipositivos = false;
            }
        }

        public void CerrarWebCam()
        {
            if (MiWebCam != null && MiWebCam.IsRunning)
            {
                MiWebCam.SignalToStop();
                MiWebCam = null;
            }
            pictureBox1.Image = null;
        }

        private void btnEncenderCamara_Click(object sender, EventArgs e)
        {
            CerrarWebCam();
            int i = comboBox1.SelectedIndex;
            string NombreVideo = MiDispositivos[i].MonikerString;
            MiWebCam = new VideoCaptureDevice(NombreVideo);
            MiWebCam.NewFrame += new NewFrameEventHandler(CapturandoImagen);
            MiWebCam.Start();
        }

        private void CapturandoImagen(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = Imagen;
        }

        private void btnApagarCamara_Click(object sender, EventArgs e)
        {
            CerrarWebCam();
        }

        private void btnCapturar_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                DateTime now = DateTime.Now;
                string fechaHora = now.ToString("yyyyMMdd-HHmmss");

                // Abre un cuadro de diálogo para elegir la ubicación de guardado
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivos de imagen|*.png";
                saveFileDialog.Title = "Guardar Captura de Pantalla";
                saveFileDialog.DefaultExt = ".png";

                saveFileDialog.FileName = "Captura de Pantalla de Vigilancia - " + fechaHora + ".png";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Guarda la imagen en el archivo seleccionado
                    pictureBox1.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show("Captura de pantalla guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna imagen para capturar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
        



