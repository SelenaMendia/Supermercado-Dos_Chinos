using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video;
using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using Emgu.CV;
//nuevo
using Emgu.Util;
using ZXing;


namespace CapaPresentacion
{
    public partial class frmVentas : Form
    {

        //nuevo
        private Mat Frame;

        private VideoCapture Camara;
        private BarcodeReader Reader;

        List<Producto> Articulos;

        //camara
        private bool Dipositivos;

        private FilterInfoCollection MiDispositivos;

        private VideoCaptureDevice MiWebCam;
        //---------


        private Usuario _Usuario;

        public frmVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
            Articulos = new List<Producto>(); //nuevo

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

            //nuevo 
            Frame = new Mat();
            Camara = new VideoCapture();
            Reader = new BarcodeReader();

            timer1.Interval = 40;
            timer2.Interval = 100; 

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            txtCodProducto.Text = string.Empty;
            rdbApagar.Checked = true;

            //cargar lista
            Articulos = new CN_Producto().Listar();
            //----

            CargaDispositivos();//camara
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
            // Crea una instancia de frmClientes
            frmClientes frmClientesForm = new frmClientes();

            // Muestra el formulario frmClientes
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





        //nuevo 
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Camara.IsOpened)
            {
                Camara.Read(Frame);
                pictureBox1.Image = Frame.ToBitmap();

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Result resultado;
            if (pictureBox1.Image != null)
            {
                resultado = Reader.Decode((Bitmap)pictureBox1.Image);
                if (resultado != null)
                {
                    txtCodProducto.Text = DetalleArticulo(resultado.Text);
                }
            }
        }

        private string DetalleArticulo(string _codigobarras)
        {

            //desde aca 
            string _detalle = string.Empty;
            if (string.IsNullOrEmpty(_codigobarras)) return _detalle;

            var articulo = Articulos.FirstOrDefault(art => art.Codigo == _codigobarras);
            if (articulo != null)
            {
                _detalle = articulo.Codigo;
                txtCodProducto.BackColor = Color.Honeydew;
                txtidProducto.Text = articulo.IdProducto.ToString();
                txtNombreProducto.Text = articulo.Nombre;
                txtPrecio.Text = articulo.PrecioVenta.ToString("0.00");
                txtStock.Text = articulo.Stock.ToString();
                txtcantidad.Select();
            }

            return _detalle;
            //hasta aca funciona
        }

        private void rdbEncender_CheckedChanged(object sender, EventArgs e)
        {
            //Camara.Start();
            //if (!timer1.Enabled)
            //{
            //    timer1.Enabled = true;
            //}
            if (!timer2.Enabled)
            {
                timer2.Enabled = true;
            }
            timer2.Start();
            CerrarWebCam();
            int i = comboBox1.SelectedIndex;
            string NombreVideo = MiDispositivos[i].MonikerString;
            MiWebCam = new VideoCaptureDevice(NombreVideo);
            MiWebCam.NewFrame += new NewFrameEventHandler(CapturandoImagen);
            MiWebCam.Start();
        }

        private void rdbApagar_CheckedChanged(object sender, EventArgs e)
        {
            timer2.Stop();

            timer2.Enabled = false;
            CerrarWebCam();
        }

        public void CerrarWebCam()
        {
            if (MiWebCam != null && MiWebCam.IsRunning)
            {
                MiWebCam.SignalToStop();
                MiWebCam = null;
            }
            pictureBox1.Image = null;

            
        }

        private void CapturandoImagen(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = Imagen;
            
        }

        public void CargaDispositivos()
        {

            MiDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (MiDispositivos.Count > 0)
            {
                Dipositivos = true;

                foreach (FilterInfo dispositivo in MiDispositivos)
                {
                    comboBox1.Items.Add(dispositivo.Name); // Agrega el nombre del dispositivo
                }

                comboBox1.SelectedIndex = 0; // Selecciona el primer dispositivo por defecto
            }
            else
            {
                Dipositivos = false;
            }
        }

        private void txtdcoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                Cliente oCliente = new CN_Cliente().Listar().Where(p => p.Documento == txtdcoCliente.Text && p.Estado == true).FirstOrDefault();

                if (oCliente != null)
                {
                    txtdcoCliente.BackColor = Color.Honeydew;
                    txtidCliente.Text = oCliente.IdCliente.ToString();
                    txtNombreCliente.Text = oCliente.NombreCompleto;
                    txtTelfCliente.Text = oCliente.Telefono;

                }
                else
                {
                    txtdcoCliente.BackColor = Color.MistyRose;
                    txtidProducto.Text = "0";
                    txtNombreCliente.Text = "";
                    txtTelfCliente.Text = "";

                }
            }
        }

    }

}
