using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _77
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        SqlConnection cnn = new SqlConnection(@"Data Source = TULENISOSUT\SERVER; Initial Catalog = t999; Integrated Security = True");

        DataSet ds = new DataSet();
        SqlDataAdapter daclient;
        SqlDataAdapter dariel;
        SqlDataAdapter dadog;

        private void button1_Click_1(object sender, EventArgs e)
        {
            cnn.Open();

            daclient = new SqlDataAdapter("select * from client", cnn);
            dariel = new SqlDataAdapter("select * from riel", cnn);
            dadog = new SqlDataAdapter("select * from dog", cnn);
            daclient.Fill(ds, "client"); dariel.Fill(ds, "riel"); dadog.Fill(ds, "dog");
            dataGridView1.DataSource = bindingSource1;
            dataGridView2.DataSource = bindingSource2;
            dataGridView3.DataSource = bindingSource3;
            bindingSource3.DataSource = ds.Tables["dog"];
            bindingSource2.DataSource = ds.Tables["client"];
            bindingSource1.DataSource = ds.Tables["riel"];
            ds.Relations.Add("C_D", ds.Tables["client"].Columns["id_client"], ds.Tables["dog"].Columns["id_client"]);
            ds.Relations.Add("R_C", ds.Tables["riel"].Columns["id_riel"], ds.Tables["client"].Columns["id_riel"]);
            bindingSource1.DataSource = ds.Tables["riel"];
            bindingSource2.DataSource = bindingSource1;
            bindingSource2.DataMember = "R_C";
            bindingSource3.DataSource = bindingSource2;
            bindingSource3.DataMember = "C_D";
            dataGridView1.Refresh();
            cnn.Close();
            button1.Enabled = false;
        }


    }
}
