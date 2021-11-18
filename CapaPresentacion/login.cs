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
        private logicaNegocio logica ;
        public login()
        {
            logica = new logicaNegocio();
            InitializeComponent();
            
        }
        private void login_Load(object sender, EventArgs e)
        {

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

       
    }
}
