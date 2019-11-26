using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeChat.Logic.Domain;
using Formula;
using Formula.Helper;
using Senparc.Weixin.QY.Containers;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Entities;
using Senparc.Weixin.QY.AdvancedAPIs.Mass;
using Senparc.Weixin.QY.Entities;
using Senparc.Weixin.QY.AdvancedAPIs.App;
using Senparc.Weixin.QY.AdvancedAPIs.Asynchronous;

namespace WeChat.Logic.BusinessFacade
{
    public class WxFO
    {
        WeChatEntities entities = FormulaHelper.GetEntities<WeChatEntities>();

        #region 令牌
        /// <summary>
        /// 获取公众号认证令牌
        /// </summary>
        /// <param name="QyID"></param>
        /// <returns></returns>
        public string GetAccessToken(string QyID, bool GetNewToken = false)
        {
            var token = "";
            //LogWriter.Info(string.Format("qyid为{0}的静默授权在通过code获取token时异常，原因：{1}", corpid, result.errmsg));
            LogWriter.Info("GetAccessToken:1");
            var account = CacheHelper.Get(string.Format("WxAccount{0}", QyID)) as QyAccount;
            if (account == null)
            {
                LogWriter.Info("GetAccessToken:2");
                account = entities.QyAccount.Where(c => c.ID == QyID && c.IsDelete == 0).FirstOrDefault();
                CacheHelper.Set(string.Format("WxAccount{0}", QyID), account);
            }
            if (account != null)
            {
                LogWriter.Info(GetNewToken + "-GetAccessToken:3-" + account.CorpID + "@" + account.CorpSecret);
                if (!AccessTokenContainer.CheckRegistered(account.CorpID + "@" + account.CorpSecret) || GetNewToken)
                {
                    LogWriter.Info("GetAccessToken:4");
                    AccessTokenContainer.Register(account.CorpID, account.CorpSecret);
                }
                var result = AccessTokenContainer.GetTokenResult(account.CorpID + "@" + account.CorpSecret, GetNewToken);
                LogWriter.Info("key=" + account.CorpID + "@" + account.CorpSecret + GetNewToken + "GetAccessToken:5-" + JsonHelper.ToJson(result));
                if (result.access_token != account.AccessToken)
                {
                    LogWriter.Info("GetAccessToken:6");
                    account = entities.QyAccount.Where(c => c.ID == QyID && c.IsDelete == 0).FirstOrDefault();
                    account.AccessToken = result.access_token;
                    account.AccessTokenExpireTime = DateTime.Now.AddSeconds(result.expires_in);
                    account.ModifyDate = DateTime.Now;
                    entities.SaveChanges();
                    CacheHelper.Set(string.Format("WxAccount{0}", QyID), account);
                }
                token = account.AccessToken;
            }
            return token;
        }

        /// <summary>
        /// 获取jssdk认证令牌
        /// </summary>
        /// <param name="QyID"></param>
        /// <returns></returns>
        public string GetJsApiTicket(string QyID, bool GetNewToken = false)
        {
            //var sss = GetAccessToken(QyID);
            LogWriter.Info("GetJsApiTicket:1");
            var token = "";
            var account = CacheHelper.Get(string.Format("WxAccount{0}", QyID)) as QyAccount;
            if (account == null)
            {
                LogWriter.Info("GetJsApiTicket:2");
                account = entities.QyAccount.Where(c => c.ID == QyID && c.IsDelete == 0).FirstOrDefault();
                CacheHelper.Set(string.Format("WxAccount{0}", QyID), account);
            }
            if (account != null)
            {
                LogWriter.Info("GetJsApiTicket:3");
                if (!JsApiTicketContainer.CheckRegistered(account.CorpID + "@" + account.CorpSecret) || GetNewToken)
                {
                    LogWriter.Info("GetJsApiTicket:4");
                    JsApiTicketContainer.Register(account.CorpID, account.CorpSecret);
                }
                var result = JsApiTicketContainer.GetTicketResult(account.CorpID + "@" + account.CorpSecret, GetNewToken);
                LogWriter.Info("GetJsApiTicket:5");
                if (result.ticket != account.JsApiTicket)
                {
                    LogWriter.Info("GetJsApiTicket:6");
                    account = entities.QyAccount.Where(c => c.ID == QyID && c.IsDelete == 0).FirstOrDefault();
                    account.JsApiTicket = result.ticket;
                    account.JsApiTicketExpireTime = DateTime.Now.AddSeconds(result.expires_in);
                    entities.SaveChanges();
                    CacheHelper.Set(string.Format("WxAccount{0}", QyID), account);
                }
                token = account.JsApiTicket;
            }
            return token;
        }
        #endregion

