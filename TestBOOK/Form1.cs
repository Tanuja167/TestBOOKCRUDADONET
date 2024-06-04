using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

using System.Windows.Forms;
using System.Xml.Linq;
using System.Data;



namespace TestBOOK
{
    public partial class Form1 : Form
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader da;
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "insert into BookTest values(@bname, @price, @author)";
                cmd = new SqlCommand(qry, con);

                cmd.Parameters.AddWithValue("@bname", txtname.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtprice.Text));
                cmd.Parameters.AddWithValue("@author", txtauthor.Text);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if(result >= 1)
                {
                    MessageBox.Show("Record Inserted");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            GetAllBooks();
            Clear();
            

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select *from BookTest where bid= @id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtid.Text));
                con.Open();
                da = cmd.ExecuteReader();
                if(da.HasRows)
                {
                    if(da.Read())
                    {
                        txtname.Text = da["bname"].ToString();
                        txtprice.Text = da["price"].ToString();
                        txtauthor.Text = da["author"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("record not found");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update BookTest set price = @price where bid = @id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtid.Text));
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtprice.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if(result >= 1)
                {
                    MessageBox.Show("Record updated");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            GetAllBooks();

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from BookTest where bid = @id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtid.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("record deleted");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            GetAllBooks();
            Clear();
        }
        private void GetAllBooks()
        {
            string qry = "select *from BookTest";
            cmd = new SqlCommand(qry, con);
            con.Open();
            da = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(da);
            dataGridView1.DataSource = table;
            con.Close();


        }
        private void Clear()
        {
            txtid.Clear();
            txtname.Clear();
            txtauthor.Clear();
            txtprice.Clear();

        }

        private void btnshowall_Click(object sender, EventArgs e)
        {

        }
    }
}
