using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor;

namespace UI.Areas.Admin.Controllers
{
    public class MetaController : BaseController
    {
        // GET: Meta
        public ActionResult Index()
        {
            return View();
        }
        MetaBLL bll = new MetaBLL();
        public ActionResult AddMeta()
        {
            MetaDTO dto = new MetaDTO();
            return View(dto);
        }
        [HttpPost]
        public ActionResult AddMeta(MetaDTO model)
        {
            if (ModelState.IsValid)           //we have to control model state is valid or not
            {
                if (bll.AddMeta(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                }
                else               //Else if we have to give a general error.
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else                      //If model state is not valid , this means there's an empty area.
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            MetaDTO newmodel = new MetaDTO();
            return View(newmodel);
        }
        public ActionResult MetaList()
        {
            List<MetaDTO> model = new List<MetaDTO>();
            model = bll.GetMetaData();
            return View(model);
        }
        public ActionResult UpdateMeta(int ID)            //view will send us a MetaID so we will get meta with ID
        {
            MetaDTO model = new MetaDTO();
            model = bll.GetMetaWithID(ID);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateMeta(MetaDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateMeta(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                    ViewBag.ProcessState =General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState=General.Messages.EmptyArea;
            }
            return View(model);
        }
        public JsonResult DeleteMeta(int ID)
        {
            bll.DeleteMeta(ID);
            return Json("");
        }
    }
}