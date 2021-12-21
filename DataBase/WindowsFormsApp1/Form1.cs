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
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.OleDb;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static string connect = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Data.mdb;";
        private OleDbConnection connection = new OleDbConnection(connect);
        private OleDbDataAdapter adapter = null;
        private DataSet dataSet = null;
        private DataTable table = null;
        public Form1()
        {
            InitializeComponent();
            connection.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adapter = new OleDbDataAdapter("SELECT * FROM Книги", connection);
            dataSet = new DataSet();
            cartesianChart1.LegendLocation = LegendLocation.Bottom;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Text = "Обновить график";
            if (dataSet.Tables["Книги"] != null) dataSet.Tables["Книги"].Clear();
            adapter.Fill(dataSet, "Книги");
            table = dataSet.Tables["Книги"];
            SeriesCollection series = new SeriesCollection();
            ChartValues<int> values = new ChartValues<int>();
            List<string> name = new List<string>();

            foreach(DataRow row in table.Rows)
            {
                values.Add(Convert.ToInt32(row["Прочитано"]));
                name.Add(Convert.ToString(row["Автор"]));
            }

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis()
            {
                Title = "Авторы",
                Labels = name
            });

            LineSeries line = new LineSeries();
            line.Title = "Прочитано раз";
            line.Values = values;

            series.Add(line);
            cartesianChart1.Series = series;

            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            Hide();
            form2.ShowDialog();
            Show();
        }
    }
}
