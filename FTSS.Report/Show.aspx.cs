using FTSS.Report.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FTSS.Report
{
	public partial class Show : System.Web.UI.Page
	{
		/// <summary>
		/// دریافت مقدار رشته ای
		/// </summary>
		/// <param name="title"></param>
		/// <returns></returns>
		public string GetString(string title)
		{
			try
			{
				var str = Request[title];
				if (string.IsNullOrEmpty(str))
					return ("");
				return (str);
			}
			catch (Exception e)
			{
				return ("");
			}
		}

		/// <summary>
		/// دریافت مقدار عددی
		/// </summary>
		/// <param name="title"></param>
		/// <returns></returns>
		public int GetInt(string title)
		{
			try
			{
				int rst = -1;
				var str = GetString(title);
				if (string.IsNullOrEmpty(str) || !int.TryParse(str, out rst))
					return (-1);
				return (rst);
			}
			catch (Exception e)
			{
				return (-1);
			}
		}

		/// <summary>
		/// دریافت مقدار تاریخ میلادی
		/// </summary>
		/// <param name="title">عنوان پارامتر مقدار تاریخ به میلادی</param>
		/// <returns></returns>
		public DateTime? GetDate(string title)
		{
			try
			{
				var d = GetString(title);
				if (string.IsNullOrEmpty(d))
					return (null);

				var rst = DateTime.Parse(d);
				return (rst);
			}
			catch (Exception e)
			{
				return null;
			}
		}
		/// <summary>
		/// توکن کاربر
		/// </summary>
		private string jwtToken
		{
			get
			{
				var rst = GetString("key");
				if (string.IsNullOrEmpty(rst))
					return (null);
				return rst;
			}
		}
		private string Kind
		{
			get
			{
				var rst = GetString("K");
				if (string.IsNullOrEmpty(rst))
					return (null);
				return rst;
			}
		}
		private string StartIndex
		{
			get
			{
				var rst = GetString("startIndex");
				if (string.IsNullOrEmpty(rst))
					return (null);
				return rst;
			}
		}
		private string MaxRecord
		{
			get
			{
				var rst = GetString("maxRecord");
				if (string.IsNullOrEmpty(rst))
					return (null);
				return rst;
			}
		}
		private string SortField
		{
			get
			{
				var rst = GetString("sortField");
				if (string.IsNullOrEmpty(rst))
					return (null);
				return rst;
			}
		}
		public string GetLogo()
		{
			try
			{
				string vPath = Server.MapPath("~/Rdlc/Images/logo.png");
				string imagePath = new Uri(vPath).AbsoluteUri;
				return (imagePath);
			}
			catch (Exception e)
			{
				return ("");
			}
		}
		private IRestResponse<dbResult> requestWebService(object data, string apiUrl = null)
		{
			if (string.IsNullOrEmpty(apiUrl))
			{
				apiUrl = Kind;
			}
			//آدرس وب سرویس را از تنظیمات برنامه واکشی کن
			var url = ConfigurationManager.AppSettings["apiUrl"] + apiUrl;
			//ایجاد شی فراخوانی وب سرویس
			var client = new RestClient(url);
			var request = new RestRequest(Method.PUT);
			request.AddJsonBody(data);
			request.AddHeader("authorization", "Bearer " + jwtToken);
			//اجرای وب سرویس
			var response = client.Execute<dbResult>(request);
			return response;
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (!IsPostBack)
				{
					if (
					  string.IsNullOrEmpty(jwtToken)
					  || string.IsNullOrEmpty(Kind)
					  )
					{
						Response.Write("Error 501, Invalid request");
						Response.End();
						return;
					}
					var names = new List<string>();
					var values = new List<string>();
					var dataValues = new List<object>();
					var dataNames = new List<string>();
					var exportRst = false;
					string rdlcFileName = Server.MapPath("~\\Rdlc\\");
					string exportsRoot = Server.MapPath("~\\Pdf\\");
					

					object data = null;
					names.Add("rpLogoAddress");
					values.Add(GetLogo());
					names.Add("rpCustomerName");
					values.Add(ConfigurationManager.AppSettings["reportTitle"]);
					switch (Kind)
					{
						#region FishDetail/GetAll=> فیش حقوقی کاربر
						case "FishDetail/GetAll":
							rdlcFileName += "Fish.rdlc";


							var responseFishDetail = requestWebService(new
							{
								fishId = GetString("FishId")
							});
							if (responseFishDetail.IsSuccessful)
							{
								data = ConvertToObject<List<FishDetailModel>>(ConvertToJson(responseFishDetail.Data.Data));
								string pdfFileName = string.Format("Fish-{0}.pdf", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ms"));
								string pdfPath = exportsRoot + pdfFileName;
								//حذف فایل های قدیمی
								Pdf.ClearLastDayFiles(exportsRoot);
								exportRst = BindRdlc.ExportToPdf(ReportViewer1, data, "DataSet", rdlcFileName, names, values,
								pdfPath);
								if (exportRst)
								{
									Response.Clear();
									Response.ContentType = "application/pdf";
									Response.AddHeader("Content-Disposition",
										string.Format("attachment;filename=\"{0}\"", pdfFileName));

									var pdfFileArray = System.IO.File.ReadAllBytes(pdfPath);
									Response.BinaryWrite(pdfFileArray);
									Response.Flush();
									ReportViewer1.Visible = false;
								}
								else
								BindRdlc.SetRVDataSource(ReportViewer1, data, "DataSet", rdlcFileName, names, values);
							}
							else
								lblError.Text = $"خطای {responseFishDetail.StatusCode} در اجرای وب سرویس:{responseFishDetail.ErrorMessage}";
							break;
						#endregion
						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				lblError.Text = string.Format("Error: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace);
			}

		}
		public static string ConvertToJson(object Data)
		{
			var JSONString = JsonConvert.SerializeObject(Data);
			return (JSONString);
		}
		public static T ConvertToObject<T>(string json)
		{
			var JSONString = JsonConvert.DeserializeObject<T>(json);
			return (JSONString);
		}
	}
}