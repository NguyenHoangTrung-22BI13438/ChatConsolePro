using ChatBusinessLogic.Model;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ChatBusinessLogic.BusinessLogic
{
    public class AccountInfoBll
    {
        public AccountInfoOutputModel AccountInfo (AccountInfoModel model)
        {
            string oradb = "User Id=HTML;Password=22112004;Data Source=SignalAndSystem:1521/ORCL;";



            using (OracleConnection conn = new OracleConnection(oradb))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT name, phone_number, avatar FROM userinfo WHERE user_id =: userid";
                    cmd.Parameters.Add(new OracleParameter("userid", model.UserID));
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        if (dt.Rows.Count > 0) {
                            return new AccountInfoOutputModel()
                            {
                                Name = dt.Rows[0][0].ToString(),
                                PhoneNumber = dt.Rows[0][1].ToString(),
                                Avatar = dt.Rows[0][2].ToString(),
                            };
                        }
                        else
                        {
                            return new AccountInfoOutputModel()
                            {
                                Name = dt.Rows[0][0]?.ToString() ?? string.Empty,
                                PhoneNumber = dt.Rows[0][1]?.ToString() ?? string.Empty,
                                Avatar = dt.Rows[0][2]?.ToString() ?? string.Empty,
                            };
                        }
                    }
                }
            }
        }
    }
}
