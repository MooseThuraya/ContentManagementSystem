using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MustafaCMS.Models;


namespace MustafaCMS.Controllers
{

    public class UserController : Controller
    {
      
        // GET: User
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            User userModel = new User();

            return View(userModel);
        }

        // Post: User
        [HttpPost]                    // User Object
        public ActionResult AddOrEdit(User userModel)
        {

            using (AuthenticationDBEntities db = new AuthenticationDBEntities())
            {
                userModel.Username = userModel.Username.Trim();
                userModel.firstName = userModel.firstName.Trim();
                userModel.lastName = userModel.lastName.Trim();
                userModel.Password = userModel.Password.Trim();
                userModel.ConfirmPassword = userModel.ConfirmPassword.Trim();
                userModel.Email = userModel.Email.Trim();
                if (db.Users.Any(x => x.Username == userModel.Username))
                {
                    userModel.LoginErrorMessage = "* Username already exists";
                    return View("AddOrEdit", userModel);
                }
                else if (db.Users.Any(x => x.Email == userModel.Email))
                {
                    userModel.LoginErrorMessage = "* Email already exists";
                    return View("AddOrEdit", userModel);
                }
                else
                {
                    if (Session["userId"] == null)
                    {
                        db.Users.Add(userModel);
                        db.SaveChanges();
                        ModelState.Clear();
                        ViewBag.SuccessMessage = "Registration Successful";
                        var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
                        Session["userId"] = userDetails.UserID;
                        Session["Username"] = userDetails.Username;
                        Session["SignUp"] = true;
                        //View("Index", new User());
                        return RedirectToAction("LoginPage", "Login", new User());
                    }
                    else
                    {
                        db.Users.Add(userModel);
                        db.SaveChanges();
                        ModelState.Clear();
                        ViewBag.SuccessMessage = "Registration Successful";
                        var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
                        //View("Index", new User());
                        return RedirectToAction("Index", "Users", new User());
                    }
                    
                }
            }



            /*
            using (AuthenticationDBEntities db = new AuthenticationDBEntities())
            {

                db.Users.Add(userModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("AddOrEdit", new User());
            */
        }
        public JsonResult IsUserNameExists(string Username)
        {
            AuthenticationDBEntities db = new AuthenticationDBEntities();

            return Json(!db.Users.Any(x => x.Username == Username), JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsEmailExists(string Email)
        {
            AuthenticationDBEntities db = new AuthenticationDBEntities();

            return Json(!db.Users.Any(x => x.Email == Email), JsonRequestBehavior.AllowGet);
        }

        public string TrimString (string s)
        {
            return s.Trim();
        }


    }
}
