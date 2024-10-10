using ChatBusinessLogic.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ChatBusinessLogic.BusinessLogic
{
    public class FriendsBll
    {
        public FriendOutputModel Friend(FriendModel model)
        {
            string oradb = "User Id=HTML;Password=22112004;Data Source=SignalAndSystem:1521/ORCL;";


            using (OracleConnection conn = new OracleConnection(oradb))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT userinfo.user_id, userinfo.name, userinfo.status, userinfo.avatar,
                                        (SELECT content FROM message WHERE message.relationship_id = relationship.relationship_id
                                        ORDER BY id DESC FETCH FIRST 1 ROW ONLY) AS content,
                                        (SELECT id FROM message WHERE message.relationship_id = relationship.relationship_id
                                        ORDER BY id DESC FETCH FIRST 1 ROW ONLY) AS id,
                                        relationship_id
                                        FROM userinfo INNER JOIN relationship ON ( userinfo.user_id = relationship.user2_id and relationship.user1_id =: userid)
                                        or (userinfo.user_id = relationship.user1_id and relationship.user2_id =: userid) order by id desc nulls last";
                    cmd.Parameters.Add(new OracleParameter("userid", model.UserID));
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        if (dt.Rows.Count >= 1)
                        {
                            string[] name = new string[dt.Rows.Count];
                            string[] lastmessage = new string[dt.Rows.Count];
                            int[] status = new int[dt.Rows.Count];
                            int[] relationshipid = new int[dt.Rows.Count];
                            string[] avatar = new string[dt.Rows.Count];
                            int count=dt.Rows.Count;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                name[i] = dt.Rows[i][1]?.ToString() ?? string.Empty; // Default to empty string
                                lastmessage[i] = dt.Rows[i][4]?.ToString() ?? string.Empty;
                                
                                if (int.TryParse(dt.Rows[i][6].ToString(), out int idValue))
                                {
                                    relationshipid[i] = idValue;
                                }
                                else
                                {
                                    relationshipid[i] = 0;
                                }
                                if (int.TryParse(dt.Rows[i][1].ToString(), out int statusValue))
                                {
                                    status[i] = statusValue;
                                }
                                else
                                {
                                    status[i] = 0; 
                                }
                                avatar[i] = dt.Rows[i][3]?.ToString() ?? string.Empty;
                            }
                            return new FriendOutputModel()
                            {
                                Name = name,
                                LastMessage = lastmessage,
                                Status = status,
                                Avatar = avatar,
                                Count = count,
                                RelationshipID = relationshipid,
                            };
                        }
                        else
                        {
                            return new FriendOutputModel()
                            {
                                Count = 0,
                            };
                        }
                    }
                }
            }            
        }
    }
}
