using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionMYSQLOrigenesDeDatos
{
    public partial class Form1 : Form
    {

        static MySqlConnection Conex = new MySqlConnection();
        static string serv = "Server=localhost;";
        static string db = "Database=coche;";
        static string usuario = "UID=root;";
        static string pwd = "";
        string cadenaDeConexion = serv + db + usuario + pwd;

        public void conectar()
        {
            try
            {
                Conex.ConnectionString = cadenaDeConexion;
                Conex.Open();
                MessageBox.Show("LA BD ESTA CONECTADA");
            }
            catch (Exception)
            {
                MessageBox.Show("AAASSSSTIAS QUE NO PUEDO CONECTAR");
                throw;
            }
        }

        public void desconectar()
        {
            Conex.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void dataReportLoad()
        {
            DataTable dt = new DataTable();
            MySqlCommand cmd = Conex.CreateCommand();
            cmd.CommandText = "SELECT * FROM coche;";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);
            CrystalReport2 report = new CrystalReport2();
            report.SetDataSource(dt);
            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.Refresh();
            cmd.Dispose();
            adapter.Dispose();
            dt.Dispose();
            desconectar();
        }

        private void cocheBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.cocheBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.cocheDataSet);
            dataReportLoad();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'cocheDataSet.coche' Puede moverla o quitarla según sea necesario.
            conectar();
            dataReportLoad();
            this.cocheTableAdapter.Fill(this.cocheDataSet.coche);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }
    }
}
