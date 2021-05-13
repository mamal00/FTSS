using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FTSS.Report
{
	public class Pdf
	{
        /// <summary>
        /// حذف فایل های قدیمی درون یک فولدر - فایل هایی که از یک روز پیش قدیمی تر هستند
        /// </summary>
        /// <param name="RootPath">آدرس فولدر</param>
        /// <returns></returns>
        public static int ClearLastDayFiles(string RootPath)
        {
            try
            {
                string[] files = Directory.GetFiles(RootPath);
                var now = DateTime.Now;
                var lastDay = now.AddDays(-1);
                int rst = 0;
                foreach (string file in files)
                {
                    FileInfo info = new FileInfo(file);
                    info.Refresh();

                    if (info.LastWriteTime <= lastDay)
                    {
                        info.Delete();
                        rst++;
                    }
                }

                return (rst);
            }
            catch (Exception e)
            {
                return (-1);
            }
        }
    }
}