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

namespace lab13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); Load += new EventHandler(Form1_Load);

        }
        SqlConnection cnn = new SqlConnection(@"Data Source=TULENISOSUT\SERVER;Initial Catalog=tt6; Integrated Security = True");
        SqlDataAdapter riel, cient, dog;
        DataSet ds = new DataSet(); DataGridViewComboBoxColumn Rc = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn Cd = new DataGridViewComboBoxColumn();
        public bool add = true;

        private void Form1_Load(object sender, EventArgs e)
        {

            cient = new SqlDataAdapter("select * from client", cnn);
            riel = new SqlDataAdapter("select * from riel", cnn);
            dog = new SqlDataAdapter("select * from dog", cnn);

            riel.InsertCommand = new SqlCommand("insert into riel values(@pid,@pfio,@ptel)", cnn);
            riel.InsertCommand.Parameters.Add("@pid", SqlDbType.Int, 4, "id_riel");
            riel.InsertCommand.Parameters.Add("@pfio", SqlDbType.VarChar, 50, "фио_р");
            riel.InsertCommand.Parameters.Add("@ptel", SqlDbType.VarChar, 50, "тел_р");
            riel.DeleteCommand = new SqlCommand("delete from riel where id_riel=@ppid", cnn);
            riel.DeleteCommand.Parameters.Add("@ppid", SqlDbType.Int, 4, "id_riel");
            cient.InsertCommand = new SqlCommand("insert into client values(@p_id,@p_fio,@p_tel, @p_ch, @p_idr)", cnn);
            cient.InsertCommand.Parameters.Add("@p_id", SqlDbType.Int, 4, "id_client");
            cient.InsertCommand.Parameters.Add("@p_fio", SqlDbType.VarChar, 50, "фио");
            cient.InsertCommand.Parameters.Add("@p_tel", SqlDbType.VarChar, 50, "телефон");
            cient.InsertCommand.Parameters.Add("@p_ch", SqlDbType.Int, 4, "кол-во детей");
            cient.InsertCommand.Parameters.Add("@p_idr", SqlDbType.Int, 4, "id_riel");
            cient.DeleteCommand = new SqlCommand("delete from client where (id_client=@pp_id)", cnn);
            cient.DeleteCommand.Parameters.Add("@pp_id", SqlDbType.Int, 4, "id_client");
            dog.InsertCommand = new SqlCommand("insert into dog values(@p_num,@p_date,@p_long, @p_idc)", cnn);
            dog.InsertCommand.Parameters.Add("@p_num", SqlDbType.Int, 7, "number");
            dog.InsertCommand.Parameters.Add("@p_date", SqlDbType.Date, 10, "дата");
            dog.InsertCommand.Parameters.Add("@p_long", SqlDbType.Int, 4, "срок действия");
            dog.InsertCommand.Parameters.Add("@p_idc", SqlDbType.Int, 4, "id_client");
            dog.DeleteCommand = new SqlCommand("delete from dog where (number=@pp_n)", cnn);
            dog.DeleteCommand.Parameters.Add("@pp_n", SqlDbType.Int, 4, "number");
            riel.UpdateCommand = new SqlCommand("update riel set [фио_р]=@p_r2, [тел_р]=@p_r3 where [id_riel]=@pppp", cnn);
            riel.UpdateCommand.Parameters.Add("pppp", SqlDbType.Int, 4, "id_riel");
            riel.UpdateCommand.Parameters.Add("p_r2", SqlDbType.VarChar, 50, "фио_р");
            riel.UpdateCommand.Parameters.Add("p_r3", SqlDbType.Int, 10, "тел_р");
            cient.UpdateCommand = new SqlCommand("update client set [фио]=@p_c2, [телефон]=@p_c3, [кол-во детей]=@p_c4 where [id_client]=@pppp1", cnn);
            cient.UpdateCommand.Parameters.Add("p_c2", SqlDbType.VarChar, 50, "фио");
            cient.UpdateCommand.Parameters.Add("p_c3", SqlDbType.Int, 10, "телефон");
            cient.UpdateCommand.Parameters.Add("p_c4", SqlDbType.Int, 4, "кол-во детей");
            dog.UpdateCommand = new SqlCommand("update dog set [дата]=@p_d2, [срок действия]=@p_d3, [id_client]=@p_ioi  where [number]=@pppp2", cnn);
            dog.UpdateCommand.Parameters.Add("pppp2", SqlDbType.Int, 4, "number");
            dog.UpdateCommand.Parameters.Add("p_d2", SqlDbType.DateTime, 50, "дата");
            dog.UpdateCommand.Parameters.Add("p_d3", SqlDbType.Int, 4, "срок действия");
            dog.UpdateCommand.Parameters.Add("p_ioi", SqlDbType.Int, 4, "id_client");


            cient.Fill(ds, "client");
            riel.Fill(ds, "riel"); dog.Fill(ds, "dog");
            SqlCommandBuilder b1 = new SqlCommandBuilder(riel);
            SqlCommandBuilder b2 = new SqlCommandBuilder(cient);
            SqlCommandBuilder b3 = new SqlCommandBuilder(dog);
            dataGridView1.DataSource = ds.Tables["dog"];
            dataGridView1.Columns["id_client"].Visible = false;
            Cd.DataSource = ds.Tables["client"];
            Cd.DisplayMember = ds.Tables["client"].Columns["фио"].ToString();
            Cd.ValueMember = ds.Tables["client"].Columns["id_client"].ToString();
            Cd.DataPropertyName = ds.Tables["dog"].Columns["id_client"].ToString();
            ds.Relations.Add("rc", ds.Tables["riel"].Columns["id_riel"], ds.Tables["client"].Columns["id_riel"]);
            //bindingSource1.DataSource = ds.Tables["riel"];
            //bindingSource1.DataMember = "rc";
            //Rc.DataSource = ds.Tables["riel"];
            //Rc.DisplayMember = ds.Tables["riel"].Columns["фио_р"].ToString();

            // Rc.ValueMember = ds.Tables["riel"].Columns["id_riel"].ToString();

            //Rc.DataPropertyName = ds.Tables["client"].Columns["id_riel"].ToString();
            Cd.Name = "клиент";
            dataGridView1.Columns.Add(Cd);
            Rc.Name = "риелтор";
            dataGridView1.Columns.Add(Rc);

            Rc.Resizable = DataGridViewTriState.True;
            Rc.ReadOnly = false;
            Cd.ReadOnly = false;
            Cd.Resizable = DataGridViewTriState.True;
            dataGridView1.Update();
            // Rc.DisplayIndex = 4;

            //GG = dataGridView1.NewRowIndex;
            // DataGridViewRow uo = dataGridView1.Rows[GG];
            // DataGridViewRowsAddedEventArgs e1=new DataGridViewRowsAddedEventArgs(GG, 1);

            //dataGridView1.RowsAdded += add_row(sender, e1);

            for (int i = 0; i < ds.Tables["riel"].Rows.Count; i++)
            {
                Rc.Items.Add(ds.Tables["riel"].Rows[i]["фио_р"].ToString());
            }
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["клиент"].Value != null)
                {
                    int iiint = Convert.ToInt32(dataGridView1.Rows[i].Cells["клиент"].Value);
                    int ip = Convert.ToInt32(ds.Tables["client"].Rows[iiint - 1]["id_riel"].ToString());


                    dataGridView1.Rows[i].Cells["риелтор"].Value = Rc.Items[ip - 1];
                }
            }
            dataGridView1.RowsRemoved += new DataGridViewRowsRemovedEventHandler(del_row);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(change_in_datagrid);
        }



        public void change_in_datagrid(object sender, DataGridViewCellEventArgs e)
        {
            cnn.Close();
            if (cnn.State != ConnectionState.Open) cnn.Open();
            int X = e.RowIndex; int Y = e.ColumnIndex;

            if (X == dataGridView1.Rows.Count - 2 && X != ds.Tables["dog"].Rows.Count - 1)
            {
                // GG = dataGridView1.NewRowIndex;
                if (ds.Tables["dog"].Rows[ds.Tables["dog"].Rows.Count - 1]["number"].ToString() != 6666.ToString())
                {
                    DataGridViewRowsAddedEventArgs e1 = new DataGridViewRowsAddedEventArgs(X, 1);
                    add_row(sender, e1);
                }
            }
            dataGridView1.Rows[X].Cells[Y].Selected = true;
            if (X == dataGridView1.Rows.Count - 2 && ds.Tables["dog"].Rows[ds.Tables["dog"].Rows.Count - 1]["number"].ToString() == 6666.ToString())
            {
                int heap = 0;
                for (int i = 0; i < ds.Tables["dog"].Columns.Count; i++)

                {
                    if (dataGridView1.Rows[X].Cells[0].Value.ToString() == 6666.ToString()) { heap = 0; break; }
                    if (i == 2) { heap++; continue; }
                    if (dataGridView1.Rows[X].Cells[i].Value != null) heap++;
                }

                if (heap == ds.Tables["dog"].Columns.Count)
                {
                    // DataRow ddt = ds.Tables["dog"].Rows[ds.Tables["dog"].Rows.Count - 1];
                    ds.Tables["dog"].Rows[ds.Tables["dog"].Rows.Count - 1]["number"] = dataGridView1.Rows[X].Cells["number"].Value;
                    ds.Tables["dog"].Rows[ds.Tables["dog"].Rows.Count - 1]["дата"] = Convert.ToDateTime(dataGridView1.Rows[X].Cells["дата"].Value);
                    if (dataGridView1.Rows[X].Cells["срок действия"].Value == null) ds.Tables["dog"].Rows[ds.Tables["dog"].Rows.Count - 1]["срок действия"] = 0;
                    else ds.Tables["dog"].Rows[ds.Tables["dog"].Rows.Count - 1]["срок действия"] = Convert.ToInt32(dataGridView1.Rows[X].Cells["срок действия"].Value.ToString());
                    ds.Tables["dog"].Rows[ds.Tables["dog"].Rows.Count - 1]["id_client"] = dataGridView1.Rows[X].Cells["клиент"].Value;
                    int ip = Convert.ToInt32(dataGridView1.Rows[X].Cells["клиент"].Value);
                    int ppp = Convert.ToInt32(ds.Tables["client"].Rows[ip - 1]["id_riel"].ToString());
                    dataGridView1.Rows[X].Cells["риелтор"].Value = Rc.Items[ppp - 1];
                    //ds.Tables["dog"].AcceptChanges(); 
                    //dog.Update(ds.Tables["dog"]);
                    add = true;
                }
                else return;
            }
            else
            {
                if (Y == 3 && X < ds.Tables["dog"].Rows.Count - 1)
                {
                    ds.Tables["dog"].Rows[X]["id_client"] = dataGridView1.SelectedCells[0].Value;
                    // ds.Tables["dog"].AcceptChanges();
                    //dog.Update(ds.Tables["dog"]);
                }
                else if (Y == 4 && X < ds.Tables["dog"].Rows.Count - 1)
                {
                    int val = Convert.ToInt16(dataGridView1.Rows[X].Cells[Y - 1].Value); int i = 0;
                    while (Convert.ToInt16(ds.Tables["client"].Rows[i]["id_client"]) != val && i < ds.Tables["client"].Rows.Count)
                    {
                        i++;
                    }
                    if (i < ds.Tables["client"].Rows.Count)
                    {
                        ds.Tables["client"].Rows[i]["id_riel"] = dataGridView1.SelectedCells[0].Value;
                        //ds.Tables["client"].AcceptChanges();
                        //cient.Update(ds.Tables["client"]);
                    }
                }
                else if (Y < 3 && X < ds.Tables["dog"].Rows.Count - 1)
                {

                    ds.Tables["dog"].Rows[X][Y] = dataGridView1.SelectedCells[0].Value;
                    //   ds.Tables["dog"].AcceptChanges();
                    //dog.Update(ds.Tables["dog"]);
                }
                dataGridView1.Refresh();
            }
            cnn.Close();
        }



        public void add_row(object sender, DataGridViewRowsAddedEventArgs e1)
        {
            try
            {
                cnn.Close();
                if (cnn.State != ConnectionState.Open) cnn.Open();
                MessageBox.Show("заполните значения ячеек");
                add = false;
                int t = e1.RowIndex;
                DataRow dt = ds.Tables["dog"].NewRow();
                dt["number"] = 6666;//Convert.ToInt32(ds.Tables["dog"].Rows[ds.Tables["dog"].Rows.Count-1]["number"].ToString())+1 ;
                dt["дата"] = Convert.ToDateTime("1.1.1900");
                dt["срок действия"] = 0;
                dt["id_client"] = 1;

                ds.Tables["dog"].Rows.Add(dt);
                


                int i = ds.Tables["dog"].Rows.Count - 1;

                
                cnn.Close();
            }
            catch {; }// (Exception ex) { MessageBox.Show(ex.Message); }
        }



        public void del_row(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (cnn.State != ConnectionState.Open) cnn.Open();
            try
            {
                int X = e.RowIndex;
                if (X >= ds.Tables["dog"].Rows.Count) return;
               
                ds.Tables["dog"].Rows[X].Delete();
                
                cnn.Close();

                dataGridView1.Rows[ds.Tables["dog"].Rows.Count].Selected = true;
            }
            catch {; }// (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void Form1_Formclosing(object sender, FormClosedEventArgs ee)
        {
            dog.Update(ds.Tables["dog"]); cient.Update(ds.Tables["client"]);
        }

    }
}
