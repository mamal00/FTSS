using FTSS.Login.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTSS.Login.Pages
{
	public class IndexModel : PageModel
	{
		private readonly IConfiguration iConfiguration;
		/// <summary>
		/// نام کاربری
		/// </summary>
		[BindProperty(SupportsGet = true)]
		public string username { get; set; }
		/// <summary>
		/// پسورد
		/// </summary>

		[BindProperty(SupportsGet = true)]
		public string password { get; set; }
		/// <summary>
		/// پیغام دریافتی
		/// </summary>
		[ViewData]
		public string message { get; set; } = null;
		public IndexModel(IConfiguration _iConfiguration)
		{
			iConfiguration = _iConfiguration;
		}

		public void OnGet()
		{

		}
		/// <summary>
		/// متد ورود
		/// </summary>
		/// <returns></returns>
		public IActionResult OnPostLogin()
		{
			try
			{
				var response = requestWebService();
				if (response!=null)
				{
					switch(response.StatusCode)
					{
						case System.Net.HttpStatusCode.OK:
							return Redirect(iConfiguration.GetValue<string>("DashboardUrl") + response.Data.data.token);
						case System.Net.HttpStatusCode.NotFound:
							message = "نام کاربری یا پسورد اشتباه است";
							return Page();
						case System.Net.HttpStatusCode.BadRequest:
							message = "لطفا نام کاربری و پسورد را وارد نمائید";
							return Page();
						default:
							if (response.Content != null)
								message = response.Content;
							else
							message = response.ErrorMessage;
							return Page();

					}
				
				}
				return Page();
			}
			catch (Exception ex)
			{
				message = ex.Message;
				return Page();
			}
		}
		private IRestResponse<ResponseLogin> requestWebService()
		{
			//آدرس وب سرویس را از تنظیمات برنامه واکشی کن
			var url = iConfiguration.GetValue<string>("LoginUrl");
			//ایجاد شی فراخوانی وب سرویس
			var client = new RestClient(url);
			var request = new RestRequest(Method.PUT);
			request.AddJsonBody(new { email = username, password = password });
			//اجرای وب سرویس
			var response = client.Execute<ResponseLogin>(request);
			return response;
		}
	}
}
