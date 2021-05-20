using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Security
{
	public class SMS
	{
		public static async Task<Models.Database.DBResult> Send(Models.SMS model, IConfiguration configuration)
		{
			var resultMessage = await requestWebService(model.Mobile, model.Code, configuration);
			if(resultMessage.IsSuccessful)
			{
				return new Models.Database.DBResult(200,"",new { },0);
			}
			return new Models.Database.DBResult((int)resultMessage.StatusCode, resultMessage.ErrorMessage, resultMessage.Content, 0);
		}
		private static async Task<IRestResponse> requestWebService(string mobile,string code,IConfiguration configuration)
		{
			//آدرس وب سرویس را از تنظیمات برنامه واکشی کن
			var url = configuration.GetValue<string>("sms:url");
			//ایجاد شی فراخوانی وب سرویس
			var client = new RestClient(url);
			var request = new RestRequest(Method.POST);
		
			request.AddHeader("Authorization", "AccessKey "+ configuration.GetValue<string>("sms:key"));
			request.AddParameter("application/json", "{\r\n\"pattern_code\": \""+ configuration.GetValue<string>("sms:pattern") + "\",\r\n\"originator\": \""+ configuration.GetValue<string>("sms:originator") + "\",\r\n\"recipient\": \""+mobile+"\",\r\n\"values\": {\r\n\"verification-code\": \""+code+"\"\r\n}\r\n}", ParameterType.RequestBody);
			//اجرای وب سرویس
			var response =await client.ExecuteAsync(request);
			return response;
		}
	}
	
}
