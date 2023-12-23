using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {                              //in this controller we will use UserBLL for the other operations ,so define BLL out of action
        UserBLL userbll = new UserBLL();
        // GET: Admin/Login
        public ActionResult Index()
        {
            UserDTO dto = new UserDTO();        //create an instance to send view.
            return View(dto);

        }
        [HttpPost]
        public ActionResult Index(UserDTO model)
        {
            if (model.Username!=null&&model.Password!=null)
            {
                UserDTO user = userbll.GetUserWithUsernameAndPassword(model);
                if (user.ID != 0)
                {
                    UserStatic.UserID=user.ID;          //  If the UserID is zero , This means that we've got to redirect pag to the login page.
                    UserStatic.isAdmin=user.isAdmin;
                    UserStatic.NameSurname = user.Name;
                    UserStatic.ImagePath = user.Imagepath;        //In each controller we can access the UserID with UesrStatic class
                    LogBLL.AddLog(General.ProcessType.Login, General.TableName.Login, 12);
                    return RedirectToAction("PostList", "Post");
                }
                else
                    return View(model);                   // In view we have to match property in model with text boxes
            }
            else
                return View(model);

        }
    }
}
//We'll make all admin operations in the admin area.
// That means that we have to add login in controller to the controller folder in the admin area.
//If there is any item with this username and password , the program will redirct to another page.
/*So now in this project , as you know ,we will use the FUOUR TIER ARCHITECTURE.
 * So it means that we won't make any logic or SQL operations and controllers for logic operations ,
 * We will send date to BLL class and in BLL for SQL operations we will send to the DAO class*/

