using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.Test.StoredProcedure
{
	[TestFixture]
	public class SP_Log_InsertTests
	{
        Mock<FTSS.DP.DapperORM.ISQLExecuter> _executer;
        Models.Database.StoredProcedures.SP_Log_Insert_Params _data;
        DP.DapperORM.StoredProcedure.SP_Log_Insert apiObject;

        [SetUp]
        public void Setup()
        {
            _data = new Models.Database.StoredProcedures.SP_Log_Insert_Params();
            _executer = new Mock<FTSS.DP.DapperORM.ISQLExecuter>();
            apiObject = new DP.DapperORM.StoredProcedure.SP_Log_Insert("my connection string", _executer.Object);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void Constructor_WhenConnectionStringIsEmpty_ThrowsArgumentNullException(string cns)
        {
            Assert.That(() => {
                new DP.DapperORM.StoredProcedure.SP_Log_Insert(cns);
            }, Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_WhenConnectionStringIsValid_NotThrowsArgumentNullException()
        {
            var apiObject = new DP.DapperORM.StoredProcedure.SP_Log_Insert("my connection string");
            Assert.That(apiObject, Is.Not.Null);
            Assert.That(apiObject, Is.TypeOf<DP.DapperORM.StoredProcedure.SP_Log_Insert>());
        }

		[Test]
		public async Task Call_WhenDataIsNull_ThrowsArgumentNullException()
		{

            Assert.That(()=> apiObject.Call(null), Throws.Exception);
		}
		[Test]
        public async Task Call_WhenDataIsValid_ReturnDBResult()
        {
            //Arrenge
            _data.Msg = "Test Error";
            var result =await apiObject.Call(_data);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ErrorCode, Is.EqualTo(200));
        }
    }
}
