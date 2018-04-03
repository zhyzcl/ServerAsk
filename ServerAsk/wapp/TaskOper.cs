using System;
using System.IO;
using System.Net;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using App;
using App.Win;
using App.Web;

namespace wapp
{
    /// <summary>任务操作</summary>
    public class TaskOper
    {
        /// <summary>执行任务操作</summary>
        /// <param name="d">数据集操作对象</param>
        /// <param name="lout">日志记录接口</param>
        /// <param name="taskid">任务id</param>
        /// <param name="taskname">任务名称</param>
        /// <param name="acturl">任务访问地址</param>
        /// <param name="reqdata">请求数据</param>
        public static void StartTaskOper(DataInfo d, IlogOut lout, string taskid, string taskname, string acturl, string reqdata)
        {
            lout.OT(0, "开始执行任务[" + taskid.ToString() + "]：" + taskname + "。");
            string errs = "";
            string reps = TaskPost(acturl, reqdata, ref errs);
            if (errs!="")
            {
                lout.OT(0, "任务[" + taskid.ToString() + "]：" + taskname + "操作失败，失败信息：" + errs);
            }
            else
            {
                lout.OT(1, "任务[" + taskid.ToString() + "]：" + taskname + "操作成功，返回数据：" + reps);
            }
            lout.OT(0, "任务[" + taskid.ToString() + "]：" + taskname + "操作结束。");
        }

        /// <summary>根据url访问地址访问指定的地址并返回访问地址返回的数据</summary>
        /// <param name="url"></param>
        /// <param name="reqdata"></param>
        /// <param name="errs">发生错误则返回错误信息</param>
        /// <returns>根据url访问地址访问指定的地址并返回访问地址返回的数据</returns>
        public static string TaskPost(string url, string reqdata, ref string errs)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] payload = System.Text.Encoding.UTF8.GetBytes(reqdata);
            request.ContentLength = payload.Length;
            Stream writer;
            try
            {
                writer = request.GetRequestStream();
            }
            catch (Exception ex)
            {
                writer = null;
                errs = ex.Message;
            }
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            if (errs!="")
            {
                return "";
            }
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
                errs = ex.Message;
            }
            if (errs != "")
            {
                return "";
            }
            Stream s = response.GetResponseStream();
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();
            return postContent;
        }
    }
}
