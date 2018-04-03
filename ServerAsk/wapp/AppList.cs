using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using App;
using App.Win;
using App.Web;

namespace wapp
{
    /// <summary>文本窗显示委托</summary>
    /// <param name="myControl">文本对象</param>
    /// <param name="myCaption">需要插入的文本</param>
    /// <param name="isqc">是否清空文本对象true 是 false 否</param>
    /// <param name="blxx">基础文本</param>
    /// <returns>返回已被截取后的字符串</returns>
    public delegate void RTBDel(System.Windows.Forms.RichTextBox myControl, string myCaption, bool isqc, string blxx);

    /// <summary>文本窗显示委托</summary>
    /// <param name="myLabel">文本对象</param>
    /// <param name="myCaption">需要显示的文本</param>
    public delegate void LabelDel(System.Windows.Forms.Label myLabel, string myCaption);

    public class AppList
    {
        /// <summary>系统显示名称</summary>
        public static string SystemShowName = "服务端口接口工具";

        /// <summary>系统配置文件名称</summary>
        public static string SystemInfoName = "SystemInfo";

        /// <summary>服务接口列表文件名称</summary>
        public static string InterfaceTableName = "InterfaceTable";

        /// <summary>缓存密匙</summary>
        public static string DeKey = "10567E012A210429E9C14A94CB111480212A0423B813C251";

        /// <summary>系统管理基础路径</summary>
        public static string AppBasePath = "";

        /// <summary>系统配置保存路径</summary>
        public static string SaveConfigPath = "";

        /// <summary>系统日志保存路径</summary>
        public static string LogSavePath = "";

        /// <summary>文件保存目录名称</summary>
        public static string AppFilesDirName = "AppFiles";

        /// <summary>日志保存目录名称</summary>
        public static string SaveLogDirName = "SaveLog";

        /// <summary>Json加密密钥</summary>
        public static string AppJsonKey = "B2E1BB267E91478BA16D7654B8DE8E3D";

        /// <summary>根据名称返回系统内存表构架</summary>
        /// <param name="name">名称</param>
        /// <returns>根据名称返回系统内存表构架</returns>
        public static DataTable GetConfigDataTable(string name)
        {
            Table t = new Table();
            t.TableName = name;
            if (name == SystemInfoName)
            {
                t.Add("SystemName", "string", 0);
                t.Add("SystemTitle", "string", 0);
                t.Add("SystemVers", "string", 0);
                t.Add("SystemVersion", "string", 0);
                t.Add("SystemVerNum", "long", 0);
                t.Add("PublishDate", "datetime", 0);
                t.Add("SystemInfo", "string", 0);
                t.Add("CreateDate", "datetime", 0);
            }
            else if (name == InterfaceTableName)
            {
                t.Add("InterfaceID", "autoint", 1);
                t.Add("InterfaceName", "string", 0);
                t.Add("ActionUrl", "string", 0);
                t.Add("action", "string", 0);
                t.Add("sessid", "string", 0);
                t.Add("param", "string", 0);
                t.Add("datas", "string", 0);
                t.Add("LogSaveMode", "byte", 0);
            }
            return App.DataTables.GetDataTable(t);
        }

        /// <summary>是否只读列表</summary>
        public static string IsReadyOnly()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("0|读写");
            sb.Append("||1|只读");
            return sb.ToString();
        }

