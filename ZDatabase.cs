using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text.RegularExpressions;

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

        public SQLiteConnection m_dbConnection;

        private static readonly Regex ValidTableNameRegex = new Regex(@"^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);

        private static void ValidateTableName(string tableName)
        {
            if (!ValidTableNameRegex.IsMatch(tableName))
                throw new ArgumentException("表名包含非法字符: " + tableName);
        }

        public SQLiteDataReader GetReaderBySql(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_dbConnection;
            cmd.CommandText = sql;
            SQLiteDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public Dictionary<string, T> GetParams<T>(string tableName) where T : ParamBase, new()
        {
            ValidateTableName(tableName);

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
                if (Param_temp.ename == "test")
                {
                    Param_temp.ValueChanged += log_level_changed;
                }
            }
            return Param_temps;
        }

        public void log_level_changed(object sender, EventArgs e)
        {
            GlobalVar.log?.AppandText("参数值变更");
        }

        public List<string> GetAllProductNames()
        {
            string sql = "SELECT name FROM sqlite_master WHERE type = 'table' and name LIKE 'pro_%';";
            SQLiteDataReader reader = GetReaderBySql(sql);
            List<string> ProductNames = new List<string>();
            while (reader.Read())
            {
                ProductNames.Add(reader.GetString(0).Substring(4, reader.GetString(0).Length - 4));
            }
            return ProductNames;
        }

        public string CreateProductParamTable(string name, string form_name = "")
        {
            ValidateTableName(name);

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_dbConnection;
            if (string.IsNullOrEmpty(form_name))
            {
                ValidateTableName(name);
                cmd.CommandText = "CREATE TABLE pro_" + name + " AS SELECT * FROM pro_default;";
            }
            else
            {
                ValidateTableName(form_name);
                cmd.CommandText = "CREATE TABLE pro_" + name + " AS SELECT * FROM pro_" + form_name + ";";
            }

            try
            {
                cmd.ExecuteNonQuery();
                return name;
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("创建产品表失败: " + name + " - " + ex.Message);
                return "";
            }
        }

        public bool DelectProductParamTable(string name)
        {
            if (name == "default")
            {
                return false;
            }
            ValidateTableName(name);

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_dbConnection;
            cmd.CommandText = "DROP TABLE pro_" + name + ";";
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("删除产品表失败: " + name + " - " + ex.Message);
                return false;
            }
        }

        public bool UpdateValueById(string tablename, string id, string value)
        {
            ValidateTableName(tablename);

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = m_dbConnection;
            cmd.CommandText = "UPDATE " + tablename + " SET value=@value WHERE id=@id";
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("更新参数失败: " + tablename + " id=" + id + " - " + ex.Message);
                return false;
            }
        }
    }
}
