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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            //Desframentar();
            if (cmbTipos.Text.Length > 0) if (!tipos.Existe(cmbTipos.Text)) tipos.Insertar(cmbTipos.Text);
            if (cmbAreas.Text.Length > 0) if (!areas.Existe(cmbAreas.Text)) areas.Insertar(cmbAreas.Text);
            if (lblDepartamento.Text.Length > 0) if (!departamentos.Existe(lblDepartamento.Text)) departamentos.Insertar(lblDepartamento.Text);
            string mes = dtpFecha.Value.ToString("yy") + dtpFecha.Value.ToString("MM") + dtpFecha.Value.ToString("dd");

            MessageBox.Show(mes);

            MessageBox.Show("Id_tipo: " + tipos.BuscarId(cmbTipos.Text) + "\n Id_area: " + areas.BuscarId(cmbAreas.Text) + "\n Id_departamento: " + departamentos.BuscarId(lblDepartamento.Text));
        }
        private void Desframentar()
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
                            string videoFilePath = txtRuta.Text; // Ruta del archivo de video

                            // Define el tamaño de los fragmentos en bytes
                            int fragmentSize = 1024 * 1024; // 1 MB (puedes ajustar este valor según tus necesidades)

                            // Lee el archivo de video en un arreglo de bytes
                            byte[] videoBytes = File.ReadAllBytes(videoFilePath);

                            // Calcula la cantidad de fragmentos necesarios
                            int numFragments = (int)Math.Ceiling((double)videoBytes.Length / fragmentSize);

                            // Divide el video en fragmentos y copia cada fragmento a la carpeta seleccionada
                            for (int i = 0; i < numFragments; i++)
                            {
                                int startIndex = i * fragmentSize;
                                int endIndex = Math.Min(startIndex + fragmentSize, videoBytes.Length);
                                int fragmentLength = endIndex - startIndex;

                                byte[] fragment = new byte[fragmentLength];
                                Array.Copy(videoBytes, startIndex, fragment, 0, fragmentLength);

                                string fragmentFilePath = Path.Combine(carpetaSeleccionada, $"fragment_{i}.mp4");
                                File.WriteAllBytes(fragmentFilePath, fragment);

                                //MessageBox.Show($"Fragmento {i} copiado a: {fragmentFilePath}");
                            }

                            // Reconstruye el video en la carpeta seleccionada
                            string joinedVideoFilePath = Path.Combine(carpetaSeleccionada, "video_joined.mp4");
                            using (FileStream outputStream = new FileStream(joinedVideoFilePath, FileMode.Create))
                            {
                                for (int i = 0; i < numFragments; i++)
                                {
                                    string fragmentFilePath = Path.Combine(carpetaSeleccionada, $"fragment_{i}.mp4");
                                    byte[] fragmentBytes = File.ReadAllBytes(fragmentFilePath);
                                    outputStream.Write(fragmentBytes, 0, fragmentBytes.Length);
                                }
                            }

                            MessageBox.Show($"Video reconstruido en: {joinedVideoFilePath}");

                            // Eliminar los fragmentos individuales
                            for (int i = 0; i < numFragments; i++)
                            {
                                string fragmentFilePath = Path.Combine(carpetaSeleccionada, $"fragment_{i}.mp4");
                                File.Delete(fragmentFilePath);
                            }

                            MessageBox.Show("Desfragmentación completada.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al desfragmentar el video: {ex.Message}");
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
