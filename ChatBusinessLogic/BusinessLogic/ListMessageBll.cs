using ChatBusinessLogic.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBusinessLogic.BusinessLogic
{
    public class ListMessageBll
    {
        public ListMessageOutputModel ListMessage(ListMessageModel model)
        {
            string oradb = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=LOCALHOST)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id=HTML;Password=22112004;DBA Privilege=SYSDBA;";

            using (OracleConnection conn = new OracleConnection(oradb))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT content,userinfo.avatar, create_time, message.status,author_id
                                        FROM message inner join userinfo on author_id=userinfo.user_id 
                                        WHERE relationship_id =: id order by create_time";
                    cmd.Parameters.Add(new OracleParameter("id", model.RelationshipID));
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        string[] content = new string[dt.Rows.Count];
                        string[] authoravatar = new string[dt.Rows.Count];
                        int[] status = new int[dt.Rows.Count];
                        int[] authorid = new int[dt.Rows.Count];
                        string[] time = new string[dt.Rows.Count];
                        int count = dt.Rows.Count;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            content[i] = dt.Rows[i][0].ToString();
                            authoravatar[i] = dt.Rows[i][1].ToString();
                            if (int.TryParse(dt.Rows[i][3].ToString(), out int StatusValue))
                            {
                                status[i] = StatusValue;
                            }
                            else
                            {
                                status[i] = 0;
                            }                                
                            time[i] = dt.Rows[i][2].ToString();
                            if (int.TryParse(dt.Rows[i][4].ToString(), out int idValue))
                            {
                                authorid[i] = idValue;
                            }
                            else
                            {
                                status[i] = 0;
                            }
                        }
                        return new ListMessageOutputModel()
                        {
                            Content = content,
                            AuthorAvatar = authoravatar,
                            Status = status,
                            Time = time,
                            AuthorID = authorid,
                            Count = count,
                        };                        
                    }
                }
            }
        }
    }
}
