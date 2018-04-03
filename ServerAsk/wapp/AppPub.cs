using System;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Web.Security;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Runtime.InteropServices;
using App;
using App.Win;
using App.Web;

namespace wapp
{
    public class AppPub
    {
        /// <summary>返回请求测试数据</summary>
        /// <param name="action">action</param>
        /// <param name="sid">sid</param>
        /// <param name="param">param</param>
        /// <param name="datas">datas</param>
        /// <returns>返回请求测试数据</returns>
        public static string GetRequestTestData(string action, string sid, string param, string datas)
        {
            string time = DateTime.Now.ToString();//yyyy-MM-dd HH:mm:ss，编码
            string rannum = App.Often.GetRanChr();//数字和字母组成，无长度
            string md5ver = GetMD5Pwd(action + rannum + time + wapp.AppList.AppJsonKey + sid);
            StringBuilder sb = new StringBuilder();
            sb.Append("\"action\":\"" + App.Often.Escape(action) + "\"");
            sb.Append(",");
            sb.Append("\"md5ver\":\"" + App.Often.Escape(md5ver) + "\"");
            sb.Append(",");
            sb.Append("\"rannum\":\"" + App.Often.Escape(rannum) + "\"");
            sb.Append(",");
            sb.Append("\"time\":\"" + App.Often.Escape(time) + "\"");
            sb.Append(",");
            sb.Append("\"sessid\":\"" + App.Often.Escape(sid) + "\"");
            sb.Append(",");
            if (param != "")
            {
                sb.Append("\"param\":[" + param + "]");
            }
            else
            {
                sb.Append("\"param\":[]");
            }
            sb.Append(",");
            if (datas != "")
            {
                sb.Append("\"datas\":{" + datas + "}");
            }
            else
            {
                sb.Append("\"datas\":{}");
            }
            return App.Often.Trim("{" + sb.ToString() + "}", 3);
        }

        /// <summary>返回md5加密字符串</summary>
        /// <param name="pwd">加密字符串</param>
        /// <returns>返回md5加密字符串</returns>
        public static string GetMD5Pwd(string pwd)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToLower();
        }

        /// <summary>返回格式化后json字符串</summary>
        /// <param name="str">json字符串</param>
        /// <param name="errs">发生错误则返回错误信息</param>
        /// <returns>返回格式化后json字符串</returns>
        public static string FormatJsonString(string str, ref string errs)
        {
            try
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                TextReader tr = new StringReader(str);
                Newtonsoft.Json.JsonTextReader jtr = new Newtonsoft.Json.JsonTextReader(tr);
                object obj = serializer.Deserialize(jtr);
                if (obj != null)
                {
                    StringWriter textWriter = new StringWriter();
                    Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(textWriter)
                    {
                        Formatting = Newtonsoft.Json.Formatting.Indented,
                        Indentation = 4,
                        IndentChar = ' '
                    };
                    serializer.Serialize(jsonWriter, obj);
                    return textWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                errs = "格式化json失败，错误信息：" + ex.Message;
            }
            return str;
        }

        /// <summary>根据url访问地址访问指定的地址并返回访问地址返回的数据</summary>
        /// <param name="url">访问地址</param>
        /// <param name="method">请求模式</param>
        /// <param name="contype">请求数据类型</param>
        /// <param name="encode">编码</param>
        /// <param name="reqdata">请求数据</param>
        /// <param name="cookie">访问地址的cookie引用</param>
        /// <param name="errs">发生错误则返回错误信息</param>
        /// <returns>根据url访问地址访问指定的地址并返回访问地址返回的数据</returns>
        public static string RequestWebServer(string url, string method, string contype, string encode, string reqdata, ref CookieContainer cookie, ref string errs)
        {
            try
            {
                Encoding en = Encoding.GetEncoding(encode);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = cookie;
                request.Method = method;
                request.ContentType = contype;
                byte[] payload = en.GetBytes(reqdata);
                request.ContentLength = payload.Length;
                Stream writer = request.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = response.GetResponseStream();
                StreamReader Reader = new StreamReader(s, en);
                string postContent = Reader.ReadToEnd();
                Reader.Close();
                return postContent;
            }
            catch (Exception ex)
            {
                errs = ex.Message;
            }
            return "";
        }

        /// <summary>写操作日志文件</summary>
        /// <param name="savePath">日志保存路径</param>
        /// <param name="errsb">日志信息</param>
        /// <param name="errs">信息</param>
        public static void SaveServerErrLog(string savePath, ref StringBuilder errsb, string errs)
        {
            if (errsb.Length > 100000)
            {
                errsb = new StringBuilder();
            }
            errsb.Append(errs);
            try
            {
                FileStream fs = File.Create(savePath);
                byte[] bContent = Encoding.GetEncoding("utf-8").GetBytes(errsb.ToString());
                fs.Write(bContent, 0, bContent.Length);
                fs.Close();
            }
            catch
            {
            }
        }

