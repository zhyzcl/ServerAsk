using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App;
using App.Win;
using App.Web;
using wapp;


namespace ServerAsk
{
    public partial class FrmInterfaceTest : Form
    {
        /// <summary>请求Url的cookie</summary>
        public System.Net.CookieContainer cookie;

        public FrmInterfaceTest()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FrmInterfaceTest_Load(object sender, EventArgs e)
        {
            cookie = new System.Net.CookieContainer();

            cBmethod.Items.Add(new App.ValTxt("POST", "POST"));
            cBmethod.Items.Add(new App.ValTxt("GET", "GET"));
            cBmethod.SelectedIndex = 0;

            cBencode.Items.Add(new App.ValTxt("utf-8", "utf-8"));
            cBencode.Items.Add(new App.ValTxt("gb2312", "gb2312(简体中文)"));
            cBencode.Items.Add(new App.ValTxt("big5", "big5(繁体中文)"));
            cBencode.SelectedIndex = 0;

            cBcontype.Items.Add(new App.ValTxt("application/json;charset=utf-8", "application/json;charset=utf-8"));
            cBcontype.Items.Add(new App.ValTxt("application/x-www-form-urlencoded;charset=utf-8", "application/x-www-form-urlencoded;charset=utf-8"));
            cBcontype.Items.Add(new App.ValTxt("text/xml", "text/xml"));
            cBcontype.SelectedIndex = 0;

            cBuncode.Items.Add(new App.ValTxt("0", "不解码"));
            cBuncode.Items.Add(new App.ValTxt("1", "escape解码"));
            cBuncode.Items.Add(new App.ValTxt("2", "Url解码(UrlDecode)"));
            cBuncode.Items.Add(new App.ValTxt("3", "encodeURIComponent 解码"));
            cBuncode.SelectedIndex = 1;

            cBformat.Items.Add(new App.ValTxt("0", "不格式化"));
            cBformat.Items.Add(new App.ValTxt("1", "JSON格式化"));
            cBformat.SelectedIndex = 1;
        }

        public bool IsRunOper()
        {
            DateTime dqrq = DateTime.Now;
            string acturl = rTBActionUrl.Text.Trim();
            if (acturl == "")
            {
                WinOften.MessShow("访问地址不能为空！", 1);
                return false;
            }
            return true;
        }

        private void bttest_Click(object sender, EventArgs e)
        {
            if (!IsRunOper())
            {
                return;
            }
            string acturl = rTBActionUrl.Text.Trim();
            string reqdata = rTBdata.Text.Trim();
            string method = ((App.ValTxt)cBmethod.SelectedItem).Value.Trim();
            string uncode = ((App.ValTxt)cBuncode.SelectedItem).Value.Trim();
            string format = ((App.ValTxt)cBformat.SelectedItem).Value.Trim();
            string encode = cBencode.Text.Trim();
            string contype = cBcontype.Text.Trim();
            string errs = "";
            string rws = wapp.AppPub.RequestWebServer(acturl, method, contype, encode, reqdata, ref cookie, ref errs);
            if (errs == "")
            {
                if (uncode == "1")
                {
                    rws = Often.Unescape(rws);
                }
                else if (uncode == "2")
                {
                    rws = Often.UrlDes(rws);
                }
                else if (uncode == "3")
                {
                    rws = Often.DecodeURI(rws);
                }
                if (format == "1")
                {
                    string ferrs = "";
                    string frws = wapp.AppPub.FormatJsonString(rws, ref ferrs);
                    if (ferrs != "")
                    {
                        rws = "格式化数据发生错误！错误信息：" + ferrs + "，原始数据：\n" + rws;
                    }
                    else
                    {
                        rws = frws;
                    }
                }
                rTBout.Text = rws;
            }
            else
            {
                rTBout.Text = errs;
            }
        }
    }
}
