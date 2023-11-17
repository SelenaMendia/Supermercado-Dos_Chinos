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
using FontAwesome.Sharp;

namespace CapaPresentacion
{
    public partial class Inicio : Form

    {
        private static Usuario usuarioActual;
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;



        public Inicio(Usuario objusuario = null)
        {

            if (objusuario == null)
                usuarioActual = new Usuario() { NombreCompleto = "ADMIN PREDEFINIDO", IdUsuario = 1 };
            else
                usuarioActual = objusuario; 

            InitializeComponent();

        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> listaPermisos = new CN_Permiso().Listar(usuarioActual.IdUsuario);//devuelve la lista de permiso del usuario

            foreach (IconMenuItem iconmenu in menu.Items) //recorro los menus
            {
                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                if (encontrado == false)
                {
                    iconmenu.Visible = false;
                }
            }


            lblusuario.Text = usuarioActual.NombreCompleto;

            
            timer1.Start();

            menuincio.PerformClick();
        }

        //mostrar formulario cada icon 

        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null)
            {
                //MenuActivo.BackColor = Color.White;
                MenuActivo.BackColor = Color.FromArgb(235, 0, 0);

                MenuActivo.ForeColor = Color.White;
            }
            menu.BackColor = Color.Silver;//

            menu.ForeColor = Color.Black;

            MenuActivo = menu;

            //mostrar formulario
            if (FormularioActivo != null)
            {
                FormularioActivo.Close();

            }
            
            //genero formulario
            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;//toma todo el espacio del contenedor
            formulario.BackColor = Color.Black; //color del sistema
            //agrego el formulario al contenedor
            contenedor.Controls.Add(formulario);
            formulario.Show();//mostrarse el formulario

            
            
        }

        private void menuincio_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmInicio());
        }

        private void menuusuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios());
        }

        private void submenucategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmCategoria());
        }

        private void submenuproducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmProducto());
        }

        private void submenuregistrarventa_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmVentas(usuarioActual));
        }

        private void subemenuverdetalleventa_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmDetalleVenta());
        }

        private void submenuregistrarcompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmCompras(usuarioActual));
        }

        private void submenuverdetallecompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmDetalleCompra());
        }

        private void menuclientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes());
        }

        private void menuproveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores());
        }

        private void submenuNegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmNegocio());
        }

        private void submenuReportesCompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmReportesCompra());
        }

        private void submenuReportesVentas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmReportesVenta());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblhora.Text = DateTime.Now.ToString("HH:mm:ss");

            lbldia.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
    }
}
