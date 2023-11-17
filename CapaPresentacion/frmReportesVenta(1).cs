using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;

namespace CapaPresentacion
{
    public partial class frmReportesVenta : Form
    {
        public frmReportesVenta()
        {
            InitializeComponent();
        }

        private void frmReportesVenta_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                cboBuscarPlanillaReporteVenta.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cboBuscarPlanillaReporteVenta.DisplayMember = "Texto";
            cboBuscarPlanillaReporteVenta.ValueMember = "Valor";
            cboBuscarPlanillaReporteVenta.SelectedIndex = 0;
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            List<Reporte_Venta> lista = new List<Reporte_Venta>();

            lista = new CN_Reportes().Venta(
                txtFechaInicio.Value.ToString(),
                txtFechaFin.Value.ToString()
                );

            dataGridView1.Rows.Clear();

            foreach (Reporte_Venta rv in lista)
            {
                dataGridView1.Rows.Add(new object[] {
                    rv.FechaRegistro,
                    rv.TipoDocumento,
                    rv.NumeroDocumento,
                    rv.MontoTotal,
                    rv.UsuarioRegistro,
                    rv.DocumentoCliente,
                    rv.NombreCliente,
                    rv.CodigoProducto,
                    rv.NombreProducto,
                    rv.Categoria,
                    rv.PrecioVenta,
                    rv.Cantidad,
                    rv.SubTotal
                });
            }

        }

        private void BTNbuscarenDataG_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBuscarPlanillaReporteVenta.SelectedItem).Valor.ToString();
            string filtro = txtbusquedaReporteVenta.Text.Trim().ToUpper();

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
            txtbusquedaReporteVenta.Text = "";
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
                savefile.FileName = string.Format("Reporte_Venta_Nro-{0}.xlsx", DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"));
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
