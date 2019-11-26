using Formula.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChat.Logic.Domain;

namespace WeChat.Controllers
{
    public class QyAccountPassTokenController : BaseController<QyAccountPassToken>
    {
        public override JsonResult GetList(MvcAdapter.QueryBuilder qb)
        {
            var qyid = GetQueryString("QyID");
            var result = entities.Set<QyAccountPassToken>().Where(c => c.QyID == qyid).WhereToGridData(qb);
            return Json(result);
        }

        [ValidateInput(false)]
        public override JsonResult Save()
        {
            #region 数据校验
            QyAccountPassToken entity = UpdateEntity<QyAccountPassToken>();
            if (string.IsNullOrEmpty(entity.PassToken))
                throw new BusinessException("PassToken不能为空");
            #endregion

            #region 关键字校验唯一性
            var passtoken = entity.PassToken;
            var QyID = GetQueryString("QyID");
            var exist = entities.Set<QyAccountPassToken>().Any(i => i.ID != entity.ID && i.QyID == QyID && i.PassToken == entity.PassToken.Trim());
            if (exist)
            {
                throw new BusinessException(string.Format("PassToken[{0}]已存在，请重新输入！", entity.PassToken.Trim()));
            }
            #endregion
            #region 保存数据
            if (string.IsNullOrEmpty(entity.QyID))
                entity.QyID = GetQueryString("QyID");
            if (string.IsNullOrEmpty(entity.AllowIP))
                entity.AllowIP = "*";
            if (string.IsNullOrEmpty(entity.OAuth2AllowDomain))
                entity.OAuth2AllowDomain = "*";
            entities.SaveChanges();

            return Json(new { ID = entity.ID });
            #endregion
        }

        public override JsonResult Delete()
        {
            string ID = Request["ListIDs"];
            var entity = GetEntity<QyAccountPassToken>(ID);
            entities.Set<QyAccountPassToken>().Remove(entity);
            entities.SaveChanges();
            return Json("");
        }
    }
}
