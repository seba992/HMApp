using System.Data.Common;
using System.Windows.Forms;
using DiamondApp;
using DiamondApp.EntityModel;
using DiamondApp.Tools;
using NUnit.Framework;

namespace DiamondAppTests
{
    [TestFixture]
    public class UnitTests
    {
        private DiamondDBEntities _testCtx = new DiamondDBEntities();

        [SetUp]
        public void SetUpMethod()
        {
            _testCtx = new DiamondDBEntities();
        }

        [TestCase("password", Result = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8")]
        [TestCase("Dictionary", Result = "f12e2c9be9b1c548380f71dc2878225386d7c11f306123cfeb9f9fa9cb9d1edd")]
        [TestCase("Zaq12wsx", Result = "9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1")]
        public string CheckSha256Alghoritm(string pass)
        {
            //given
            //when
            var hashPassword = ShaConverter.sha256_hash(pass);

            //then
            return hashPassword;
        }
    }
}
