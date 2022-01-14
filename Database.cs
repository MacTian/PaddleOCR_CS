using System;

namespace CV_app
{
    internal class Database
    {
        public string getValue(string key)
        {
            try
            {
                return GlobalVar.ZAppParam_CCDs[key].value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(key);
                throw;
            }
        }

        public bool setValue(string key, string val)
        {
            try
            {
                GlobalVar.ZAppParam_CCDs[key].value = val;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(key);
                throw;
            }
        }
    }
}