        #region 组织
        #region 创建组织
        /// <summary>
        /// 创建组织
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="name">部门名称。长度限制为1~64个字符</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="id">部门ID。用指定部门ID新建部门，不指定此参数时，则自动生成</param>
        /// <returns></returns>
        public CreateDepartmentResult CreateDepartment(string QyID, string name, int parentId, int order = 1, int? id = null)
        {
            CreateDepartmentResult result = null;
            try
            {
                result = MailListApi.CreateDepartment(GetAccessToken(QyID), name, parentId, order, id);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("创建QyID为{0}的部门失败", QyID));
                result = MailListApi.CreateDepartment(GetAccessToken(QyID, true), name, parentId, order, id);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("创建QyID为{0}的部门失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion

        #region 更新组织
        /// <summary>
        /// 更新组织
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="id">部门id</param>
        /// <param name="name">更新的部门名称。长度限制为0~64个字符。修改部门名称时指定该参数</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <returns></returns>
        public QyJsonResult UpdateDepartment(string QyID, string id, string name, int parentId, int order = 1)
        {
            QyJsonResult result = null;
            try
            {
                result = MailListApi.UpdateDepartment(GetAccessToken(QyID), id, name, parentId, order);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("更新QyID为{0}的部门失败", QyID));
                result = MailListApi.UpdateDepartment(GetAccessToken(QyID, true), id, name, parentId, order);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("更新QyID为{0}的部门失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion

        #region 删除组织
        /// <summary>
        /// 删除组织
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="id">部门id</param>
        /// <returns></returns>
        public QyJsonResult DeleteDepartment(string QyID, string id)
        {
            QyJsonResult result = null;
            try
            {
                result = MailListApi.DeleteDepartment(GetAccessToken(QyID), id);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("删除QyID为{0}的部门失败", QyID));
                result = MailListApi.DeleteDepartment(GetAccessToken(QyID, true), id);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("删除QyID为{0}的部门失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion

        #region 获取组织
        /// <summary>
        /// 获取组织
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="id">部门ID。获取指定部门ID下的子部门</param>
        /// <returns>指定部门及其下的子部门</returns>
        public GetDepartmentListResult GetDepartmentList(string QyID, int? id = null)
        {
            GetDepartmentListResult result = null;
            try
            {
                result = MailListApi.GetDepartmentList(GetAccessToken(QyID), id);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("获取QyID为{0}的部门失败", QyID));
                result = MailListApi.GetDepartmentList(GetAccessToken(QyID, true), id);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("获取QyID为{0}的部门失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion
        #endregion

        #region 成员
        #region 创建成员
        /// <summary>
        /// 创建成员
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="userId">员工UserID。必须企业内唯一</param>
        /// <param name="name">成员名称。长度为1~64个字符</param>
        /// <param name="department">成员所属部门id列表。注意，每个部门的直属员工上限为1000个</param>
        /// <param name="position">职位信息。长度为0~64个字符</param>
        /// <param name="mobile">手机号码。必须企业内唯一</param>
        /// <param name="tel">办公电话。长度为0~64个字符</param>
        /// <param name="email">邮箱。长度为0~64个字符。必须企业内唯一</param>
        /// <param name="weixinId">微信号。必须企业内唯一</param>
        /// <returns></returns>
        public QyJsonResult CreateUser(string QyID, string userId, string name, int[] department = null,
            string position = null, string mobile = null, string email = null, string weixinId = null, string tel = null,
            int gender = 0, string avatarMediaid = null, Extattr extattr = null)
        {
            QyJsonResult result = null;
            try
            {
                result = MailListApi.CreateMember(GetAccessToken(QyID), userId, name, department, position, mobile, email, weixinId, tel, gender, avatarMediaid, extattr);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("创建QyID为{0}的成员失败", QyID));
                result = MailListApi.CreateMember(GetAccessToken(QyID, true), userId, name, department, position, mobile, email, weixinId, tel, gender, avatarMediaid, extattr);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("创建QyID为{0}的成员失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion

        #region 更新成员
        /// <summary>
        /// 更新成员
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="userId">员工UserID。必须企业内唯一</param>
        /// <param name="name">成员名称。长度为1~64个字符</param>
        /// <param name="department">成员所属部门id列表。注意，每个部门的直属员工上限为1000个</param>
        /// <param name="position">职位信息。长度为0~64个字符</param>
        /// <param name="mobile">手机号码。必须企业内唯一</param>
        /// <param name="tel">办公电话。长度为0~64个字符</param>
        /// <param name="email">邮箱。长度为0~64个字符。必须企业内唯一</param>
        /// <param name="weixinId">微信号。必须企业内唯一</param>
        /// <returns></returns>
        public QyJsonResult UpdateUser(string QyID, string userId, string name, int[] department = null,
            string position = null, string mobile = null, string email = null, string weixinId = null, int enable = 1, string tel = null,
            int gender = 0, string avatarMediaid = null, Extattr extattr = null)
        {
            QyJsonResult result = null;
            try
            {
                result = MailListApi.UpdateMember(GetAccessToken(QyID), userId, name, department, position, mobile, email, weixinId, enable, tel, gender, avatarMediaid, extattr);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("更新QyID为{0}的成员失败", QyID));
                result = MailListApi.UpdateMember(GetAccessToken(QyID, true), userId, name, department, position, mobile, email, weixinId, enable, tel, gender, avatarMediaid, extattr);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("更新QyID为{0}的成员失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion

        #region 删除成员
        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="userId">员工UserID。必须企业内唯一</param>
        /// <returns></returns>
        public QyJsonResult DeleteUser(string QyID, string userId)
        {
            QyJsonResult result = null;
            try
            {
                result = MailListApi.DeleteMember(GetAccessToken(QyID), userId);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("删除QyID为{0}的成员失败", QyID));
                result = MailListApi.DeleteMember(GetAccessToken(QyID, true), userId);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("删除QyID为{0}的成员失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion

        #region 获取成员
        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="userId">员工UserID。必须企业内唯一</param>
        /// <returns></returns>
        public GetMemberResult GetUser(string QyID, string userId)
        {
            GetMemberResult result = null;
            try
            {
                result = MailListApi.GetMember(GetAccessToken(QyID), userId);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("获取QyID为{0}的成员失败", QyID));
                result = MailListApi.GetMember(GetAccessToken(QyID, true), userId);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("获取QyID为{0}的成员失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion

        #region 获取部门成员
        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>
        /// <returns></returns>
        public GetDepartmentMemberInfoResult GetUserList(string QyID, int departmentId, int fetchChild, int status)
        {
            GetDepartmentMemberInfoResult result = null;
            try
            {
                result = MailListApi.GetDepartmentMemberInfo(GetAccessToken(QyID), departmentId, fetchChild, status);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("获取QyID为{0}的部门成员失败", QyID));
                result = MailListApi.GetDepartmentMemberInfo(GetAccessToken(QyID, true), departmentId, fetchChild, status);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("获取QyID为{0}的部门成员失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion

        #region 邀请成员
        /// <summary>
        /// 邀请成员
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="userId">员工UserID。必须企业内唯一</param>
        /// <returns></returns>
        public AsynchronousJobId BatchInviteUser(string QyID, string toUser, string toParty, string toTag, string inviteTips, Asynchronous_CallBack callBack)
        {
            AsynchronousJobId result = null;
            try
            {
                result = AsynchronousApi.BatchInviteUser(GetAccessToken(QyID), toUser, toParty, toTag, inviteTips, callBack);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("QyID为{0}的邀请成员失败", QyID));
                result = AsynchronousApi.BatchInviteUser(GetAccessToken(QyID, true), toUser, toParty, toTag, inviteTips, callBack);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("QyID为{0}的邀请成员失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion
        #endregion

        #region 消息
        #region 发送文本消息
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="content">消息内容</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <returns></returns>
        public MassResult SendTextMsg(string QyID, string toUser, string toParty, string toTag, string agentId, string content, int safe = 0)
        {
            MassResult result = null;
            try
            {
                result = MassApi.SendText(GetAccessToken(QyID), toUser, toParty, toTag, agentId, content, safe);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("发送QyID为{0}的文本消息失败，接收人：{1}，PartID：{2}，totag：{3}", toUser, QyID,toParty,toTag));
                result = MassApi.SendText(GetAccessToken(QyID, true), toUser, toParty, toTag, agentId, content, safe);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("发送QyID为{0}的文本消息失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion

        #region 发送链接消息
        /// <summary>
        /// 发送链接消息
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="articles">图文信息内容，包括title（标题）、description（描述）、url（点击后跳转的链接。企业可根据url里面带的code参数校验员工的真实身份）和picurl（图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片）</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <returns></returns>
        public MassResult SendNewsMsg(string QyID, string toUser, string toParty, string toTag, string agentId, List<Article> articles, int safe = 0)
        {
            MassResult result = null;
            try
            {
                result = MassApi.SendNews(GetAccessToken(QyID), toUser, toParty, toTag, agentId, articles, safe);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("发送QyID为{0}的链接消息失败", QyID));
                result = MassApi.SendNews(GetAccessToken(QyID, true), toUser, toParty, toTag, agentId, articles, safe);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("发送QyID为{0}的链接消息失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion
        #endregion

        #region 更新通讯录
        /// <summary>
        /// 更新通讯录
        /// </summary>
        /// <param name="QyID"></param>
        public void ReFreshUser(string QyID)
        {
            ReFreshDepart(QyID);
            ReFreshDepartUser(QyID);
            ReFreshTag(QyID);
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="QyID"></param>
        private void ReFreshDepart(string QyID)
        {
            GetDepartmentListResult result = null;
            try
            {
                result = MailListApi.GetDepartmentList(GetAccessToken(QyID));
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("获取QyID为{0}的部门失败", QyID));
                result = MailListApi.GetDepartmentList(GetAccessToken(QyID, true));
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("获取QyID为{0}的部门失败，原因：{1}", QyID, result.errmsg));
                throw new Exception(string.Format("获取QyID为{0}的部门失败，原因：{1}", QyID, result.errmsg));
            }
            var olddepts = entities.Set<QyDepart>().Where(c => c.QyID == QyID).ToList();
            List<QyDepart> addlist = new List<QyDepart>(), alllist = new List<QyDepart>();
            //新增
            foreach (var add in result.department.Where(c => !olddepts.Select(d => d.DepartID).Contains(c.id)))
            {
                QyDepart qd = new QyDepart();
                qd.ID = FormulaHelper.CreateGuid();
                qd.QyID = QyID;
                qd.DepartName = add.name;
                qd.ParentDepartID = add.parentid;
                qd.DepartID = add.id;
                qd.DepartOrder = add.order;
                addlist.Add(qd);
                alllist.Add(qd);
            }
            //修改
            foreach (var update in olddepts.Where(c => result.department.Select(d => d.id).Contains(c.DepartID.Value)))
            {
                var item = result.department.Where(c => c.id == update.DepartID).FirstOrDefault();
                update.DepartName = item.name;
                update.ParentDepartID = item.parentid;
                update.DepartOrder = item.order;
                alllist.Add(update);
            }
            //删除
            foreach (var delete in olddepts.Where(c => !result.department.Select(d => d.id).Contains(c.DepartID.Value)))
            {
                var relateusers = entities.Set<QyUser>().Where(c => c.DepartIDs.Contains(delete.ID)).ToList();
                for (int i = 0; i < relateusers.Count(); i++)
                {
                    var olddeparts = relateusers[i].DepartIDs.Split(',').ToList();
                    olddeparts.Remove(delete.ID);
                    relateusers[i].DepartIDs = string.Join(",", olddeparts);
                }
                entities.Set<QyDepart>().Remove(delete);
            }
            //更新树形字段
            var root = alllist.Where(c => !alllist.Select(d => d.DepartID).Contains(c.ParentDepartID)).FirstOrDefault();
            if (root != null)
            {
                root.ParentID = "";
                root.Length = 1;
                root.FullPath = root.ID;
                root.ChildCount = alllist.Where(c => c.ParentDepartID == root.DepartID).Count();

                var ids = new int?[] { root.DepartID };
                var parentlength = 1;
                while (ids.Count() > 0)
                {
                    var children = alllist.Where(c => ids.Contains(c.ParentDepartID)).ToList();
                    for (int i = 0; i < children.Count(); i++)
                    {
                        var child = children[i];
                        var parent = alllist.Where(c => c.DepartID == child.ParentDepartID).FirstOrDefault();
                        if (parent == null)
                            continue;
                        child.ParentID = parent.ID;
                        child.Length = parentlength + 1;
                        child.FullPath = parent.FullPath + "." + child.ID;
                        child.ChildCount = alllist.Where(c => c.ParentDepartID == child.DepartID).Count();
                    }
                    ids = children.Select(c => c.DepartID).ToArray();
                    parentlength++;
                }

            }
            foreach (var item in addlist)
            {
                entities.Set<QyDepart>().Add(item);
            }
            entities.SaveChanges();
        }

        /// <summary>
        /// 更新标签
        /// </summary>
        /// <param name="QyID"></param>
        private void ReFreshTag(string QyID)
        {
            GetTagListResult result = null;
            try
            {
                result = MailListApi.GetTagList(GetAccessToken(QyID));
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("获取QyID为{0}的标签失败", QyID));
                result = MailListApi.GetTagList(GetAccessToken(QyID, true));
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("获取QyID为{0}的标签失败，原因：{1}", QyID, result.errmsg));
                throw new Exception(string.Format("获取QyID为{0}的标签失败，原因：{1}", QyID, result.errmsg));
            }
            var oldtags = entities.Set<QyTags>().Where(c => c.QyID == QyID).ToList();
            var users = entities.Set<QyUser>().Where(c => c.QyID == QyID).ToList();
            var departs = entities.Set<QyDepart>().Where(c => c.QyID == QyID).ToList();
            for (int i = 0; i < users.Count(); i++)
                users[i].TagIDs = "";
            for (int i = 0; i < departs.Count(); i++)
                departs[i].TagIDs = "";
            //新增
            foreach (var add in result.taglist.Where(c => !oldtags.Select(d => d.TagID).Contains(c.tagid)))
            {
                QyTags qt = new QyTags();
                qt.ID = FormulaHelper.CreateGuid();
                qt.QyID = QyID;
                qt.TagID = add.tagid;
                qt.Name = add.tagname;
                entities.Set<QyTags>().Add(qt);

                GetTagMemberResult addresult = null;
                try
                {
                    addresult = MailListApi.GetTagMember(GetAccessToken(QyID), int.Parse(add.tagid));
                }
                catch (Exception ex)
                {
                    LogWriter.Error(ex, string.Format("获取QyID为{0}的标签成员失败", QyID));
                    addresult = MailListApi.GetTagMember(GetAccessToken(QyID, true), int.Parse(add.tagid));
                }
                if (addresult.errcode != ReturnCode_QY.请求成功)
                {
                    LogWriter.Info(string.Format("获取QyID为{0}的标签成员失败，原因：{1}", QyID, result.errmsg));
                    throw new Exception(string.Format("获取QyID为{0}的标签成员失败，原因：{1}", QyID, result.errmsg));
                }
                if (addresult.userlist != null)
                {
                    var tagusers = users.Where(c => addresult.userlist.Select(d => d.userid).Contains(c.UserID)).ToList();
                    for (int i = 0; i < tagusers.Count(); i++)
                        tagusers[i].TagIDs += tagusers[i].TagIDs == "" ? qt.ID : ("," + qt.ID);
                }
                if (addresult.partylist != null)
                {
                    var tagdeparts = departs.Where(c => addresult.partylist.Contains(c.DepartID.Value)).ToList();
                    for (int i = 0; i < tagdeparts.Count(); i++)
                        tagdeparts[i].TagIDs += tagdeparts[i].TagIDs == "" ? qt.ID : ("," + qt.ID);
                }
            }
            //修改
            foreach (var update in oldtags.Where(c => result.taglist.Select(d => d.tagid).Contains(c.TagID)))
            {
                var item = result.taglist.Where(c => c.tagid == update.TagID).FirstOrDefault();
                update.TagID = item.tagid;
                update.Name = item.tagname;


                GetTagMemberResult updateresult = null;
                try
                {
                    updateresult = MailListApi.GetTagMember(GetAccessToken(QyID), int.Parse(item.tagid));
                }
                catch (Exception ex)
                {
                    LogWriter.Error(ex, string.Format("获取QyID为{0}的标签成员失败", QyID));
                    updateresult = MailListApi.GetTagMember(GetAccessToken(QyID, true), int.Parse(item.tagid));
                }
                if (updateresult.errcode != ReturnCode_QY.请求成功)
                {
                    LogWriter.Info(string.Format("获取QyID为{0}的标签成员失败，原因：{1}", QyID, result.errmsg));
                    throw new Exception(string.Format("获取QyID为{0}的标签成员失败，原因：{1}", QyID, result.errmsg));
                }
                if (updateresult.userlist != null)
                {
                    var tagusers = users.Where(c => updateresult.userlist.Select(d => d.userid).Contains(c.UserID)).ToList();
                    for (int i = 0; i < tagusers.Count(); i++)
                        tagusers[i].TagIDs += tagusers[i].TagIDs == "" ? update.ID : ("," + update.ID);
                }
                if (updateresult.partylist != null)
                {
                    var tagdeparts = departs.Where(c => updateresult.partylist.Contains(c.DepartID.Value)).ToList();
                    for (int i = 0; i < tagdeparts.Count(); i++)
                        tagdeparts[i].TagIDs += tagdeparts[i].TagIDs == "" ? update.ID : ("," + update.ID);
                }
            }
            //删除
            foreach (var delete in oldtags.Where(c => !result.taglist.Select(d => d.tagid).Contains(c.TagID)))
            {
                var relateusers = entities.Set<QyUser>().Where(c => c.TagIDs.Contains(delete.ID)).ToList();
                for (int i = 0; i < relateusers.Count(); i++)
                {
                    var oldchildtags = relateusers[i].TagIDs.Split(',').ToList();
                    oldchildtags.Remove(delete.ID);
                    relateusers[i].TagIDs = string.Join(",", oldchildtags);
                }
                entities.Set<QyTags>().Remove(delete);
            }
            entities.SaveChanges();
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="QyID"></param>
        private void ReFreshDepartUser(string QyID)
        {
            GetDepartmentMemberInfoResult result = null;
            try
            {
                result = MailListApi.GetDepartmentMemberInfo(GetAccessToken(QyID), 1, 1, 0);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("获取QyID为{0}的部门成员失败", QyID));
                result = MailListApi.GetDepartmentMemberInfo(GetAccessToken(QyID, true), 1, 1, 0);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("获取QyID为{0}的部门成员失败，原因：{1}", QyID, result.errmsg));
                throw new Exception(string.Format("获取QyID为{0}的部门成员失败，原因：{1}", QyID, result.errmsg));
            }
            var oldusers = entities.Set<QyUser>().Where(c => c.QyID == QyID).ToList();
            var depts = entities.Set<QyDepart>().Where(c => c.QyID == QyID).ToList();
            //新增
            foreach (var add in result.userlist.Where(c => !oldusers.Select(d => d.UserID).Contains(c.userid)))
            {
                QyUser qu = new QyUser();
                qu.ID = FormulaHelper.CreateGuid();
                qu.QyID = QyID;
                qu.UserID = add.userid;
                qu.UserName = add.name;
                qu.Position = add.position;
                qu.Mobile = add.mobile;
                qu.Gender = add.gender;
                qu.Email = add.email;
                qu.WeixinID = add.weixinid;
                qu.AvatarMediaid = add.avatar;
                qu.Status = add.status;
                qu.DepartIDs = add.department == null ? "" : string.Join(",", depts.Where(c => add.department.Contains(c.DepartID.Value)).Select(c => c.ID));
                entities.Set<QyUser>().Add(qu);
            }
            //修改
            foreach (var update in oldusers.Where(c => result.userlist.Select(d => d.userid).Contains(c.UserID)))
            {
                var item = result.userlist.Where(c => c.userid == update.UserID).FirstOrDefault();
                update.UserName = item.name;
                update.Position = item.position;
                update.Mobile = item.mobile;
                update.Gender = item.gender;
                update.Email = item.email;
                update.WeixinID = item.weixinid;
                update.AvatarMediaid = item.avatar;
                update.Status = item.status;
                update.DepartIDs = item.department == null ? "" : string.Join(",", depts.Where(c => item.department.Contains(c.DepartID.Value)).Select(c => c.ID));
            }
            //删除
            foreach (var delete in oldusers.Where(c => !result.userlist.Select(d => d.userid).Contains(c.UserID)))
            {
                var relateexts = entities.Set<QyUserExt>().Where(c => c.UserID == delete.ID).ToList();
                for (int i = 0; i < relateexts.Count(); i++)
                {
                    entities.Set<QyUserExt>().Remove(relateexts[i]);
                }
                entities.Set<QyUser>().Remove(delete);
            }
            entities.SaveChanges();
        }
        #endregion

        #region 应用
        #region 获取应用详情
        /// <summary>
        /// 获取应用详情
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <returns></returns>
        public GetAppInfoResult GetAppInfo(string QyID, int agentId)
        {
            GetAppInfoResult result = null;
            try
            {
                result = AppApi.GetAppInfo(GetAccessToken(QyID), agentId);
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("获取QyID为{0}的应用详情失败", QyID));
                result = AppApi.GetAppInfo(GetAccessToken(QyID, true), agentId);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("获取QyID为{0}的应用详情失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion

        #region 获取应用详情
        /// <summary>
        /// 获取应用详情
        /// </summary>
        /// <param name="QyID">企业ID</param>
        /// <returns></returns>
        public GetAppListResult GetAppList(string QyID)
        {
            GetAppListResult result = null;
            try
            {
                result = AppApi.GetAppList(GetAccessToken(QyID));
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("获取QyID为{0}的应用列表失败", QyID));
                result = AppApi.GetAppList(GetAccessToken(QyID, true));
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("获取QyID为{0}的应用列表失败，原因：{1}", QyID, result.errmsg));
            }
            return result;
        }
        #endregion
        #endregion
    }
}
