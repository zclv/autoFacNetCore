using MySql.Data.MySqlClient;
using NetCore.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class UserContext
    {
        public string ConnectionString { get; set; }
        public UserContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public List<userDto> GetAllUser()
        {
            List<userDto> list = new List<userDto>();
            //连接数据库
            using (MySqlConnection msconnection = GetConnection())
            {
                msconnection.Open();
                //查找数据库里面的表
                MySqlCommand mscommand = new MySqlCommand("select host,user,authentication_string from user", msconnection);
                using (MySqlDataReader reader = mscommand.ExecuteReader())
                {
                    //读取数据
                    while (reader.Read())
                    {
                        list.Add(new userDto()
                        {
                            host = reader.GetString("host"),
                            user = reader.GetString("user"),
                            authentication_string = reader.GetString("authentication_string")
                        });
                    }
                }
            }
            return list;
        }
    }
}
