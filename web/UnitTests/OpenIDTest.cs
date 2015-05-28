using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILS.Domain;
using ILS.Domain.GameAchievements;
using System.Web.Mvc;
using ILS.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace UnitTests
{
    [TestClass]
    public class OpenIDTest : BaseTest
    {

        protected override void AddMockData(ILSContext context)
        {
        }

        [TestMethod]
        public void TestLogin()
        {
            var controller = CreateController<OpenIDController>();
            OpenIDModel model = new OpenIDModel();
            model.Email = "testemail@mail.ru";
            model.Login = "testuser";
            double timestamp = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            model.Key = timestamp.ToString();
            model.Hash = CalculateSHA1("Something" + timestamp.ToString());
            List<OpenIDModel> models = new List<OpenIDModel>();
            models.Add(model);
            ActionResult result = controller.Index(models);
            Assert.IsTrue(result is JsonResult);
            dynamic data = ((JsonResult)result).Data;
            Assert.IsTrue(data.success);
        }



        static string CalculateSHA1(string text)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }
    }
}
