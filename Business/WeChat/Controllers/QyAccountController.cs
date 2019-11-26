using Formula;
using Formula.Exceptions;
using Formula.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using WeChat.Logic.Domain;
using WeChat.Logic;

namespace WeChat.Controllers
{
    public class QyAccountController : BaseController<QyAccount>
    {
        public override JsonResult GetList(MvcAdapter.QueryBuilder qb)
        {
            var result = entities.Set<QyAccount>().Where(c => c.IsDelete == 0).WhereToGridData(qb);
            return Json(result);
        }

        public override JsonResult Save()
        {

            #region 企业号唯一性校验
            QyAccount entity = UpdateEntity<QyAccount>();
            var exists = entities.Set<QyAccount>().Where(c => c.Name == entity.Name && c.IsDelete == 0);
            string id = GetQueryString("ID");
            if (!string.IsNullOrEmpty(id))
                exists = exists.Where(c => c.ID != id);
            if (exists.Count() > 0)
            {
                throw new BusinessException(string.Format("系统中已存在名称为“{0}”的企业号", exists.First().Name));
            }
            #endregion

            #region 保存数据
            if (entity.IsDelete == null)
                entity.IsDelete = 0;
            entities.SaveChanges();

            return Json(new { ID = entity.ID });
            #endregion
        }

        public override JsonResult Delete()
        {
            #region 假删除
            string ID = Request["ListIDs"];
            var entity = GetEntity<QyAccount>(ID);
            entity.IsDelete = 1;
            entities.SaveChanges();
            return Json("");
            #endregion
        }

        public JsonResult SetRoleUser(string qyid, string relationData)
        {
            string[] arrRelateID = GetValues(relationData, "ID").Distinct().Where(c=>!string.IsNullOrEmpty(c)).ToArray();
            var originalList = entities.Set<QyAccountUserRelation>().Where(c => c.QyID == qyid).ToArray();
            //新增的用户
            var addlist = arrRelateID.Where(c => !originalList.Select(d => d.UserID).Contains(c));
            //需要删除的用户
            var dellist = originalList.Where(c => !arrRelateID.Contains(c.UserID));
            foreach (var item in dellist)
                entities.Set<QyAccountUserRelation>().Remove(item);
            foreach (var id in addlist)
            {
                var model = new QyAccountUserRelation();
                model.ID = FormulaHelper.CreateGuid();
                model.QyID = qyid;
                model.UserID = id;
                model.IsUsed = SysBool.F.ToString();
                model.IsDefault = SysBool.F.ToString();
                entities.Set<QyAccountUserRelation>().Add(model);
            }
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult GetRoleUser(string qyid)
        {
            var result = entities.Set<QyAccountUserRelation>().Where(c => c.QyID == qyid);
            return Json(result);
        }
    }
}
