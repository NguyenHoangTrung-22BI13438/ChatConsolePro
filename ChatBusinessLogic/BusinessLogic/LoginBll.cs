using ChatBusinessLogic.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ChatBusinessLogic.BusinessLogic
{
    public class LoginBll
    {
        public LoginOutputModel Login(LoginModel model)
        {
            bool found = false;
            string returnMessage = string.Empty;
            int userId=0;
            string oradb = "User Id=HTML;Password=22112004;Data Source=localhost:1521/ORCL;";


            using (OracleConnection conn = new OracleConnection(oradb))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT account_name, pass_word, user_id FROM account WHERE account_name = :username AND pass_word = :password";
                    cmd.Parameters.Add(new OracleParameter("username", model.UserName));
                    cmd.Parameters.Add(new OracleParameter("password", model.Password));

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        if (dt.Rows.Count==1)
                        {
                            found = true;
                            userId = int.Parse(dt.Rows[0]["user_id"].ToString());
                            returnMessage = "Đăng nhập thành công";
                        } else
                        {
                            returnMessage = "Đăng nhập khong thành công";
                        }
                    }
                }
            }

            return new LoginOutputModel()
            {
                IsSuccess = found,
                ReturnMessage = returnMessage,
                UserID = userId,
            };
        }
        public string Laylaimatkhau(QuenMkModel model)
        {
            string oradb = "User Id=HTML;Password=22112004;Data Source=localhost:1521/ORCL;";



            using (OracleConnection conn = new OracleConnection(oradb))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = @"SELECT account.account_name, account.pass_word, account.user_id, userinfo.phone_number 
                                        from account inner join userinfo on userinfo.user_id=account.user_id
                                        WHERE account.account_name = :username AND account.user_id = :userid and userinfo.phone_number = :phonenumber ";
                    cmd.Parameters.Add(new OracleParameter("username", model.UserName));
                    cmd.Parameters.Add(new OracleParameter("userid", OracleDbType.Int32)).Value = model.UserId;
                    cmd.Parameters.Add(new OracleParameter("phonenumber", model.PhoneNumber));
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        if (dt.Rows.Count == 1)
                        {
                            DataRow row = dt.Rows[0];
                            return row["pass_word"].ToString();
                        }
                    }
                }
            }
            return "INVALID";
        }
    }
}