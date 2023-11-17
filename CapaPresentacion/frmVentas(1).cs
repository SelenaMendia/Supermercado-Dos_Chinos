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
    public partial class frmVentas : Form
    {
        private Usuario _Usuario;

        public frmVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            cbotipodocumento.DisplayMember = "Texto";
            cbotipodocumento.ValueMember = "Valor";
            cbotipodocumento.SelectedIndex = 0;

            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtidProducto.Text = "0";

            txtReciboPago.Text = "";
            txtCambio.Text = "";
            txtTotalPagar.Text = "0";


        }

        private void btnClientebuscar_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtdcoCliente.Text = modal._Cliente.Documento;
                    txtNombreCliente.Text = modal._Cliente.NombreCompleto;
                    txtTelfCliente.Text = modal._Cliente.Telefono;
                    txtCodProducto.Select();
                }
                else
                {
                    txtdcoCliente.Select();
                }

            }
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            // Crear una instancia de frmClientes
            frmClientes frmClientesForm = new frmClientes();

            // Mostrar el formulario frmClientes
            frmClientesForm.Show();
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
                    txtPrecio.Text = modal._Producto.PrecioVenta.ToString("0.00");
                    txtStock.Text = modal._Producto.Stock.ToString();
                    txtcantidad.Select();
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
                    txtPrecio.Text = oProducto.PrecioVenta.ToString("0.00");
                    txtStock.Text = oProducto.Stock.ToString();
                    txtcantidad.Select();
                }
                else
                {
                    txtCodProducto.BackColor = Color.MistyRose;
                    txtidProducto.Text = "0";
                    txtNombreProducto.Text = "";
                    txtPrecio.Text = "";
                    txtStock.Text = "";
                    txtcantidad.Value = 1;
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool producto_existe = false;

            if (int.Parse(txtidProducto.Text) == 0)
            {
                MessageBox.Show("Debe de seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecio.Select();
                return;
            }

            if (Convert.ToInt32(txtStock.Text) < Convert.ToInt32(txtcantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor al stock actual", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

                //if (fila.Cells["IdProducto"].Value.ToString() == txtidProducto.Text)
                //{
                //    producto_existe = true;
                //    break;
                //}
            }

            if (!producto_existe)
            {

                bool respuesta = new CN_Venta().RestarStock(
                    Convert.ToInt32(txtidProducto.Text),
                    Convert.ToInt32(txtcantidad.Value.ToString())
                    );

                if (respuesta)
                {
                    dataGridView1.Rows.Add(new object[] {
                        txtidProducto.Text,
                        txtNombreProducto.Text,
                        precio.ToString("0.00"),
                        txtcantidad.Value.ToString(),
                        (txtcantidad.Value * precio).ToString("0.00")
                    });

                    calcularTotal();
                    limpiarProducto();
                    txtCodProducto.Select();
                }
            }

        }

        private void calcularTotal()
        {
            decimal total = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    //total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());

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
            txtTotalPagar.Text = total.ToString("0.00");
        }

        private void limpiarProducto()
        {
            txtidProducto.Text = "0";
            txtCodProducto.Text = "";
            txtNombreProducto.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtcantidad.Value = 1;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 5)
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
                int index = e.RowIndex;
                if (index >= 0)
                {
                    bool respuesta = new CN_Venta().SumarStock(
                        Convert.ToInt32(dataGridView1.Rows[index].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dataGridView1.Rows[index].Cells["Cantidad"].Value.ToString()));

                    if (respuesta)
                    {
                        dataGridView1.Rows.RemoveAt(index);
                        calcularTotal();
                    }

                }
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPrecio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
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

        //permite que solo ingrese los numeros y nada mas el coma 

        private void txtReciboPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtReciboPago.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
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

        private void calcularCambio()
        {

            if (txtTotalPagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            decimal pagacon;
            decimal total = Convert.ToDecimal(txtTotalPagar.Text);

            if (txtReciboPago.Text.Trim() == "")
            {
                txtReciboPago.Text = "0";
            }

            if (decimal.TryParse(txtReciboPago.Text.Trim(), out pagacon))
            {

                if (pagacon < total)
                {
                    txtCambio.Text = "0.00";
                }
                else
                {
                    decimal cambio = pagacon - total;
                    txtCambio.Text = cambio.ToString("0.00");

                }
            }
        }

        private void txtReciboPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                calcularCambio();
            }
        }

        private void btnregistrarVenta_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable detalle_venta = new DataTable();

            detalle_venta.Columns.Add("IdProducto", typeof(int));
           // detalle_venta.Columns.Add("Producto", typeof(int));
            detalle_venta.Columns.Add("Precio", typeof(decimal));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("SubTotal", typeof(decimal));


            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{ 
            //    detalle_venta.Rows.Add(new object[] {
            //        row.Cells["IdProducto"].Value.ToString(),
            //        //row.Cells["Producto"].Value.ToString(),
            //        row.Cells["Precio"].Value.ToString(),
            //        row.Cells["Cantidad"].Value.ToString(),
            //        row.Cells["SubTotal"].Value.ToString()
            //    });
            //}

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["IdProducto"].Value != null &&
                    row.Cells["Precio"].Value != null &&
                    row.Cells["Cantidad"].Value != null &&
                    row.Cells["SubTotal"].Value != null)
                {
                    detalle_venta.Rows.Add(new object[] {
                    row.Cells["IdProducto"].Value.ToString(),
                    row.Cells["Precio"].Value.ToString(),
                    row.Cells["Cantidad"].Value.ToString(),
                    row.Cells["SubTotal"].Value.ToString()

                    });
                }
            }


            int idcorrelativo = new CN_Venta().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idcorrelativo); //formato de mi ticket el numero 
            calcularCambio();

            Venta oVenta = new Venta()
            {

                oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                TipoDocumento = ((OpcionCombo)cbotipodocumento.SelectedItem).Texto,
                NumeroDocumento = numeroDocumento,
                DocumentoCliente = txtdcoCliente.Text,
                NombreCliente = txtNombreCliente.Text,
                MontoPago = Convert.ToDecimal(txtReciboPago.Text),
                MontoCambio = Convert.ToDecimal(txtCambio.Text),
                MontoTotal = Convert.ToDecimal(txtTotalPagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Numero de venta generada:\n" + numeroDocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    Clipboard.SetText(numeroDocumento);

                txtdcoCliente.Text = "";
                txtNombreCliente.Text = "";
                dataGridView1.Rows.Clear();
                calcularTotal();
                txtReciboPago.Text = "";
                txtCambio.Text = "";
            }
            else
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);



        }
    }
    
}
