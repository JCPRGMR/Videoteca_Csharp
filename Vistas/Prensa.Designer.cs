namespace Videoteca_Csharp.Vistas
{
    partial class Prensa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTipos = new System.Windows.Forms.ComboBox();
            this.cmbAreas = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelectVideo = new System.Windows.Forms.Button();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvPrensa = new System.Windows.Forms.DataGridView();
            this.btnUpVideo = new System.Windows.Forms.Button();
            this.btnUbicacion = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrensa)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnUbicacion);
            this.groupBox1.Controls.Add(this.btnUpVideo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.btnSelectVideo);
            this.groupBox1.Controls.Add(this.cmbAreas);
            this.groupBox1.Controls.Add(this.cmbTipos);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpFecha);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(611, 167);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INSERTAR VIDEO";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "FECHA DE SUBIDA:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(119, 60);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(200, 20);
            this.dtpFecha.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "AREA:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "TIPO:";
            // 
            // cmbTipos
            // 
            this.cmbTipos.FormattingEnabled = true;
            this.cmbTipos.Location = new System.Drawing.Point(48, 30);
            this.cmbTipos.Name = "cmbTipos";
            this.cmbTipos.Size = new System.Drawing.Size(132, 21);
            this.cmbTipos.TabIndex = 6;
            // 
            // cmbAreas
            // 
            this.cmbAreas.FormattingEnabled = true;
            this.cmbAreas.Location = new System.Drawing.Point(231, 30);
            this.cmbAreas.Name = "cmbAreas";
            this.cmbAreas.Size = new System.Drawing.Size(164, 21);
            this.cmbAreas.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AllowDrop = true;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(264, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "PRENSA";
            // 
            // btnSelectVideo
            // 
            this.btnSelectVideo.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSelectVideo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectVideo.Location = new System.Drawing.Point(401, 30);
            this.btnSelectVideo.Name = "btnSelectVideo";
            this.btnSelectVideo.Size = new System.Drawing.Size(193, 23);
            this.btnSelectVideo.TabIndex = 8;
            this.btnSelectVideo.Text = "SELECCIONAR VIDEO";
            this.btnSelectVideo.UseVisualStyleBackColor = false;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(93, 118);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(501, 32);
            this.txtDescripcion.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "DESCRIPCION";
            // 
            // dgvPrensa
            // 
            this.dgvPrensa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPrensa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrensa.Location = new System.Drawing.Point(12, 223);
            this.dgvPrensa.Name = "dgvPrensa";
            this.dgvPrensa.Size = new System.Drawing.Size(611, 215);
            this.dgvPrensa.TabIndex = 9;
            // 
            // btnUpVideo
            // 
            this.btnUpVideo.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnUpVideo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpVideo.Location = new System.Drawing.Point(401, 57);
            this.btnUpVideo.Name = "btnUpVideo";
            this.btnUpVideo.Size = new System.Drawing.Size(193, 23);
            this.btnUpVideo.TabIndex = 11;
            this.btnUpVideo.Text = "SUBIR VIDEO";
            this.btnUpVideo.UseVisualStyleBackColor = false;
            // 
            // btnUbicacion
            // 
            this.btnUbicacion.BackColor = System.Drawing.Color.Orange;
            this.btnUbicacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUbicacion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUbicacion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUbicacion.Location = new System.Drawing.Point(10, 89);
            this.btnUbicacion.Name = "btnUbicacion";
            this.btnUbicacion.Size = new System.Drawing.Size(584, 23);
            this.btnUbicacion.TabIndex = 12;
            this.btnUbicacion.Text = "SELECCIONAR UBICACION";
            this.btnUbicacion.UseVisualStyleBackColor = false;
            this.btnUbicacion.Click += new System.EventHandler(this.btnUbicacion_Click);
            // 
            // Prensa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(635, 450);
            this.Controls.Add(this.dgvPrensa);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Prensa";
            this.Text = "Prensa";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrensa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAreas;
        private System.Windows.Forms.ComboBox cmbTipos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectVideo;
        private System.Windows.Forms.Button btnUpVideo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.DataGridView dgvPrensa;
        private System.Windows.Forms.Button btnUbicacion;
    }
}