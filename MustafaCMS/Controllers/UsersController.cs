using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MustafaCMS.Models;

namespace MustafaCMS.Controllers
{
    public class UsersController : Controller
    {
        private AuthenticationDBEntities db = new AuthenticationDBEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "UserID,Username,Password,IsAdmin,Email,firstName,lastName,ImagePath")]
        public ActionResult Create(User user)
        {
            user.Username = user.Username.Trim();
            user.firstName = user.firstName.Trim();
            user.lastName = user.lastName.Trim();
            user.Password = user.Password.Trim();
            user.ConfirmPassword = user.ConfirmPassword.Trim();
            user.Email = user.Email.Trim();

            using (AuthenticationDBEntities db = new AuthenticationDBEntities())
            {
                if (db.Users.Any(x => x.Username == user.Username && x.UserID != user.UserID))
                {
                    user.LoginErrorMessage = "* Username already exists";
                    return View("Create", user);
                }
                else if (db.Users.Any(x => x.Email == user.Email && x.UserID != user.UserID))
                {
                    user.LoginErrorMessage = "* Email already exists";
                    return View("Create", user);
                }
                else
                {
                    Session["userAdded"] = user.firstName + " "+ user.lastName;
                    db.Users.Add(user);
                    db.SaveChanges();
                    Session["add"] = true;
                    return RedirectToAction("Index");
                }
                
            }


            

            //return View(user);
        }

        // GET: Users/Edit/5
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "UserID,Username,Password,IsAdmin,Email,firstName,lastName,ImagePath")]
        public ActionResult Edit( User user )

        {

            user.Username = user.Username.Trim();
            user.firstName = user.firstName.Trim();
            user.lastName = user.lastName.Trim();
            user.Password = user.Password.Trim();
            user.ConfirmPassword = user.ConfirmPassword.Trim();
            user.Email = user.Email.Trim();

            using (AuthenticationDBEntities db = new AuthenticationDBEntities())
            {
                if (db.Users.Any(x => x.Username == user.Username && x.UserID != user.UserID))
                {
                    user.LoginErrorMessage = "* Username already exists";
                    return View("Edit", user);
                }
                else if (db.Users.Any(x => x.Email == user.Email && x.UserID != user.UserID))
                {
                    user.LoginErrorMessage = "* Email already exists";
                    return View("Edit", user);
                }
                else
                {

                    if (ModelState.IsValid)
                    {
                        Session["userEdited"] = user.firstName + " " + user.lastName;
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        Session["edit"] = true;
                        return RedirectToAction("Index");
                    }
                    return View(user);
                    
                }
            }
           
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            Session["delete"] = true;
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
