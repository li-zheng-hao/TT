using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TTServer
{
    public class FriendDAO
    {
        /// <summary>
        /// 获得好友id
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public List<int> GetFriendIdList(SqlConnection conn,int ownerId)
        {
            var result =new List<int>();
            string sqlStr = string.Empty;
            SqlCommand command = new SqlCommand(sqlStr, conn);
            try
            {
                sqlStr = "select * from Friend where ownerid=@ownerid";
                command.CommandText = sqlStr;
                command.Parameters.Add("@ownerid", SqlDbType.Int).Value = ownerId;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(2);
                    result.Add(id);
                }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {

                DBHelper.CloseConnection(conn);
            }
            
        }
        /// <summary>
        /// 插入好友关系
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="ownerid">用户ID</param>
        /// <param name="friendid">好友ID</param>
        /// <returns>插入成功返回true</returns>
        public bool InsertFriend(SqlConnection conn,int ownerid,int friendid)
        {
            string sqlStr = string.Empty;
            SqlCommand command = new SqlCommand(sqlStr, conn);
            try
            {
                sqlStr = "insert into Friend values(@ownerid,@friendid)";
                command.CommandText = sqlStr;
                command.Parameters.Add("@ownerid", SqlDbType.Int).Value = ownerid;
                command.Parameters.Add("@friendid", SqlDbType.Int).Value = friendid;

                var result = command.ExecuteNonQuery();
                return result == 1 ? true : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                DBHelper.CloseConnection(conn);
            }

        }
    }
}
