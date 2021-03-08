using FTSS.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FTSS.API.Test
{
	public class Tests
	{
	
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task Test1()
		{
			var mockCTX = new Mock<Logic.Database.IDBCTX>();
			var mockLog = new Mock<Logic.Log.ILog>();
			var data=new Models.Database.Tables.Users();
			var controller = new UserProfileController(mockCTX.Object, mockLog.Object);
			var request =await controller.Update(data);
			Assert.That(request,Is.TypeOf<OkResult>());
		}
	}
}