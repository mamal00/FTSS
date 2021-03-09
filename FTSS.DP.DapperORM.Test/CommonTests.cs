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
			Assert.IsNotNull(daynamicParams.ParameterNames);
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
		[TestCase(null)]
		[Test]
		public void Get_SearchBaseParams_With_Null_Exception(Models.Database.BaseSearchParams filterParams)
		{
			//Assert
			Assert.That(() => Common.GetSearchParams(filterParams), Throws.Exception);
		}
		[Test]
		public void Check_SearchBaseParams_Count()
		{
			//Arreng
			var filterParams = new Models.Database.BaseSearchParams();
			filterParams.PageSize = 10;
			filterParams.Sort = "";
			filterParams.StartIndex = 0;
			filterParams.Token = "1";
			//Act
			daynamicParams = Common.GetSearchParams(filterParams);
			//Assert
			Assert.IsNotNull(daynamicParams.ParameterNames);
			Assert.AreEqual(7, daynamicParams.ParameterNames.AsList().Count);
		}
		[TestCase(0, 0)]
		[TestCase(-1, 10)]
		[TestCase(-1, 0)]
		[Test]
		public void Check_Validate_SearchParams(int start,int size)
		{
			//Arreng
			var filterParams = new Models.Database.BaseSearchParams();
			filterParams.PageSize = size;
			filterParams.Sort = "";
			filterParams.StartIndex = start;
			filterParams.Token = "1";
			//Assert
			Assert.That(()=>Common.GetSearchParams(filterParams), Throws.Exception);
		}
		[TestCase(null)]
		[Test]
		public void Get_SearchBaseParams_With_Null_Exception(Models.Database.BaseModel param)
		{
			//Assert
			Assert.That(() => Common.GetDataParams(param), Throws.Exception);
		}
		[TestCase(null)]
		[Test]
		public void Get_Result_With_Null_Exception(DynamicParameters p)
		{
			//Arrenge
			object data = null;
			//Assert
			Assert.That(() => Common.GetResult(p,data), Throws.Exception);
		}
		[Test]
		public void Get_Result_Check_Type()
		{
			//Arrenge
			var p=new DynamicParameters();
			//Act
			 p=Common.GetSearchParams();
			var result= Common.GetResult(p, null);
			//Assert
			Assert.That(result, Is.TypeOf<Models.Database.DBResult>());
		}
		[Test]
		public void GenerateParams_Check_Type()
		{
			//Arrenge
			var p = new DynamicParameters();
			//Act
			p = Common.GenerateParams(null,null);
			//Assert
			Assert.That(p, Is.TypeOf<DynamicParameters>());
		}

	}
}
