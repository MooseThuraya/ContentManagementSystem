using MustafaCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MustafaCMS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginPage()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        //an action for post URL
        //default action is GET
        [HttpPost] //this is a custom type of action
        public ActionResult Authorize(User userModel)
        {
           
            using (AuthenticationDBEntities db = new AuthenticationDBEntities())
            {
                var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
                
                if(userDetails == null)
                {
                    userModel.LoginErrorMessage = "* Incorrect Username or Password";
                    return View("LoginPage", userModel);
                }
                
                else if (userDetails.IsAdmin == false || userDetails.IsAdmin == null)
                {
                    userModel.LoginErrorMessage = "* There are currently no pages for non admins";
                    return View("LoginPage", userModel);
                }
                else
                {
                    Session["userId"] = userDetails.UserID;
                    Session["Username"] = userDetails.Username;
                    Session["firstName"] = userDetails.firstName;
                    Session["lastName"] = userDetails.lastName;
                    Session["email"] = userDetails.Email;

                    return RedirectToAction("Index", "Home");
                }
            }
                //return View(); not needed anymore
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userId"];
            Session.Abandon();
            return RedirectToAction("LoginPage", "Login");
        }
    }
}