using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FTSS.Report
{
	public class BindRdlc
	{
    
        public static void SetRVDataSource(Microsoft.Reporting.WebForms.ReportViewer RV, List<object> q,
       List<string> DataSourceNames, string FilePath, List<string> ParameterName, List<string> ParameterValue)
        {
            var obj = new BindRdlc();
            obj.mySetRVDataSource(RV, q, DataSourceNames, FilePath, ParameterName, ParameterValue);
        }
        public static void SetRVDataSource(ReportViewer RV, object q, string DataSourceName, string FilePath, List<string> ParameterName, List<string> ParameterValue)
        {
            var obj = new BindRdlc();
            obj.mySetRVDataSource(RV, q, DataSourceName, FilePath, ParameterName, ParameterValue);
        }
        public void mySetRVDataSource(Microsoft.Reporting.WebForms.ReportViewer RV, List<object> q, List<string> DataSourceNames, string FilePath, List<string> ParameterName, List<string> ParameterValue)
        {
            try
            {
                RV.Reset();
                RV.LocalReport.DataSources.Clear();

                RV.LocalReport.ReportPath = FilePath;
                if (q != null && q.Count > 0)
                {
                    for (int i = 0; i < DataSourceNames.Count; i++)
                    {
                        Microsoft.Reporting.WebForms.ReportDataSource ds = new Microsoft.Reporting.WebForms.ReportDataSource(DataSourceNames[i], q[i]);
                        RV.LocalReport.DataSources.Add(ds);
                    }
                }

                RV.LocalReport.EnableExternalImages = true;


                if (ParameterName != null && ParameterName.Count > 0)
                {
                    List<ReportParameter> parameters = new List<ReportParameter>();
                    for (int i = 0; i < ParameterName.Count; i++)
                    {
                        var parameter = new ReportParameter(ParameterName[i], ParameterValue[i]);
                        parameters.Add(parameter);
                    }

                    RV.LocalReport.SetParameters(parameters);
                }

                RV.LocalReport.EnableExternalImages = true;
                RV.DataBind();

                RV.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(my_SubreportProcessing);

                RV.LocalReport.Refresh();
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public void mySetRVDataSource(ReportViewer RV, object q, string DataSourceName, string FilePath, List<string> ParameterName, List<string> ParameterValue)
        {
            try
            {
                if (q != null)
                {
                    List<object> qs = new List<object>();
                    List<string> DatasourceNames = new List<string>();
                    qs.Add(q);
                    DatasourceNames.Add(DataSourceName);
                    mySetRVDataSource(RV, qs, DatasourceNames, FilePath, ParameterName, ParameterValue);
                }
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }




        public static bool ExportToExcel(Microsoft.Reporting.WebForms.ReportViewer RV, object q,
            string DataSourceName, string RdlcFilePath,
            List<string> ParameterName, List<string> ParameterValue, string FileName)
        {
            List<object> DSs = new List<object>();
            DSs.Add(q);

            var dsNames = new List<string>();
            dsNames.Add(DataSourceName);

            return (ExportToExcel(RV, DSs, dsNames, RdlcFilePath, ParameterName, ParameterValue, FileName));
        }

        public static bool ExportToExcel(Microsoft.Reporting.WebForms.ReportViewer RV, List<object> q,
            List<string> DataSourceName, string RdlcFilePath,
            List<string> ParameterName, List<string> ParameterValue, string FileName)
        {
            try
            {
                SetRVDataSource(RV, q, DataSourceName, RdlcFilePath, ParameterName, ParameterValue);

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                //byte[] bytes = RV.LocalReport.Render("EXCELOPENXML", null,
                //    out mimeType, out encoding, out extension, out streamIds, out warnings);
                byte[] bytes = RV.LocalReport.Render("Excel", null,
                    out mimeType, out encoding, out extension, out streamIds, out warnings);
                System.IO.File.WriteAllBytes(FileName, bytes);

                return (true);
            }
            catch (System.Exception e)
            {

                return (false);
            }
        }
        public static bool ExportToPdf(Microsoft.Reporting.WebForms.ReportViewer RV, List<object> q,
        List<string> DataSourceName, string RdlcFilePath,
        List<string> ParameterName, List<string> ParameterValue, string PdfFileName)
        {
            try
            {
                SetRVDataSource(RV, q, DataSourceName, RdlcFilePath, ParameterName, ParameterValue);

                /*
                //ReportViewer viewer2 = new ReportViewer();
                viewer2.ProcessingMode = ProcessingMode.Local;
                viewer2.LocalReport.ReportPath = RdlcFilePath;

                if (q != null)
                {
                    for (int i = 0; i < q.Count; i++)
                    {
                        Microsoft.Reporting.WebForms.ReportDataSource ds = new Microsoft.Reporting.WebForms.ReportDataSource(DataSourceName[i], q[i]);
                        viewer2.LocalReport.DataSources.Add(ds);
                    }
                }
                if (ParameterName != null && ParameterName.Count > 0)
                {
                    List<ReportParameter> parameters = new List<ReportParameter>();
                    for (int i = 0; i < ParameterName.Count; i++)
                    {
                        var parameter = new ReportParameter(ParameterName[i], ParameterValue[i]);
                        parameters.Add(parameter);
                    }
                    viewer2.LocalReport.EnableExternalImages = true;
                    viewer2.LocalReport.SetParameters(parameters);
                }

                viewer2.LocalReport.EnableExternalImages = true;
                viewer2.DataBind();
                */

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                byte[] bytes = RV.LocalReport.Render("PDF", null,
                    out mimeType, out encoding, out extension, out streamIds, out warnings);
                System.IO.File.WriteAllBytes(PdfFileName, bytes);

                return (true);
            }
            catch (System.Exception e)
            {
                return (false);
            }
        }

        public static bool ExportToPdf(Microsoft.Reporting.WebForms.ReportViewer RV, object q, string DataSourceName, string RdlcFilePath,
            List<string> ParameterName, List<string> ParameterValue, string PdfFileName)
        {
            var dsList = new List<string>();
            dsList.Add(DataSourceName);
            List<object> qq = null;
            if (q != null)
            {
                qq = new List<object>();
                qq.Add(q);
            }

            return (ExportToPdf(RV, qq, dsList, RdlcFilePath, ParameterName, ParameterValue, PdfFileName));
        }

        private void my_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            var Hokm_ID = "";
            if (e.Parameters["Hokm_ID"] != null && e.Parameters["Hokm_ID"].Values != null && e.Parameters["Hokm_ID"].Values.Count > 0)
                Hokm_ID = e.Parameters["Hokm_ID"].Values[0];

            int Kind = -1;
            if (e.Parameters["Kind"] != null && e.Parameters["Kind"].Values != null && e.Parameters["Kind"].Values.Count > 0
                && !string.IsNullOrEmpty(e.Parameters["Kind"].Values[0]))
                Kind = int.Parse(e.Parameters["Kind"].Values[0]);

            object ds = null;

            switch (e.ReportPath)
            {
                case "MISprsnHokm_Sub":
                    //ds = Logic.MisPrsn.dbo.Hokm.GetAll(Hokm_ID, Kind).ToList();
                    ReportDataSource rds = new ReportDataSource("SP_HokmKrg", ds);
                    e.DataSources.Add(rds);
                    break;
            }
        }
    }
}