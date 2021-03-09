using Dapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.DP.DapperORM.Test
{
	[TestFixture]
	public class CommonTests
	{
		DynamicParameters daynamicParams;
		[SetUp]
		public void Setup()
		{
			//Arrenge
			daynamicParams = new DynamicParameters();
		}
		[Test]
		public void Get_SearchParams_Empty_With_OutputTest()
		{
			//Arrenge & Act
		 	var param= Common.GetSearchParams();
			//Assert
			Assert.That(param, Is.TypeOf<DynamicParameters>());
			Assert.IsNotNull(param);
		}
		[Test]
		public void Check_Count_SearchParams_Empty_With_OutputTest()
		{
			//Arrenge & Act
			daynamicParams = Common.GetSearchParams();
			//Assert
			Assert.AreEqual(2, daynamicParams.ParameterNames.AsList().Count);
		}
		[Test]
		public void Check_Names_OutputParam()
		{
			//Arrenge & Act
			daynamicParams = Common.GetSearchParams();
			//Assert
			Assert.AreEqual("ErrorCode", daynamicParams.ParameterNames.AsList()[0].ToString());
			Assert.AreEqual("ErrorMessage", daynamicParams.ParameterNames.AsList()[1].ToString());
		}
		[Test]
		public void Get_SearchParams_With_Add_Token(string token)
		{
			//Arrenge & Act
			var param = Common.GetSearchParams(token);
			//Assert
			Assert.That(param, Is.TypeOf<DynamicParameters>());
			Assert.IsNotNull(param);
		}
		[TestCase("")]
		[TestCase(null)]
		[Test]
		public void Get_SearchParams_With_Add_Token_Null_Empty_Exception(string token)
		{
			//Assert
			Assert.That(()=>Common.GetSearchParams(token), Throws.Exception);
		}

	}
}
