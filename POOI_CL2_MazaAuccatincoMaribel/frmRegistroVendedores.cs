using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Json;


namespace POOI_CL2_MazaAuccatincoMaribel
{
    public partial class frmRegistroVendedores : Form
    {
        int n;
        ArrayList aVendedores = new ArrayList();
        public frmRegistroVendedores()
        {
            InitializeComponent();
            llenarCboDivision();
            generarCodigo();
        }

        void llenarCboDivision()
        {
            cboDivision.Items.Add("Electrodométicos");
            cboDivision.Items.Add("Linea Blanca");
            cboDivision.Items.Add("Juguetes");
            cboDivision.Items.Add("Tecnología");
            cboDivision.Items.Add("Muebles");
            cboDivision.Items.Add("Peluches");


        }
        void generarCodigo()
        {
            n++;
            txtCodigo.Text = n.ToString("0000");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Obtener los valores del formulario
            // Instanciamos de la clase Vendedor
            Vendedor objV = new Vendedor()
            {
                cod_vend = int.Parse(txtCodigo.Text),
                nombre_vend = txtNombre.Text,
                direcc_vend = txtDireccion.Text,
                fecContrato_vend = dtFechaContrato.Value,
                div_vend = cboDivision.Text,
            };

            //Enviar un empleado al arrayList 
            aVendedores.Add(objV);
            //Imprimir
            listadoVendedores();
            generarCodigo();
        }

        void listadoVendedores()
        {
            lvVendedores.Items.Clear();
            foreach (Vendedor ven in aVendedores)
            {
                ListViewItem fila = new ListViewItem(ven.cod_vend.ToString());
                fila.SubItems.Add(ven.nombre_vend);
                fila.SubItems.Add(ven.direcc_vend);
                fila.SubItems.Add(ven.fecContrato_vend.ToShortDateString());
                fila.SubItems.Add(ven.div_vend);
                lvVendedores.Items.Add(fila);
            }
}
        private void btnModificar_Click(object sender, EventArgs e)
        {

        }

        private void btnSerializar_Click(object sender, EventArgs e)
        {

        }

        private void btnDeserializar_Click(object sender, EventArgs e)
        {

        }
    }
}
