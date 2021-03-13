using FTSS.Models.Database.StoredProcedures;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.Test.StoredProcedure
{
	[TestFixture]
	public class SP_User_ChangePasswordTests
	{
        Mock<FTSS.DP.DapperORM.ISQLExecuter> _executer;
        Models.Database.StoredProcedures.SP_User_ChangePassword_Params _data;
        DP.DapperORM.StoredProcedure.SP_User_ChangePassword apiObject;

        [SetUp]
        public void Setup()
        {
            _data = new Models.Database.StoredProcedures.SP_User_ChangePassword_Params();
            _executer = new Mock<FTSS.DP.DapperORM.ISQLExecuter>();
            apiObject = new DP.DapperORM.StoredProcedure.SP_User_ChangePassword("my connection string", _executer.Object);
        }
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void Constructor_WhenConnectionStringIsEmpty_ThrowsArgumentNullException(string cns)
        {
            Assert.That(() => {
                new DP.DapperORM.StoredProcedure.SP_User_ChangePassword(cns);
            }, Throws.ArgumentNullException);
        }
        [Test]
        public void Constructor_WhenConnectionStringIsValid_NotThrowsArgumentNullException()
        {
            var apiObject = new DP.DapperORM.StoredProcedure.SP_User_ChangePassword("my connection string");
            Assert.That(apiObject, Is.Not.Null);
            Assert.That(apiObject, Is.TypeOf<DP.DapperORM.StoredProcedure.SP_User_ChangePassword>());
        }

        [Test]
        public async Task Call_WhenDataIsNull_ThrowsArgumentNullException()
        {
            //Act&Assert
            Assert.That(() => apiObject.Call(null), Throws.Exception);
        }


        [Test]
        public async Task Call_WhenCallIsVertified_ReturnDBResult()
        {
            //Arrenge
            var callTest=new Mock<Models.Database.ISP<DP.DapperORM.StoredProcedure.SP_User_ChangePassword>>();
            callTest.Setup(s => s.Call(null)).Returns(Task.FromResult(new Models.Database.DBResult()));

            //Act
           var result = await callTest.Object.Call(null);
            //Assert
            callTest.Verify(s => s.Call(null));
        }
        [Test]
        public async Task Call_WhenDataIsValid_ReturnDBResult()
        {
            //Arrenge
            var callTest = new Mock<Models.Database.ISP<DP.DapperORM.StoredProcedure.SP_User_ChangePassword>>();
            callTest.Setup(s => s.Call(null)).Returns(Task.FromResult(new Models.Database.DBResult(200, "", null, 1)));

            //Act
            var result = await callTest.Object.Call(null);
            //Assert
            Assert.AreEqual(200,result.ErrorCode);
        }
    }
}
