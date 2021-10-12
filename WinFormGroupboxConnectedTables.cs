using System;
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

namespace lab15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); Load += new EventHandler(Form1_Load);
        }
        SqlConnection cnn = new SqlConnection(@"Data Source=TULENISOSUT\SERVER;Initial Catalog=tt67; Integrated Security = True");
        SqlDataAdapter daRiel, daClient, daDog;
        DataSet ds = new DataSet();
        DataView dvC = new DataView();
        DataView dvD = new DataView();

        //редактировать
        private void button1_Click(object sender, EventArgs e)
        {
            dvC.AllowEdit = true;
            dvC.RowFilter = "id_cient=" + comboBox1.SelectedValue.ToString();
            dvD.RowFilter = "id_client=" + comboBox1.SelectedValue.ToString();
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
            Refresh_client();

        }
        //добавить изменения
        private void button4_Click(object sender, EventArgs e)
        {
            if (dvC.Count != 0)
            {
                dvC[0]["fio"] = textBox2.Text;
                dvC[0]["tel"] = textBox4.Text;
                dvC[0]["children"] = Convert.ToInt16(textBox3.Text);
                dvC[0]["id_riel"] = comboBox2.SelectedValue;
            }
            else
            {
                dvC.AddNew();
                dvC.RowStateFilter = DataViewRowState.CurrentRows;
                dvC[0]["fio"] = textBox2.Text;
                dvC[0]["tel"] = textBox4.Text;
                dvC[0]["children"] = Convert.ToInt16(textBox3.Text);
                dvC[0]["id_riel"] = comboBox2.SelectedValue;
            }
            ds.AcceptChanges();
            daClient.Update(ds.Tables["client"]);
            daDog.Update(ds.Tables["dog"]);
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
        }
        //cancel
        private void button5_Click(object sender, EventArgs e)
        {
            ds.RejectChanges();
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
            dvC.RowFilter = "id_cient=" + comboBox1.SelectedValue.ToString();
            Refresh_client();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dvC.AllowEdit = true;
            dvC.RowFilter = "id_cient=" + comboBox1.SelectedValue.ToString();
            dvC[0].Delete();
            daClient.Update(ds.Tables["client"]);
            daDog.Update(ds.Tables["dog"]);
            daClient.Update(ds.Tables["client"]);
            daDog.Update(ds.Tables["dog"]);
            Refresh_client();
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dvC.AllowEdit = true;
            dvC.RowFilter = "id_cient=-1";
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
            dvD.RowFilter = "id_client=-1";
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dvC.RowFilter = "id_cient=" + comboBox1.SelectedValue.ToString();
                Refresh_client();
            }
            catch {; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            daRiel = new SqlDataAdapter("select * from riel", cnn);
            daClient = new SqlDataAdapter("select * from client", cnn);
            daDog = new SqlDataAdapter("select * from dog", cnn);
            daRiel.Fill(ds, "riel");
            daClient.Fill(ds, "client");
            daDog.Fill(ds, "dog");
            daClient.UpdateCommand = new SqlCommand("update client set fio=@fio, tel=@tel, children=@c, id_riel=@idr where id_cient=@idc", cnn);
            daClient.UpdateCommand.Parameters.Add("@fio", SqlDbType.VarChar, 50, "fio");
            daClient.UpdateCommand.Parameters.Add("@tel", SqlDbType.NChar, 10, "tel");
            daClient.UpdateCommand.Parameters.Add("@c", SqlDbType.Int, 4, "children");
            daClient.UpdateCommand.Parameters.Add("@idr", SqlDbType.Int, 4, "id_riel");
            daClient.UpdateCommand.Parameters.Add("@idc", SqlDbType.Int, 4, "id_cient");
            daClient.InsertCommand = new SqlCommand("insert into client (fio, tel, children, id_riel) values (@Fio, @Tel, @C, @Idr)", cnn);
            daClient.InsertCommand.Parameters.Add("@Fio", SqlDbType.VarChar, 50, "fio");
            daClient.InsertCommand.Parameters.Add("@Tel", SqlDbType.NChar, 10, "tel");
            daClient.InsertCommand.Parameters.Add("@C", SqlDbType.Int, 4, "children");
            daClient.InsertCommand.Parameters.Add("@Idr", SqlDbType.Int, 4, "id_riel");
            daClient.DeleteCommand = new SqlCommand("delete from client where id_cient=@IDC", cnn);
            daClient.DeleteCommand.Parameters.Add("@IDC", SqlDbType.Int, 4, "id_cient");
            dvC.Table = ds.Tables["client"];
            dvD.Table = ds.Tables["dog"];
            comboBox1.DataSource = ds.Tables["client"];
            comboBox1.ValueMember = "id_cient";
            comboBox1.DisplayMember = "fio";
            comboBox2.DataSource = ds.Tables["riel"];
            comboBox2.ValueMember = "id_riel";
            comboBox2.DisplayMember = "fio_r";

            dataGridView1.DataSource = dvD;
            Refresh_client();
            groupBox2.Enabled = false;

        }

        private void Refresh_client()
        {

            textBox1.Text = dvC[0]["id_cient"].ToString();
            textBox2.Text = dvC[0]["fio"].ToString();
            textBox3.Text = dvC[0]["children"].ToString();
            textBox4.Text = dvC[0]["tel"].ToString();
            comboBox2.SelectedValue = dvC[0]["id_riel"];
            dvD.RowFilter = "id_client=" + comboBox1.SelectedValue.ToString();

        }
    }
}
