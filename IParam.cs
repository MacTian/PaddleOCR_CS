using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_app
{
    public class ParamBase
    {
        public event EventHandler<EventArgs> ValueChanged;
        public virtual string tablename { get; set; }
        /// <summary>
        /// 参数名（中文）
        /// </summary>
        public virtual string name { get; set; }
        /// <summary>
        /// 参数名（英文）
        /// </summary>
        public virtual string ename { get; set; }
        /// <summary>
        /// 参数设置字符串
        /// 可选项1,可选项2
        /// </summary>
        public virtual string setting { get; set; }
        /// <summary>
        /// 数据库ID
        /// </summary>
        public virtual int id { get; set; }
        /// <summary>
        /// DataGridView中的index
        /// </summary>
        public virtual int index { get; set; }

        private string _value="";

        /// <summary>
        /// 参数值,赋值后会自动保存到数据库
        /// </summary>
        public virtual string value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value!="")
                {
                        ZDatabase.Instance().UpdateValueById(tablename, id.ToString(), value);
                }
                // 触发事件
                ValueChanged?.Invoke(this,EventArgs.Empty);
                _value = value;
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string note { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public virtual string type { get; set; }

        /// <summary>
        /// 根据参数字符串构建参数
        /// </summary>
        public virtual string build_value()
        {
            return value;
        }
    }
    // 标刻卡参数
    public class AppParam_Laser : ParamBase
    {
        
    }
    // CCD 参数
    public class AppParam_CCD : ParamBase
    {
    }
    // 其他参数
    public class AppParam_Other : ParamBase
    {
    }

    // 产品参数
    public class ProParam : ParamBase
    {

    }
}
