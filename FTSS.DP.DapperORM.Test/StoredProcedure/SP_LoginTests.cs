using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.Test.StoredProcedure
{
	[TestFixture]
	public class SP_LoginTests
	{
        Mock<FTSS.DP.DapperORM.ISQLExecuter> _executer;
        Models.Database.StoredProcedures.SP_Login_Params _data;
        DP.DapperORM.StoredProcedure.SP_Login apiObject;

        [SetUp]
        public void Setup()
        {
            _data = new Models.Database.StoredProcedures.SP_Login_Params();
            _executer = new Mock<FTSS.DP.DapperORM.ISQLExecuter>();
            apiObject = new DP.DapperORM.StoredProcedure.SP_Login("my connection string", _executer.Object);
        }
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void Constructor_WhenConnectionStringIsEmpty_ThrowsArgumentNullException(string cns)
        {
            Assert.That(() => {
                new DP.DapperORM.StoredProcedure.SP_Login(cns);
            }, Throws.ArgumentNullException);
        }
        [Test]
        public void Constructor_WhenConnectionStringIsValid_NotThrowsArgumentNullException()
        {
            var apiObject = new DP.DapperORM.StoredProcedure.SP_Login("my connection string");
            Assert.That(apiObject, Is.Not.Null);
            Assert.That(apiObject, Is.TypeOf<DP.DapperORM.StoredProcedure.SP_Login>());
        }

        [Test]
        public async Task Call_WhenDataIsNull_ThrowsArgumentNullException()
        {
            //Act&Assert
            Assert.That(() => apiObject.Call(null), Throws.Exception);
        }

    }
}
