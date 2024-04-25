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
                            string nombreArchivo = Path.GetFileName(txtRuta.Text);
                            string destinoArchivo = Path.Combine(carpetaSeleccionada, nombreArchivo);
                            File.Copy(txtRuta.Text, destinoArchivo, true);

                            ThreadPool.QueueUserWorkItem((state) =>
                            {
                                File.Copy(txtRuta.Text, destinoArchivo, true);

                                Invoke((Action)(() => pgbPrensa.Visible = true));

                                for (int i = 0; i <= 100; i++)
                                {
                                    Thread.Sleep(50); // Ajusta el tiempo de acuerdo a la velocidad de copia real

                                    Invoke((Action)(() => pgbPrensa.Value = i));
                                }

                                Invoke((Action)(() => MessageBox.Show($"Se movió de la ruta {txtRuta.Text} a {destinoArchivo}.")));

                                Invoke((Action)(() => pgbPrensa.Visible = false));
                            });
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
