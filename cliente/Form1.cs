﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Cliente
{

    public static class Global
    {
        public static string texto = "Hello";
        public static int logado = 0;
    }

    public partial class Form1 : Form
    {
        // int logado = 0;
        Socket server;
        public Form1()
        {     
            InitializeComponent();
            Load += new EventHandler(Form1_Load);

        }

        // Conecta ao carregar
        private void Form1_Load(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("10.211.55.9");
            IPEndPoint ipep = new IPEndPoint(direc, 9000);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // Tentamos conectar usando socket
                server.Connect(ipep);
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");
            }
            catch (SocketException)
            {
                dynamic result = MessageBox.Show("No he podido conectar con el servidor\n\t Cierrar aplicacion?", "GameSO", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
                // Se nao foi possivel
                return;
            }
        }

        // 1- Registrado corretamente
        // 2- Usuario ja existe
        // 3- erro ao registrar
        private void buttonRegistra_Click(object sender, EventArgs e)
        {
            // Remove usuario
            if (Global.logado == 1)
            {
                dynamic result = MessageBox.Show("Seguro que quieres\n\t borrar usuario?", "GameSO", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) // Somente se quiser deletar
                {

                    string mensaje = "3/" + textUser.Text + "/" + textPassword.Text;

                    // Enviamos ao servidor a mensagem
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    //Recibimos la respuesta del servidor
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);

                    // Toda a linha de mensagem
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                    if (String.Compare(mensaje, "1" + textUser.Text) == 0)
                    {
                        MessageBox.Show("Excluido com sucesso");
                        Global.logado = 0;
                        textUser.Enabled = true;
                        textPassword.Enabled = true;
                        buttonRegistra.Text = "Registrar";
                        buttonLogin.Text = "Login";
                        this.BackColor = Color.Purple;

                    }
                    else if (String.Compare(mensaje, "2" + textUser.Text) == 0)
                    {
                        MessageBox.Show("Erro ao excluir usuario");
                    }
                    else // Eh impossivel chegar nesse caso
                    {
                        MessageBox.Show("Credenciais incorretas");
                    }
                }
            }
            else { 

                // Mensagem Login
                string mensaje = "5/" + textUser.Text + "/" + textPassword.Text;

                // Enviamos ao servidor a mensagem
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);

                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                if (String.Compare(mensaje, "1" + textUser.Text) == 0)
                {
                    MessageBox.Show("Registrado com sucesso");
                    this.BackColor = Color.Orange;
                    buttonLogin.PerformClick();

                }
                else if (String.Compare(mensaje, "2" + textUser.Text) == 0)
                {
                    MessageBox.Show("Usuario ja existe");
                }
                else
                {
                    MessageBox.Show("Erro ao registrar usuario");
                }

            }
        }

        // 1- Logado corretamente
        // 2- Credenciais incorretas
        // 3- Erro ao logar
        // 0- Deslogado ok
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Se logado pede logout
            if (Global.logado == 1)
            {
                string mensaje = "2/" + textUser.Text + "/" + textPassword.Text; // logout

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);

                // Toda a linha de mensagem
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                if (String.Compare(mensaje, "0" + textUser.Text) == 0)
                {
                    MessageBox.Show("Deslogado com sucesso");
                    Global.logado = 0;
                    textUser.Enabled = true;
                    textPassword.Enabled = true;
                    buttonLogin.Text = "Login";
                    buttonRegistra.Text = "Registrar";
                    this.BackColor = Color.Blue;

                }
                else
                {
                    MessageBox.Show("Erro ao deslogar");
                }

            }
            else // Efetua login
            {
                // Mensagem Login
                string mensaje = "1/" + textUser.Text + "/" + textPassword.Text;

                // Enviamos ao servidor a mensagem
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);

                // Toda a linha de mensagem
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                if (String.Compare(mensaje, "1" + textUser.Text) == 0)
                {
                    MessageBox.Show("Logado com sucesso");
                    Global.logado = 1;
                    textUser.Enabled = false;
                    textPassword.Enabled = false;
                    this.BackColor = Color.Orange;
                    buttonRegistra.Text = "Deletar";
                    buttonLogin.Text = "Logout";
                }
                else if (String.Compare(mensaje, "2" + textUser.Text) == 0)
                {
                    MessageBox.Show("Usuario ou senha incorretos");
                }
                else if (String.Compare(mensaje, "0" + textUser.Text) == 0)
                {
                    MessageBox.Show("Usuario nao existe");
                }
                else
                {
                    MessageBox.Show("Erro ao efetuar login");
                }
            }


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}
