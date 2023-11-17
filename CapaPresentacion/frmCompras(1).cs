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
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;

namespace CapaPresentacion
{
    public partial class frmCompras : Form
    {
        private Usuario _Usuario;

        public frmCompras(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            cbotipodocumento.DisplayMember = "Texto";
            cbotipodocumento.ValueMember = "Valor";
            cbotipodocumento.SelectedIndex = 0;

            textfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtidProveedor.Text = "0";
            txtidProducto.Text = "0";
        }

        private void btnproveedor_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidProveedor.Text = modal._Proveedor.IdProveedor.ToString();
                    txtdcoProveedor.Text = modal._Proveedor.Documento;
                    txtRazSocProveedor.Text = modal._Proveedor.RazonSocial;
                }
                else
                {
                    txtdcoProveedor.Select();
                }

            }
        }

        private void btnodProducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidProducto.Text = modal._Producto.IdProducto.ToString();
                    txtCodProducto.Text = modal._Producto.Codigo;
                    txtNombreProducto.Text = modal._Producto.Nombre;
                    txtprecioCompra.Select();
                }
                else
                {
                    txtCodProducto.Select();
                }

            }
        }

        private void txtCodProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCodProducto.Text && p.Estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtCodProducto.BackColor = Color.Honeydew;
                    txtidProducto.Text = oProducto.IdProducto.ToString();
                    txtNombreProducto.Text = oProducto.Nombre;
                    txtprecioCompra.Select();
                }
                else
                {
                    txtCodProducto.BackColor = Color.MistyRose;
                    txtidProducto.Text = "0";
                    txtNombreProducto.Text = "";
                }
            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal preciocompra = 0;
            decimal precioventa = 0;
            bool producto_existe = false;

            if (int.Parse(txtidProducto.Text) == 0)
            {
                MessageBox.Show("Seleccione un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtprecioCompra.Text, out preciocompra))
            {
                MessageBox.Show("Precio Compra - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecioCompra.Select();
                return;
            }

            if (!decimal.TryParse(txtprecioVenta.Text, out precioventa))
            {
                MessageBox.Show("Precio Venta - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecioVenta.Select();
                return;
            }


            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {


                var cell = fila.Cells["IdProducto"];
                if (cell != null && cell.Value != null && cell.Value.ToString() == txtidProducto.Text)
                {
                    producto_existe = true;
                    break;
                }
            }

            if (!producto_existe)
            {

                dataGridView1.Rows.Add(new object[] {
                    txtidProducto.Text,
                    txtNombreProducto.Text,
                    preciocompra.ToString("0.00"),
                    precioventa.ToString("0.00"),
                    txtcantidad.Value.ToString(),
                    (txtcantidad.Value * preciocompra).ToString("0.00")

                });


                LimpiarProducto();
                CalcularTotal();
                txtCodProducto.Select();

            }


        }

        private void LimpiarProducto()
        {
            txtidProducto.Text = "0";
            txtCodProducto.Text = "";
            txtCodProducto.BackColor = Color.White;
            txtNombreProducto.Text = "";
            txtprecioCompra.Text = "";
            txtprecioVenta.Text = "";
            txtcantidad.Value = 1;
        }


        private void CalcularTotal()
        {
            decimal total = 0;

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Verificar que la celda "SubTotal" no sea nula y sea convertible a decimal
                    if (row.Cells["SubTotal"].Value != null)
                    {
                        if (decimal.TryParse(row.Cells["SubTotal"].Value.ToString(), out decimal subtotal))
                        {
                            total += subtotal;
                        }
                        else
                        {
                            MessageBox.Show("El valor en la celda 'SubTotal' no es un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }

            txtTotalPagar.Text = total.ToString("0.00");
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 6)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.trash_can_solid.Width;
                var h = Properties.Resources.trash_can_solid.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.trash_can_solid, new Rectangle(x, y, w, h));
                e.Handled = true;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btneliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    dataGridView1.Rows.RemoveAt(indice);
                    CalcularTotal();
                }
            }


            
        }

        private void txtprecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtprecioCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ",")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtprecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtprecioVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ",")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void btnregistrarCompra_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtidProveedor.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //DataTable detalle_compra = new DataTable();

            //detalle_compra.Columns.Add("IdProducto", typeof(int));
            //detalle_compra.Columns.Add("PrecioCompra", typeof(decimal));
            //detalle_compra.Columns.Add("PrecioVenta", typeof(decimal));
            //detalle_compra.Columns.Add("Cantidad", typeof(int));
            //detalle_compra.Columns.Add("MontoTotal", typeof(decimal));

            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    detalle_compra.Rows.Add(
            //        new object[] {
            //           Convert.ToInt32(row.Cells["IdProducto"].Value.ToString()),
            //           row.Cells["PrecioCompra"].Value.ToString(),
            //           row.Cells["PrecioVenta"].Value.ToString(),
            //           row.Cells["Cantidad"].Value.ToString(),
            //           row.Cells["SubTotal"].Value.ToString()
            //        });

            //}
            //----------------------------------------------------------------------------
            //    DataTable detalle_compra = new DataTable();

            //    detalle_compra.Columns.Add("IdProducto", typeof(int));
            //    detalle_compra.Columns.Add("PrecioCompra", typeof(decimal));
            //    detalle_compra.Columns.Add("PrecioVenta", typeof(decimal));
            //    detalle_compra.Columns.Add("Cantidad", typeof(int));
            //    detalle_compra.Columns.Add("MontoTotal", typeof(decimal));

            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        int idProducto = 0;
            //        decimal precioCompra = 0;
            //        decimal precioVenta = 0;
            //        int cantidad = 0;
            //        decimal subTotal = 0;

            //        if (row.Cells["IdProducto"].Value != null &&
            //row.Cells["PrecioCompra"].Value != null &&
            //row.Cells["PrecioVenta"].Value != null &&
            //row.Cells["Cantidad"].Value != null &&
            //row.Cells["SubTotal"].Value != null)

            //        {
            //            if (int.TryParse(row.Cells["IdProducto"].Value.ToString(), out idProducto) &&
            //                decimal.TryParse(row.Cells["PrecioCompra"].Value.ToString(), out precioCompra) &&
            //                decimal.TryParse(row.Cells["PrecioVenta"].Value.ToString(), out precioVenta) &&
            //                int.TryParse(row.Cells["Cantidad"].Value.ToString(), out cantidad) &&
            //                decimal.TryParse(row.Cells["SubTotal"].Value.ToString(), out subTotal))
            //            {
            //                detalle_compra.Rows.Add(
            //                    new object[] {
            //           idProducto,
            //           precioCompra,
            //           precioVenta,
            //           cantidad,
            //           subTotal
            //                    });
            //            }
            //            else
            //            {
            //                // Manejar el error de conversión
            //                MessageBox.Show("Error de conversión en la fila: " + (row.Index + 1), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                return;  // o continue; dependiendo de cómo desees manejarlo
            //            }
            //        }

            DataTable detalle_compra = new DataTable();

            detalle_compra.Columns.Add("IdProducto", typeof(int));
            detalle_compra.Columns.Add("PrecioCompra", typeof(decimal));
            detalle_compra.Columns.Add("PrecioVenta", typeof(decimal));
            detalle_compra.Columns.Add("Cantidad", typeof(int));
            detalle_compra.Columns.Add("MontoTotal", typeof(decimal));

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int idProducto = 0;
                decimal precioCompra = 0;
                decimal precioVenta = 0;
                int cantidad = 0;
                decimal montoTotal = 0;

                if (row.Cells["IdProducto"].Value != null &&
                    row.Cells["PrecioCompra"].Value != null &&
                    row.Cells["PrecioVenta"].Value != null &&
                    row.Cells["Cantidad"].Value != null)
                {
                    if (int.TryParse(row.Cells["IdProducto"].Value.ToString(), out idProducto) &&
                        decimal.TryParse(row.Cells["PrecioCompra"].Value.ToString(), out precioCompra) &&
                        decimal.TryParse(row.Cells["PrecioVenta"].Value.ToString(), out precioVenta) &&
                        int.TryParse(row.Cells["Cantidad"].Value.ToString(), out cantidad))
                    {
                        montoTotal = precioCompra * cantidad; // Calcular el monto total para este detalle
                        detalle_compra.Rows.Add(idProducto, precioCompra, precioVenta, cantidad, montoTotal);
                    }
                    else
                    {
                        MessageBox.Show("Error de conversión en la fila: " + (row.Index + 1), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            //-------------------------------------------------------------------------
            int idcorrelativo = new CN_Compra().ObtenerCorrelativo();
                string numerodocumento = string.Format("{0:00000}", idcorrelativo);

                Compra oCompra = new Compra()
                {
                    oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                    oProveedor = new Proveedor() { IdProveedor = Convert.ToInt32(txtidProveedor.Text) },
                    TipoDocumento = ((OpcionCombo)cbotipodocumento.SelectedItem).Texto,
                    NumeroDocumento = numerodocumento,
                    MontoTotal = Convert.ToDecimal(txtTotalPagar.Text)
                };

                string mensaje = string.Empty;
                bool respuesta = new CN_Compra().Registrar(oCompra, detalle_compra, out mensaje);

                if (respuesta)
                {
                    var result = MessageBox.Show("Numero de compra generada:\n" + numerodocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                        Clipboard.SetText(numerodocumento);

                    txtidProveedor.Text = "0";
                    txtdcoProveedor.Text = "";
                    txtRazSocProveedor.Text = "";
                    dataGridView1.Rows.Clear();
                    CalcularTotal();

                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }

