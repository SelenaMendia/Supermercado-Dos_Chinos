namespace CapaPresentacion
{
    partial class frmInicio
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
            this.contenedor = new System.Windows.Forms.Panel();
            this.LABELdia = new System.Windows.Forms.Label();
            this.LABELhora = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // contenedor
            // 
            this.contenedor.BackColor = System.Drawing.Color.Black;
            this.contenedor.Controls.Add(this.LABELdia);
            this.contenedor.Controls.Add(this.LABELhora);
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(0, 0);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1361, 602);
            this.contenedor.TabIndex = 4;
            // 
            // LABELdia
            // 
            this.LABELdia.AutoSize = true;
            this.LABELdia.BackColor = System.Drawing.Color.Black;
            this.LABELdia.Font = new System.Drawing.Font("Mongolian Baiti", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LABELdia.ForeColor = System.Drawing.Color.White;
            this.LABELdia.Location = new System.Drawing.Point(462, 432);
            this.LABELdia.Name = "LABELdia";
            this.LABELdia.Size = new System.Drawing.Size(177, 64);
            this.LABELdia.TabIndex = 38;
            this.LABELdia.Text = "label3";
            this.LABELdia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LABELhora
            // 
            this.LABELhora.AutoSize = true;
            this.LABELhora.BackColor = System.Drawing.Color.Black;
            this.LABELhora.Font = new System.Drawing.Font("Modern No. 20", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LABELhora.ForeColor = System.Drawing.Color.White;
            this.LABELhora.Location = new System.Drawing.Point(542, 252);
            this.LABELhora.Name = "LABELhora";
            this.LABELhora.Size = new System.Drawing.Size(349, 123);
            this.LABELhora.TabIndex = 37;
            this.LABELhora.Text = "label3";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1361, 602);
            this.Controls.Add(this.contenedor);
            this.Name = "frmInicio";
            this.Text = "frmInicio";
            this.contenedor.ResumeLayout(false);
            this.contenedor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel contenedor;
        private System.Windows.Forms.Label LABELdia;
        private System.Windows.Forms.Label LABELhora;
        private System.Windows.Forms.Timer timer1;
    }
}