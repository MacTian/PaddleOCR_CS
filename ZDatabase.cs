//using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CV_app
{
    public class ZDatabase
    {
        private static ZDatabase zDatabase;
        private static readonly object locker = new object();
        private ZDatabase()
        {
            m_dbConnection = new SQLiteConnection("Data Source = app.cfg");
            m_dbConnection.Open();
        }

        /// <summary>
        /// 获取数据库操作类的实例
        /// </summary>
        /// <returns></returns>
        public static ZDatabase Instance()
        {
            if (zDatabase == null)
            {
                lock (locker)
                {
                    if (zDatabase == null)
                    {
                        zDatabase = new ZDatabase();
                    }
                }
            }
            return zDatabase;
        }

        /// <summary>
        ///  数据库连接
        /// </summary>
        public SQLiteConnection m_dbConnection;


        /// <summary>
        /// 获取sql语句的执行结果
        /// </summary>
        public SQLiteDataReader GetReaderBySql(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_dbConnection;
            cmd.CommandText = sql;
            SQLiteDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public Dictionary<string, T> GetParams<T>(string tableName) where T : ParamBase,new()
        {
            string sql = "SELECT id,name,ename,value,setting,note,type FROM " + tableName;
            SQLiteDataReader reader = GetReaderBySql(sql);
            Dictionary<string, T> Param_temps = new Dictionary<string, T>();
            while (reader.Read())
            {
                T Param_temp = new T();
                Param_temp.id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                Param_temp.name = reader.IsDBNull(1) ? "" : reader.GetString(1);
                Param_temp.ename = reader.IsDBNull(2) ? "" : reader.GetString(2);
                Param_temp.value = reader.IsDBNull(3) ? "" : reader.GetString(3);
                Param_temp.setting = reader.IsDBNull(4) ? "" : reader.GetString(4);
                Param_temp.note = reader.IsDBNull(5) ? "" : reader.GetString(5);
                Param_temp.type = reader.IsDBNull(6) ? "" : reader.GetString(6);
                Param_temp.tablename = tableName;
                Param_temps.Add(Param_temp.ename, Param_temp);
                if (Param_temp.ename=="test")
                {
                    Param_temp.ValueChanged += log_level_changed;
                }
            }
            return Param_temps;
        }
        public void log_level_changed(object sender, EventArgs e)
        {
            Console.WriteLine("变更更");
        }

        public List<string> GetAllProductNames()
        {
            string sql = "SELECT name FROM sqlite_master WHERE type = 'table' and name LIKE 'pro_%';";
            SQLiteDataReader reader = GetReaderBySql(sql);
            List<string> ProductNames = new List<string>();
            while (reader.Read())
            {
                ProductNames.Add(reader.GetString(0).Substring(4, reader.GetString(0).Length-4));
            }
            return ProductNames;
        }

        // 创建产品参数
        public string CreateProductParamTable(string name,string form_name="")
        {
            // 如果表不存在创建表
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_dbConnection;
            if (form_name == "")
            {
                cmd.CommandText = "CREATE TABLE  pro_" + name + "  AS SELECT * FROM pro_default;";

            }
            else
            {
                cmd.CommandText = "CREATE TABLE  pro_" + name + "  AS SELECT * FROM pro_"+ form_name + ";";
            }

            try
            {
                cmd.ExecuteNonQuery();
                //ZGlobalVar.ProductNameSelected = name;
                return name;
            }
            catch (Exception)
            {
                return "";
            }
        }
        // 删除产品参数
        public bool DelectProductParamTable(string name)
        {
            if (name=="default")
            {
                return false;
            }
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_dbConnection;
            cmd.CommandText = "DROP TABLE  pro_" + name + ";";
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateValueById(string tablename,string id,string value)
        {
            // 如果表不存在创建表
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_dbConnection;
            cmd.CommandText = "UPDATE   " + tablename + " SET value='"+value+"' WHERE id="+id;
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
