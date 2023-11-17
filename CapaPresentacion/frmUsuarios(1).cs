using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaPresentacion.Utilidades;

using CapaEntidad;
using CapaNegocio;
using System.IO;
using System.Drawing.Imaging;
using DocumentFormat.OpenXml.Spreadsheet;
using FontAwesome.Sharp;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {


        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            //para que me muestre activo y no activo de estado
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;

            List<Rol> listaRol = new CN_Rol().Listar();

            foreach (Rol item in listaRol)
            {
                cborol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }
            //para que me muestre ADMINISTRADOR y OPERADOR de estado
            cborol.DisplayMember = "Texto";
            cborol.ValueMember = "Valor";
            cborol.SelectedIndex = 0;

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




            //mostrar todo los usuarios en el formulario
            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuario)
            {

                dgvdata.Rows.Add(new object[] {"",item.IdUsuario,item.Documento,item.NombreCompleto,item.Correo,item.Clave, item.Telefono,
                                            item.oRol.IdRol,
                                            item.oRol.Descripcion,
                                            item.Estado == true ? 1 : 0,
                                            item.Estado == true ? "Activo" : "No Activo",

                });


            }


        }


        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Usuario objusuario = new Usuario()
            {
                Documento = textdocumento.Text,
                NombreCompleto = textnombrecompleto.Text,
                Correo = textcorreo.Text,
                Clave = textclave.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cborol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1 ? true : false,
                Telefono = texttelefono.Text,
            };


            if (objusuario.IdUsuario == 0)
            {
                int idusuariogenerado = new CN_Usuario().Registrar(objusuario, out mensaje);

                if (idusuariogenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {"",idusuariogenerado,textdocumento.Text,textnombrecompleto.Text,textcorreo.Text,textclave.Text,texttelefono.Text,
                ((OpcionCombo)cborol.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cborol.SelectedItem).Texto.ToString(),
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
        }

        private void Limpiar()
        {
            txtindice.Text = "-1";
            txtid.Text = "";
            textdocumento.Text = "";
            textnombrecompleto.Text = "";
            textcorreo.Text = "";
            textclave.Text = "";
            textconfirmarclave.Text = "";
            texttelefono.Text = "";
            cborol.SelectedIndex = 0;
            cboestado.SelectedIndex = 0;

            textdocumento.Select();



        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el usuario ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    Usuario objusuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtid.Text),

                    };

                    bool resultado = new CN_Usuario().Eliminar(objusuario, out mensaje);

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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Usuario objusuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(txtid.Text),
                Documento = textdocumento.Text,
                NombreCompleto = textnombrecompleto.Text,
                Correo = textcorreo.Text,
                Clave = textclave.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cborol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1 ? true : false,
                Telefono = texttelefono.Text,
 
            };

            
            bool resultado = new CN_Usuario().Editar(objusuario, out mensaje);

            if (resultado)
            {
                DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                row.Cells["Id"].Value = txtid.Text;
                row.Cells["Documento"].Value = textdocumento.Text;
                row.Cells["NombreCompleto"].Value = textnombrecompleto.Text;
                row.Cells["Correo"].Value = textcorreo.Text;
                row.Cells["Clave"].Value = textclave.Text;
                row.Cells["IdRol"].Value = ((OpcionCombo)cborol.SelectedItem).Valor.ToString();
                row.Cells["Rol"].Value = ((OpcionCombo)cborol.SelectedItem).Texto.ToString();
                row.Cells["EstadoValor"].Value = ((OpcionCombo)cboestado.SelectedItem).Valor.ToString();
                row.Cells["Estado"].Value = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString();
                row.Cells["Telefono"].Value = texttelefono.Text;           

                Limpiar();
            }
            else
            {
                MessageBox.Show(mensaje);
            }
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.circle_check_solid.Width;
                var h = Properties.Resources.circle_check_solid.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.circle_check_solid, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {

                int indice = e.RowIndex;

                if (indice >= 0)
                {

                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
                    textdocumento.Text = dgvdata.Rows[indice].Cells["Documento"].Value.ToString();
                    textnombrecompleto.Text = dgvdata.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    textcorreo.Text = dgvdata.Rows[indice].Cells["Correo"].Value.ToString();
                    textclave.Text = dgvdata.Rows[indice].Cells["Clave"].Value.ToString();
                    textconfirmarclave.Text = dgvdata.Rows[indice].Cells["Clave"].Value.ToString();
                    texttelefono.Text = dgvdata.Rows[indice].Cells["Telefono"].Value.ToString();



                    foreach (OpcionCombo oc in cborol.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indice_combo = cborol.Items.IndexOf(oc);
                            cborol.SelectedIndex = indice_combo;
                            break;
                        }
                    }


                    foreach (OpcionCombo oc in cboestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboestado.Items.IndexOf(oc);
                            cboestado.SelectedIndex = indice_combo;
                            break;
                        }
                    }


                }
            }  
        }
    }
}



 