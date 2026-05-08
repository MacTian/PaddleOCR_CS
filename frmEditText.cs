using System;
using System.Windows.Forms;

namespace CV_app
{
    public partial class frmEditText : Form
    {
        public string id;
        public string ename;
        public string value;
        public string type;
        public string setting;

        public frmEditText()
        {
            InitializeComponent();
        }

        private void frmEditText_Load(object sender, EventArgs e)
        {
            txtValue.Text = value;
            if (setting != null && setting.Length > 0)
            {
                string[] items = setting.Split(',');
                cmbValue.Items.AddRange(items);
                cmbValue.Visible = true;
                txtValue.Visible = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string newValue = cmbValue.Visible ? cmbValue.Text : txtValue.Text;
            ZDatabase.Instance().UpdateValueById("app_ccd", id, newValue);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
