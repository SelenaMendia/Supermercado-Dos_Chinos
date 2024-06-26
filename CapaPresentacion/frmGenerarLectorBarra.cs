﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion.Utilidades;
using iTextSharp.text.pdf;
using iTextSharp.text;
using CapaPresentacion.Modales;
using BarcodeLib;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class frmGenerarLectorBarra : Form
    {

        private static string rutaguardado = "";
        private static int valorCodigo = -1;
        private static int verticalDocumento = -1;

        public frmGenerarLectorBarra()
        {
            InitializeComponent();
        }

        private void frmGenerarLectorBarra_Load(object sender, EventArgs e)
        {
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 1, Texto = "UPCA" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 2, Texto = "UPCE" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 3, Texto = "UPC_SUPPLEMENTAL_2DIGIT" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 4, Texto = "UPC_SUPPLEMENTAL_5DIGIT" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 5, Texto = "EAN13" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 6, Texto = "EAN8" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 7, Texto = "Interleaved2of5" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 8, Texto = "Standard2of5" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 9, Texto = "Industrial2of5" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 10, Texto = "CODE39" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 11, Texto = "CODE39Extended" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 12, Texto = "CODE39_Mod43" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 13, Texto = "Codabar" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 14, Texto = "PostNet" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 15, Texto = "BOOKLAND" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 16, Texto = "ISBN" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 17, Texto = "JAN13" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 18, Texto = "MSI_Mod10" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 19, Texto = "MSI_2Mod10" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 20, Texto = "MSI_Mod11" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 21, Texto = "MSI_Mod11_Mod10" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 22, Texto = "Modified_Plessey" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 23, Texto = "CODE11" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 24, Texto = "USD8" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 25, Texto = "UCC12" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 26, Texto = "UCC13" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 27, Texto = "LOGMARS" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 28, Texto = "CODE128" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 29, Texto = "CODE128A" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 30, Texto = "CODE128B" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 31, Texto = "CODE128C" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 32, Texto = "ITF14" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 33, Texto = "CODE93" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 34, Texto = "TELEPEN" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 35, Texto = "FIM" });
            cbotipocodigo.Items.Add(new OpcionCombo() { Valor = 36, Texto = "PHARMACODE" });

            cbotipocodigo.DisplayMember = "Texto";
            cbotipocodigo.ValueMember = "Valor";
            cbotipocodigo.SelectedIndex = 27;

            cboorientacion.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Vertical" });
            cboorientacion.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Horizontal" });
            cboorientacion.DisplayMember = "Texto";
            cboorientacion.ValueMember = "Valor";
            cboorientacion.SelectedIndex = 0;


        }
        
        private void btngenerardocumento_Click(object sender, EventArgs e)
        {
            if (txtcodigo.Text.Trim() != "")
            {
                bool error = false;
                progressBar1.Maximum = Convert.ToInt32(txtnumeroetiquetas.Value.ToString());
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                valorCodigo = Convert.ToInt32(((OpcionCombo)cbotipocodigo.SelectedItem).Valor.ToString());
                verticalDocumento = Convert.ToInt32(((OpcionCombo)cboorientacion.SelectedItem).Valor.ToString());
                CheckForIllegalCrossThreadCalls = false;

                try
                {
                    BarcodeLib.Barcode etiqueta = new BarcodeLib.Barcode();
                    var etiquetaImagen = etiqueta.Encode(((BarcodeLib.TYPE)valorCodigo), txtcodigo.Text.Trim(), Color.Black, Color.White, 400, 100);
                }
                catch (Exception err)
                {
                    error = true;
                    MessageBox.Show("Ocurrió un problema\nMayor Detalle:\n" + err.Message + "\n\n*Si muestra en ingles, proceda a traducirlo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (!error)
                {
                    SaveFileDialog savefile = new SaveFileDialog();
                    savefile.FileName = string.Format("{0}.pdf", txtcodigo.Text.Trim());
                    savefile.Filter = "Pdf Files|*.pdf";

                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        rutaguardado = savefile.FileName;
                        backgroundWorker1.RunWorkerAsync();
                    }
                }


            }
        }

        private int totalFilas()
        {

            int numeroEtiquetas = Convert.ToInt32(txtnumeroetiquetas.Value.ToString());
            int numeroColumna = 1;
            int numeroFila = 1;

            for (int i = 1; i <= numeroEtiquetas; i++)
            {
                if (numeroColumna == 3)
                {
                    numeroFila++;
                    numeroColumna = 1;
                }
                else
                {
                    numeroColumna++;
                }
            }

            return numeroFila;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (FileStream stream = new FileStream(rutaguardado, FileMode.Create))
            {
                int valor = valorCodigo;
                int vertical = verticalDocumento;
                int widthImage = vertical == 0 ? 170 : 230;
                int HeightImage = vertical == 0 ? 80 : 110;
                //string descripcionProducto = chkmostrardescripcion.Checked ? $"{txtnombre.Text.Trim()} - {txtdescripcion.Text.Trim()}" : "";
                string descripcionProducto = chkmostrardescripcion.Checked ? $"$ {txtPrecioVENTA.Text.Trim()}\n {txtnombre.Text.Trim()} - {txtdescripcion.Text.Trim()}" : "";



                int descripcionSpacingAfter = chkmostrardescripcion.Checked ? 5 : 8;
                BaseColor descripcionBaseColor = chkmostrardescripcion.Checked ? BaseColor.BLACK : BaseColor.WHITE;
                iTextSharp.text.Rectangle orientacionDocumento = vertical == 0 ? PageSize.A4 : PageSize.A4.Rotate();
                float sizeFont = vertical == 0 ? 16 : 14;



                //CONFIGURACION DE ETIQUETA
                BarcodeLib.Barcode etiqueta = new BarcodeLib.Barcode();
                etiqueta.IncludeLabel = chkmostrarcodigo.Checked;
                etiqueta.AlternateLabel = txtcodigo.Text.Trim();
                etiqueta.LabelPosition = LabelPositions.BOTTOMCENTER;
                etiqueta.LabelFont = new System.Drawing.Font(FontFamily.GenericMonospace, sizeFont, FontStyle.Regular);
                etiqueta.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
                var etiquetaImagen = etiqueta.Encode(((BarcodeLib.TYPE)valor), txtcodigo.Text.Trim(), Color.Black, Color.White, 400, 100);

                //CONFIGURACION DE DOCUMENTO
                Document pdfDoc = new Document(orientacionDocumento, 15, 15, 15, 15);

                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                PdfPTable table = new PdfPTable(3);
                table.WidthPercentage = 100;

                int numeroEtiquetas = Convert.ToInt32(txtnumeroetiquetas.Value.ToString());
                int numeroEtiquetasOrigen = numeroEtiquetas;
                numeroEtiquetas = (numeroEtiquetas % 3) > 0 ? (3 * totalFilas()) : numeroEtiquetas;


                for (int i = 1; i <= numeroEtiquetas; i++)
                {
                    PdfPCell celda = new PdfPCell();

                    if (i > numeroEtiquetasOrigen)
                    {
                        celda.AddElement(new Paragraph(""));
                    }
                    else
                    {
                        Paragraph para = new Paragraph();
                        para.Alignment = Element.ALIGN_CENTER;
                        para.Font = FontFactory.GetFont("Webdings", 10, iTextSharp.text.Font.NORMAL, descripcionBaseColor);
                        para.Add(descripcionProducto);
                        para.SpacingAfter = descripcionSpacingAfter;
                        celda.AddElement(para);


                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(etiquetaImagen, System.Drawing.Imaging.ImageFormat.Png);
                        img.ScaleToFit(widthImage, HeightImage);
                        img.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                        if (!chkmostrarcodigo.Checked)
                            img.SpacingAfter = 7;

                        celda.AddElement(img);
                        celda.HorizontalAlignment = Element.ALIGN_CENTER;

                        backgroundWorker1.ReportProgress(i);
                    }

                    table.AddCell(celda);
                }

                pdfDoc.Add(table);

                pdfDoc.Close();
                stream.Close();
                MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                progressBar1.Value = 0;
                rutaguardado = "";
                valorCodigo = -1;
                verticalDocumento = -1;

            }
        }



        private void btnodProducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProductoGBarra())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidProducto.Text = modal._Producto.IdProducto.ToString();
                    txtcodigo.Text = modal._Producto.Codigo;
                    txtnombre.Text = modal._Producto.Nombre;
                    txtdescripcion.Text = modal._Producto.Descripcion;
                    txtPrecioVENTA.Text = modal._Producto.PrecioVenta.ToString();
                }
                else
                {
                    txtcodigo.Select();
                }

            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}






