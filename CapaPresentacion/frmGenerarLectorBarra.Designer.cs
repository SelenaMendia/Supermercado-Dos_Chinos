namespace CapaPresentacion
{
    partial class frmGenerarLectorBarra
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
            this.btngenerardocumento = new FontAwesome.Sharp.IconButton();
            this.chkmostrardescripcion = new System.Windows.Forms.CheckBox();
            this.chkmostrarcodigo = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboorientacion = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbotipocodigo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnumeroetiquetas = new System.Windows.Forms.NumericUpDown();
            this.txtdescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPrecioVENTA = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtidProducto = new System.Windows.Forms.TextBox();
            this.btnodProducto = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnumeroetiquetas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btngenerardocumento
            // 
            this.btngenerardocumento.BackColor = System.Drawing.Color.LightGreen;
            this.btngenerardocumento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btngenerardocumento.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btngenerardocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenerardocumento.ForeColor = System.Drawing.Color.Black;
            this.btngenerardocumento.IconChar = FontAwesome.Sharp.IconChar.Barcode;
            this.btngenerardocumento.IconColor = System.Drawing.Color.Black;
            this.btngenerardocumento.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btngenerardocumento.IconSize = 40;
            this.btngenerardocumento.Location = new System.Drawing.Point(1032, 116);
            this.btngenerardocumento.Margin = new System.Windows.Forms.Padding(4);
            this.btngenerardocumento.Name = "btngenerardocumento";
            this.btngenerardocumento.Size = new System.Drawing.Size(199, 91);
            this.btngenerardocumento.TabIndex = 16;
            this.btngenerardocumento.Text = "Generar Documento";
            this.btngenerardocumento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btngenerardocumento.UseVisualStyleBackColor = false;
            this.btngenerardocumento.Click += new System.EventHandler(this.btngenerardocumento_Click);
            // 
            // chkmostrardescripcion
            // 
            this.chkmostrardescripcion.AutoSize = true;
            this.chkmostrardescripcion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkmostrardescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkmostrardescripcion.Location = new System.Drawing.Point(173, 25);
            this.chkmostrardescripcion.Margin = new System.Windows.Forms.Padding(4);
            this.chkmostrardescripcion.Name = "chkmostrardescripcion";
            this.chkmostrardescripcion.Size = new System.Drawing.Size(184, 24);
            this.chkmostrardescripcion.TabIndex = 1;
            this.chkmostrardescripcion.Text = "Mostrar Descripción";
            this.chkmostrardescripcion.UseVisualStyleBackColor = true;
            // 
            // chkmostrarcodigo
            // 
            this.chkmostrarcodigo.AutoSize = true;
            this.chkmostrarcodigo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkmostrarcodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkmostrarcodigo.Location = new System.Drawing.Point(15, 25);
            this.chkmostrarcodigo.Margin = new System.Windows.Forms.Padding(4);
            this.chkmostrarcodigo.Name = "chkmostrarcodigo";
            this.chkmostrarcodigo.Size = new System.Drawing.Size(146, 24);
            this.chkmostrarcodigo.TabIndex = 0;
            this.chkmostrarcodigo.Text = "Mostrar Codigo";
            this.chkmostrarcodigo.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.cboorientacion);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbotipocodigo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(542, 51);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(451, 198);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuracion";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkmostrardescripcion);
            this.groupBox3.Controls.Add(this.chkmostrarcodigo);
            this.groupBox3.Location = new System.Drawing.Point(13, 114);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(385, 57);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "En Etiqueta";
            // 
            // cboorientacion
            // 
            this.cboorientacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboorientacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboorientacion.FormattingEnabled = true;
            this.cboorientacion.Location = new System.Drawing.Point(208, 59);
            this.cboorientacion.Margin = new System.Windows.Forms.Padding(4);
            this.cboorientacion.Name = "cboorientacion";
            this.cboorientacion.Size = new System.Drawing.Size(217, 30);
            this.cboorientacion.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 62);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Orientación Documento:";
            // 
            // cbotipocodigo
            // 
            this.cbotipocodigo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbotipocodigo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbotipocodigo.FormattingEnabled = true;
            this.cbotipocodigo.Location = new System.Drawing.Point(208, 23);
            this.cbotipocodigo.Margin = new System.Windows.Forms.Padding(4);
            this.cbotipocodigo.Name = "cbotipocodigo";
            this.cbotipocodigo.Size = new System.Drawing.Size(217, 30);
            this.cbotipocodigo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(97, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tipo Codigo:";
            // 
            // txtnumeroetiquetas
            // 
            this.txtnumeroetiquetas.Location = new System.Drawing.Point(196, 162);
            this.txtnumeroetiquetas.Margin = new System.Windows.Forms.Padding(4);
            this.txtnumeroetiquetas.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.txtnumeroetiquetas.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtnumeroetiquetas.Name = "txtnumeroetiquetas";
            this.txtnumeroetiquetas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtnumeroetiquetas.Size = new System.Drawing.Size(266, 28);
            this.txtnumeroetiquetas.TabIndex = 7;
            this.txtnumeroetiquetas.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtdescripcion
            // 
            this.txtdescripcion.Location = new System.Drawing.Point(196, 95);
            this.txtdescripcion.Margin = new System.Windows.Forms.Padding(4);
            this.txtdescripcion.Name = "txtdescripcion";
            this.txtdescripcion.Size = new System.Drawing.Size(266, 28);
            this.txtdescripcion.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(75, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Descripción:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 164);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Número de Etiquetas:";
            // 
            // txtcodigo
            // 
            this.txtcodigo.Location = new System.Drawing.Point(196, 23);
            this.txtcodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Size = new System.Drawing.Size(266, 28);
            this.txtcodigo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(113, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Código:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 270);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(977, 42);
            this.progressBar1.TabIndex = 18;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPrecioVENTA);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtnombre);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtidProducto);
            this.groupBox1.Controls.Add(this.btnodProducto);
            this.groupBox1.Controls.Add(this.txtcodigo);
            this.groupBox1.Controls.Add(this.txtnumeroetiquetas);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtdescripcion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 51);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(518, 198);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Etiqueta";
            // 
            // txtPrecioVENTA
            // 
            this.txtPrecioVENTA.Location = new System.Drawing.Point(196, 128);
            this.txtPrecioVENTA.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrecioVENTA.Name = "txtPrecioVENTA";
            this.txtPrecioVENTA.Size = new System.Drawing.Size(266, 28);
            this.txtPrecioVENTA.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(69, 130);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 30;
            this.label7.Text = "Precio Venta:";
            // 
            // txtnombre
            // 
            this.txtnombre.Location = new System.Drawing.Point(196, 59);
            this.txtnombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(266, 28);
            this.txtnombre.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(106, 59);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Nombre:";
            // 
            // txtidProducto
            // 
            this.txtidProducto.Location = new System.Drawing.Point(478, 59);
            this.txtidProducto.Name = "txtidProducto";
            this.txtidProducto.Size = new System.Drawing.Size(37, 28);
            this.txtidProducto.TabIndex = 29;
            this.txtidProducto.Visible = false;
            // 
            // btnodProducto
            // 
            this.btnodProducto.BackColor = System.Drawing.Color.White;
            this.btnodProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnodProducto.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnodProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnodProducto.ForeColor = System.Drawing.Color.White;
            this.btnodProducto.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnodProducto.IconColor = System.Drawing.Color.Black;
            this.btnodProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnodProducto.IconSize = 24;
            this.btnodProducto.Location = new System.Drawing.Point(478, 23);
            this.btnodProducto.Name = "btnodProducto";
            this.btnodProducto.Size = new System.Drawing.Size(33, 28);
            this.btnodProducto.TabIndex = 28;
            this.btnodProducto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnodProducto.UseVisualStyleBackColor = false;
            this.btnodProducto.Click += new System.EventHandler(this.btnodProducto_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.btngenerardocumento);
            this.panel1.Location = new System.Drawing.Point(43, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1271, 382);
            this.panel1.TabIndex = 20;
            // 
            // frmGenerarLectorBarra
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1370, 1055);
            this.Controls.Add(this.panel1);
            this.Name = "frmGenerarLectorBarra";
            this.Text = "Generador de Codigo de Barra";
            this.Load += new System.EventHandler(this.frmGenerarLectorBarra_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnumeroetiquetas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private FontAwesome.Sharp.IconButton btngenerardocumento;
        private System.Windows.Forms.CheckBox chkmostrardescripcion;
        private System.Windows.Forms.CheckBox chkmostrarcodigo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cboorientacion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbotipocodigo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtnumeroetiquetas;
        private System.Windows.Forms.TextBox txtdescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtcodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnodProducto;
        private System.Windows.Forms.TextBox txtidProducto;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPrecioVENTA;
        private System.Windows.Forms.Label label7;
    }
}