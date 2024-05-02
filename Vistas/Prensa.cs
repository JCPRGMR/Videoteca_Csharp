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
using System.Runtime.InteropServices;
using System.Reflection;

namespace Videoteca_Csharp.Vistas
{
    public partial class Prensa : Form
    {
        Tipos tipos = new Tipos();
        Areas areas = new Areas();
        Departamentos departamentos = new Departamentos();
        Videos videosdbb = new Videos();

        public Prensa()
        {
            InitializeComponent();
            txtRuta.Enabled = false;
            pgbPrensa.Maximum = 100;

            if (lblDepartamento.Text.Length > 0) if (!departamentos.Existe(lblDepartamento.Text)) departamentos.Insertar(lblDepartamento.Text);
            Llenartipos();
            LlenarAreas();
            dgvPrensa.AutoGenerateColumns = false;
            dgvPrensa.Columns.Add("CodigoVideo", "CODIGO DE VIDEO");
            dgvPrensa.Columns.Add("Area", "AREA");
            dgvPrensa.Columns.Add("Tipo", "TIPO");
            dgvPrensa.Columns.Add("Titulo", "TITULO");
            dgvPrensa.Columns.Add("Acciones", "ACCIONES");
            dgvPrensa.DataSource = videosdbb.Mostrar();
        }
        public void Llenartipos()
        {
            cmbTipos.Items.Clear();

            List<object[]> datos = tipos.Mostrar();

            foreach (var fila in datos)
            {
                cmbTipos.Items.Add(fila[1].ToString());
            }
        }
        public void LlenarAreas()
        {
            cmbAreas.Items.Clear();

            List<object[]> datos = areas.Mostrar();

            foreach (var fila in datos)
            {
                cmbAreas.Items.Add(fila[1].ToString());
            }
        }
        private void btnSelectVideo_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Archivos de Video|*.mp4;*.avi;*.mkv;*.mov;*.wmv|Todos los archivos|*.*";
                dialog.Title = "Seleccionar video";

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtRuta.Text = dialog.FileName;
                    FileInfo fileInfo = new FileInfo(txtRuta.Text);
                    pgbPrensa.Maximum = Convert.ToInt32(fileInfo.Length / 1024);
                    MessageBox.Show($"El tamaño del archivo es: {pgbPrensa.Maximum} KB");
                    pgbPrensa.Value = 0;
                    MessageBox.Show("Valor actual: " + pgbPrensa.Value);
                }
            }
        }
        private void btnUpVideo_Click(object sender, EventArgs e)
        {
            // SUBIDA DE VIDEOS ASINCRONA ESTE CONDIGO HACERLO FUNCIONAL DESPUES DE QUE LOS DATOS DEL VIDEO SE GUARDEN
            //Desframentar();

            // COODIGO PARA INSERTAR EN LA BASE DE DATOS
            //if (cmbTipos.Text.Length > 0) if (!tipos.Existe(cmbTipos.Text)) tipos.Insertar(cmbTipos.Text.ToUpper());
            //if (cmbAreas.Text.Length > 0) if (!areas.Existe(cmbAreas.Text)) areas.Insertar(cmbAreas.Text.ToUpper());

            // CODIGO PARA GUARGAR EL VIDEO YYYYMMDD()(PRIMERAS 3 LETRAS DE AREA)001.EXTENSION

            string codigoVideo = dtpFecha.Value.ToString("yyyy") + dtpFecha.Value.ToString("MM") + dtpFecha.Value.ToString("dd") + cmbAreas.Text.Substring(0, 3).ToUpper();
            MessageBox.Show(codigoVideo);
            //MessageBox.Show("Id_tipo: " + tipos.BuscarId(cmbTipos.Text) + "\n Id_area: " + areas.BuscarId(cmbAreas.Text) + "\n Id_departamento: " + departamentos.BuscarId(lblDepartamento.Text));

            Llenartipos();
            LlenarAreas();
        }
        private void Desframentar()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Selecciona una carpeta compartida";
                //dialog.SelectedPath = @"Z:";  //Establecer una ruta
                //dialog.RootFolder = Environment.SpecialFolder.DesktopDirectory;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string carpetaSeleccionada = dialog.SelectedPath;

                    if (!string.IsNullOrEmpty(txtRuta.Text) && File.Exists(txtRuta.Text))
                    {
                        var ainc = new Task(() =>
                        {
                            try
                            {
                                string videoFilePath = txtRuta.Text; // Ruta del archivo de video
                                int fragmentSize = (1024 * 1024) * 3; // 1 MB (puedes ajustar este valor según tus necesidades)
                                int fragmentIndex = 0; // Declaramos fragmentIndex aquí

                                FileInfo extension = new FileInfo(txtRuta.Text);
                                // Dividir el video en fragmentos
                                using (FileStream input = File.OpenRead(videoFilePath))
                                {
                                    byte[] buffer = new byte[fragmentSize];
                                    int bytesRead;
                                    while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        string fragmentFilePath = Path.Combine(carpetaSeleccionada, $"fragment_{fragmentIndex}." + extension.Extension);
                                        using (FileStream output = File.Create(fragmentFilePath))
                                        {
                                            output.Write(buffer, 0, bytesRead);
                                        }

                                        this.Invoke((MethodInvoker)delegate {
                                            // Actualizar pgbPrensa aquí
                                            if (pgbPrensa.Value + (1024 * 3) <= pgbPrensa.Maximum)
                                            {
                                                pgbPrensa.Value += (1024 * 3);
                                            }
                                        });

                                        fragmentIndex++;
                                    }
                                }
                                // Reconstruir el video en la carpeta seleccionada
                                string joinedVideoFilePath = Path.Combine(carpetaSeleccionada, "video_joined." + extension.Extension);
                                using (FileStream outputStream = new FileStream(joinedVideoFilePath, FileMode.Create))
                                {
                                    for (int i = 0; i < fragmentIndex; i++)
                                    {
                                        string fragmentFilePath = Path.Combine(carpetaSeleccionada, $"fragment_{i}." + extension.Extension);

                                        // Lee y escribe cada fragmento en el archivo de video reconstruido
                                        using (FileStream fragmentStream = File.OpenRead(fragmentFilePath))
                                        {
                                            byte[] buffer = new byte[fragmentSize];
                                            int bytesRead;

                                            while ((bytesRead = fragmentStream.Read(buffer, 0, buffer.Length)) > 0)
                                            {
                                                outputStream.Write(buffer, 0, bytesRead);   // REESCRIBE EL DISCO
                                                // INVOKE PARA PODER ACCEDER A LOS OBJETOS FORM
                                                this.Invoke((MethodInvoker)delegate {
                                                    if (pgbPrensa.Value + (1024 * 3) <= pgbPrensa.Maximum)
                                                    {
                                                        pgbPrensa.Value += (1024 * 3);
                                                    }
                                                });
                                            }
                                        }
                                        // Elimina el fragmento individual después de escribirlo en el archivo reconstruido
                                        File.Delete(fragmentFilePath);
                                    }
                                }
                                MessageBox.Show("Reconstruccion completada.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error al desfragmentar el video: {ex.Message}");
                            }
                        });
                        ainc.Start();
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
