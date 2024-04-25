using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Videoteca_Csharp.Modelos;
using System.Threading;

namespace Videoteca_Csharp.Vistas
{
    public partial class Prensa : Form
    {
        Tipos tipos = new Tipos();
        Areas areas = new Areas();
        Departamentos departamentos = new Departamentos();
        public Prensa()
        {
            InitializeComponent();
            txtRuta.Enabled = false;
            pgbPrensa.Hide();
        }
        private void btnSelectVideo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Archivos de Video|*.mp4;*.avi;*.mkv;*.mov;*.wmv|Todos los archivos|*.*";
                dialog.Title = "Sleecionar video";

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK) txtRuta.Text = dialog.FileName;
            }
        }

        private void btnUpVideo_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Selecciona una carpeta compartida";
                dialog.SelectedPath = @"Z:";
                //dialog.RootFolder = Environment.SpecialFolder.DesktopDirectory;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string carpetaSeleccionada = dialog.SelectedPath;

                    if (!string.IsNullOrEmpty(txtRuta.Text) && File.Exists(txtRuta.Text))
                    {
                        try
                        {
                            string archivo = Path.GetFileName(txtRuta.Text);
                            string nombreArchivo = Path.GetFileName(archivo);
                            FileInfo fileInfo = new FileInfo(nombreArchivo);
                            //string destinoArchivo = Path.Combine(carpetaSeleccionada, nombreArchivo); // Esta combina la ruta de la carpeta escogida con el nombre del archivo y su extencion
                            MessageBox.Show(nombreArchivo.Length.ToString());
                            //File.Copy(txtRuta.Text, destinoArchivo, true); // Proceso para copiar un video de una ruta a otra
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al mover el archivo: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("La ruta del archivo no es válida o el archivo no existe.");
                    }
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
