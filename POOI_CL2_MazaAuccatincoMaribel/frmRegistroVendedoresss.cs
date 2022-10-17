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
    public partial class frmRegistroVendedoresss : Form
    {
        int n = 0000; //variable global
        List<Vendedor> arrVendedores = new List<Vendedor>();
        public frmRegistroVendedoresss()
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
                ide_vend = int.Parse(txtCodigo.Text),
                nombre_vend = txtNombre.Text,
                direcc_vend = txtDireccion.Text,
                fecContrato_vend = dtFechaContrato.Value,
                div_vend = cboDivision.Text,
            };

            //Enviamos el vendedor al arrayList 
            arrVendedores.Add(objV);
            listadoVendedores();
            limpiarInputs();
            generarCodigo();

        }

        void limpiarInputs() 
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            dtFechaContrato.Value = DateTime.Now; 
            cboDivision.SelectedIndex = -1;
            txtNombre.Focus();
            
        }

        void listadoVendedores()
        {
            lvVendedores.Items.Clear();
            foreach (Vendedor ven in arrVendedores)
            {
                ListViewItem fila = new ListViewItem(ven.ide_vend.ToString());
                fila.SubItems.Add(ven.nombre_vend);
                fila.SubItems.Add(ven.direcc_vend);
                fila.SubItems.Add(ven.fecContrato_vend.ToShortDateString());
                fila.SubItems.Add(ven.div_vend);
                lvVendedores.Items.Add(fila);
            }
        }

        /// Modificar
        private void lvVendedores_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvVendedores.GetItemAt(e.X, e.Y);

            txtCodigo.Text = item.Text;
            txtNombre.Text = item.SubItems[1].Text;
            txtDireccion.Text = item.SubItems[2].Text;
            dtFechaContrato.Value = DateTime.Parse(item.SubItems[3].Text);
            cboDivision.Text = item.SubItems[4].Text;
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            foreach (Vendedor ven in arrVendedores)
            {
                if (ven.ide_vend == int.Parse(txtCodigo.Text))
                {
                    ven.nombre_vend = txtNombre.Text;
                    ven.direcc_vend = txtDireccion.Text;
                    ven.fecContrato_vend = dtFechaContrato.Value;
                    ven.div_vend = cboDivision.Text; 
                    break;
                }
            }
            listadoVendedores();
            limpiarInputs();
           
        }

        private void btnSerializar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Archivo JSON|*.json";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(sv.FileName, FileMode.Create))
                {
                        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Vendedor>));
                        json.WriteObject(fs, arrVendedores);
   
                }
            }
        }

        private void btnDeserializar_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Archivo JSON|*.json";
            if (op.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(op.FileName, FileMode.Open))
                {
                   
                        DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Vendedor>));
                    arrVendedores = (List<Vendedor>)json.ReadObject(fs);
                    listadoVendedores();


                }
            }
        }

       
    }
}
