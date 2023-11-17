namespace CapaPresentacion
{
    partial class frmCamaras
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCapturar = new FontAwesome.Sharp.IconButton();
            this.btnApagarCamara = new FontAwesome.Sharp.IconButton();
            this.btnEncenderCamara = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(109, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1042, 555);
            this.panel1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(66, 107);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(537, 24);
            this.comboBox1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.btnCapturar);
            this.panel2.Controls.Add(this.btnApagarCamara);
            this.panel2.Controls.Add(this.btnEncenderCamara);
            this.panel2.Location = new System.Drawing.Point(655, 154);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(364, 366);
            this.panel2.TabIndex = 2;
            // 
            // btnCapturar
            // 
            this.btnCapturar.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnCapturar.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapturar.IconChar = FontAwesome.Sharp.IconChar.Camera;
            this.btnCapturar.IconColor = System.Drawing.Color.White;
            this.btnCapturar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCapturar.Location = new System.Drawing.Point(92, 266);
            this.btnCapturar.Name = "btnCapturar";
            this.btnCapturar.Size = new System.Drawing.Size(182, 48);
            this.btnCapturar.TabIndex = 3;
            this.btnCapturar.Text = "Capturar";
            this.btnCapturar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCapturar.UseVisualStyleBackColor = false;
            this.btnCapturar.Click += new System.EventHandler(this.btnCapturar_Click);
            // 
            // btnApagarCamara
            // 
            this.btnApagarCamara.BackColor = System.Drawing.Color.Brown;
            this.btnApagarCamara.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApagarCamara.IconChar = FontAwesome.Sharp.IconChar.EyeSlash;
            this.btnApagarCamara.IconColor = System.Drawing.Color.White;
            this.btnApagarCamara.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnApagarCamara.Location = new System.Drawing.Point(92, 165);
            this.btnApagarCamara.Name = "btnApagarCamara";
            this.btnApagarCamara.Size = new System.Drawing.Size(182, 48);
            this.btnApagarCamara.TabIndex = 1;
            this.btnApagarCamara.Text = "Apagar";
            this.btnApagarCamara.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApagarCamara.UseVisualStyleBackColor = false;
            this.btnApagarCamara.Click += new System.EventHandler(this.btnApagarCamara_Click);
            // 
            // btnEncenderCamara
            // 
            this.btnEncenderCamara.BackColor = System.Drawing.Color.SteelBlue;
            this.btnEncenderCamara.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEncenderCamara.IconChar = FontAwesome.Sharp.IconChar.Eye;
            this.btnEncenderCamara.IconColor = System.Drawing.Color.White;
            this.btnEncenderCamara.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEncenderCamara.Location = new System.Drawing.Point(92, 57);
            this.btnEncenderCamara.Name = "btnEncenderCamara";
            this.btnEncenderCamara.Size = new System.Drawing.Size(182, 52);
            this.btnEncenderCamara.TabIndex = 0;
            this.btnEncenderCamara.Text = "Encender";
            this.btnEncenderCamara.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEncenderCamara.UseVisualStyleBackColor = false;
            this.btnEncenderCamara.Click += new System.EventHandler(this.btnEncenderCamara_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.Location = new System.Drawing.Point(66, 152);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(537, 368);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(361, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "Camara de Vigilancia";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(62, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Seleccione una camara a usar";
            // 
            // frmCamaras
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1250, 602);
            this.Controls.Add(this.panel1);
            this.Name = "frmCamaras";
            this.Text = "frmCamaras";
            this.Load += new System.EventHandler(this.frmCamaras_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton btnEncenderCamara;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnApagarCamara;
        private System.Windows.Forms.Timer timer1;
        private FontAwesome.Sharp.IconButton btnCapturar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
    }
}