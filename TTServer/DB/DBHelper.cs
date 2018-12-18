using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TTServer
{
    public class DBHelper : IDisposable
    {
        public static DBHelper Instance { get; } = new DBHelper();

        public const string CONNECTIONSTRING =
            "server =DESKTOP-A5R9VN4;uid = sa;pwd =123456;database=TT;";

        private Queue<SqlConnection> connections;
        private const int MaxConnections = 50;
        private bool isDispose = false;

        #region 私有构造

        private DBHelper()
        {
            connections = new Queue<SqlConnection>();
            try
            {
                for (int i = 0; i < MaxConnections; i++)
                {
                    SqlConnection conn = new SqlConnection(CONNECTIONSTRING);
                    Connect(conn);
                    connections.Enqueue(conn);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("无法连接上数据库");
                Console.WriteLine(e);
                connections.Clear();
                throw;
            }
           
        }
       
        #endregion

        /// <summary>
        /// 获取一个connection 
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection()
        {
            return connections.Dequeue();
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        private SqlConnection Connect(SqlConnection conn)
        {

            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine("链接数据库的时候实现异常：" + e);
                return null;
            }

        }

        /// <summary>
        /// 返还这个conn
        /// </summary>
        /// <param name="conn"></param>
        public static void CloseConnection(SqlConnection conn)
        {
            if (conn != null)
                Instance.connections.Enqueue(conn);
            else
            {
                Console.WriteLine("SqlConnection不能为空");
            }
        }

        #region 资源销毁

        public virtual void Dispose()
        {
            if (!isDispose)
            {
                try
                {
                    foreach (var conn in connections)
                    {
                        conn.Close();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    isDispose = true;
                    GC.SuppressFinalize(this);
                }

            }
        }

        ~DBHelper()
        {
            Dispose();
        }

        #endregion

    }
}
