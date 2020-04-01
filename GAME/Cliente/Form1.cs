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
    public partial class Form1 : Form
    {

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
                // Se nao foi possivel
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
