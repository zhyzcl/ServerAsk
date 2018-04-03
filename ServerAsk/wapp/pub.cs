using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using App;
using App.Web;
using EMManage;

namespace wapp
{
    public class pub
    {
        /// <summary>删除Url参数列表指定参数名并返回</summary>
        /// <param name="urlname">url参数名称</param>
        /// <param name="urls">url参数列表，例: a=3&b=1&c=2</param>
        /// <returns>删除Url参数列表指定参数名并返回</returns>
        public static string DeleteUrlName(string urlname, string urls)
        {
            urls = urls.Trim();
            if (urls != "")
            {
                StringBuilder sb = new StringBuilder();
                string[] urlarr = urls.Split('&');
                for (int i = 0; i < urlarr.Length; i++)
                {
                    string str = urlarr[i].Trim();
                    if (str != "")
                    {
                        string[] strarr = str.Split('=');
                        if (strarr.Length == 2)
                        {
                            string sval = strarr[0].Trim();
                            if (sval.ToLower() != urlname.ToLower())
                            {
                                string stxt = strarr[1].Trim();
                                if (sb.Length > 0)
                                {
                                    sb.Append("&");
                                }
                                sb.Append(sval + "=" + stxt);
                            }
                        }
                    }
                }
                return sb.ToString();
            }
            return urls;
        }

        /// <summary>返回页面url参数</summary>
        /// <param name="sb">Url字符串</param>
        /// <param name="urlname">Url参数名</param>
        /// <param name="urlval">Url参数值</param>
        public static void PageUrlAdd(ref StringBuilder sb, string urlname, string urlval)
        {
            if (sb.Length > 0)
            {
                sb.Append("&");
            }
            sb.Append(urlname);
            sb.Append("=");
            sb.Append(Often.UrlEns(urlval));
        }

        /// <summary>返回开始与结束日期</summary>
        /// <param name="u">用户页面对象</param>
        /// <param name="sdate">返回开始日期</param>
        /// <param name="edate">返回结束日期</param>
        public static void LoadRequestSEDate(IUserPageInfo u, ref string sdate, ref string edate)
        {
            sdate = u.TeP("sdate");
            edate = u.TeP("edate");
            string ssdate = u.TeP("isdate");
            if (Often.IsDate(ssdate))
            {
                sdate = ssdate;
            }
            string sedate = u.TeP("iedate");
            if (Often.IsDate(sedate))
            {
                edate = sedate;
            }
        }

        /// <summary>如果输入的字符串为空则返回默认字符串值</summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="defstr">默认字符串值</param>
        /// <returns>如果输入的字符串为空则返回默认字符串值</returns>
        public static string GetStr(string str, string defstr)
        {
            if (str.Trim() == "")
            {
                return defstr;
            }
            return str;
        }

        /// <summary>如果输入的字符串不是长整数则返回默认字符串值</summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="defstr">默认字符串值</param>
        /// <returns>如果输入的字符串不是长整数则返回默认字符串值</returns>
        public static string GetInt64(string str, string defstr)
        {
            if (!Often.IsInt64(str))
            {
                return defstr;
            }
            return str;
        }

        /// <summary>如果输入的字符串不是整数则返回默认字符串值</summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="defstr">默认字符串值</param>
        /// <returns>如果输入的字符串不是整数则返回默认字符串值</returns>
        public static string GetInt32(string str, string defstr)
        {
            if (!Often.IsInt32(str))
            {
                return defstr;
            }
            return str;
        }

        /// <summary>如果输入的字符串不是数字则返回默认字符串值</summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="defstr">默认字符串值</param>
        /// <returns>如果输入的字符串不是数字则返回默认字符串值</returns>
        public static string GetNum(string str, string defstr)
        {
            if (!Often.IsNum(str))
            {
                return defstr;
            }
            return str;
        }

        /// <summary>在字符串右侧增加文本，如果文本值不为空，添加指定分隔字符串</summary>
        /// <param name="sb">字符串</param>
        /// <param name="s">增加的文本</param>
        /// <param name="compart">分隔字符串</param>
        public static void StrAdd(ref StringBuilder sb, string s, string compart)
        {
            if (sb.Length > 0)
            {
                sb.Append(compart);
            }
            sb.Append(s);
        }

