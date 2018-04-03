using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Threading;
using System.Runtime.InteropServices;
using App;
using App.Win;
using App.Web;
using Microsoft.Win32;

namespace wapp
{
    /// <summary>服务接口内存列表</summary>
    public class InterfaceList
    {
        /// <summary>任务列表缓存表</summary>
        private static DataTable _InterfaceTable;

        /// <summary>任务列表缓存表</summary>
        public static DataTable InterfaceTable
        {
            get
            {
                if (_InterfaceTable == null)
                {
                    CreateInterfaceTable();
                    LoadInterfaceTable();
                }
                return _InterfaceTable;
            }
            set { _InterfaceTable = value; }
        }

        /// <summary>创建任务列表</summary>
        public static void CreateInterfaceTable()
        {
            _InterfaceTable = AppList.GetConfigDataTable(AppList.InterfaceTableName);
        }

        /// <summary>读取任务列表</summary>
        public static void LoadInterfaceTable()
        {
            string dpath = AppList.SaveConfigPath + _InterfaceTable.TableName + ".xml";
            if (File.Exists(dpath))
            {
                try
                {
                    _InterfaceTable = new DataTable();
                    _InterfaceTable.ReadXml(dpath);
                }
                catch
                {
                    _InterfaceTable.Clear();
                }
            }
        }

        /// <summary>保存任务列表到文件</summary>
        public static void SaveInterfaceTableXmlFile()
        {
            string upath = AppList.SaveConfigPath + _InterfaceTable.TableName + ".xml";
            if (File.Exists(upath))
            {
                File.Delete(upath);
            }
            _InterfaceTable.WriteXml(upath, XmlWriteMode.WriteSchema);
        }
    }
}
