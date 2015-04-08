using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using ILS.Domain;
using ILS.Models;

using System.Security.Cryptography;

namespace ILS.Web.Controllers
{
    public class OpenIDController : Controller
    {
        

		ILSContext context;
		public OpenIDController(ILSContext context)
		{
			this.context = context;

		}

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }


        [HttpGet]
        public ActionResult Index()
        {
			return View();
        }


        [HttpPost]
        public ActionResult Index(List<OpenIDModel> data)
        {
            String login = data[0].Login;
            if (data[0].Login == null || data[0].Login == "")
            {
                return Json(new { success = false });
            }
            int count = context.User.Count(x => x.Name == login);
            if (count > 0)
            {
                User selectedUser = Enumerable.Single<User>(context.User, x => x.Name == login);
                if (selectedUser.PasswordHash == null  && CalculateSHA1("Something"+data[0].Key) == data[0].Hash)
                {
                    selectedUser.Email = data[0].Email;
                    //selectedUser.FirstName = data[0].FirstName;
                    //selectedUser.LastName = data[0].LastName;
                    context.SaveChanges();
                    FormsService.SignIn(login, false);
                    bool isAdmin = selectedUser.Roles.Count<Role>(x => x.Name == "Admin" || x.Name == "Teacher") > 0 ? true : false;
                    return Json(new 
                        { 
                            success = true, 
                            admin = isAdmin, 
                            firstName = selectedUser.FirstName, 
                            lastName = selectedUser.LastName
                        }
                    );
                }
                return Json(new { success = false });
                
            }
            if (CalculateSHA1("Something" + data[0].Key) == data[0].Hash)
            {
                context.User.Add(new User()
                {
                    Name = data[0].Login,
                    FirstName = data[0].FirstName,
                    LastName = data[0].LastName,
                    Email = data[0].Email,
                    Roles = new List<Role> { context.Role.Add(new Role() { Name = "Student" }) }
                    //Roles = new List<Role> { Enumerable.Single<Role>(context.Role, x => x.Name == "Student") }
                });
                FormsService.SignIn(login, false);
                context.SaveChanges();
                return Json(new
                {
                    success = true,
                    admin = false,
                    firstName = data[0].FirstName,
                    lastName = data[0].LastName
                });
            }
            return Json(new { success = false });
        }

        /*
        [HttpPost]
        public JsonResult AcceptPassword(string key, string login, string password)
        {
            if (key != "Jeronimo" || login == null || login == "" || password == null)
            {
                return Json(new { success = false });
            }
            int count = context.User.Count(x => x.Name == login);
            if (count > 0)
            {
                User selectedUser = Enumerable.Single<User>(context.User, x => x.Name == login);
                if (selectedUser.PasswordHash == null) 
                {
                    selectedUser.OpenIDPassword = password;
                    context.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            return Json(new { success = false });
        }
        */

        static string CalculateSHA1(string text)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }

    }

    
}
