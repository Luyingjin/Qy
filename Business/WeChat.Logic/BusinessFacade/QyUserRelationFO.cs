using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formula;
using WeChat.Logic.Domain;

namespace WeChat.Logic.BusinessFacade
{
    public class QyUserRelationFO
    {
        WeChatEntities entities = FormulaHelper.GetEntities<WeChatEntities>();

        /// <summary>
        /// 获取人员登录的默认公众号，默认为上次进入的空间，如果用户首次登录，则默认选择第一个用户参与的公众号
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>返回默认的公众号ID</returns>
        public string GetDefaultQyID(string userID)
        {
            string result = string.Empty;
            var t = SysBool.T.ToString();
            var defaultMp = entities.QyAccountUserRelation.Where(d => d.UserID == userID && d.IsDefault == t).OrderByDescending(d => d.ID).FirstOrDefault();
            if (defaultMp != null)
                result = defaultMp.QyID;
            else
            {
                var re = entities.QyAccountUserRelation.FirstOrDefault(d => d.UserID == userID);
                if (re != null)
                {
                    result = re.QyID;
                    this.SetDefaultQy(userID, result);
                }
            }
            return result;
        }

        /// <summary>
        /// 设置用户默认公众号
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="qyID">企业号ID</param>
        public void SetDefaultQy(string userID, string qyID)
        {
            var t = SysBool.T.ToString();
            var f = SysBool.F.ToString();
            var defaultMp = entities.QyAccountUserRelation.Where(d => d.UserID == userID && d.QyID == qyID).OrderByDescending(d => d.ID).FirstOrDefault();
            if (defaultMp != null)
            {
                defaultMp.IsDefault = t;
                defaultMp.IsUsed = t;
            }
            else
            {
                var dm = new QyAccountUserRelation();
                dm.ID = FormulaHelper.CreateGuid();
                dm.UserID = userID;
                dm.QyID = qyID;
                dm.IsDefault = t;
                dm.IsUsed = t;
                entities.QyAccountUserRelation.Add(dm);
            }
            var notdefault = entities.QyAccountUserRelation.Where(d => d.UserID == userID && d.QyID != qyID).ToList();
            for (int i = 0; i < notdefault.Count(); i++)
                notdefault[i].IsDefault = f;

            entities.SaveChanges();
        }

        /// <summary>
        /// 设置常用公众号
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="qyID">企业号ID</param>
        public void SetFocusQy(string userID, string qyID)
        {
            var mprelation = entities.Set<QyAccountUserRelation>().Where(c => c.QyID == qyID && c.UserID == userID).FirstOrDefault();
            if (mprelation == null)
            {
                mprelation = new QyAccountUserRelation();
                mprelation.ID = FormulaHelper.CreateGuid();
                mprelation.QyID = qyID;
                mprelation.UserID = userID;
                mprelation.IsUsed = SysBool.T.ToString();
                entities.Set<QyAccountUserRelation>().Add(mprelation);

            }
            else
            {
                mprelation.IsUsed = SysBool.T.ToString();
            }
            entities.SaveChanges();
        }

        /// <summary>
        /// 取消常用公众号
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="qyID">企业号ID</param>
        public void CancelFocusQy(string userID, string qyID)
        {
            var mprelation = entities.Set<QyAccountUserRelation>().Where(c => c.QyID == qyID && c.UserID == userID).FirstOrDefault();
            if (mprelation != null)
            {
                mprelation.IsUsed = SysBool.F.ToString();
            }
            entities.SaveChanges();
        }
    }
}
