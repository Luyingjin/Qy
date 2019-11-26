using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChat.Logic.Domain;
using Formula.Helper;
using MvcAdapter;
using Formula;
using WeChat.Logic.BusinessFacade;
using WeChat.Logic;

namespace WeChat.Controllers
{
    public class SpaceController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string qyID = this.Request["QyID"];
            var qyUserRelationFO = Formula.FormulaHelper.CreateFO<QyUserRelationFO>();
            if (String.IsNullOrEmpty(qyID))
                qyID = qyUserRelationFO.GetDefaultQyID(this.CurrentUserInfo.UserID);
            else
            {
                qyUserRelationFO.SetDefaultQy(this.CurrentUserInfo.UserID, qyID);
                qyUserRelationFO.SetFocusQy(this.CurrentUserInfo.UserID, qyID);
            }
            this.ViewBag.CurrentUserID = this.CurrentUserInfo.UserID;
            if (String.IsNullOrEmpty(qyID)) throw new Exception("您没有参与企业号，无法进入微信管理");
            string userID = this.ViewBag.CurrentUserID;
            var qyInfo = entities.Set<QyAccount>().Where(c => c.ID == qyID).FirstOrDefault();
            this.ViewBag.QyInfo = qyInfo;
            this.ViewBag.QyJson = JsonHelper.ToJson(qyInfo);
            var t = SysBool.T.ToString();
            var userQy = entities.Set<QyAccountUserRelation>()
                .Where(c => c.UserID == this.CurrentUserInfo.UserID && c.IsUsed == t && c.QyAccount != null)
                .Select(c => c.QyAccount).ToList();
            this.ViewBag.RelationQy = JsonHelper.ToJson(userQy);
            return View();
        }

        /// <summary>
        /// 获取用户常用企业号
        /// </summary>
        /// <param name="qb"></param>
        /// <returns></returns>
        public JsonResult GetMyQyInfo(QueryBuilder qb)
        {
            string userID = this.CurrentUserInfo.UserID;
            var used = SysBool.T.ToString();
            var qyInfoList = entities.Set<QyAccountUserRelation>()
                .Where(c => c.UserID == userID && c.IsUsed == used && c.QyAccount != null&&c.QyAccount.IsDelete==0).Select(c => c.QyAccount).Where(qb);

            GridData gridData = new GridData(qyInfoList);
            gridData.total = qb.TotolCount;
            return Json(gridData);
        }

        /// <summary>
        /// 获取用户所有企业号
        /// </summary>
        /// <param name="qb"></param>
        /// <returns></returns>
        public JsonResult GetAllQyInfo(QueryBuilder qb)
        {
            string userID = this.CurrentUserInfo.UserID;
            var qyInfoList = entities.Set<QyAccountUserRelation>()
                .Where(c => c.UserID == userID && c.QyAccount != null && c.QyAccount.IsDelete == 0).Select(c => c.QyAccount).Where(qb);

            GridData gridData = new GridData(qyInfoList);
            gridData.total = qb.TotolCount;
            return Json(gridData);
        }

        /// <summary>
        /// 获取企业号下的菜单
        /// </summary>
        /// <param name="DefineID"></param>
        /// <param name="SpaceCode"></param>
        /// <param name="ProjectInfoID"></param>
        /// <returns></returns>
        public JsonResult GetSpaceMenu()
        {
            var data=CacheHelper.Get("WxSpaceMenu");
            if (data == null)
            {
                var menu = entities.Set<QyAccountSpaceMenu>().OrderBy(c => c.SortIndex).ToList();
                CacheHelper.Set("WxSpaceMenu", menu);
                return Json(menu);
            }
            return Json(data);
        }

        /// <summary>
        /// 设置常用企业号
        /// </summary>
        /// <param name="QyID"></param>
        /// <returns></returns>
        public JsonResult SetFocusQy(string QyID)
        {
            var qyUserRelationFO = Formula.FormulaHelper.CreateFO<QyUserRelationFO>();
            qyUserRelationFO.SetFocusQy(this.CurrentUserInfo.UserID, QyID);
            return Json(JsonAjaxResult.Successful());
        }

        /// <summary>
        /// 取消常用企业号
        /// </summary>
        /// <param name="QyID"></param>
        /// <returns></returns>
        public JsonResult CancelFocusQy(string QyID)
        {
            var qyUserRelationFO = Formula.FormulaHelper.CreateFO<QyUserRelationFO>();
            qyUserRelationFO.CancelFocusQy(this.CurrentUserInfo.UserID, QyID);
            return Json(JsonAjaxResult.Successful());
        }

        /// <summary>
        /// 设置默认企业号
        /// </summary>
        /// <param name="QyID"></param>
        /// <returns></returns>
        public JsonResult SetDefaultQy(string QyID)
        {
            var qyUserRelationFO = Formula.FormulaHelper.CreateFO<QyUserRelationFO>();
            qyUserRelationFO.SetDefaultQy(this.CurrentUserInfo.UserID, QyID);
            return Json(JsonAjaxResult.Successful());
        }
    }
}
