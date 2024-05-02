using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Videoteca_Csharp.Modelos;
using System.Net.NetworkInformation;

namespace Videoteca_Csharp.Vistas
{
    public partial class Login : Form
    {
        Usuarios usuarios = new Usuarios();
        //Areas areas = new Areas();
        //Tipos tipos = new Tipos();
        //Departamentos departamentos = new Departamentos();
        //Videos videos = new Videos();
        //Actividades actividades = new Actividades();


        public Login()
        {
            InitializeComponent();
            string Ip = "192.168.0.88";
            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send(Ip);
                if (reply.Status == IPStatus.Success)
                    this.Text = $"La ip {Ip} es accesible";
                else
                    this.Text = "No se pudo acceder";
            }
            catch (Exception ex)
            {
                this.Text = ex.Message;
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string[] datos = new string[]
            {
                txtUsername.Text,
                txtClave.Text,
            };
            if (!usuarios.ExisteClave(txtClave.Text)) usuarios.Insertar(datos);
            Form1 form = new Form1(usuarios.ObtenerDatos(datos[1]));
            form.Show();
            this.Hide();
        }
    }
}
