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


namespace ContactManager
{
    public partial class ContactManager : Form
    {

        SqlConnection con = new SqlConnection("Data Source=.;Integrated Security=SSPI;Initial Catalog=Phone");
        public ContactManager()
        {
            InitializeComponent();
        }

        private void ContactManager_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox3.Text = "";
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();
        }
        //INSERT button
        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [Table]
                         ([First], [Last], Contact, Email, Category)
               VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "')", con);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Saved!");
            Display();
        }

        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from [Table]",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }
        //DELETE button
        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"DELETE FROM [Table]
            WHERE        (Contact = '" + textBox3.Text + "')", con);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Deleted Successfully!");
            Display();
        }
        //UPDATE button
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE       [Table]
            SET                [First] ='" + textBox1.Text + "', [Last] ='" + textBox2.Text + "', Contact ='" + textBox3.Text + "', Email ='" + textBox4.Text + "', Category = '" + comboBox1.Text + "' WHERE        (Contact = '" + textBox3.Text +"')", con);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Updated Successfully!");
            Display();
        }
        //SEARCH field
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from [Table] Where (Contact like '" + textBox5.Text + "%') or (First like '%" + textBox5.Text + "%') or (Last like '%" + textBox5.Text + "%')", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }
        //close program
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
