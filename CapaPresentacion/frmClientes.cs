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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            //para que me muestre activo y no activo de estado
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;



            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }

            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;


            List<Cliente> listaCliente = new CN_Cliente().Listar();

            foreach (Cliente item in listaCliente)
            {
                dgvdata.Rows.Add(new object[] {"",item.IdCliente,item.Documento,item.NombreCompleto,item.Correo, item.Telefono,
                                            item.Estado == true ? 1 : 0,
                                            item.Estado == true ? "Activo" : "No Activo"

                });

            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Cliente objcliente = new Cliente()
            {

                Documento = textdocumento.Text,
                NombreCompleto = textnombrecompleto.Text,
                Correo = textcorreo.Text,
                Telefono = texttelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1 ? true : false
            };

            if (objcliente.IdCliente == 0)
            {


                int idclientegenerado = new CN_Cliente().Registrar(objcliente, out mensaje);

                if (idclientegenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {"",idclientegenerado,textdocumento.Text,textnombrecompleto.Text,textcorreo.Text,texttelefono.Text,
                                            ((OpcionCombo)cboestado.SelectedItem).Valor.ToString(),
                                            ((OpcionCombo)cboestado.SelectedItem).Texto.ToString(),
            });

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }



            Limpiar();
        }

        private void Limpiar()
        {
            txtindice.Text = "-1";
            txtid.Text = "";
            textdocumento.Text = "";
            textnombrecompleto.Text = "";
            textcorreo.Text = "";
            texttelefono.Text = "";
            cboestado.SelectedIndex = 0;

            textdocumento.Select();

        }


        private void dgvdata_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var w = Properties.Resources.circle_check_solid.Width;
                var h = Properties.Resources.circle_check_solid.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;// pone al medio la imagen
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.circle_check_solid, new Rectangle(x, y, h, w));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["id"].Value.ToString();
                    textdocumento.Text = dgvdata.Rows[indice].Cells["Documento"].Value.ToString();
                    textnombrecompleto.Text = dgvdata.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    textcorreo.Text = dgvdata.Rows[indice].Cells["Correo"].Value.ToString();
                    texttelefono.Text = dgvdata.Rows[indice].Cells["Telefono"].Value.ToString();


                    foreach (OpcionCombo oc in cboestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_cboestado = cboestado.Items.IndexOf(oc);
                            cboestado.SelectedIndex = indice_cboestado;
                            break;
                        }
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Cliente objcliente = new Cliente()
            {
                IdCliente = Convert.ToInt32(txtid.Text),
                Documento = textdocumento.Text,
                NombreCompleto = textnombrecompleto.Text,
                Correo = textcorreo.Text,
                Telefono = texttelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1 ? true : false
            };

            bool resultado = new CN_Cliente().Editar(objcliente, out mensaje);


            if (resultado)
            {
                DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                row.Cells["Id"].Value = txtid.Text;
                row.Cells["Documento"].Value = textdocumento.Text;
                row.Cells["NombreCompleto"].Value = textnombrecompleto.Text;
                row.Cells["Correo"].Value = textcorreo.Text;
                row.Cells["Telefono"].Value = texttelefono.Text;
                row.Cells["EstadoValor"].Value = ((OpcionCombo)cboestado.SelectedItem).Valor.ToString();
                row.Cells["Estado"].Value = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString();


                Limpiar();
            }
            else
            {
                MessageBox.Show(mensaje);
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el cliente ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    Cliente objcliente = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtid.Text),

                    };

                    bool resultado = new CN_Cliente().Eliminar(objcliente, out mensaje);

                    if (resultado)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvdata.Columns)
                {
                    if (columna.HeaderText != "" && columna.Visible)
                        dt.Columns.Add(columna.HeaderText, typeof(string));
                }
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Visible)
                        dt.Rows.Add(new object[] {
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            row.Cells[7].Value.ToString(),

                        });
                }

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("Clientes-{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {

                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(savefile.FileName);
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
