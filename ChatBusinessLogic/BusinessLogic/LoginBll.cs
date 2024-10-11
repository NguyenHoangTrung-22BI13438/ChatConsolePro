using ChatBusinessLogic.Model;
using Oracle.ManagedDataAccess.Client;
using System.Data;

public class LoginBll
{
    public string oradb = "User Id=WEB;Password=H04ngTrung;Data Source=localhost:1521/ORCL;";
    public LoginOutputModel Login(LoginModel model)
    {
        bool found = false;
        string returnMessage = string.Empty;
        int userId = 0;



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
                    if (dt.Rows.Count == 1)
                    {
                        found = true;
                        userId = int.TryParse(dt.Rows[0]["user_id"]?.ToString(), out int id) ? id : 0;
                        returnMessage = "Đăng nhập thành công";
                    }
                    else
                    {
                        returnMessage = "Đăng nhập không thành công";
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
        


        using (OracleConnection conn = new OracleConnection(oradb))
        {
            conn.Open();
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;

                cmd.CommandText = @"SELECT account.account_name, account.pass_word, account.user_id, userinfo.phone_number 
                                    FROM account INNER JOIN userinfo ON userinfo.user_id=account.user_id
                                    WHERE account.account_name = :username AND account.user_id = :userid AND userinfo.phone_number = :phonenumber";
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
                        return row["pass_word"]?.ToString() ?? "INVALID_PASSWORD";
                    }
                }
            }
        }

        return "INVALID";
    }
}