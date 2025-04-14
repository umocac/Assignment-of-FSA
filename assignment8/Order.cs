using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace OrderWeb
{
    public class ODmysql
    {
        string connectStr = "server=localhost;port=3306;database=orderdb;user=root;password=123456;";
        MySqlConnection cnn;
        public ODmysql()
        {
            cnn = new MySqlConnection(connectStr);
            cnn.Open();
            string query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'orderdb' AND TABLE_NAME = 'orderdetail';";
            MySqlCommand command = new MySqlCommand(query, cnn);
            int tableCount = Convert.ToInt32(command.ExecuteScalar());
            if (tableCount > 0)
            {
                Console.WriteLine("orderdetail exists");
            }
            else
            {
                query = "CREATE TABLE orderdetail(" +
                    "id VARCHAR(100) PRIMARY KEY, " +
                    "obname VARCHAR(100) NOT NULL, " +
                    "client VARCHAR(100) NOT NULL, " +
                    "price DOUBLE NOT NULL);";
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
            cnn.Close();
            command.Dispose();
        }
        public void AddData(string id, string obname, string client, double price)
        {
            cnn.Open();
            string query = "INSERT INTO orderdetail value(@id,@obname,@client,@price);";
            MySqlCommand command = new MySqlCommand(query, cnn);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@obname", obname);
            command.Parameters.AddWithValue("@client", client);
            command.Parameters.AddWithValue("@price", price);
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                cnn.Close();
                command.Dispose();
                throw new ODopFail("订单号相同，创建失败！");
            }
            cnn.Close();
            command.Dispose();
        }
        public bool DeleteData(string id)
        {
            cnn.Open();
            string query = "DELETE FROM orderdetail where id = @id;";
            MySqlCommand command = new MySqlCommand(query, cnn);
            command.Parameters.AddWithValue("@id", id);
            if (command.ExecuteNonQuery() <= 0)
            {
                cnn.Close();
                command.Dispose();
                return false;
            }
            cnn.Close();
            command.Dispose();
            return true;
        }
        public void ChangeData(string id, string obname, string client, double price)
        {
            if (obname == "null" && client == "null" && price == -1) 
            {
                throw new ODopFail("需要修改的信息不能为空！");
            }
            bool FronNull = true;
            cnn.Open();
            string query = "update orderdetail set ";
            List<MySqlParameter> pmt = new List<MySqlParameter>();
            if (obname != "null")
            {
                query += "obname = @obname";
                pmt.Add(new MySqlParameter("@obname", obname));
                FronNull = false;
            }
            if (client != "null")
            {
                query += FronNull ? "client = @client" : ",client = @client";
                pmt.Add(new MySqlParameter("@client", client));
                FronNull = false;
            }
            if (price != -1)
            {
                query += FronNull ? "price = @price" : ",price = @price";
                pmt.Add(new MySqlParameter("@price", price));
            }
            query += " where id = @id;";
            pmt.Add(new MySqlParameter("@id", id));
            MySqlCommand command = new MySqlCommand(query, cnn);
            command.Parameters.AddRange(pmt.ToArray());
            if (command.ExecuteNonQuery() <= 0)
            {
                cnn.Close();
                command.Dispose();
                throw new ODopFail("找不到对应数据！");
            }
            cnn.Close();
            command.Dispose();
        }
        public DataSet SeleteAll()
        {
            string query = "SELECT * FROM orderdetail;";
            DataSet ds = new DataSet();
            MySqlDataAdapter mda = new MySqlDataAdapter(query, connectStr);
            mda.Fill(ds);
            mda.Dispose();
            return ds;
        }
        public DataSet DSSort(string way)
        {
            string query = $"SELECT * FROM orderdetail order by {way};";
            DataSet ds = new DataSet();
            MySqlDataAdapter mda = new MySqlDataAdapter(query, connectStr);
            mda.Fill(ds);
            mda.Dispose();
            return ds;
        }
        public DataSet FindOrder(string id, string obname, string client, double price)
        {
            string baseQuery = "SELECT * FROM orderdetail WHERE ";
            List<string> conditions = new List<string>();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            if (id != "null")
            {
                conditions.Add("id = @id");
                parameters.Add(new MySqlParameter("@id", id));
            }
            if (obname != "null")
            {
                conditions.Add("obname = @obname");
                parameters.Add(new MySqlParameter("@obname", obname));
            }
            if (client != "null")
            {
                conditions.Add("client = @client");
                parameters.Add(new MySqlParameter("@client", client));
            }
            if (price != -1)
            {
                conditions.Add("price = @price");
                parameters.Add(new MySqlParameter("@price", price));
            }

            if (conditions.Count == 0)
            {
                throw new ODopFail("至少填写一项！");
            }

            string query = baseQuery + string.Join(" AND ", conditions) + " ORDER BY id;";

            DataSet ds = new DataSet();
            using (MySqlConnection cnn = new MySqlConnection(connectStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    using (MySqlDataAdapter mda = new MySqlDataAdapter(cmd))
                    {
                        try
                        {
                            mda.Fill(ds);
                        }
                        catch (MySqlException ex)
                        {
                            throw new ODopFail("发生错误！");
                        }
                    }
                }
            }
            return ds;
        }

    }

    public class ODopFail : ApplicationException
    {
        public ODopFail(string message) : base(message) { }
    }
}