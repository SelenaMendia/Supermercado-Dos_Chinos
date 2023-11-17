using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace CapaPresentacion
{
    public partial class frmDetalleCompra : Form
    {
        public frmDetalleCompra()
        {
            InitializeComponent();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            Compra oCompra = new CN_Compra().ObtenerCompra(txtnroDocumentobuscar.Text);

            if (oCompra.IdCompra != 0)
            {

                txtnroDocumento.Text = oCompra.NumeroDocumento;

                txtfecha.Text = oCompra.FechaRegistro;
                txtTipoDocuemento.Text = oCompra.TipoDocumento;
                txtUsuario.Text = oCompra.oUsuario.NombreCompleto;

                txtnroDocumentoProveedor.Text = oCompra.oProveedor.Documento;
                txtRazonSocial.Text = oCompra.oProveedor.RazonSocial;

                dataGridView1.Rows.Clear();
                //muestra y recorre la lista
                foreach (Detalle_Compra dc in oCompra.oDetalle_Compra)
                {
                    dataGridView1.Rows.Add(new object[] { dc.oProducto.Nombre, dc.PrecioCompra, dc.Cantidad, dc.MontoTotal });
                }

                txtTotal.Text = oCompra.MontoTotal.ToString("0.00");

            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtfecha.Text = "";
            txtTipoDocuemento.Text = "";
            txtUsuario.Text = "";
            txtnroDocumentoProveedor.Text = "";
            txtRazonSocial.Text = "";

            dataGridView1.Rows.Clear();
            txtTotal.Text = "0.00";
        }

        private void btnDescargarPDF_Click(object sender, EventArgs e)
        {
            if (txtTipoDocuemento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados de una compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_Html = Properties.Resources.PlantillaCompra.ToString(); //primero agregue la pantilla compra
            Negocio odatos = new CN_Negocio().ObtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", odatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", odatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", odatos.Direccion);
            Texto_Html = Texto_Html.Replace("@telnegocio", odatos.Telefono);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocuemento.Text.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtnroDocumento.Text);


            Texto_Html = Texto_Html.Replace("@docproveedor", txtnroDocumentoProveedor.Text);
            Texto_Html = Texto_Html.Replace("@nombreproveedor", txtRazonSocial.Text);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtfecha.Text);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuario.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dataGridView1.Rows) //recorro todas mi filas del DataGridView
            {
                if (!row.IsNewRow)
                {
                    filas += "<tr>";
                    filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                    filas += "</tr>";
                }
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", txtTotal.Text);

            //gaurdar documento y nos abre la ventana para decidir donde guardar el pdf
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Compra_{0}.pdf", txtnroDocumento.Text);
            savefile.Filter = "Pdf Files|*.pdf";

            //si hemos elegido una ruta correcta 
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    //Tamaño del documento  y es el using itextsharp.text
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    //using itextsharp.text.pdf
                    PdfWriter escribir = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);

                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(escribir, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Documento se ha generado exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
