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

namespace Videoteca_Csharp.Vistas
{
    public partial class Login : Form
    {
        Usuarios usuarios = new Usuarios();
        public Login()
        {
            InitializeComponent();
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
