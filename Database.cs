using System;

namespace CV_app
{
    internal class Database
    {
        public string getValue(string key)
        {
            try
            {
                if (GlobalVar.ZAppParam_CCDs.ContainsKey(key))
                    return GlobalVar.ZAppParam_CCDs[key].value;
                GlobalVar.log?.AppandText("配置项不存在: " + key);
                return null;
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("读取配置失败: " + key + " - " + ex.Message);
                return null;
            }
        }

        public bool setValue(string key, string val)
        {
            try
            {
                if (GlobalVar.ZAppParam_CCDs.ContainsKey(key))
                {
                    GlobalVar.ZAppParam_CCDs[key].value = val;
                    return true;
                }
                GlobalVar.log?.AppandText("配置项不存在: " + key);
                return false;
            }
            catch (Exception ex)
            {
                GlobalVar.log?.AppandText("写入配置失败: " + key + " - " + ex.Message);
                return false;
            }
        }
    }
}
