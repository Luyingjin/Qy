using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChat.Logic.Domain;
using WeChat.Logic.BusinessFacade;
using Formula.Exceptions;
using Formula.Helper;

namespace WeChat.Controllers
{
    public class QyDepartController : BaseController<QyDepart>
    {
        public override ActionResult Tree()
        {
            var QyID = GetQueryString("QyID");
            var exist = entities.Set<QyDepart>().Where(c => c.QyID == QyID).Any();
            if (!exist)
            {
                var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();
                wxFO.ReFreshUser(QyID);
            }
            var ge = CacheHelper.Get(string.Format("WxDepartEnum{0}", QyID));
            if (ge == null)
            {
                var departenum = entities.Set<QyDepart>().Where(c => c.QyID == QyID).Select(c => new { text = c.DepartName, value = c.ID });
                CacheHelper.Set(string.Format("WxDepartEnum{0}", QyID), JsonHelper.ToJson(departenum));
                ge = CacheHelper.Get(string.Format("WxDepartEnum{0}", QyID));
            }
            TempData["departEnum"] = ge;
            return base.Tree();
        }

        public override JsonResult GetTree()
        {
            var QyID = GetQueryString("QyID");
            var query = entities.Set<QyDepart>().Where(c => c.QyID == QyID).OrderBy(i => i.DepartOrder);
            return Json(query.ToList(),JsonRequestBehavior.AllowGet);
        }

        public override JsonResult GetModel(string id)
        {
            var pId = Request["ParentID"];
            var model = GetEntity<QyDepart>(id);
            if (!String.IsNullOrWhiteSpace(pId))
            {
                var parentEntity = GetEntity<QyDepart>(pId);
                model.FullPath = parentEntity.FullPath;
                model.Length = parentEntity.Length;
            }
            return Json(model);
        }
    }
}
