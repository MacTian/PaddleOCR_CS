using System;
using System.Windows.Forms;

namespace CV_app
{
    public partial class frmEditText : Form
    {
        //public string tablename;
        public string id;
        public string ename;
        public string type;
        public string value;
        public string setting;
        private string _value;

        public frmEditText()
        {
            InitializeComponent();
        }

        private void frmEditText_Load(object sender, EventArgs e)
        {
            _value = value;
            switch (type)
            {
                case "file":
                    {
                        btnFile.Visible = true;
                        txtValue.Visible = true;
                        txtValue.Text = value;
                        break;
                    }
                case "list":
                    {
                        cbValue.Visible = true;
                        string[] values = setting.Split(',');
                        cbValue.Items.AddRange(values);
                        cbValue.Text = value;
                        break;
                    }
                case "bool":
                    {
                        cbValue.Visible = true;
                        cbValue.Items.Add("true");
                        cbValue.Items.Add("false");
                        cbValue.Text = value;
                        break;
                    }
                default:
                    txtValue.Width += btnFile.Width;
                    txtValue.Visible = true;
                    txtValue.Text = value;
                    break;
            }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtValue.Text = dlg.FileName;
            }
        }

        private void frmEditText_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cbValue_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            _value = txtValue.Text;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_value != value)
            {

                GlobalVar.ZAppParam_CCDs[ename].value = _value;
                GlobalVar.frmSetting.BuildDataGridView();
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
