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

namespace _6666
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); Load += new System.EventHandler(Form1_Load);
        }

        SqlConnection cnn = new SqlConnection(@"Data Source=TULENISOSUT\SERVER;Initial Catalog=t666; Integrated Security = True");
        private void Form1_Load(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {

                this.nedvigTableAdapter1.Fill(this.t666DataSet1.nedvig);
                dataGridView1.DataSource = nedvigBindingSource1;
                for (int i = 0; i < this.t666DataSet1.Tables[0].Rows.Count; i++)
                {

                    comboBox1.Items.Add(this.t666DataSet1.Tables[0].Rows[i]["цена"].ToString());
                }

                comboBox1.Items.Add("квартира"); comboBox1.Items.Add("комната"); comboBox1.Items.Add("дом"); comboBox1.Items.Add("аренда"); comboBox1.Items.Add("продажа");

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cnn.Close(); cnn.Open();
            try
            {
                DataRow row = this.t666DataSet1.Tables[0].NewRow();
                int i = this.t666DataSet1.Tables[0].Rows.Count;
                row["тип"] = textBox1.Text;
                row["Id"] = Convert.ToDecimal(this.t666DataSet1.Tables[0].Rows[i - 1][0]) + 1;
                row[2] = textBox3.Text;
                row["вид сделки"] = textBox4.Text;
                row["общий метраж"] = Convert.ToDecimal(textBox5.Text);
                row["цена"] = Convert.ToDecimal(textBox6.Text);
                this.t666DataSet1.Tables[0].Rows.Add(row);
                this.t666DataSet1.AcceptChanges();
                this.t666DataSet1.Tables[0].AcceptChanges(); MessageBox.Show("БД обновлена");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cnn.Close(); cnn.Open();
            try
            {
                this.t666DataSet1.AcceptChanges(); int g = 0;
                while (ds.Tables[0].Rows[g][0].ToString() != textBox2.Text.ToString()) g++;
                dataGridView1.Rows.RemoveAt(g); MessageBox.Show("сторока удалена"); y--;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cnn.Close(); cnn.Open();
            try
            { this.t666DataSet1.RejectChanges(); MessageBox.Show("строка восстановлена"); y++; this.t666DataSet1.AcceptChanges(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cnn.Close(); cnn.Open();
            richTextBox1.Clear(); try
            {
                string filter = string.Format("[цена]>'{0}'", textBox7.Text);
                DataRow[] row = this.t666DataSet1.Tables[0].Select(filter, "[цена] DESC");
                if (row.Length == 0) MessageBox.Show("нет таких");
                else
                {
                    for (int i = 0; i < row.Length; i++)
                    {
                        for (int j = 0; j < this.t666DataSet1.Tables[0].Columns.Count; j++)
                        {
                            richTextBox1.Text += this.t666DataSet1.Tables[0].Columns[j].ColumnName + "\t" + row[i][j] + "  ";
                        }
                        richTextBox1.Text += "\n";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cnn.Close(); cnn.Open();
            dataGridView1.BeginEdit(true);
            int x = dataGridView1.SelectedCells[0].RowIndex; int y = dataGridView1.SelectedCells[0].ColumnIndex;
            DataGridViewCell cell = dataGridView1.Rows[x].Cells[y];
            dataGridView1.CurrentCell = cell; dataGridView1.CurrentCell.Selected = true;


            try
            {
                if (comboBox1.Text != "")
                {
                    if (dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 0)
                        dataGridView1.CurrentCell.Value = Convert.ToDecimal(comboBox1.Text);
                    else dataGridView1.CurrentCell.Value = Convert.ToString(comboBox1.Text);
                    nedvigBindingSource1.EndEdit();
                    nedvigTableAdapter1.Update(t666DataSet1.nedvig); MessageBox.Show("База обновлена");
                }
                else { MessageBox.Show("нет значения"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            cnn.Close(); cnn.Open();
            try

            {
                nedvigBindingSource1.EndEdit();
                nedvigTableAdapter1.Update(t666DataSet1.nedvig);
                this.t666DataSet1.AcceptChanges();
                MessageBox.Show("БД обновлена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "t666DataSet1.nedvig". При необходимости она может быть перемещена или удалена.
            this.nedvigTableAdapter1.Fill(this.t666DataSet1.nedvig);


        }


    }
}