        /// <summary>根据商品分类id返回商品分类名称</summary>
        /// <param name="alldt">商品分类表</param>
        /// <param name="ptype1">商品分类1</param>
        /// <param name="ptype2">商品分类2</param>
        /// <param name="ptype3">商品分类3</param>
        /// <returns>根据商品分类id返回商品分类名称</returns>
        public static string GetProductType(DataTable alldt, string ptype1, string ptype2, string ptype3)
        {
            string title1 = "";
            string title2 = "";
            string title3 = "";
            if (Often.IsInt64(ptype1))
            {
                DataRow[] pdr1 = alldt.Select("id=" + ptype1);
                if (pdr1.Length > 0)
                {
                    title1 = pdr1[0]["title"].ToString().Trim();
                }
            }
            if (Often.IsInt64(ptype2))
            {
                DataRow[] pdr2 = alldt.Select("id=" + ptype2);
                if (pdr2.Length > 0)
                {
                    title2 = pdr2[0]["title"].ToString().Trim();
                }
            }
            if (Often.IsInt64(ptype3))
            {
                DataRow[] pdr3 = alldt.Select("id=" + ptype3);
                if (pdr3.Length > 0)
                {
                    title3 = pdr3[0]["title"].ToString().Trim();
                }
            }
            return title1 + "﹥" + title2 + "﹥" + title3;
        }

        /// <summary>返回运费</summary>
        /// <param name="yflx">运费类型</param>
        /// <param name="jffs">运费计费方式</param>
        /// <param name="gsyf">按个数计费运费</param>
        /// <param name="gjzl">按公斤计费重量</param>
        /// <param name="gjsz">按公斤计费首重</param>
        /// <param name="gjcz">按公斤计费次重</param>
        /// <param name="pnum">商品数量</param>
        /// <returns>返回运费</returns>
        public static double GetCarriage(int yflx, int jffs, double gsyf, int gjzl, double gjsz, double gjcz, int pnum)
        {
            double yf = 0;
            if (yflx == 1 && pnum > 0)
            {
                if (jffs == 1)
                {
                    return gsyf * pnum;
                }
                if (jffs == 2 && gjzl > 0)
                {
                    double ys = pnum / gjzl;
                    ys = Math.Ceiling(ys);
                    if (ys > 1)
                    {
                        yf = gjsz + (gjcz * (ys - 1));
                    }
                    else
                    {
                        return gjsz;
                    }
                }
            }
            return yf;
        }

        /// <summary>从选中的行建立新的表并返回</summary>
        /// <param name="alldt">指定内存表</param>
        /// <param name="pids">选中id集合</param>
        /// <returns>从选中的行建立新的表并返回</returns>
        public static DataTable GetSelectTable(DataTable alldt, string pids)
        {
            DataTable seldt = alldt.Clone();
            seldt.Clear();
            if (pids.Trim() != "")
            {
                string[] arrids = pids.Split(',');
                for (int i = 0; i < arrids.Length; i++)
                {
                    string sid = arrids[i];
                    if (Often.IsInt64(sid))
                    {
                        DataRow[] dr = alldt.Select("id=" + sid);
                        if (dr.Length > 0)
                        {
                            seldt.ImportRow(dr[0]);
                        }
                    }
                }
            }
            return seldt;
        }

        /// <summary>根据文件名与指定后缀组返回随机生成的文件名</summary>
        /// <param name="fileName">文件名</param>
        /// <param name="ext">后缀名组，多个后缀使用|分隔（例如：jpg|gif|jpeg）</param>
        /// <returns>根据文件名与指定后缀组返回随机生成的文件名</returns>
        public static string GetFileName(string fileName, string ext)
        {
            if (Often.IsExt(fileName, ext))
            {
                string exts = FileSys.GetFileExtension(fileName);
                return Often.GetLongAddup() + "." + exts;
            }
            return "";
        }


        /// <summary>保留指定位小数，并舍去保留位之后的所有小数</summary>
        /// <param name="num">数字</param>
        /// <param name="digit">保留的小数位数</param>
        /// <returns>保留指定位小数，并舍去保留位之后的所有小数</returns>
        public static double GetHoldDigit(double num, int digit)
        {
            num = Math.Round(num, 4);
            if (num > 0)
            {
                if (digit >= 0)
                {
                    int xindex = num.ToString().IndexOf(".");
                    if (xindex > -1)
                    {
                        if (digit == 0)
                        {
                            return Convert.ToDouble(num.ToString().Split('.')[0]);
                        }
                        else
                        {
                            string[] sarr = num.ToString().Split('.');
                            string s1 = sarr[0].Trim();
                            string s2 = sarr[1].Trim();
                            if (s2.Length > digit)
                            {
                                return Convert.ToDouble(s1 + "." + s2.Remove(digit));
                            }
                        }
                    }
                }
                return num;
            }
            return 0;
        }

        /// <summary>返回数字的保留2位小数格式</summary>
        /// <param name="dnum">数字</param>
        /// <returns>返回数字的保留2位小数格式</returns>
        public static double GetNumber(double dnum)
        {
            return GetHoldDigit(dnum, 2);
        }

