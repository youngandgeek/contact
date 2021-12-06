using contact.econtactClasses;
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
using System.Configuration;

namespace contact
{
    public partial class contact : Form
    {
        public contact()
        {
            InitializeComponent();
        }
        //creating an obj of contactClass(contain database query)
        contactClass c = new contactClass();

      
        //ADD Contact BUTTON HANDLER
        private void button1_Click(object sender, EventArgs e)
        {

            //create paramter to add data into database with cmd (values,class.properties)

            //assign the textBox field(in form) to  properties in contactClass
            c.FirstName = txtboxFirstName.Text;
                c.LastName = txtboxLastName.Text;
                c.ContactNo = txtboxContactNo.Text;
                //INSERTING DATA INTO DATABASE
                

              
            //check if successfull , calling the method
            bool success = c.Insert(c);
     
                
            if (success == true)
            {
                //show this Message after data inserted successfully 
                
                MessageBox.Show("NEW CONTACT SUCCEFULLY ADDED ");
                
                //call the clear method to clear textbox fields after data insertion 
                Clear();

            }
           else
            {
                MessageBox.Show("FAILED TO ADD CONTACT");
            }
            //LOAD DATA IN DATAGRIDVIEW & copy,paste it in load method
            // calling the select method also call these 2 lines to update data 
            DataTable dt = c.Select();
            //assign the dataTable to datagridView (Name)
            dgvContactList.DataSource = dt;
        
        }


        private void txtboxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxContactNo_TextChanged(object sender, EventArgs e)
        {

        }
        // LOADS WHEN THE APP STARTS (form load method)
        private void contact_Load(object sender, EventArgs e)
        {
            //loading the dataGridView when starting the app
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;

        }
       
        //Clear Method to clear the textbox fields after inserting the data into datagridview 
        //call it in Add Method after Data Successfully inserted message 
        public void Clear()
        {
            //assign an empty value to the textbox fields
            txtboxFirstName.Text = " ";
            txtboxLastName.Text = " ";
            txtboxContactNo.Text = " ";
            txtboxContactID.Text = " ";
        }
        
        // EXIT button handler
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //close the app when exit button is clicked
            this.Close();
        }
        /* update data steps
         step 1 : select the row of gridview.
         step 2 : load data from gridview into textbox Fields.
         step 3 : in the Update Button handler method.
       (1)CREATE  rowmouse click handler to identify which row u r in when clicking on row in datagridView
        select DataGridView > click event icon in properties >search for ROWHEADERMOUSECLICK &double click it
        >> will direct u to this method
        */
        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //identify the row where the mouse is clicked
            int rowIndex = e.RowIndex;

            //assign the textbox fields to the datagridview rows
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text=dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();  
            txtboxLastName.Text=dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();    
            txtboxContactNo.Text=dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            
        }

        private void txtboxContactID_TextChanged(object sender, EventArgs e)
        {

        }
        //UPDATE BUTTON handler method
        private void button2_Click(object sender, EventArgs e)
        {
            //get data from textbox fields
            //parse the integer value to string
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNo.Text;
            // update data in database by calling the update method from contactClass
            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("Contact Updated Successfully");
                //refresh the database (loading the data by calling select method).
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //clear the fields after succefully updating by calling clear method
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update Contact please try again");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //calling the clear method
            Clear();
        }
           
        //Delete Button handler 
        private void button3_Click(object sender, EventArgs e)
        {
            //get the id 
            c.ContactID=Convert.ToInt32(txtboxContactID.Text);
            //call the delete method
            bool success = c.Delete(c);
            if(success == true)
            {
                MessageBox.Show("Contact Deleted Successfully ");
                //reload the data into gridview
                DataTable dt=c.Select();
                dgvContactList.DataSource=dt;
                //clear the textbox fields by calling clear method
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Delete Contact Please Try Again");
            }

        }
        //first create db connection for the search textbox

        static string myconnstr=ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        //Search TextBox handler
        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            //get contax , value from text box
            string keyword = txtboxSearch.Text;
            SqlConnection conn=new SqlConnection(myconnstr);
 SqlDataAdapter sda= new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%"+keyword+"%' OR LastName LIKE '%"+keyword+"%'",conn);

            //fill the datatable
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;

        }


    }
}
