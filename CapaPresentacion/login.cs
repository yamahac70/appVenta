using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogica;
namespace CapaPresentacion
{
    public partial class login : Form
    {
        int mEvento = 0,
            ex ,
            ey ;
        private logicaNegocio logica ;
        public login()
        {
            logica = new logicaNegocio();
            InitializeComponent();
            
        }
        private void login_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbContraseña.Text!="" || tbUsuario.Text!="")
            {
                if (logica.verificarUsuario(tbUsuario.Text, tbContraseña.Text))
                {
                    inicio inicio = new inicio();
                    MessageBox.Show($"El usuario {tbUsuario.Text} fue encontrado ");
                    logica.NombreUsuario = tbUsuario.Text;
                    this.Hide();
                    inicio.Show();

                }
                else
                {
                    MessageBox.Show("Contraseña o usuario no existe");
                }
            }
            else
            {
                MessageBox.Show("Porfavor ingresa los campos solicitados");
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mEvento = 1;
            ex = e.X;
            ey = e.Y;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mEvento == 1)
            {
                this.SetDesktopLocation(MousePosition.X - ex, MousePosition.Y - ey);
            }

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mEvento = 0;
        }
    }
}
