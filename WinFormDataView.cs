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

namespace lab9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();/* Load += new System.EventHandler(Form1_Load);*/
        }
        SqlConnection cnn = new SqlConnection(@"Data Source=TULENISOSUT\SERVER;Initial Catalog=tt6; Integrated Security = True");
        SqlDataAdapter da, da1, da2; DataSet ds = new DataSet();
        int i, j, glo;

        //next rieltor
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                j = 0;
                if (i != ds.Tables["riel"].Rows.Count - 1)
                {
                    i++;
                    bindingSource1.Position = i;
                    if (comboBox2.SelectedIndex >= 0)
                    {
                        while (ds.Tables["dog"].Rows[j][0].ToString() != comboBox2.SelectedValue.ToString()) j++;
                        dogBindingSource.Position = j;
                    }
                };
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //add rietor
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int h = ds.Tables["riel"].Rows.Count - 1;
                int id = Convert.ToInt32(ds.Tables["riel"].Rows[h][0].ToString());
                DataRow row = ds.Tables["riel"].NewRow();
                row["id_riel"] = id + 1;
                row["фио_р"] = textBox1.Text;
                row["тел_р"] = Convert.ToDecimal(textBox2.Text);
                ds.Tables["riel"].Rows.Add(row);
                da.Update(ds, "riel");
                textBox1.Undo(); textBox2.Undo(); MessageBox.Show("риелтор добавлен");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //delete rieltor
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                int g = bindingSource1.Position;
                bindingSource1.RemoveAt(g);
                cnn.Close();
                ds.Tables["riel"].Rows[g].Delete(); da.Update(ds, "riel");
                ds.AcceptChanges();
                MessageBox.Show("риелтор удален");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //add  client
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = ds.Tables["client"].NewRow();
                row["id_client"] = Convert.ToInt32(comboBox1.Text);
                row["фио"] = textBox3.Text.ToString();
                row["телефон"] = Convert.ToDecimal(textBox4.Text);
                row["кол-во детей"] = Convert.ToDecimal(textBox5.Text);
                row["id_riel"] = bindingSource1.Position + 1;
                ds.Tables["client"].Rows.Add(row);
                da1.Update(ds, "client"); comboBox1.Update();
                if (bindingSource2.Count != 1)
                { textBox3.Undo(); textBox4.Undo(); textBox5.Undo(); }
                textBox3.Refresh(); MessageBox.Show("клиент добавлен");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        //delete client
        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                cnn.Open();
                int k = 0, gg = 0;
                k = comboBox1.SelectedIndex;
                while (ds.Tables["client"].Rows[gg][0].ToString() != comboBox1.SelectedValue.ToString())
                {
                    gg++;
                }
                bindingSource2.RemoveAt(k);
                cnn.Close();
                ds.Tables["client"].Rows[gg].Delete();
                da1.Update(ds, "client"); MessageBox.Show("клиент удален");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //add dogovor
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int gg = 0;
                while (ds.Tables["client"].Rows[gg][1].ToString() != textBox3.Text.ToString()) gg++;
                DataRow row = ds.Tables["dog"].NewRow();
                row["number"] = Convert.ToInt32(comboBox2.Text);
                row["дата"] = dateTimePicker1.Value;
                row["срок действия"] = Convert.ToDecimal(textBox6.Text);
                row["id_client"] = Convert.ToInt32(ds.Tables["client"].Rows[gg][0].ToString());
                ds.Tables["dog"].Rows.Add(row);
                da2.Update(ds, "dog");
                comboBox2.Update(); dateTimePicker1.Update();
                if (ds.Tables["dog"].Rows.Count != 1)
                { textBox6.Undo(); textBox5.Undo(); comboBox2.Update(); }
                MessageBox.Show("договор добавлен");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //change riel
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                ds.Tables["riel"].Rows[bindingSource1.Position][1] = textBox1.Text.ToString();
                ds.Tables["riel"].Rows[bindingSource1.Position][2] = Convert.ToInt32(textBox2.Text);
                da.Update(ds, "riel"); MessageBox.Show("риелтор изменен");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //change client
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                int gg = 0;
                while (ds.Tables["client"].Rows[gg][0].ToString() != comboBox1.SelectedValue.ToString())
                {
                    gg++;
                }
                ds.Tables["client"].Rows[gg][1] = textBox3.Text.ToString();
                ds.Tables["client"].Rows[gg][2] = Convert.ToInt32(textBox4.Text);
                ds.Tables["client"].Rows[gg][3] = Convert.ToInt32(textBox5.Text);
                da1.Update(ds, "client"); MessageBox.Show("клиент изменен");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //change dog
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                int gg = 0;
                while (ds.Tables["dog"].Rows[gg][0].ToString() != comboBox2.SelectedValue.ToString())
                {
                    gg++;
                }
                ds.Tables["dog"].Rows[gg][1] = dateTimePicker1.Value;
                ds.Tables["dog"].Rows[gg][2] = Convert.ToInt32(textBox6.Text);

                da2.Update(ds, "dog"); MessageBox.Show("договор изменен");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //delete dogovor
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                int k = 0; int g = 0;
                while (ds.Tables["dog"].Rows[g][0].ToString() != comboBox2.SelectedValue.ToString())
                {
                    g++;
                }
                k = comboBox2.SelectedIndex;
                bindingSource3.RemoveAt(k);
                cnn.Close();
                ds.Tables["dog"].Rows[g].Delete();
                da2.Update(ds, "dog"); MessageBox.Show("договор удален");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //prev rieltor
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                j = 0;
                if (i != 0)
                {
                    i--; bindingSource1.Position = i;
                    if (comboBox2.SelectedIndex >= 0)
                    {

                        while (ds.Tables["dog"].Rows[j][0].ToString() != comboBox2.SelectedValue.ToString()) j++;
                        dogBindingSource.Position = j;
                    };
                };
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //load
        private void button1_Click(object sender, EventArgs e)
        {
            cnn.Open();
            da = new SqlDataAdapter("select * from riel", cnn); da.Fill(ds, "riel");
            da1 = new SqlDataAdapter("select * from client", cnn); da1.Fill(ds, "client");
            da2 = new SqlDataAdapter("select * from dog", cnn); da2.Fill(ds, "dog");
            ds.Relations.Add("R_C", ds.Tables["riel"].Columns["id_riel"], ds.Tables["client"].Columns["id_riel"]);
            ds.Relations.Add("C_D", ds.Tables["client"].Columns["id_client"], ds.Tables["dog"].Columns["id_client"]);
            bindingSource1.DataSource = ds.Tables["riel"];
            bindingSource2.DataSource = bindingSource1; bindingSource2.DataMember = "R_C";
            bindingSource3.DataSource = bindingSource2; bindingSource3.DataMember = "C_D";
            da.InsertCommand = new SqlCommand("insert into riel values(@pid,@pfio,@ptel)", cnn);
            da.InsertCommand.Parameters.Add("@pid", SqlDbType.Int, 4, "id_riel");
            da.InsertCommand.Parameters.Add("@pfio", SqlDbType.VarChar, 50, "фио_р");
            da.InsertCommand.Parameters.Add("@ptel", SqlDbType.VarChar, 50, "тел_р");
            da.DeleteCommand = new SqlCommand("delete from riel where id_riel=@ppid", cnn);
            da.DeleteCommand.Parameters.Add("@ppid", SqlDbType.Int, 4, "id_riel");
            da1.InsertCommand = new SqlCommand("insert into client values(@p_id,@p_fio,@p_tel, @p_ch, @p_idr)", cnn);
            da1.InsertCommand.Parameters.Add("@p_id", SqlDbType.Int, 4, "id_client");
            da1.InsertCommand.Parameters.Add("@p_fio", SqlDbType.VarChar, 50, "фио");
            da1.InsertCommand.Parameters.Add("@p_tel", SqlDbType.VarChar, 50, "телефон");
            da1.InsertCommand.Parameters.Add("@p_ch", SqlDbType.Int, 4, "кол-во детей");
            da1.InsertCommand.Parameters.Add("@p_idr", SqlDbType.Int, 4, "id_riel");
            da1.DeleteCommand = new SqlCommand("delete from client where (id_client=@pp_id)", cnn);
            da1.DeleteCommand.Parameters.Add("@pp_id", SqlDbType.Int, 4, "id_client");
            da2.InsertCommand = new SqlCommand("insert into dog values(@p_num,@p_date,@p_long, @p_idc)", cnn);
            da2.InsertCommand.Parameters.Add("@p_num", SqlDbType.Int, 7, "number");
            da2.InsertCommand.Parameters.Add("@p_date", SqlDbType.Date, 10, "дата");
            da2.InsertCommand.Parameters.Add("@p_long", SqlDbType.Int, 4, "срок действия");
            da2.InsertCommand.Parameters.Add("@p_idc", SqlDbType.Int, 4, "id_client");
            da2.DeleteCommand = new SqlCommand("delete from dog where (number=@pp_n)", cnn);
            da2.DeleteCommand.Parameters.Add("@pp_n", SqlDbType.Int, 4, "number");
            da.UpdateCommand = new SqlCommand("update riel set [фио_р]=@p_r2, [тел_р]=@p_r3 where [id_riel]=@pppp", cnn);
            da.UpdateCommand.Parameters.Add("pppp", SqlDbType.Int, 4, "id_riel");
            da.UpdateCommand.Parameters.Add("p_r2", SqlDbType.VarChar, 50, "фио_р");
            da.UpdateCommand.Parameters.Add("p_r3", SqlDbType.Int, 10, "тел_р");
            da1.UpdateCommand = new SqlCommand("update client set [фио]=@p_c2, [телефон]=@p_c3, [кол-во детей]=@p_c4 where [id_client]=@pppp1", cnn);
            da1.UpdateCommand.Parameters.Add("p_c2", SqlDbType.VarChar, 50, "фио");
            da1.UpdateCommand.Parameters.Add("p_c3", SqlDbType.Int, 10, "телефон");
            da1.UpdateCommand.Parameters.Add("p_c4", SqlDbType.Int, 4, "кол-во детей");
            da2.UpdateCommand = new SqlCommand("update dog set [дата]=@p_d2, [срок действия]=@p_d3  where [number]=@pppp2", cnn);
            da2.UpdateCommand.Parameters.Add("pppp2", SqlDbType.Int, 4, "number");
            da2.UpdateCommand.Parameters.Add("p_d2", SqlDbType.DateTime, 50, "дата");
            da2.UpdateCommand.Parameters.Add("p_d3", SqlDbType.Int, 4, "срок действия");
            textBox1.DataBindings.Add("Text", bindingSource1, "фио_р");
            bindingSource1.Position = 0; i = bindingSource1.Position;
            textBox2.DataBindings.Add("Text", bindingSource1, "тел_р");

            comboBox1.DataSource = bindingSource2;
            comboBox1.DisplayMember = "id_client";
            comboBox1.ValueMember = "id_client";
            comboBox1.DataBindings.Add(new Binding("SelectedItem", ds, "dog.id_client"));
            textBox3.DataBindings.Add("Text", bindingSource2, "фио");
            textBox4.DataBindings.Add("Text", bindingSource2, "телефон");
            textBox5.DataBindings.Add("Text", bindingSource2, "кол-во детей");

            comboBox2.DataSource = bindingSource3;
            comboBox2.DisplayMember = "number";
            comboBox2.ValueMember = "number";
            comboBox2.DataBindings.Add("SelectedValue", ds, "dog.id_client");
            dateTimePicker1.Enabled = true; dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Checked = false;
            dogBindingSource.Position = 0;
            textBox6.DataBindings.Add("Text", bindingSource3, "срок действия");
            cnn.Close();
            button1.Enabled = false; button2.Enabled = true; button3.Enabled = true; button4.Enabled = true;
            button5.Enabled = true; button6.Enabled = true; button7.Enabled = true; button8.Enabled = true;
            button9.Enabled = true; button10.Enabled = true; button11.Enabled = true; button12.Enabled = true;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tt6DataSet.riel". При необходимости она может быть перемещена или удалена.
            this.rielTableAdapter.Fill(this.tt6DataSet.riel);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "t99DataSet.dog". При необходимости она может быть перемещена или удалена.
            this.dogTableAdapter.Fill(this.tt6DataSet1.dog);
            button2.Enabled = false; button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false; button6.Enabled = false; button7.Enabled = false; button8.Enabled = false;
            button9.Enabled = false; button10.Enabled = false; button11.Enabled = false; button12.Enabled = false;


        }

    }
}
