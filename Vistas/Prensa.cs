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
using System.Diagnostics;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Drawing.Drawing2D;

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
            txtRuta.Validating += txtRuta_Validating;
            txtRuta.CausesValidation = true;
            txtRuta.Focus();
            pgbPrensa.Maximum = 100;
            if (lblDepartamento.Text.Length > 0) if (!departamentos.Existe(lblDepartamento.Text)) departamentos.Insertar(lblDepartamento.Text);
            Llenartipos();
            LlenarAreas();
            llenarDGV();
        }
        /**
         * CAMBIO DE COLOR DEGRADADO
         */
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Define los colores para el degradado
            Color[] colors = {
                Color.Black,
                Color.FromArgb(128, 0, 128), // Púrpura intermedio
                Color.FromArgb(148, 0, 211) // Violeta oscuro
            };

            // Crea un objeto LinearGradientBrush
            LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                colors[0],
                colors[colors.Length - 1],
                LinearGradientMode.Vertical); // Inicia desde arriba y baja

            // Define los pasos de color para el degradado
            ColorBlend colorBlend = new ColorBlend();
            colorBlend.Colors = colors;
            float[] positions = { 0.0f, 0.5f, 1.0f }; // Posiciones de los colores en el degradado
            colorBlend.Positions = positions;

            brush.InterpolationColors = colorBlend;

            // Dibuja el degradado
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }
        public void llenarDGV()
        {
            List<object[]> datos = videosdbb.Mostrar();

            dgvPrensa.Columns.Clear();

            dgvPrensa.Columns.Add("cod_video", "CODIGO DE VIDEO");
            dgvPrensa.Columns.Add("id_fk_tipo", "ID TIPO");
            dgvPrensa.Columns.Add("id_fk_area", "ID ÁREA");
            dgvPrensa.Columns.Add("titulo", "TITULO");
            DataGridViewLinkColumn columnaRuta = new DataGridViewLinkColumn();
            columnaRuta.LinkColor = Color.White;
            columnaRuta.Name = "ruta_reproduccion";
            columnaRuta.HeaderText = "RUTA DE REPRODUCCIÓN";
            columnaRuta.DataPropertyName = "ruta_reproduccion"; // Nombre de la columna en el origen de datos
            dgvPrensa.Columns.Add(columnaRuta);

            // Asignar los datos al DataGridView
            foreach (var fila in datos)
            {
                //MessageBox.Show(fila[1].ToString());
                dgvPrensa.Rows.Add(fila);
            }

            dgvPrensa.CellContentClick += (sender, e) =>
            {
                if (e.ColumnIndex >= 0 && dgvPrensa.Columns[e.ColumnIndex].Name == "ruta_reproduccion")
                {
                    string rutaArchivo = dgvPrensa.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    FileInfo info = new FileInfo(rutaArchivo);
                    //MessageBox.Show(info.FullName.ToString());    //MUESTRA LA RUTA DE REPRODUCCION DEL VIDEO
                    //MessageBox.Show($@"{rutaArchivo}" + "." + info.Extension.ToString());
                    if (File.Exists($@"{rutaArchivo}"))
                    {
                        //MessageBox.Show("Archivo leido");   // MENSAJE PARA DE VALIDACION PARA ARCHIVO LEIDO
                        Process.Start("mpc-hc64.exe", $@"{rutaArchivo}");
                    }
                    else
                    {
                        MessageBox.Show("Porfavor instale El productor K-lite o vlc");
                    }
                }
            };

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
                    pgbPrensa.Value = 0;
                }
            }
        }
        private void btnUpVideo_Click(object sender, EventArgs e)
        {
            int aux = 0;
            do 
            {
                if (txtRuta.Text.Length > 0 && cmbAreas.Text.Length > 0 && cmbTipos.Text.Length > 0)
                {
                    // COODIGO PARA INSERTAR EN LA BASE DE DATOS
                    if (cmbTipos.Text.Length > 0) if (!tipos.Existe(cmbTipos.Text)) tipos.Insertar(cmbTipos.Text.ToUpper());
                    if (cmbAreas.Text.Length > 0) if (!areas.Existe(cmbAreas.Text)) areas.Insertar(cmbAreas.Text.ToUpper());

                    // CODIGO PARA GUARGAR EL VIDEO YYYYMMDD()(PRIMERAS 3 LETRAS DE AREA)001.EXTENSION
                    string codigoVideo = dtpFecha.Value.ToString("yyyy") + dtpFecha.Value.ToString("MM") + dtpFecha.Value.ToString("dd") + cmbAreas.Text.Substring(0, 3).ToUpper();
                    int n_codigo = int.Parse((videosdbb.VerUltimoCodVideo(codigoVideo)?.ToString()?.Substring(11) ?? "0")) + 1;

                    FileInfo file = new FileInfo(txtRuta.Text);
                    //MessageBox.Show(codigoVideo + n_codigo.ToString("000") + file.Extension.ToString());

                    object[] DatosVideo = new object[]
                    {
                        codigoVideo + n_codigo.ToString("000"),
                        txtTitulo.Text,
                        txtDescripcion.Text,
                        "El video se esta cargando...",
                        file.Name,
                        "Sin miniatura",
                        departamentos.BuscarId(lblDepartamento.Text),
                        tipos.BuscarId(cmbTipos.Text),
                        areas.BuscarId(cmbAreas.Text),
                    };
                    videosdbb.Insertar(DatosVideo);
                    /**
                     * RUTA DEL VIDEO => Actulizar la base de datos para agregar su ruta
                     */
                    //string rutaXD;
                    Desframentar(codigoVideo + n_codigo.ToString("000") + file.Extension.ToString(), codigoVideo + n_codigo.ToString("000"));

                }
                else
                {
                    aux = 0;
                    MessageBox.Show("Seleccione un video");
                }
            } while (aux != 0);
            Llenartipos();
            LlenarAreas();
            llenarDGV();
        }
        private void Desframentar(string cod_video, string codigo)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Selecciona una carpeta compartida";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string carpetaSeleccionada = dialog.SelectedPath;

                    if (!string.IsNullOrEmpty(txtRuta.Text) && File.Exists(txtRuta.Text))
                    {
                        string videoFilePath = txtRuta.Text; // Ruta del archivo de video
                        int fragmentSize = 1024 * 1024 * 5; // 5 MB (puedes ajustar este valor según tus necesidades)
                        int fragmentIndex = 0; // Declaramos fragmentIndex aquí

                        FileInfo extension = new FileInfo(txtRuta.Text);

                        try
                        {
                            var task = Task.Run(() =>
                            {
                                // Reconstruir el video en la carpeta seleccionada mientras se fragmenta
                                using (FileStream input = File.OpenRead(videoFilePath))
                                {
                                    byte[] buffer = new byte[fragmentSize];
                                    int bytesRead;
                                    string joinedVideoFilePath = Path.Combine(carpetaSeleccionada, $"{cod_video}");

                                    using (FileStream outputStream = new FileStream(joinedVideoFilePath, FileMode.Create))
                                    {
                                        while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            string fragmentFilePath = Path.Combine(carpetaSeleccionada, $"{cod_video}_{fragmentIndex}" + extension.Extension);
                                            using (FileStream output = File.Create(fragmentFilePath))
                                            {
                                                output.Write(buffer, 0, bytesRead);
                                            }

                                            // Lee y escribe cada fragmento en el archivo de video reconstruido
                                            outputStream.Write(buffer, 0, bytesRead);

                                            // Elimina el fragmento individual después de escribirlo en el archivo reconstruido
                                            File.Delete(fragmentFilePath);

                                            fragmentIndex++;

                                            this.Invoke((MethodInvoker)delegate {
                                                if (pgbPrensa.Value + bytesRead <= pgbPrensa.Maximum)
                                                {
                                                    pgbPrensa.Value += bytesRead;
                                                }
                                            });
                                        }
                                        object[] datos =
                                        {
                                            joinedVideoFilePath,
                                            codigo,
                                        };
                                        videosdbb.ActualizarRuta(datos);

                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            llenarDGV();
                                        });
                                    }
                                }
                            });
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

        private void cmbTipos_Validated(object sender, EventArgs e)
        {
        }

        private void cmbTipos_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbTipos.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbTipos, "Este campo es obligatorio");
            }
            else
            {
                errorProvider1.SetError(cmbTipos, "");
            }
        }

        private void txtRuta_Validating(object sender, CancelEventArgs e)
        {
            if (txtRuta.Text.Length > 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtRuta, "Debe seleccionar un video");
            }
            else
            {
                errorProvider1.SetError(txtRuta, "");
            }
        }
    }
}
