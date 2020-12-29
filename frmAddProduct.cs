using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace skelot
{
    public partial class frmAddProduct : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
       // string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Data.accdb";

        ListViewItem lst;
        frmLogin login = new frmLogin();

        public frmAddProduct()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        } 
        public void getManufacturer()
        { 
          
            try
            {
          

                string sql2 = @"Select * from tblManufacturer";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    cboManufac.Items.Add(dr[1].ToString());
      
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    
        }

        private void Form11_Load(object sender, EventArgs e)
        {
           
            cboManufac.SelectedIndex = 0;
            cn = new SqlConnection(login.connection);
            cn.Open();
            getData();
            getManufacturer();
            timer1.Start();
          
        }
      
        private void btnUpdateI_Click(object sender, EventArgs e)
        {

        }
        public void CLear() 
        {
          //  txtIDCode.Text = "";
            txtName.Text = "";
            txtQty.Text = "";
            
        
        }

    

        private void txtIDCode_TextChanged(object sender, EventArgs e)
        {
           
        }
 
       

        private void cboGender_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            FrmAdminMenu frmAM = new FrmAdminMenu();
            frmAM.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTempID.Text = listView1.FocusedItem.Text;
            lblTempName.Text = listView1.FocusedItem.SubItems[2].Text;            
        }


        //private void txtDeliveryDate_TextChanged(object sender, EventArgs e)
        //{


           
        //    if (txtDeliveryDate.Text == "")
        //    {

        //        lblTempDate.Text = "";

        //    }
        //    else 
        //    {
                
        //    //    string format2 = "MMM-dd-yyy HH:mm:ss";
        //        int arrive = Convert.ToInt32(txtDeliveryDate.Text);
        //        DateTime date = Convert.ToDateTime(lblDate.Text);
        //        date = date.AddDays(arrive);
             
        //        lblTempDate.Text = date.ToString();
            
            
            
        //    }


        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
         //   string format = "MMM-dd-yyy HH:mm:ss";
            //lblTimer.Text = time.ToString(format);
            lblDate.Text = time.ToString();

        }


        public void getData()
        {
            //displaying data from Database to lstview
            try
            {
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listView1.Columns.Add("PID", 90);
                listView1.Columns.Add("Product Name", 190);
                listView1.Columns.Add("Unit Price", 90);
                listView1.Columns.Add("Quantity", 100);
                listView1.Columns.Add("Manufacturer", 190);
               
                string sql2 = @"Select * from tblProduct";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = listView1.Items.Add(dr[0].ToString());
                    lst.SubItems.Add(dr[1].ToString());
                    lst.SubItems.Add(dr[2].ToString());
                    lst.SubItems.Add(dr[3].ToString());
                    lst.SubItems.Add(dr[4].ToString());
                    //lst.SubItems.Add(dr[5].ToString());
                    //lst.SubItems.Add(dr[6].ToString());
                    //lst.SubItems.Add(dr[7].ToString());
                    //lst.SubItems.Add(dr[8].ToString());
                    //lst.SubItems.Add(dr[9].ToString());
                    //lst.SubItems.Add(dr[10].ToString());
                    //lst.SubItems.Add(dr[11].ToString());
                    //lst.SubItems.Add(dr[12].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddOrder(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtUnitPrice.Text == "" || txtQty.Text == "" || cboManufac.Text == "-Select-")
            {
                MessageBox.Show("Fill textboxes to proceed.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }



            else
            {
                try
                {
                    string sql = @"INSERT INTO tblProduct VALUES(@Name,@UnitPrice,@Stock,@Manufacturer)";
                    cm = new SqlCommand(sql, cn);
                     cm.Parameters.AddWithValue("@Name", txtName.Text);
                     cm.Parameters.AddWithValue("@UnitPrice", txtUnitPrice.Text);
                    cm.Parameters.AddWithValue("@Stock", txtQty.Text);
                    cm.Parameters.AddWithValue("@Manufacturer", cboManufac.Text);
                   
                    cm.ExecuteNonQuery();
                    MessageBox.Show("Record successfully saved!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InsertTrail();
                    this.Clear();
                   
                    cboManufac.SelectedIndex = 0;
                    getData();
                    
                   
                }
                catch (SqlException l)
                {
                    MessageBox.Show("Re-input again. ID may already be taken!");
                    MessageBox.Show(l.Message);
                }

            }
    
        }
        public void InsertTrail()
        {
            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", lblDate.Text);
                cm.Parameters.AddWithValue("@Transactype", "Insertion");
                cm.Parameters.AddWithValue("@Description", "Product:" + txtName.Text + " has been Added to Products!");
                cm.Parameters.AddWithValue("@Authority", "Admin");


                cm.ExecuteNonQuery();
                //   MessageBox.Show("Record successfully saved!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again. your username may already be taken!");
                MessageBox.Show(l.Message);
            }
        }
        public void DeleteTrail() 
        {
            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", lblDate.Text);
                cm.Parameters.AddWithValue("@Transactype", "Deletion");
                cm.Parameters.AddWithValue("@Description", "Item: " + lblTempName.Text + " has been removed from orders!");
                cm.Parameters.AddWithValue("@Authority", "Admin");


                cm.ExecuteNonQuery();
                //   MessageBox.Show("Record successfully saved!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again. your username may already be taken!");
                MessageBox.Show(l.Message);
            }


        }
        public void AllDelTrail()
        {

            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", lblDate.Text);
                cm.Parameters.AddWithValue("@Transactype", "Deletion");
                cm.Parameters.AddWithValue("@Description", "All Items from orders were REMOVED!");
                cm.Parameters.AddWithValue("@Authority", "Admin");

                cm.ExecuteNonQuery();
               
            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again. your username may already be taken!");
                MessageBox.Show(l.Message);
            }

        }
        public void Clear() 
        {

           // txtOrderID.Text = "";
           // txtIDCode.Text = "";
            txtName.Text = "";
            txtUnitPrice.Text = "";
            txtQty.Text = "";
         
         
            lblTempID.Text = "";
        
        }

        private void txtDeliveryDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNetPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void button2_Click(object sender, EventArgs e)
        {
           this.Dispose();
            frmAddManufac frmManufac = new frmAddManufac();
            frmManufac.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            getManufacturer();
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0 || lblTempID.Text == "")
            {
                MessageBox.Show("Nothing to Delete!. Please Select an item.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else 
            {
                if (MessageBox.Show("Do you really want to delete this Order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DeleteTrail();
                    deleteRecords();                  
                }
            }       
        }
        public void deleteRecords()
        {
            try
            {
             
             //   listView1.FocusedItem.Remove();
                string del = "DELETE from tblNewOrder where IDOrder='" + lblTempID.Text + "'";
                cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Successfully Deleted!");
                Clear();
                getData();
        
            

            }
            catch (Exception)
            {
                MessageBox.Show("No Item to Remove", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if(listView1.Items.Count == 0)
            {

                return;
            }

            if (MessageBox.Show("Do you really want to delete ALL Order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                AllDelTrail();
                DeleteAll();
               
               
            }
        }
        public void DeleteAll()
        {

            try
            {

                // listView1.FocusedItem.Remove();
                string del = "DELETE * from tblNewOrder ";
                cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Successfully Deleted!");
                getData();
                Clear();
                
            }
            catch (Exception)
            {
                MessageBox.Show("No Item to Remove", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void cboManufac_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCritLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
