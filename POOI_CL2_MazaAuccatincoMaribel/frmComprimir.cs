using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.IO.Compression;

namespace POOI_CL2_MazaAuccatincoMaribel
{
    public partial class frmComprimir : Form
    {
        public frmComprimir()
        {
            InitializeComponent();
        }

        private void tsNuevo_Click(object sender, EventArgs e)
        {
            txtEditor.Clear();
            txtEditor.Focus();


        }

        private void tsAbrir_Click(object sender, EventArgs e)
        {
            tsNuevo_Click(sender, e);
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Archivo de Texto|*.txt";
            if (op.ShowDialog() == DialogResult.OK)
            {
                StreamReader dr = new StreamReader(op.FileName);
                txtEditor.Text = dr.ReadToEnd();
                dr.Close();
            }
        }

        private void tsGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Archivo de Texto|*.txt";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sv.FileName);
                sw.Write(txtEditor.Text);
                sw.Close();
            }
        }

        private void tsComprimir_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog(); //abre la  ventana para guardar
            sv.Filter = "Archivo comprimido ZIP | *.zip ";
            if (sv.ShowDialog() == DialogResult.OK)
            {  //si guarda
                ////Enviando la informacion en un espacio de memoria
                MemoryStream ms = new MemoryStream();
                StreamWriter sw = new StreamWriter(ms);
                sw.Write(txtEditor.Text);
                sw.Flush();//que guarde de manera secuencial

                ////Comprimir los datos desde la memoria
                FileStream fs = new FileStream(sv.FileName, FileMode.Create); //mode creacion
                GZipStream zip = new GZipStream(fs, CompressionMode.Compress); //zipeado, debo indicarle el modo de compresion : Compress

                zip.Write(ms.ToArray(), 0, ms.ToArray().Length); //compara como esta el archivo antiguo con el que se esta comprimiendo en la memoria
                                                                 // el 0 se coloca para comparar
                zip.Close();
                fs.Close(); //siempre se cierra el GZipStream y el FileSTream
            }
        }

        private void tsDescomprimir_Click(object sender, EventArgs e)
        {
            ///Para mostrar un archivo que ya fue comprimido en el txtEditor

            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Archivo ZIP | *.zip ";
            if (op.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(op.FileName, FileMode.Open);
                //des-zipeado
                GZipStream zip = new GZipStream(fs, CompressionMode.Decompress);

                Byte[] info = new Byte[4096];
                zip.Read(info, 0, info.Length); //compara la información antigua en la memoria

                MemoryStream ms = new MemoryStream();
                ms.Write(info, 0, info.Length);

                //Obtener la información de la memoria y enviar al txtEditos
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                txtEditor.Text = sr.ReadToEnd();
                sr.Close();
                fs.Close();
            }
        }

        private void tsSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}