        /// <summary>返回当前程序运行路径</summary>
        /// <returns>返回当前程序运行路径</returns>
        public static string GetAssemblyPath()
        {
            string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            _CodeBase = _CodeBase.Substring(8, _CodeBase.Length - 8); // 8是 file:// 的长度   
            string[] arrSection = _CodeBase.Split(new char[] { '/' });
            string _FolderPath = "";
            for (int i = 0; i < arrSection.Length - 1; i++)
            {
                _FolderPath += arrSection[i] + "\\";
            }
            return _FolderPath;
        }

        /// <summary>文本窗显示委托方法</summary>
        /// <param name="myControl">文本对象</param>
        /// <param name="myCaption">需要插入的文本</param>
        /// <param name="isqc">是否清空文本对象true 是 false 否</param>
        /// <param name="blxx">基础文本</param>
        public static void RTB(RichTextBox myControl, string myCaption, bool isqc, string blxx)
        {
            if (isqc)
            {
                myControl.Text = "";
                myControl.AppendText(blxx);
            }
            myControl.AppendText(myCaption);
            myControl.ScrollToCaret();
        }

        /// <summary>文本显示委托方法</summary>
        /// <param name="myLabel">文本对象</param>
        /// <param name="myCaption">需要显示的文本</param>
        public static void LB(Label myLabel, string myCaption)
        {
            myLabel.Text=myCaption;
        }

        /// <summary>返回object数组</summary>
        /// <param name="objs">object数组</param>
        public static object[] GetArray(params object[] objs)
        {
            return objs;
        }

        /// <summary>根据列表值返回列表名称</summary>
        /// <param name="cdt">列表</param>
        /// <param name="val">列表值</param>
        /// <returns>根据列表值返回列表名称</returns>
        public static string GetListValue(DataTable cdt, string val)
        {
            if (cdt != null && Often.IsInt32(val))
            {
                if (cdt.Rows.Count > 0)
                {
                    DataRow[] dr = cdt.Select("id=" + val);
                    if (dr.Length > 0)
                    {
                        return dr[0]["name"].ToString().Trim();
                    }
                }
            }
            return "";
        }

        /// <summary>自动调整图片控件大小</summary>
        /// <param name="pic">图片控件</param>
        /// <param name="pan">图片空间容器</param>
        public static void AutoPictureBoxSize(PictureBox pic, Panel pan)
        {
            if (pic.Image != null)
            {
                try
                {
                    System.Drawing.Image imfile = pic.Image;
                    int imsw = pan.Width - 1;
                    int imsh = pan.Height - 1;
                    int plx = pan.Width / 2;
                    int ply = pan.Height / 2;
                    Often.AutoSizeNarrow(ref imsw, ref imsh, imfile.Size.Width, imfile.Size.Height);
                    pic.Width = imsw;
                    pic.Height = imsh;
                    pic.Top = ply - (pic.Height / 2);
                    pic.Left = plx - (pic.Width / 2);
                }
                catch
                {
                }
            }
        }

        /// <summary>根据条件设置ListViewItem内的值</summary>
        /// <param name="itema">需要设置的ListViewItem</param>
        /// <param name="val">值</param>
        /// <param name="defval">默认值</param>
        /// <param name="isval">条件</param>
        public static void SetListViewItem(ListViewItem itema, string val, string defval, bool isval)
        {
            if (isval)
            {
                itema.SubItems.Add(val);
            }
            else
            {
                itema.SubItems.Add(defval);
            }
        }

        /// <summary>设置导入文件内存表结构</summary>
        /// <param name="dt">内存表</param>
        public static void SetDataFilesName(DataTable dt)
        {
            dt.TableName = "DataFilesName";
            dt.Columns.Add("AddDate", Type.GetType("System.DateTime"));
            dt.Columns.Add("ExportMode", Type.GetType("System.Int32"));
            dt.Columns.Add("StatDate", Type.GetType("System.DateTime"));
            dt.Columns.Add("EndDate", Type.GetType("System.DateTime"));
        }

        /// <summary>设置DataGridView列</summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="index">列索引</param>
        /// <param name="title">列名</param>
        /// <param name="width">宽度</param>
        /// <param name="readOnly">是否只读</param>
        public static void SetDataGridView(DataGridView dgv, int index, string title, int width, bool readOnly)
        {
            SetDataGridView(dgv, index, title, width, readOnly, true, DataGridViewColumnSortMode.Automatic);
        }

        /// <summary>设置DataGridView列</summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="index">列索引</param>
        /// <param name="title">列名</param>
        /// <param name="width">宽度</param>
        /// <param name="readOnly">是否只读</param>
        /// <param name="visible">是否显示</param>
        public static void SetDataGridView(DataGridView dgv, int index, string title, int width, bool readOnly, bool visible)
        {
            SetDataGridView(dgv, index, title, width, readOnly, visible, DataGridViewColumnSortMode.Automatic);
        }

