using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ILS.Domain;

namespace ILS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        List<UserModel> contact;

        ILSContext context;
        public AdminController(ILSContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReadUser()
        {
            JsonResult jr = (JsonResult)UsersList();

            return jr;
        }



        [HttpPost]
        public JsonResult CreateUser(List<UserModel> data)
        {

            contact = data;
            foreach (User user in context.User)
            {
                int count = 0;
                foreach (UserModel model in data)
                {
                    if (model.Name == user.Name)
                    {
                        user.IsApproved = model.IsApproved;
                        count++;
                        break;
                    }

                }
                if (count == 0)
                {
                    user.Coins = 0;
                    user.Roles = new HashSet<Role>();
                    user.IsApproved = false;
                }

            }
            context.SaveChanges();
            return Json(new
            {
                contact
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult UpdateUser(List<UserModel> data)
        {
            contact = data;
            return Json(new
            {
                contact
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteUser(List<UserModel> data)
        {
            contact = data;
            return Json(new
            {
                contact
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UsersList()
        {
            List<UserModel> jsonList = new List<UserModel>();
            IEnumerable<User> activeUsers = Enumerable.Where<User>(context.User, x => x.Roles.Count > 0);
            foreach (User user in activeUsers)
            {
                UserModel temp = new UserModel();
                temp.Name = user.Name;
                temp.FirstName = user.FirstName;
                temp.LastName = user.LastName;
                temp.Email = user.Email;
                temp.EXP = user.Coins;
                temp.IsApproved = user.IsApproved;
                jsonList.Add(temp);
            }
            return Json(new
            {
                jsonList
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UserProfile(string login)
        {
            var query = from user in context.User
                        where user.Name == login
                        select new
                        {
                            Name = user.Name,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            IsAdmin = Enumerable.Count<Role>(user.Roles, x => x.Name == "Admin") > 0 ? true : false,
                            IsTeacher = Enumerable.Count<Role>(user.Roles, x => x.Name == "Teacher") > 0 ? true : false,
                            IsStudent = Enumerable.Count<Role>(user.Roles, x => x.Name == "Student") > 0 ? true : false,
                            EXP = user.Coins
                        };

            JsonResult jr = new JsonResult();

            jr.Data = query;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }

        public JsonResult UpdateProfile(string login, string email, string firstName, string lastName, bool isAdmin, bool isTeacher, bool isStudent)
        {
            User selectedUser = Enumerable.Single<User>(context.User, x => x.Name == login);
            if (selectedUser == null)
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }
            selectedUser.Email = email;
            selectedUser.FirstName = firstName;
            selectedUser.LastName = lastName;

            context.SaveChanges();
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);

        }


    }
}
