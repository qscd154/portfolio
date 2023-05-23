using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace emedit
{
    public partial class Form7 : LoginForm.LoginForm
    {
        private OracleConnection LocalConn;

        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

            string id = base.textid.Text;
            string pw = base.textpwd.Text;
            string shapw = SHA256Hash(pw);
            OracleDataReader myReader;
            string sql = null;
            try
            {
                LocalConn = DBConnection.DBCon();
                LocalConn.Open();
                sql = "select PASSWD from USR_MST where USRID = '" + id + "'";
                myReader = DBConnection.DataSelect(sql, LocalConn);
                if (myReader.Read())
                {
                    string row = myReader.GetString(0);
                    bool result = isSame(pw, row);
                    if (result)
                    {
                        MessageBox.Show("로그인 완료");
                    }
                    else
                    {
                        MessageBox.Show("로그인 실패");
                    }
                }
                else
                {
                    MessageBox.Show("비밀번호가 틀렸거나 잘못된 아이디 입니다.");
                }

            }
            catch (Exception ex) { }
        }

        public static string SHA256Hash(string data)
        {
            System.Security.Cryptography.SHA256 sha = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        public static bool isSame(string pw1, string pw2)
        {
            string shapw = SHA256Hash(pw1);
            if (pw2 == shapw)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
