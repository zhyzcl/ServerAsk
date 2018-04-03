using System;
using System.IO;
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
using Microsoft.Win32;

namespace wapp
{
    public class SysConfig
    {
        /// <summary>系统内部版本</summary>
        public static string SystemVers = "1.0.0.0";

        /// <summary>系统版本号</summary>
        public static string SystemVersion = "v" + SystemVers;

        /// <summary>系统版本数字</summary>
        public static long SystemVerNum = Convert.ToInt64(SystemVers.Replace(".", ""));

        /// <summary>系统名称</summary>
        public static string SystemName = "ServerAsk";

        /// <summary>系统创建日期</summary>
        public static DateTime SystemCreateDate = Convert.ToDateTime("2017-6-6");

        /// <summary>系统发布日期</summary>
        public static DateTime SystemPublishDate = Convert.ToDateTime("2017-6-9");

        /// <summary>系统提示查看次数</summary>
        public static int SysCueSeeCount = 0;


        /// <summary>系统版本信息表</summary>
        private static DataTable _SystemInfo;

        /// <summary>系统版本信息表</summary>
        public static DataTable SystemInfo
        {
            get
            {
                if (_SystemInfo == null)
                {
                    CreateSysConfig();
                }
                return _SystemInfo;
            }
        }

        /// <summary>初始化系统配置</summary>
        public static void NewSysConfig() 
        {
            AppList.AppBasePath = Application.StartupPath;
            AppList.AppBasePath = AppList.AppBasePath.Replace("/", "\\");
            if (!AppList.AppBasePath.EndsWith("\\"))
            {
                AppList.AppBasePath = AppList.AppBasePath + "\\";
            }
            CreateSysConfig();
        }

        /// <summary>创建系统配置</summary>
        public static void CreateSysConfig()
        {
            AppList.SaveConfigPath = AppList.AppBasePath  + AppList.AppFilesDirName + "\\";
            FileSys.NewDir(AppList.SaveConfigPath);
            AppList.LogSavePath = AppList.AppBasePath  + AppList.SaveLogDirName + "\\";
            FileSys.NewDir(AppList.LogSavePath);
            _SystemInfo = AppList.GetConfigDataTable(AppList.SystemInfoName);
            string spath = AppList.SaveConfigPath + _SystemInfo.TableName + ".xml";
            if (File.Exists(spath))
            {
                File.Delete(spath);
            }
            DataRow newRow = _SystemInfo.NewRow();
            newRow["SystemName"] = SystemName;
            newRow["SystemTitle"] = AppList.SystemShowName;
            newRow["SystemVers"] = SystemVers;
            newRow["SystemVersion"] = SystemVersion;
            newRow["SystemVerNum"] = SystemVerNum;
            newRow["PublishDate"] = SystemPublishDate;
            newRow["SystemInfo"] = AppList.SystemShowName;
            newRow["CreateDate"] = SystemCreateDate;
            _SystemInfo.Rows.Add(newRow);
            _SystemInfo.WriteXml(spath, XmlWriteMode.WriteSchema);
        }
    }
}