        /// <summary>返回数字的保留2位小数格式</summary>
        /// <param name="dnum">数字</param>
        /// <returns>返回数字的保留2位小数格式</returns>
        public static double GetNumber(string dnum)
        {
            if (!Often.IsNum(dnum))
            {
                dnum = "0";
            }
            return GetHoldDigit(Convert.ToDouble(dnum), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataTable dt, string cname)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataTable dt, string cname, int row)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataRow[] dr, string cname)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataRow[] dr, string cname, int row)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataTable dt, string cname, string defval)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataTable dt, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataRow[] dr, string cname, string defval)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataRow[] dr, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数字的保留2位小数格式</summary>
        /// <param name="dnum">数字</param>
        /// <returns>返回数字的保留2位小数格式</returns>
        public static double GetDouble(string dnum)
        {
            if (!Often.IsNum(dnum))
            {
                dnum = "0";
            }
            return Convert.ToDouble(dnum);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataTable dt, string cname)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataTable dt, string cname, int row)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataRow[] dr, string cname)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataRow[] dr, string cname, int row)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数字的保留2位小数格式</summary>
        /// <param name="dnum">数字</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数字的保留2位小数格式</returns>
        public static double GetDouble(string dnum, string defval)
        {
            if (!Often.IsNum(dnum))
            {
                dnum = defval;
            }
            return Convert.ToDouble(dnum);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataTable dt, string cname, string defval)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataTable dt, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataRow[] dr, string cname, string defval)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataRow[] dr, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数字的整数格式，如果不是数字则返回0</summary>
        /// <param name="dnum">数字</param>
        /// <returns>返回数字的整数格式，如果不是数字则返回0</returns>
        public static int GetInt(string dnum)
        {
            if (Often.IsInt32(dnum))
            {
                return Convert.ToInt32(dnum);
            }
            return 0;
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataTable dt, string cname)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsInt32(val))
            {
                val = "0";
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataTable dt, string cname, int row)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsInt32(val))
            {
                val = "0";
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataRow[] dr, string cname)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsInt32(val))
            {
                val = "0";
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataRow[] dr, string cname, int row)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsInt32(val))
            {
                val = "0";
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数字的整数格式</summary>
        /// <param name="dnum">数字</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数字的整数格式</returns>
        public static int GetInt(string dnum, string defval)
        {
            if (!Often.IsInt32(dnum))
            {
                dnum = defval;
            }
            return Convert.ToInt32(dnum);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataTable dt, string cname, string defval)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsInt32(val))
            {
                val = defval;
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataTable dt, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsInt32(val))
            {
                val = defval;
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataRow[] dr, string cname, string defval)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsInt32(val))
            {
                val = defval;
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataRow[] dr, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsInt32(val))
            {
                val = defval;
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回用户验证密钥</summary>
        /// <param name="sid">sid</param>
        /// <param name="uid">用户id</param>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>返回用户验证密钥</returns>
        public static string GetVerKey(string sid, string uid, string name, string pwd)
        {
            string mpwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "SHA1");
            if (WebInfo.IsSysManageUser(name, mpwd))
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(sid);
            sb.Append("|");
            sb.Append(uid);
            sb.Append("|");
            sb.Append(name);
            sb.Append("|");
            sb.Append(pwd);
            return TripleDes.DesEn(sb.ToString());
        }

        /// <summary>设置数据集值</summary>
        /// <param name="ds">数据集</param>
        /// <param name="colname">列名</param>
        /// <param name="isval">是否写入值</param>
        /// <param name="val">值</param>
        /// <param name="defval">默认写入值</param>
        public static void SetDataSaveValue(ref DataSave ds, string colname, bool isval, string val, string defval)
        {
            if (isval)
            {
                ds.Operp(val, colname);
            }
            else
            {
                ds.Operp(defval, colname);
            }
        }

        /// <summary>设置数据集值</summary>
        /// <param name="ds">数据集</param>
        /// <param name="colname">列名</param>
        /// <param name="isval">是否写入值</param>
        /// <param name="val">值</param>
        public static void SetDataSaveValue(ref DataSave ds, string colname, bool isval, string val)
        {
            if (isval)
            {
                ds.Operp(val, colname);
            }
        }

        /// <summary>根据字符串内存表返回索引列为0的数组列表</summary>
        /// <param name="s">字符串内存表</param>
        /// <returns>根据字符串内存表返回索引列为0的数组列表</returns>
        public static List<string> GetList(string s)
        {
            return GetList(s, 0);
        }

        /// <summary>根据字符串内存表返回数组列表</summary>
        /// <param name="s">字符串内存表</param>
        /// <param name="index">字符串内存表列索引</param>
        /// <returns>根据字符串返回数组列表</returns>
        public static List<string> GetList(string s, int index)
        {
            List<string> li = new List<string>();
            DataTable vdt = WebOften.StrToDataTable(s);
            for (int i = 0; i < vdt.Rows.Count; i++)
            {
                if (index >= 0 && index < vdt.Columns.Count)
                {
                    li.Add(vdt.Rows[i][index].ToString());
                }
            }
            return li;
        }




    }
}