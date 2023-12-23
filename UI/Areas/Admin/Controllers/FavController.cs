using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace UI.Areas.Admin.Controllers
{
    public class FavController : BaseController
    {
        // GET: Admin/Fav
        FavBLL bll = new FavBLL();
        public ActionResult UpdateFav()         //Each website needs only a title and one fav icon and a logo, So we dont need to make an ad page
        {
            FavDTO dto = new FavDTO();
            dto = bll.GetFav();
            return View(dto);
        }
        [HttpPost]
        public ActionResult UpdateFav(FavDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.FavImage != null)
                {
                    string favname = " ";
                    HttpPostedFileBase postedfilefav = model.FavImage;
                    Bitmap FavImage = new Bitmap(postedfilefav.InputStream);
                    Bitmap resizefavImage = new Bitmap(FavImage, 100, 100);
                    string ext = Path.GetExtension(postedfilefav.FileName);
                    if (ext == ".icom" || ext == ".jpeg" || ext == ".jpg"||ext == ".png")
                    {
                        string favunique = Guid.NewGuid().ToString();
                        favname = favunique + postedfilefav.FileName;
                        resizefavImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + favname));
                        model.Fav = favname;
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                }
                if (model.LogoImage != null)
                {
                    string logoname = " ";
                    HttpPostedFileBase postedFilelogo = model.LogoImage;
                    Bitmap LogoImage = new Bitmap(postedFilelogo.InputStream);
                    Bitmap resizeLogoImage = new Bitmap(LogoImage, 100, 100);
                    string ext = Path.GetExtension(postedFilelogo.FileName);
                    if (ext == ".icom" || ext == ".jpeg" || ext == ".jpg"|| ext == ".png")
                    {
                        string logounique = Guid.NewGuid().ToString();
                        logoname = logounique + postedFilelogo.FileName;
                        resizeLogoImage.Save(Server.MapPath("~/Areas/Admin/Content/FavImage/" + logoname));
                        model.Logo = logoname;
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                }
                FavDTO retuendto = new FavDTO();
                retuendto = bll.UpdateFav(model);
                if(model.FavImage!=null)
                {
                    if(System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/FavImage/" + retuendto.Fav)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/FavImage/" + retuendto.Fav));
                    }
                }
                if (model.LogoImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~Areas/Admin/Content/FavImage/" + retuendto.Logo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~Areas/Admin/Content/FavImage/" + retuendto.Logo));
                    }
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }
            return View(model);
        }
    }
}