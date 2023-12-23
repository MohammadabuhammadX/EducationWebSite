
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Models.Attirbutes
{
    public class LoginControl : FilterAttribute, IActionFilter          // We have to derive class from anthoer attribute
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserStatic.UserID == 0)         // Now we can use this attribute for each page ,But in order to make it easier , I Just want to add a new controller
                filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");
            /*if (!HttpContext.Current.User.Identity.IsAuthenticated)
                filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");     //This is going to be enough , but sometimes you might get an error for the UserID // So I do want to check wherther the user is legitimate ot not*/
        }
    }
}

//In using interfaces we have to override interfaces methodes in this particuler class


// FilterAttribute : Is an abstract class and IActionFilter: Is an interface and we can control our cookies