using System;
using System.Data;
using System.Data.SqlClient;

namespace TTServer
{
    public class AccountDAO
    {
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
                    string username = reader.GetString(0);
                    Console.WriteLine($"查询成功，当前查询用户名为{username}");
                    string newemail = reader.GetString(2);
                    string newphone = reader.GetString(3);
                    Account user = new Account()
                    { UserName = username, Password = password, Email = email, Phone = newphone };
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
    }
}
