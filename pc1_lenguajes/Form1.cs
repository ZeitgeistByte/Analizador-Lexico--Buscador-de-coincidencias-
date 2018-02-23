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

namespace pc1_lenguajes
{
    public partial class Form1 : Form
    {
        int noPestania = 0;
        TabPage nueva;
        TextBox tb;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("¿Está seguro que desea abandonar el programa?", "Atención!", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }   

        private void nuevaPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crearPestana();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void crearPestana()
        {
            nueva = new TabPage();
            tb = new TextBox();

            //Configuracion de la Pestaña
            tabEditor.TabPages.Add(nueva);
            nueva.Text = "Archivo" + noPestania++;
            nueva.Name = "tab"+ noPestania;

            //Configuracion del textbox correspondiente a la pestaña
            tb.Name = "tab" + noPestania;
            tb.Multiline = true;
            tb.ScrollBars = ScrollBars.Vertical;
            tb.Size = nueva.Size;

            nueva.Controls.Add(tb);

            if (tabEditor.TabCount >= 2)
            {
                análisisLéxicoToolStripMenuItem.Enabled = true;
            }
            tabEditor.SelectedTab = nueva;
        }

        private void guardarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String texto ="";
            // Dialogo para guardar archivo
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Archivo de texto|*.txt";
            saveFileDialog1.Title = "Guardar archivo";
            //Obtiene el texto de la pestaña en la que estamos trabajando
            if (tabEditor.SelectedTab.Controls.ContainsKey(tabEditor.SelectedTab.Name))
            {
                texto = tabEditor.SelectedTab.Controls[tabEditor.SelectedTab.Name].Text;
            }
            saveFileDialog1.ShowDialog();
            //Guarda el archivo siempre que ni el nombre del archivo o el textbox este vacio
            if ((saveFileDialog1.FileName != "") && (texto != null))
            {
                    try
                {

                    //Nombre y ruta del archivo
                    StreamWriter escritor = new StreamWriter(saveFileDialog1.FileName);
                    
                    //escribe el texto
                    escritor.Write(texto);

                    //Cerramos el archivo
                    escritor.Close();

                    tabEditor.SelectedTab.Text = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                }
                //Excepciones
                catch (Exception fe)
                {
                    Console.WriteLine("Exception: " + fe.Message);
                }
                finally
                {
                }
            }
        }

        private void cargarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivo de texto|*.txt";
            openFileDialog1.Title = "Cargar Archivo";
            
            String texto = "";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                texto = sr.ReadToEnd();
                sr.Close();
                    crearPestana();
                    tabEditor.SelectedTab.Controls["tab" + noPestania].Text = texto;
                    tabEditor.SelectedTab.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            }
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("César David Juárez González \n 201503515 \n Universidad de San Carlos de Guatemala \n Facultad de Ingeniería");
        }
    }
}
