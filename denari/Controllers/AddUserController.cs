using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRIMAS.Models;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

namespace CRIMAS.Controllers
{
    public class AddUserController : Controller
    {
        private CrimasDb db = new CrimasDb();

        //
        // GET: /AddUser/

        public ActionResult Index()
        {

            return View(db.UserProfiles.ToList());
        }

        //
        // GET: /AddUser/Details/5

        public ActionResult Details(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // GET: /AddUser/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AddUser/Create

        [HttpPost]
        public ActionResult Create(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                //db.UserProfiles.Add(userprofile);
                //db.SaveChanges();
                //return RedirectToAction("Index");

                try
                {
                    //WebSecurity.CreateUserAndAccount(userprofile.UserName, userprofile.Password, new {userprofile.FirstName, userprofile.LastName, userprofile.Address, userprofile.ConfirmPassword, userprofile.role});
                    //db.UserProfiles.Add(userprofile);
                    WebSecurity.CreateUserAndAccount(userprofile.UserName, userprofile.Password, new { userprofile.FirstName, userprofile.LastName, userprofile.Address, userprofile.ConfirmPassword, userprofile.role });
                    db.UserProfiles.Add(userprofile);
                    db.SaveChanges();

                    var roles = (SimpleRoleProvider)Roles.Provider;
                    var membership = (SimpleMembershipProvider)Membership.Provider;

                    //Add user to selected role
                    if (!roles.RoleExists(userprofile.role))
                    {
                        roles.CreateRole(userprofile.role);
                    }
                    if (membership.GetUser(userprofile.UserName, false) == null)
                    {
                        membership.CreateUserAndAccount(userprofile.UserName, userprofile.Password);
                    }
                    if (!roles.GetRolesForUser(userprofile.UserName).Contains(userprofile.role))
                    {
                        roles.AddUsersToRoles(new[] { userprofile.UserName }, new[] { userprofile.role });
                    }

                    return RedirectToAction("Index");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            return View(userprofile);
        }

        private string ErrorCodeToString(MembershipCreateStatus membershipCreateStatus)
        {
            return "Unable to add user";
        }
        public ActionResult ManageRoles()
        {
            return View();
        }
        //
        // GET: /AddUser/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /AddUser/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        //
        // GET: /AddUser/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /AddUser/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            UserProfile userprofile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userprofile);

            //remove user from membership
            //membership.DeleteUser(userprofile.UserName, true);
            membership.DeleteAccount(userprofile.UserName);
            roles.RemoveUsersFromRoles(new[] { userprofile.UserName }, new[] { userprofile.role });

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}