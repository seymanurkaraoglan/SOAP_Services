using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Services;

namespace SOAP_Services
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public DataTable  Get()
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using(SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM users"))
                {
                    using(SqlDataAdapter da = new SqlDataAdapter())
                    {
                    
                        cmd.Connection = con;
                        da.SelectCommand = cmd;
                        using(DataTable dt = new DataTable()) {
                            dt.TableName = "customer";
                            da.Fill(dt);
                            return dt;
                        }

                    }
                }
            }
        }

        [WebMethod]
        public string update()
        {
            string result = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand();

            string Query = "UPDATE Users SET Username=@Username, Email=@Email, Password=@Password WHERE Id = @Id";
            cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@Id", "1");
            cmd.Parameters.AddWithValue("@UserName","seyma1");
            cmd.Parameters.AddWithValue("@Email", "seyma@gmail.com");
            cmd.Parameters.AddWithValue("@Password","123456");
            
            con.Open();
            cmd.ExecuteNonQuery();
            result = "record updated successfully!";
            con.Close();

            return result;
        }
        [WebMethod]
        public string Insert(string Id, string Username, string Email,string Password)
        {
            string result = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand();

            string Query = "insert into Users values('"+ Id+ "','"+ Username + "','"+ Email + "','"+ Password + "')";
            cmd = new SqlCommand(Query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            result = "Record added successfully!";
            con.Close();
            return result;
        }

        [WebMethod]
        public string Delete(string Id)
        {
            string result = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand();

            string Query = "Delete from Users WHERE Id = '"+Id+"'";
            cmd = new SqlCommand(Query, con);

            con.Open();
            cmd.ExecuteNonQuery();
            result = "delete record successfully!";
            con.Close();

            return result;
        }
        [WebMethod]
        public string Login(string Username,string Password)
        {
            string result = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand();

            string Query = "SELECT * FROM users WHERE Username = '"+Username+"' and Password= '"+Password+"'";
            cmd = new SqlCommand(Query, con);

            con.Open();
            cmd.ExecuteNonQuery();
            if(cmd != null)
            {
                result = "login successfully!";
                return result;
            }
            string res = "error";
            con.Close();
            return res;
            

        }

        //SELECT * FROM users WHERE Username= @Username and Password= @Password

    }
}
