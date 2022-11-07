using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApp.Context;
using WebApp.Handlers;
using WebApp.Models;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        MyContext myContext;

        public AccountController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        //Get account
        public IActionResult Login()
        {
            return View();
        }
                

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            /*var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .Where(x => x.Employee.Email.Equals(username) && x.Password.Equals(password))
                .Select(x => new { x.Employee.FullName, x.Employee.Email, x.Role });*/

            

            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .SingleOrDefault(x => x.Employee.Email.Equals(username));

            var valPassword = Hashing.ValidatePassword(password, data.Password);

            //var datauser = TempData["UserID"] = data.Id;

            /*ResponseLogin responseLogin = new ResponseLogin()
            {
                FullName = data.Employee.FullName,
                Email = data.Employee.Email,
                Role = data.Role.Name
            };*/
            if (data != null && valPassword)
            {
                HttpContext.Session.SetInt32("Id", data.Id);
                HttpContext.Session.SetString("FullName", data.Employee.FullName);
                HttpContext.Session.SetString("Email", data.Employee.Email);
                HttpContext.Session.SetString("Role", data.Role.Name);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Register()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullname, string email, DateTime birthday, string password)
        {
            Employee employee = new()
            {
                FullName = fullname,
                Email = email,
                BirthDate = birthday
            };
            myContext.Employees.Add(employee);

            var validate = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email));
            if (validate == null)
            {
                var result = myContext.SaveChanges();

                if (result > 0)
                {
                    var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                    //var id = myContext.Employees.Where(x => x.Email.Equals(email)).Select(x => new {x.Email}).Select(x => new {x.Id});
                    User user = new User()
                    {
                        Id = id,
                        Password = Hashing.HashPassword(password),
                        RoleId = 1
                    };
                    myContext.Users.Add(user);
                    var resultUser = myContext.SaveChanges();
                    if (resultUser > 0)
                    {
                        return RedirectToAction("Login", "Account");
                    }

                }
            }
            return RedirectToAction("Forbidden", "ErrorPage");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string Email,string Password, string ConfirmPassword)
        {

            var data = myContext.Users
                .Join(myContext.Employees, u => u.Id, emp => emp.Id, (u, emp) => new { u, emp })
                .Join(myContext.Roles, ur => ur.u.RoleId, r => r.Id, (ur, r) => new
                {
                    Email = ur.emp.Email,
                    Password = ur.u.Password,
                    RoleId = ur.u.RoleId,
                    UserId = ur.u.Id,
                    EmployeeId = ur.u.Id
                })
                .SingleOrDefault(x => x.Email.Equals(Email));

            /*.Include(x => x.Employee)
            .Include(x => x.Role)                
            .SingleOrDefault(x => x.Employee.Email.Equals(email) && x.Password.Equals(oldPassword));*/
            //.Select(x => new { x.RoleId, x.Employee.Email, x.Password });
            if (data != null)
            {

                User user = new User()
                {
                    Id = data.UserId,
                    Password = Hashing.HashPassword(ConfirmPassword),
                    RoleId = data.RoleId
                };

                myContext.Entry(user).State = EntityState.Modified;
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(string email, string newPassword)
        {
            var data = myContext.Users
                .Join(myContext.Employees, u => u.Id, emp => emp.Id, (u, emp) => new { u, emp })
                .Join(myContext.Roles, ur => ur.u.RoleId, r => r.Id, (ur, r) => new
                {
                    Email = ur.emp.Email,
                    Password = ur.u.Password,
                    RoleId = ur.u.RoleId,
                    UserId = ur.u.Id,
                    EmployeeId = ur.u.Id                    
                })
                .SingleOrDefault(x => x.Email.Equals(email));

            if (data != null)
            {

                User user = new()
                {
                    Id = data.UserId,
                    Password = Hashing.HashPassword(newPassword),
                    RoleId = data.RoleId,
                };

                myContext.Entry(user).State = EntityState.Modified;
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                {
                    return RedirectToAction("Login","Account");
                }
            }

            return View();
        }
    }
}
