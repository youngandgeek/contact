using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace contact.econtactClasses
{
    internal class contactClass
    {
        //creating getter&setter for the form fields
        public int  ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }

        //creating a connection of database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //method to import data from database , DataTable is temporary table to show data
        public DataTable Select()
        { //an instance of database
            SqlConnection conn = new SqlConnection(myconnstrng);
            
            //obj for datatable

            DataTable dt = new DataTable();

            try
            {
                //sql query for selecting data
                string sql = "SELECT * FROM tbl_contact";
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                //fill the adapter & pass the datatable obj
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
            //return datatable
            return dt;
        
     }
//END OF METHOD 1

//inserting data into database

        public bool Insert(contactClass c)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);
            
            try
            {
                string sql = "INSERT INTO tbl_contact (FirstName , LastName, ContactNo) VALUES (@FirstName , @LastName ,@ContactNo)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create paramter to add data into database with cmd (values,class.properties)
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                //open database connection
                conn.Open();
                //check if query run successfully,returns the number of rows affected
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                //close connection
                conn.Close();
            }
            return isSuccess;
        }

//END OF INSERTING DATA INTO DATABASE METHOD

//UPDATE DATA INTO DATABASE METHOD

        public bool Update(contactClass c)
        {
            bool isSuccess = false;
            //create sql connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //update  query
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName,LastName=@LastName,ContactNo=@ContactNo  WHERE ContactID=@ContactID";
                //update data

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName",c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);

                //open connection
                conn.Open();
                //check if query run successfull
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //close connection
                conn.Close() ;
            }
            return isSuccess ;
           
        }
//END OF UPDATING METHOD

//DELETE METHOD

        public bool Delete(contactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;

                }
            }
            //will popup the error 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
            }
//END OF DELETE METHOD
    }
}