        /// <summary>设置DataGridView列</summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="index">列索引</param>
        /// <param name="title">列名</param>
        /// <param name="width">宽度</param>
        /// <param name="readOnly">是否只读</param>
        /// <param name="visible">是否显示</param>
        /// <param name="dgvcs">列排序模式</param>
        public static void SetDataGridView(DataGridView dgv, int index, string title, int width, bool readOnly, bool visible, DataGridViewColumnSortMode dgvcs)
        {
            dgv.Columns[index].HeaderText = title;
            dgv.Columns[index].Width = width;
            dgv.Columns[index].ReadOnly = readOnly;
            dgv.Columns[index].Visible = visible;
            dgv.Columns[index].SortMode = dgvcs;
        }

        /// <summary>关闭子窗口,如果不传递需要关闭的窗口名称则关闭所有窗口，关闭成功返回true，否则返回false</summary>
        /// <param name="f">父窗口</param>
        /// <param name="formName">需要关闭的窗口名称集合</param>
        public static bool CloseForms(Form f, params string[] formNames)
        {
            List<string> li = new List<string>(formNames);
            bool isall = false;
            Form[] AddFrm = f.MdiChildren;
            for (int i = 0; i < AddFrm.Length; i++)
            {
                if (AddFrm[i] != null)
                {
                    bool isclose = false;
                    string formName = AddFrm[i].Name.Trim();
                    if (li.Count > 0)
                    {
                        if (li.IndexOf(formName) > -1)
                        {
                            isclose = true;
                        }
                    }
                    else
                    {
                        isclose = true;
                    }
                    if (isclose)
                    {
                        isclose = false;
                        if (!WinOften.IsFormAction(formName))
                        {
                            isclose = true;
                        }
                        else
                        {
                            if (MessageBox.Show("[" + AddFrm[i].Text + "]操作正在执行中，确认退出？", "系统警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                            {
                                isclose = true;
                            }
                        }
                        if (isclose)
                        {
                            AddFrm[i].Close();
                            AddFrm[i].Dispose();
                        }
                    }
                }
            }
            return isall;
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="cb">组合框</param>
        /// <param name="lists">列表字符串</param>
        /// <param name="selval">选中值</param>
        public static void SetComboBoxItems(ComboBox cb, string lists, string selval)
        {
            List<string> notVals = new List<string>();
            DataTable rdt = WebOften.StrToDataTable(lists);
            SetComboBoxItems(cb, rdt, 0, 1, 0, selval, notVals);
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="cb">组合框</param>
        /// <param name="lists">列表字符串</param>
        /// <param name="valnum">值索引</param>
        /// <param name="txtnum">文本索引</param>
        /// <param name="mode">组合框列模式：0 值加文本模式 1值模式</param>
        /// <param name="selval">选中值</param>
        public static void SetComboBoxItems(ComboBox cb, string lists, int valnum, int txtnum, int mode, string selval)
        {
            List<string> notVals = new List<string>();
            DataTable rdt = WebOften.StrToDataTable(lists);
            SetComboBoxItems(cb, rdt, valnum, txtnum, mode, selval, notVals);
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="cb">组合框</param>
        /// <param name="lists">列表字符串</param>
        /// <param name="valnum">值索引</param>
        /// <param name="txtnum">文本索引</param>
        /// <param name="mode">组合框列模式：0 值加文本模式 1值模式</param>
        /// <param name="selval">选中值</param>
        /// <param name="notVals">不出现在列表值的集合</param>
        public static void SetComboBoxItems(ComboBox cb, string lists, int valnum, int txtnum, int mode, string selval, List<string> notVals)
        {
            DataTable rdt = WebOften.StrToDataTable(lists);
            SetComboBoxItems(cb, rdt, valnum, txtnum, mode, selval, notVals);
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="cb">组合框</param>
        /// <param name="rdt">列表</param>
        /// <param name="valnum">值索引</param>
        /// <param name="txtnum">文本索引</param>
        /// <param name="mode">组合框列模式：0 值加文本模式 1值模式</param>
        /// <param name="selval">选中值</param>
        public static void SetComboBoxItems(ComboBox cb, DataTable rdt, int valnum, int txtnum, int mode, string selval)
        {
            List<string> notVals = new List<string>();
            SetComboBoxItems(cb, rdt, valnum, txtnum, mode, selval, notVals);
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="cb">组合框</param>
        /// <param name="rdt">列表</param>
        /// <param name="valnum">值索引</param>
        /// <param name="txtnum">文本索引</param>
        /// <param name="mode">组合框列模式：0 值加文本模式 1值模式</param>
        /// <param name="selval">选中值</param>
        /// <param name="notVals">不出现在列表值的集合</param>
        public static void SetComboBoxItems(ComboBox cb, DataTable rdt, int valnum, int txtnum, int mode, string selval, List<string> notVals)
        {
            cb.Items.Clear();
            int index = 0;
            for (int i = 0; i < rdt.Rows.Count; i++)
            {
                string val = rdt.Rows[i][valnum].ToString().Trim();
                string txt = rdt.Rows[i][txtnum].ToString().Trim();
                if (mode == 0)
                {
                    if (notVals.Count == 0 || notVals.IndexOf(val) > -1)
                    {
                        cb.Items.Add(new ValTxt(val, txt));
                        if (selval.Trim() != "" && selval == val)
                        {
                            cb.SelectedIndex = index;
                        }
                        index++;
                    }
                }
                else
                {
                    if (notVals.Count == 0 || notVals.IndexOf(val) > -1)
                    {
                        cb.Items.Add(val);
                        if (selval.Trim() != "" && selval == val)
                        {
                            cb.SelectedIndex = index;
                        }
                        index++;
                    }
                }
            }
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="selval">选中值</param>
        /// <param name="cb">组合框</param>
        /// <param name="vals">列表值集合</param>
        public static void SetComboBoxItems(string selval, ComboBox cb, params string[] vals)
        {
            List<string> notVals = new List<string>();
            SetComboBoxItems(selval, cb, notVals, vals);
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="selval">选中值</param>
        /// <param name="cb">组合框</param>
        /// <param name="notVals">不出现在列表值的集合</param>
        /// <param name="vals">列表值集合</param>
        public static void SetComboBoxItems(string selval, ComboBox cb, List<string> notVals, params string[] vals)
        {
            cb.Items.Clear();
            int index = 0;
            for (int i = 0; i < vals.Length; i++)
            {
                string val = vals[i].Trim();
                if (notVals.Count == 0 || notVals.IndexOf(val) > -1)
                {
                    cb.Items.Add(val);
                    if (selval.Trim() != "" && selval == val)
                    {
                        cb.SelectedIndex = index;
                    }
                    index++;
                }
            }
        }

        /// <summary>设置时分秒组合框列表内容</summary>
        /// <param name="cbs">时组合框</param>
        /// <param name="cbf">分组合框</param>
        /// <param name="cbm">秒组合框</param>
        /// <param name="tst">秒分秒字符串</param>
        public static void SetTimeComboBox(ComboBox cbs, ComboBox cbf, ComboBox cbm, string tst)
        {
            bool istst = false;
            if (tst != "")
            {
                string[] tarr = tst.Split(':');
                if (tarr.Length == 3)
                {
                    string s = tarr[0].Trim();
                    string f = tarr[1].Trim();
                    string m = tarr[2].Trim();
                    if (Often.IsInt32(s) && Often.IsInt32(f) && Often.IsInt32(m))
                    {
                        AppPub.SetComboBoxItems(cbs, AppList.Hour(), s);
                        AppPub.SetComboBoxItems(cbf, AppList.Minute(), f);
                        AppPub.SetComboBoxItems(cbm, AppList.Minute(), m);
                        istst = true;
                    }
                }
            }
            if (!istst)
            {
                AppPub.SetComboBoxItems(cbs, AppList.Hour(), "0");
                AppPub.SetComboBoxItems(cbf, AppList.Minute(), "0");
                AppPub.SetComboBoxItems(cbm, AppList.Minute(), "0");
            }
        }

        /// <summary>返回时分秒组合框时间字符串</summary>
        /// <param name="cbs">时组合框</param>
        /// <param name="cbf">分组合框</param>
        /// <param name="cbm">秒组合框</param>
        /// <returns>返回时分秒组合框时间字符串</returns>
        public static string GetTimeComboBox(ComboBox cbs, ComboBox cbf, ComboBox cbm)
        {
            string s = ((ValTxt)cbs.SelectedItem).Value;
            string f = ((ValTxt)cbf.SelectedItem).Value;
            string m = ((ValTxt)cbm.SelectedItem).Value;
            return s + ":" + f + ":" + m;
        }

        /// <summary>根据时间返回日期</summary>
        /// <param name="dTime">指定日期</param>
        /// <param name="hour">小时数</param>
        /// <returns>根据时间返回日期</returns>
        public static DateTime GetRandomTime(DateTime dTime, int hour)
        {
            Random random = new Random(Often.Seed);
            int minute = random.Next(20, 59);
            int second = random.Next(0, 59);
            return Convert.ToDateTime(DateOften.ReDateTime("{$Year}-{$Month}-{$Day} " + hour.ToString() + ":" + minute.ToString() + ":" + second.ToString(), dTime));
        }
    }
}
