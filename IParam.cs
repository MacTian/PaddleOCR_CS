using System;

namespace CV_app
{
    public class ParamBase
    {
        public event EventHandler<EventArgs> ValueChanged;
        public virtual string tablename { get; set; }
        public virtual string name { get; set; }
        public virtual string ename { get; set; }
        public virtual string setting { get; set; }
        public virtual int id { get; set; }
        public virtual int index { get; set; }

        private string _value = "";

        public virtual string value
        {
            get { return _value; }
            set
            {
                if (_value != "")
                {
                    ZDatabase.Instance().UpdateValueById(tablename, id.ToString(), value);
                }
                ValueChanged?.Invoke(this, EventArgs.Empty);
                _value = value;
            }
        }

        public virtual string note { get; set; }
        public virtual string type { get; set; }

        public virtual string build_value()
        {
            return value;
        }
    }

    public class AppParam_Laser : ParamBase { }
    public class AppParam_CCD : ParamBase { }
    public class AppParam_Other : ParamBase { }
    public class ProParam : ParamBase { }
}