        /// <summary>日志保存模式列表</summary>
        public static string LogSaveMode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("0|不记录");
            sb.Append("||1|记录");
            return sb.ToString();
        }

        /// <summary>是否启用列表</summary>
        public static string IsUser()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("0|关闭");
            sb.Append("||1|启用");
            return sb.ToString();
        }

        /// <summary>时列表</summary>
        public static string Hour()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("0|00");
            sb.Append("||1|01");
            sb.Append("||2|02");
            sb.Append("||3|03");
            sb.Append("||4|04");
            sb.Append("||5|05");
            sb.Append("||6|06");
            sb.Append("||7|07");
            sb.Append("||8|08");
            sb.Append("||9|09");
            sb.Append("||10|10");
            sb.Append("||11|11");
            sb.Append("||12|12");
            sb.Append("||13|13");
            sb.Append("||14|14");
            sb.Append("||15|15");
            sb.Append("||16|16");
            sb.Append("||17|17");
            sb.Append("||18|18");
            sb.Append("||19|19");
            sb.Append("||20|20");
            sb.Append("||21|21");
            sb.Append("||22|22");
            sb.Append("||23|23");
            return sb.ToString();
        }

        /// <summary>分与秒列表</summary>
        public static string Minute()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("0|00");
            sb.Append("||1|01");
            sb.Append("||2|02");
            sb.Append("||3|03");
            sb.Append("||4|04");
            sb.Append("||5|05");
            sb.Append("||6|06");
            sb.Append("||7|07");
            sb.Append("||8|08");
            sb.Append("||9|09");
            sb.Append("||10|10");
            sb.Append("||11|11");
            sb.Append("||12|12");
            sb.Append("||13|13");
            sb.Append("||14|14");
            sb.Append("||15|15");
            sb.Append("||16|16");
            sb.Append("||17|17");
            sb.Append("||18|18");
            sb.Append("||19|19");
            sb.Append("||20|20");
            sb.Append("||21|21");
            sb.Append("||22|22");
            sb.Append("||23|23");
            sb.Append("||24|24");
            sb.Append("||25|25");
            sb.Append("||26|26");
            sb.Append("||27|27");
            sb.Append("||28|28");
            sb.Append("||29|29");
            sb.Append("||30|30");
            sb.Append("||31|31");
            sb.Append("||32|32");
            sb.Append("||33|33");
            sb.Append("||34|34");
            sb.Append("||35|35");
            sb.Append("||36|36");
            sb.Append("||37|37");
            sb.Append("||38|38");
            sb.Append("||39|39");
            sb.Append("||40|40");
            sb.Append("||41|41");
            sb.Append("||42|42");
            sb.Append("||43|43");
            sb.Append("||44|44");
            sb.Append("||45|45");
            sb.Append("||46|46");
            sb.Append("||47|47");
            sb.Append("||48|48");
            sb.Append("||49|49");
            sb.Append("||50|50");
            sb.Append("||51|51");
            sb.Append("||52|52");
            sb.Append("||53|53");
            sb.Append("||54|54");
            sb.Append("||55|55");
            sb.Append("||56|56");
            sb.Append("||57|57");
            sb.Append("||58|58");
            sb.Append("||59|59");
            return sb.ToString();
        }

        /// <summary>数据表字段类型</summary>
        public static string DataType()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("string|字符串");
            sb.Append("||int|整数(32位 int)");
            sb.Append("||long|长整数(64位 long)");
            sb.Append("||autoint|自动编号(32位 int)");
            sb.Append("||autolong|自动编号(64位 long)");
            sb.Append("||byte|byte(8位正整数)");
            sb.Append("||datetime|长日期(datetime)");
            sb.Append("||decimal|高精度浮点数字(decimal)");
            sb.Append("||double|双精度浮点数字(double)");
            sb.Append("||byte[]|字节数组(byte[])");
            return sb.ToString();
        }

        /// <summary>判断数据类型是否一致，一致则返回true, 否则返回false</summary>
        /// <param name="types">数据类型</param>
        /// <param name="intype">用户数据类型</param>
        /// <returns>判断数据类型是否一致，一致则返回true, 否则返回false</returns>
        public static bool IsDataType(string types, string intype)
        {
            string stype = GetDataType(intype).Trim();
            if (types.Trim() == stype)
            {
                return true;
            }
            return false;
        }

        /// <summary>返回内存表类型所对应的系统类型简写</summary>
        /// <param name="types">内存表类型</param>
        /// <returns>返回内存表类型所对应的系统类型简写</returns>
        public static string GetDataSType(string types)
        {
            types = types.ToLower();
            if (types == "int" || types == "int32" || types == "system.int32" || types == "i")
            {
                return "i";
            }
            else if (types == "long" || types == "int64" || types == "system.int64" || types == "l")
            {
                return "l";
            }
            else if (types == "autoint" || types == "ai")
            {
                return "ai";
            }
            else if (types == "autolong" || types == "al")
            {
                return "al";
            }
            else if (types == "bit" || types == "tinyint" || types == "byte" || types == "b")
            {
                return "b";
            }
            else if (types == "datetime" || types == "t")
            {
                return "t";
            }
            else if (types == "decimal" || types == "de")
            {
                return "de";
            }
            else if (types == "double" || types == "d")
            {
                return "d";
            }
            else if (types == "byte[]" || types == "bts")
            {
                return "bts";
            }
            return "string";
        }

        /// <summary>返回内存表类型所对应的系统类型</summary>
        /// <param name="types">内存表类型</param>
        /// <returns>返回内存表类型所对应的系统类型</returns>
        public static string GetDataType(string types)
        {
            types = types.ToLower();
            if (types == "int" || types == "int32" || types == "system.int32" || types == "i")
            {
                return "int";
            }
            else if (types == "long" || types == "int64" || types == "system.int64" || types == "l")
            {
                return "long";
            }
            else if (types == "autoint" || types == "ai")
            {
                return "autoint";
            }
            else if (types == "autolong" || types == "al")
            {
                return "autolong";
            }
            else if (types == "bit" || types == "tinyint" || types == "byte" || types == "b")
            {
                return "byte";
            }
            else if (types == "datetime" || types == "t")
            {
                return "datetime";
            }
            else if (types == "decimal" || types == "de")
            {
                return "decimal";
            }
            else if (types == "double" || types == "d")
            {
                return "double";
            }
            else if (types == "byte[]" || types == "bts")
            {
                return "byte[]";
            }
            return "string";
        }

        /// <summary>返回内存表类型所对应的系统类型</summary>
        /// <param name="types">内存表类型</param>
        /// <returns>返回内存表类型所对应的系统类型</returns>
        public static string GetDataTypes(string types)
        {
            types = types.ToLower();
            if (types == "int" || types == "int32" || types == "system.int32" || types == "i")
            {
                return "整数(32位 int)";
            }
            else if (types == "long" || types == "int64" || types == "system.int64" || types == "l")
            {
                return "长整数(64位 long)";
            }
            else if (types == "autoint" || types == "ai")
            {
                return "自动编号(32位 int)";
            }
            else if (types == "autolong" || types == "al")
            {
                return "自动编号(64位 long)";
            }
            else if (types == "bit" || types == "tinyint" || types == "byte" || types == "b")
            {
                return "byte(8位正整数)";
            }
            else if (types == "datetime" || types == "t")
            {
                return "长日期(datetime)";
            }
            else if (types == "decimal" || types == "de")
            {
                return "高精度浮点数字(decimal)";
            }
            else if (types == "double" || types == "d")
            {
                return "双精度浮点数字(double)";
            }
            else if (types == "byte[]" || types == "bts")
            {
                return "字节数组(byte[])";
            }
            return "字符串";
        }

        /// <summary>将指定列表字符串插入到指定内存列表并返回内存列表</summary>
        /// <param name="rdt">内存列表</param>
        /// <param name="str">指定列表字符串</param>
        /// <returns>将指定列表字符串插入到指定内存列表并返回内存列表</returns>
        public static DataTable AddTable(DataTable rdt, string str)
        {
            DataTable sdt = WebOften.StrToDataTable(str, "||", "|");
            for (int i = 0; i < sdt.Rows.Count; i++)
            {
                DataRow dr = rdt.NewRow();
                dr[0] = sdt.Rows[i][0];
                dr[1] = sdt.Rows[i][1];
                rdt.Rows.Add(dr);
            }
            return rdt;
        }

        /// <summary>将指定列表字符串插入到指定内存列表索引位置并返回内存列表</summary>
        /// <param name="rdt">内存列表</param>
        /// <param name="str">指定列表字符串</param>
        /// <param name="pos">内存列表索引位置</param>
        /// <returns>将指定列表字符串插入到指定内存列表索引位置并返回内存列表</returns>
        public static DataTable AddTable(DataTable rdt, string str, int pos)
        {
            DataTable sdt = WebOften.StrToDataTable(str, "||", "|");
            for (int i = 0; i < sdt.Rows.Count; i++)
            {
                DataRow dr = rdt.NewRow();
                dr[0] = sdt.Rows[i][0];
                dr[1] = sdt.Rows[i][1];
                rdt.Rows.InsertAt(dr, pos);
                pos++;
            }
            return rdt;
        }

        /// <summary>根据字符串数组返回列表字符串</summary>
        /// <param name="lists">字符串数组</param>
        /// <returns>根据字符串数组返回列表字符串</returns>
        public static string GetLists(string[] lists)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lists.Length; i++)
            {
                if (sb.Length > 0)
                {
                    sb.Append("||");
                }
                sb.Append(lists[i] + "|" + lists[i]);
            }
            return sb.ToString();
        }
    }
}
