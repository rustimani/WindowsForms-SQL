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
using System.Xml;
using System.Xml.Serialization.Advanced;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace lab12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); Load += new EventHandler(Form1_Load);
        }
        SqlConnection cnn = new SqlConnection(@"Data Source = TULENISOSUT\SERVER; Initial Catalog = tt6; Integrated Security = True");
        DataSet ds = new DataSet();
        bool chang = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable ril = tt6DataSet.riel.Copy();
            DataTable cli = tt6DataSet3.client.Copy();
            ds.Tables.Add(ril); ds.Tables.Add(cli);
            ds.Relations.Add("R_C", ds.Tables[0].Columns["id_riel"], ds.Tables[1].Columns["id_riel"]);
            bindingSource1.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bindingSource1;
            bindingSource2.DataSource = bindingSource1; bindingSource2.DataMember = "R_C";
            dataGridView2.DataSource = bindingSource2;
            ds.Relations["R_C"].Nested = true;

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

            // TODO: данная строка кода позволяет загрузить данные в таблицу "tt6DataSet2.client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.tt6DataSet3.client);

            // TODO: данная строка кода позволяет загрузить данные в таблицу "tt6DataSet.riel". При необходимости она может быть перемещена или удалена.
            this.rielTableAdapter.Fill(this.tt6DataSet.riel);

        }

        //запись в xml
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ds.WriteXml(@"C:\Users\Татьяна\Documents\Visual Studio 2015\Projects\TRPO\lab12\BD.xml");
                ds.WriteXmlSchema(@"C:\Users\Татьяна\Documents\Visual Studio 2015\Projects\TRPO\lab12\BD.xsd");
                ds.WriteXml(@"C:\Users\Татьяна\Documents\Visual Studio 2015\Projects\TRPO\lab12\BD1.xml", XmlWriteMode.WriteSchema);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //чтение из xml
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ds.Clear();
                ds.ReadXml(@"C:\Users\Татьяна\Documents\Visual Studio 2015\Projects\TRPO\lab12\BD.xml", XmlReadMode.Auto);
                ds.ReadXmlSchema(@"C:\Users\Татьяна\Documents\Visual Studio 2015\Projects\TRPO\lab12\BD.xsd");
                XDocument doc = XDocument.Load(@"C:\Users\Татьяна\Documents\Visual Studio 2015\Projects\TRPO\lab12\BD.xml");
                richTextBox1.Text += doc.Declaration.ToString() + '\n';
                XmlReader oo = doc.CreateReader();
                oo.Read();
                string ii = oo.ReadOuterXml();
                ii = ii.Replace("><", ">\r<");
                richTextBox1.Text += ii;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        //очистить
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            ds.Clear();
            MessageBox.Show("Dataset очищен!");
        }

        //выборка
        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            textBox1.TextChanged += new EventHandler(textchange);
            if (!chang) { return; }
            int i = Convert.ToInt16(textBox1.Text);
            XDocument doc = XDocument.Load(@"C:\Users\Татьяна\Documents\Visual Studio 2015\Projects\TRPO\lab12\BD.xml");
            IEnumerable<XElement> o = (from item in doc.Root.Elements("riel").Elements("client")
                                       where Convert.ToInt16(item.Element("кол-во_x0020_детей").Value) < i
                                       orderby item.Element("id_client").Value
                                       select item).ToList();
            if (o.Count() != 0)
            {
                richTextBox1.Text += "клиенты у которых детей меньше чем вы ввели :" + '\n';
                foreach (XElement st in o)
                {
                    richTextBox1.Text += st.Element("id_client").Value.ToString() + ". " + st.Element("фио").Value.ToString() + '\n';
                }
            }
            else richTextBox1.Text += "нет людей у которых детей меньше чем вы ввели";

        }
        private void textchange(object sender, EventArgs e)
        {

            int i;
            try { i = Convert.ToInt16(textBox1.Text); chang = true; }
            catch { MessageBox.Show("введите кол-во детей для выполнения запроса select"); textBox1.Clear(); chang = false; return; }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("сначала загрузите данные из xml"); return;
            }
            Excel.Application exap = new Excel.Application();
            Excel.Workbook exwb;
            Excel.Worksheet exws;
            Excel.Worksheet exws1;
            exwb = exap.Workbooks.Add();
            exws = (Excel.Worksheet)exwb.Worksheets.get_Item(1);
            exws.Name = "rieltors";
            exws1 = (Excel.Worksheet)exwb.Worksheets.get_Item(2);
            exws1.Name = "clients";
            for (int i = 1; i < dataGridView1.ColumnCount + 1; i++)
            {
                exws.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    exws.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            };
            dataGridView2.DataSource = ds.Tables[1];
            for (int i = 1; i < dataGridView2.ColumnCount + 1; i++)
            {
                exws1.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    exws1.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
            }
            exap.Visible = true;
            dataGridView2.DataSource = bindingSource2;
        }
    }
}
