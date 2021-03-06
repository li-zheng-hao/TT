﻿using System;
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
            string sqlStr1 = string.Empty;
            SqlCommand command1 = new SqlCommand(sqlStr1, conn);
            string sqlStr2 = string.Empty;
            SqlCommand command2 = new SqlCommand(sqlStr2, conn);
            try
            {
                sqlStr1 = "insert into Friend values(@ownerid,@friendid)";
                command1.CommandText = sqlStr1;
                command1.Parameters.Add("@ownerid", SqlDbType.Int).Value = ownerid;
                command1.Parameters.Add("@friendid", SqlDbType.Int).Value = friendid;

                var result1 = command1.ExecuteNonQuery();

                sqlStr2 = "insert into Friend values(@ownerid,@friendid)";
                command2.CommandText = sqlStr2;
                command2.Parameters.Add("@ownerid", SqlDbType.Int).Value = friendid;
                command2.Parameters.Add("@friendid", SqlDbType.Int).Value = ownerid;

                var result2 = command2.ExecuteNonQuery();

                return result1==1 && result2==1;

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
