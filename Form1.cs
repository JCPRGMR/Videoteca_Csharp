using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Videoteca_Csharp.Vistas;

namespace Videoteca_Csharp
{
    public partial class Form1 : Form
    {
        private object[] Usuario;

        public Form1(object objeto)
        {
            InitializeComponent();
            this.Usuario = (object[])objeto;
            this.Text = $"Videoteca - {Usuario[1]}";
        }

        private void btnPrensa_Click(object sender, EventArgs e)
        {
            OpenView(new Prensa());
        }
        private void OpenView(object view)
        {
            // Contador de controles si es mayor a cero se limpiaran o eliminar todos los controles
            if(this.panelContenedor.Controls.Count > 0) this.panelContenedor.Controls.RemoveAt(0);
            Form form = view as Form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(form);
            this.panelContenedor.Tag = view;
            form.Show();
        }

        private void btnProgramacion_Click(object sender, EventArgs e)
        {
            OpenView(new Programacion());
        }
    }
}
