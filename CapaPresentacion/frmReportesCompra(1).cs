using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;

namespace CapaPresentacion
{
    public partial class frmReportesCompra : Form
    {
        public frmReportesCompra()
        {
            InitializeComponent();
        }


        private void frmReportesCompra_Load(object sender, EventArgs e)
        {
            List<Proveedor> lista = new CN_Proveedor().Listar();

            cbobusquedaProveedor.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Todos" });
            foreach (Proveedor item in lista)
            {
                cbobusquedaProveedor.Items.Add(new OpcionCombo() { Valor = item.IdProveedor, Texto = item.RazonSocial });
            }
            cbobusquedaProveedor.DisplayMember = "Texto";
            cbobusquedaProveedor.ValueMember = "Valor";
            cbobusquedaProveedor.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                cboBuscarPlanillaReporteCompra.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cboBuscarPlanillaReporteCompra.DisplayMember = "Texto";
            cboBuscarPlanillaReporteCompra.ValueMember = "Valor";
            cboBuscarPlanillaReporteCompra.SelectedIndex = 0;

        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            int idproveedor = Convert.ToInt32(((OpcionCombo)cbobusquedaProveedor.SelectedItem).Valor.ToString());

            List<Reporte_Compra> lista = new List<Reporte_Compra>();

            lista = new CN_Reportes().Compra(
                txtFechaInicio.Value.ToString(),
                txtFechaFin.Value.ToString(),
                idproveedor
                );


            dataGridView1.Rows.Clear();

            foreach (Reporte_Compra rc in lista)
            {
                dataGridView1.Rows.Add(new object[] {
                    rc.FechaRegistro,
                    rc.TipoDocumento,
                    rc.NumeroDocumento,
                    rc.MontoTotal,
                    rc.UsuarioRegistro,
                    rc.DocumentoProveedor,
                    rc.RazonSocial,
                    rc.CodigoProducto,
                    rc.NombreProducto,
                    rc.Categoria,
                    rc.PrecioCompra,
                    rc.PrecioVenta,
                    rc.Cantidad,
                    rc.SubTotal
                });

            }


        }

        private void BTNbuscarenDataG_Click(object sender, EventArgs e)
        {
            

            string columnaFiltro = ((OpcionCombo)cboBuscarPlanillaReporteCompra.SelectedItem).Valor.ToString();
            string filtro = txtbusquedaReporteCompra.Text.Trim().ToUpper();

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[columnaFiltro].Value != null)
                    {
                        string valorCelda = row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper();
                        bool mostrarFila = valorCelda.Contains(filtro);

                        if (mostrarFila)
                        {
                            row.Visible = true;
                        }
                        else
                        {
                            row.Visible = false;
                        }
                    }
                   
                }
                // Refresca el DataGridView después de ocultar las filas.
                dataGridView1.Refresh();
            }




        }

        private void BTNlimpiarenDataG_Click(object sender, EventArgs e)
        {
            txtbusquedaReporteCompra.Text = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1) //si no existe registro en la tabla que muestre esta caja de mensaje
            {

                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else //comienza su exportacion

            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dataGridView1.Columns)
                {
                    if (columna.HeaderText != "" && columna.Visible)
                        dt.Columns.Add(columna.HeaderText, typeof(string));
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Visible)
                    {
                        List<string> rowData = new List<string>();

                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            if (row.Cells[i].Value != null)
                            {
                                rowData.Add(row.Cells[i].Value.ToString());
                            }
                            else
                            {
                                rowData.Add(""); 
                            }
                        }

                        dt.Rows.Add(rowData.ToArray());
                    }
                }


                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("Reporte_Compras_Nro-{0}.xlsx", DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"));
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {

                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(savefile.FileName);
                        MessageBox.Show("Reporte Generado exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                        MessageBox.Show("Error al generar reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }

            }
        }
    }
}
