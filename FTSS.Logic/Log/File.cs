﻿using FTSS.Models.Database.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    /// <summary>
    /// Log messages at a text file
    /// </summary>
    public class File : ILog
    {
        private FileIO.IFileOperation _file;

        public File(FileIO.IFileOperation f = null, string fileName = "FTSS.txt")
        {
            if (f == null)
                _file = new FileIO.TextFile(fileName);
            else
                _file = f;
        }

        /// <summary>
        /// Log an Exception with custom message
        /// </summary>
        /// <param name="customMessage"></param>
        /// <param name="e"></param>
        public void Add(Exception e, string customMessage = null)
        {
            string text = string.Format("{0}\nException: {1}\nStackTrace: {2}\n",
                customMessage ?? "", e.Message, e.StackTrace);
            var model = new SP_Log_Insert_Params(text);
            this.Add(model);
        }

        /// <summary>
        /// Log a simple text at a file
        /// </summary>
        /// <param name="msg"></param>
        public void Add(SP_Log_Insert_Params model)
        {
            string text = string.Format("{0}: {1}\n", DateTime.Now, model.Msg);
            _file.Append(text);
        }
    }
}
