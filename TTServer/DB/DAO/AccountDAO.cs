using System;
using System.Data;
using System.Data.SqlClient;
using Core;
namespace TTServer
{
    public class AccountDAO
    {
        /// <summary>
        /// 查询账号密码是否正确
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Account VerifyAccount(SqlConnection conn, string email, string password, string phone)
        {
            string sqlStr = string.Empty;
            SqlCommand command = new SqlCommand(sqlStr, conn);
            try
            {
                if (email != null)
                {
                    sqlStr = "select * from Account where email=@email and password=@password";
                    command.CommandText = sqlStr;
                }
                else
                {
                    sqlStr = "select * from Account where phone=@phone and password=@password";
                    command.CommandText = sqlStr;
                }

                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string username = reader.GetString(1);
                    Console.WriteLine($"查询成功，当前查询用户名为{username}");
                    string newemail = reader.GetString(3);
                    string newphone = reader.GetString(4);
                    Account user = new Account()
                    { UserName= username, Password = password, Email = email, Phone = newphone ,Id=id};
                    reader?.Close();
                    return user;
                }

                return null;
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
        /// 判断注册信息是否已经存在，比如手机号或者邮箱是否已经存在
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="account"></param>
        /// <returns>0：不存在，其他值：错误信息<see cref="ErrorCode"/></returns>
        public int ExistAccount(SqlConnection conn, Account account)
        {
            string sqlStr = string.Empty;
            SqlCommand command = new SqlCommand(sqlStr, conn);
            try
            {
                sqlStr = "select * from Account where email=@email";
                command.CommandText = sqlStr;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = account.Email;
                var reader = command.ExecuteReader();
               
                if (reader.HasRows)
                {
                    return Core.ErrorCode.ERR_EXISTEMAIL;
                }
                reader.Close();

                sqlStr = "select * from Account where phone=@phone";
                command.CommandText = sqlStr;
                command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = account.Phone;
                reader=command.ExecuteReader();
                if (reader.HasRows)
                {
                    return Core.ErrorCode.ERR_EXISTPHONE;
                }
                reader.Close();
                return 0;
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
        /// 插入一条新的数据
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public int InsertAccount(SqlConnection conn, Account account)
        {
            string sqlStr = string.Empty;
            SqlCommand command = new SqlCommand(sqlStr, conn);
            try
            {
                sqlStr = "insert into Account values(@username,@password,@email,@phone)";
                command.CommandText = sqlStr;
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = account.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = account.Password;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = account.Email;
                command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = account.Phone;

                return command.ExecuteNonQuery();
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
        public Account GetAccountById(SqlConnection conn,int id)
        {
            string sqlStr = string.Empty;
            SqlCommand command = new SqlCommand(sqlStr, conn);
            try
            {
                sqlStr = "select * from Account where id=@id";
                command.CommandText = sqlStr;
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string username = reader.GetString(1);
                    Console.WriteLine($"查询成功，当前查询用户名为{username}");
                    string password = reader.GetString(2);
                    string newemail = reader.GetString(3);
                    string newphone = reader.GetString(4);
                    Account user = new Account()
                    { UserName = username, Password = password, Email = newemail, Phone = newphone };
                    return user;
                }

                return null;
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

        public int GetAccountIdByEmail(SqlConnection conn,string email)
        {
            string sqlStr = string.Empty;
            SqlCommand command = new SqlCommand(sqlStr, conn);
            try
            {
                sqlStr = "select * from Account where email=@email";
                command.CommandText = sqlStr;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return reader.GetInt32(0);
                }

                return -1;
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
        public int GetAccountByByPhone(SqlConnection conn,string phone)
        {
            string sqlStr = string.Empty;
            SqlCommand command = new SqlCommand(sqlStr, conn);
            try
            {
                sqlStr = "select * from Account where phone=@phone";
                command.CommandText = sqlStr;
                command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phone;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return reader.GetInt32(0);
                }

                return -1;
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
