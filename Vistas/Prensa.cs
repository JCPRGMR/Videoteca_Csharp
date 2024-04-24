using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Videoteca_Csharp.Vistas
{
    public partial class Prensa : Form
    {
        public Prensa()
        {
            InitializeComponent();
        }

        private void btnUbicacion_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                DialogResult result = folder.ShowDialog();
                if(result == DialogResult.OK) btnUbicacion.Text = folder.SelectedPath;
            }
        }
    }
}
