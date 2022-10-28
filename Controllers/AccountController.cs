using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApp.Context;
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
            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .Where(x => x.Employee.Email.Equals(username) && x.Password.Equals(password))
                .Select(x => new { x.Employee.FullName, x.Employee.Email, x.Role });

            /* var data = myContext.Users
                 .Include(x => x.Employee)
                 .Include(x => x.Role)
                 .SingleOrDefault(x => x.Employee.Email.Equals(username) && x.Password.Equals(password));

             ResponseLogin responseLogin = new ResponseLogin()
             {
                 FullName = data.Employee.FullName,
                 Email = data.Employee.Email,
                 Role = data.Role.Name
             };*/
            if (data != null)
            {               

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
            var result = myContext.SaveChanges();

            if (result > 0)
            {
                var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                //var id = myContext.Employees.Where(x => x.Email.Equals(email)).Select(x => new {x.Email}).Select(x => new {x.Id});
                User user = new User()
                {
                    Password = password,
                    RoleId = 1,
                    EmployeeId = id
                };
                myContext.Users.Add(user);
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                {
                    return RedirectToAction("Login", "Account");
                }

            }
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string email, string oldPassword, string newPassword)
        {
            var data = myContext.Employees
                .Join(myContext.Users, e => e.Id, u => u.EmployeeId, (e, u) => new { e, u })
                .Join(myContext.Roles, eu => eu.u.RoleId, r => r.Id, (eu, r) => new
                {
                    Email = eu.e.Email,
                    Password = eu.u.Password,
                    UserId = eu.u.Id,
                    RoleId = eu.u.RoleId,
                    EmployeeId = eu.e.Id
                })
                .SingleOrDefault(x => x.Email.Equals(email) && x.Password.Equals(oldPassword));

                /*.Include(x => x.Employee)
                .Include(x => x.Role)                
                .SingleOrDefault(x => x.Employee.Email.Equals(email) && x.Password.Equals(oldPassword));*/
                //.Select(x => new { x.RoleId, x.Employee.Email, x.Password });
            if (data != null)
            {

                User user = new User()
                {
                    Id = data.UserId,
                    Password = newPassword,
                    RoleId = data.RoleId,
                    EmployeeId = data.EmployeeId
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
        public IActionResult ForgotPassword(string email, string newPassword)
        {
            return View();
        }
    }
}
