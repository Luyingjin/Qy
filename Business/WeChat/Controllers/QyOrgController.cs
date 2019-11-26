using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChat.Logic.BusinessFacade;

namespace WeChat.Controllers
{
    public class QyOrgController : BaseController
    {
        public JsonResult UpdateUser(string QyID)
        {
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();
            wxFO.ReFreshUser(QyID);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
