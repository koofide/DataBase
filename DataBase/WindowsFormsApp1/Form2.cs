using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private static string connect = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Data.mdb;";
        private OleDbConnection connection = new OleDbConnection(connect);
        private OleDbDataAdapter adapter = null;
        private DataSet dataSet = null;
        private DataTable table = null;
        int id=1;
        public Form2()
        {
            InitializeComponent();
            connection.Open();
        }

        private void dataup()
        {
            dataSet.Tables["Книги"].Clear();
            adapter.Fill(dataSet, "Книги");
        }
        private void Initialization()
        {
            try
            {
                textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
                textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
                textBox3.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
                textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                textBox5.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            }
            catch
            {
                MessageBox.Show("Данного элемента нет!");
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            adapter = new OleDbDataAdapter("SELECT * FROM Книги", connection);
            dataSet = new DataSet();
            adapter.Fill(dataSet, "Книги");
            table = dataSet.Tables["Книги"];
            dataGridView1.DataSource = table;
            Initialization();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Initialization();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string janr = textBox1.Text.ToString();
                string autor = textBox2.Text.ToString();
                int year = int.Parse(textBox3.Text);
                int times = int.Parse(textBox4.Text);
                OleDbCommand update = connection.CreateCommand();
                update.CommandText = $"UPDATE [Книги] SET [Жанр]='{janr}' ,[Автор]='{autor}' ,[Год выпуска]='{year}' ,[Прочитано]='{times}' WHERE ID={id}";
                update.ExecuteNonQuery();
                dataup();
            }
            catch
            {
                MessageBox.Show("Неверный формат данных!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand delete = connection.CreateCommand();
            delete.CommandText = $"DELETE FROM [Книги] WHERE [ID] = {id}";
            delete.ExecuteNonQuery();
            dataup();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = dataGridView1.Rows.Count;
                string janr = textBox6.Text.ToString();
                string autor =textBox7.Text.ToString();
                int year = int.Parse(textBox8.Text);
                int times = int.Parse(textBox9.Text);
                OleDbCommand Insert = connection.CreateCommand();
                Insert.CommandText = $"INSERT INTO [Книги] ([ID],[Жанр],[Автор],[Год выпуска],[Прочитано]) VALUES('{ID}','{janr}','{autor}','{year}','{times}')";
                Insert.ExecuteNonQuery();
                dataup();
                textBox6.Text ="";
                textBox7.Text ="";
                textBox8.Text ="";
                textBox9.Text ="";
            }
            catch
            {
                MessageBox.Show("Неверный формат данных!");
            }
        }
    }
}
