using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class VideooController : BaseController
    {
        VideooBLL bll = new VideooBLL();
        // GET: Admin/Videoo
        public ActionResult VideoList()
        {
            List<VideooDTO> dtolist = new List<VideooDTO>();
            dtolist = bll.GetVideos();
            return View(dtolist);
        }
        public ActionResult AddVideoo()
        {
            VideooDTO dto = new VideooDTO();
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddVideoo(VideooDTO model)
        {
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/";
                mergelink += path;
                model.VideoPath = string.Format(@"< iframe width = ""300"" height = ""200"" src = ""{0}""frameborder = ""0""  allowfullscreen ></ iframe >", mergelink);
                if (bll.AddVideoo(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new VideooDTO();
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;


            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
        public ActionResult UpdateVideo(int ID)
        {
            VideooDTO dto = bll.GetVideoWithID(ID);           //Getting video information 
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateVideo(VideooDTO model)
        {
            if (ModelState.IsValid)               //for title we dont't need to change anything , But for the video path we have to adjust it again and add video
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/";
                mergelink += path;
                model.VideoPath = string.Format(@"< iframe width = ""300"" height = ""200"" src = ""{0}""frameborder = ""0""  allowfullscreen ></ iframe >", mergelink);
                if (bll.UpdateVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                    ViewBag.ProcessState =General.Messages.GeneralError;
            }
            else
                ViewBag.ProcessState = General.Messages.EmptyArea;
            return View(model);
        }
        public JsonResult DeleteVideo(int ID)
        {
            bll.DeleteVideo(ID);
            return Json("");
        }
    }
}
