
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Task_4.Models;
using Task_4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Task_4.Controllers
{
    public class UsersController : Controller
    {
       UsersContext db;
        public UsersController(UsersContext context)
        {
           db=context;
          
        }
        [Authorize]
        public IActionResult Index()
        {
            db.Users.Load();
            var list = db.Users.ToList();
            return View(list);
        }

 

        public async Task<IActionResult> Block(int[] selectedUsers)
        {
  
            foreach (var item in selectedUsers)
            {
                    User user = await db.Users.FindAsync(item);
                if (user == null)
                {
                    return NotFound();
                }
                user.Status = "block";
                if (user.Status.Equals("block") && User.Identity.Name.Equals(user.Email))
                {
                   await Logout();
                }
                await db.SaveChangesAsync();    

            }
            
            return RedirectToAction("Index");
        }
 
        public async Task<IActionResult> UnBlock(int[] selectedUsers)
        {

            foreach (var item in selectedUsers)
            {

                User user = await db.Users.FindAsync(item);

                if (user == null)
                {
                    return NotFound();
                }
                user.Status = "active";
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int[] selectedUsers)
       {
            foreach (var item in selectedUsers)
            {
                User user = await db.Users.FindAsync(item);
                if (user == null)
                {
                    return NotFound();
                }
                user.Status = "block";
                if (User.Identity.Name.Equals(user.Email))
                {
                    await Logout();
                }
                if (user.Status.Equals("block") )
                {
                    db.Users.Remove(user);
                }
                await db.SaveChangesAsync();
                
             
            }

            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
