using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;Password=1234";
        void Employees()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * From Employees";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
        void DepartmentList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * From Departments";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbEmployeeDepartmentId.DisplayMember = "DepartmentName";
            cmbEmployeeDepartmentId.ValueMember = "DepartmentId";
            cmbEmployeeDepartmentId.DataSource = dataTable;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            Employees();
            DepartmentList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            decimal employeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int departmentId = int.Parse(cmbEmployeeDepartmentId.SelectedValue.ToString());
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "insert into Employees (EmployeeName,EmployeeSurname,EmployeeSalary,Departmentid) values (@employeeName,@employeeSurname,@employeeSalary,@departmentid)";
            var command = new NpgsqlCommand(query,connection);
            command.Parameters.AddWithValue("@employeeName", employeeName);
            command.Parameters.AddWithValue("@employeeSurname", employeeSurname);
            command.Parameters.AddWithValue("@employeeSalary", employeeSalary);
            command.Parameters.AddWithValue("@departmentid", departmentId);
            command.ExecuteNonQuery();
            MessageBox.Show("Ekleme işlemi başarılı!");
            connection.Close();
            Employees();
        }
    }
}
