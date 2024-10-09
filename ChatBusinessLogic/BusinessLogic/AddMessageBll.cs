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
    public class AddMessageBll
    {
        public void AddMessage(AddMessageModel model)
        {
            string oradb = "User Id=HTML;Password=22112004;Data Source=localhost:1521/ORCL;";


            using (OracleConnection conn = new OracleConnection(oradb))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"insert into message(relationship_id,author_id,create_time,type,content,status) 
                                        values(:relationship_id,:author_id,sysdate,1,:content,0)";
                    cmd.Parameters.Add(new OracleParameter("relationship_id", model.RelationshipID));
                    cmd.Parameters.Add(new OracleParameter("author_id", model.AuthorID));
                    cmd.Parameters.Add(new OracleParameter("content", model.Content));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
