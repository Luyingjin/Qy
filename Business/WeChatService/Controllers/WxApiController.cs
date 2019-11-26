using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin;
using WeChat.Logic.Domain;
using WeChat.Logic.BusinessFacade;
using Formula;
using Formula.Helper;
using System.Collections.Specialized;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
using System.Xml.Linq;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using Senparc.Weixin.Entities;
using WeChatService.Models;
using Senparc.Weixin.QY.AdvancedAPIs.Mass;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.OAuth2;
using Senparc.Weixin.QY.AdvancedAPIs.Asynchronous;
using Senparc.Weixin.CommonAPIs;

namespace WeChat.Controllers
{
    public class WxApiController : BaseController
    {
        #region 私有静态变量
        static int _cachesecond = -1;
        /// <summary>
        /// 缓存时间
        /// </summary>
        static int cachesecond
        {
            get
            {
                if (_cachesecond > 0)
                    return _cachesecond;
                else
                {
                    _cachesecond = 300;
                    int.TryParse(System.Configuration.ConfigurationManager.AppSettings["CacheTime"], out _cachesecond);
                    return _cachesecond;
                }
            }
        }
        #endregion

        #region 组织信息接口
        #region 创建组织信息
        /// <summary>
        /// 创建组织信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateDepartment(QyOrgModel model)
        {
            #region 校验
            var account = GetAccount(model.corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的CreateDepartment获取失败，原因：企业号不存在", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }

            if (!ValidateAccessToken(account, model.accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的CreateDepartment获取失败，原因：accesstoken错误", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            #region 获取用户信息
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            CreateDepartmentResult userinfo = null;

            try
            {
                userinfo = wxFO.CreateDepartment(model.corpid, model.name, model.parentid, model.order, model.id);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    errorcode = "500",
                    errormsg = ex.Message,
                });
            }
            #endregion

            return Json(userinfo);
        }
        #endregion

        #region 更新组织信息
        /// <summary>
        /// 更新组织信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateDepartment(QyOrgModel model)
        {
            #region 校验
            var account = GetAccount(model.corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的UpdateDepartment获取失败，原因：企业号不存在", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }

            if (!ValidateAccessToken(account, model.accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的UpdateDepartment获取失败，原因：accesstoken错误", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            #region 获取用户信息
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            QyJsonResult userinfo = null;

            try
            {
                userinfo = wxFO.UpdateDepartment(model.corpid, (model.id ?? -1).ToString(), model.name, model.parentid, model.order);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    errorcode = "500",
                    errormsg = ex.Message,
                });
            }
            #endregion

            return Json(userinfo);
        }
        #endregion

        #region 删除组织信息
        /// <summary>
        /// 删除组织信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteDepartment(QyOrgModel model)
        {
            #region 校验
            var account = GetAccount(model.corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的DeleteDepartment获取失败，原因：企业号不存在", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }

            if (!ValidateAccessToken(account, model.accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的DeleteDepartment获取失败，原因：accesstoken错误", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            #region 获取用户信息
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            QyJsonResult userinfo = null;

            try
            {
                userinfo = wxFO.DeleteDepartment(model.corpid, (model.id ?? -1).ToString());
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    errorcode = "500",
                    errormsg = ex.Message,
                });
            }
            #endregion

            return Json(userinfo);
        }
        #endregion

        #region 获取组织信息
        /// <summary>
        /// 获取组织信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDepartmentList(QyOrgModel model)
        {
            #region 校验
            var account = GetAccount(model.corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetDepartmentList获取失败，原因：企业号不存在", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }

            if (!ValidateAccessToken(account, model.accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetDepartmentList获取失败，原因：accesstoken错误", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            #region 获取用户信息
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            GetDepartmentListResult userinfo = null;

            try
            {
                userinfo = wxFO.GetDepartmentList(model.corpid, model.id);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    errorcode = "500",
                    errormsg = ex.Message,
                });
            }
            #endregion

            return Json(userinfo);
        }
        #endregion
        #endregion

        #region 用户信息接口
        #region 创建用户信息
        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser(QyUserModel model)
        {
            #region 校验
            var account = GetAccount(model.corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetUser获取失败，原因：企业号不存在", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }

            if (!ValidateAccessToken(account, model.accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetUser获取失败，原因：accesstoken错误", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            #region 获取用户信息
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            QyJsonResult userinfo = null;

            try
            {
                userinfo = wxFO.CreateUser(model.corpid, model.userid, model.name, model.department, model.position, model.mobile, model.email, model.weixinid, null, model.gender, model.avatar_mediaid, model.extattr);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    errorcode = "500",
                    errormsg = ex.Message,
                });
            }
            #endregion

            return Json(userinfo);
        }
        #endregion

        #region 更新用户信息
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUser(QyUserModel model)
        {
            #region 校验
            var account = GetAccount(model.corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetUser获取失败，原因：企业号不存在", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }

            if (!ValidateAccessToken(account, model.accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetUser获取失败，原因：accesstoken错误", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            #region 获取用户信息
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            QyJsonResult userinfo = null;

            try
            {
                userinfo = wxFO.UpdateUser(model.corpid, model.userid, model.name, model.department, model.position, model.mobile, model.email, model.weixinid, model.enable, null, model.gender, model.avatar_mediaid, model.extattr);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    errorcode = "500",
                    errormsg = ex.Message,
                });
            }
            #endregion

            return Json(userinfo);
        }
        #endregion

        #region 删除用户信息
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteUser(QyUserModel model)
        {
            #region 校验
            var account = GetAccount(model.corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetUser获取失败，原因：企业号不存在", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }

            if (!ValidateAccessToken(account, model.accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetUser获取失败，原因：accesstoken错误", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            #region 获取用户信息
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            QyJsonResult userinfo = null;

            try
            {
                userinfo = wxFO.DeleteUser(model.corpid, model.userid);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    errorcode = "500",
                    errormsg = ex.Message,
                });
            }
            #endregion

            return Json(userinfo);
        }
        #endregion

        #region 获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUser(QyUserModel model)
        {
            #region 校验
            var account = GetAccount(model.corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetUser获取失败，原因：企业号不存在", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }

            if (!ValidateAccessToken(account, model.accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetUser获取失败，原因：accesstoken错误", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            #region 获取用户信息
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            GetMemberResult userinfo = null;

            try
            {
                userinfo = wxFO.GetUser(model.corpid, model.userid);
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    errorcode = "500",
                    errormsg = ex.Message,
                });
            }
            #endregion

            return Json(userinfo);
        }
        #endregion

        #region 获取部门用户信息
        /// <summary>
        /// 获取部门用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserList(QyDeptUserModel model)
        {
            #region 校验
            var account = GetAccount(model.corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetUserList获取失败，原因：企业号不存在", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }

            if (!ValidateAccessToken(account, model.accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的GetUserList获取失败，原因：accesstoken错误", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            #region 获取用户信息
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            GetDepartmentMemberInfoResult userinfo = null;

            try
            {
                userinfo = wxFO.GetUserList(model.corpid, model.department_id, model.fetch_child, model.status);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    errorcode = "500",
                    errormsg = ex.Message,
                });
            }
            #endregion

            return Json(userinfo);
        }
        #endregion

        #region 邀请用户(已被微信废弃)
        /// <summary>
        /// 邀请用户
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult BatchInviteUser(QyBatchInviteUserModel model)
        //{
        //    #region 校验
        //    var account = GetAccount(model.corpid);
        //    if (account == null)
        //    {
        //        LogWriter.Info(string.Format("corpid为“{0}”的BatchInviteUser获取失败，原因：企业号不存在", model.corpid));
        //        return Json(new
        //        {
        //            errorcode = "500",
        //            errormsg = "企业号不存在",
        //        });
        //    }

        //    if (!ValidateAccessToken(account, model.accesstoken))
        //    {
        //        LogWriter.Info(string.Format("corpid为“{0}”的BatchInviteUser获取失败，原因：accesstoken错误", model.corpid));
        //        return Json(new
        //        {
        //            errorcode = "500",
        //            errormsg = "非法访问",
        //        });
        //    }
        //    #endregion

        //    #region 获取用户信息
        //    var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

        //    AsynchronousJobId userinfo = null;

        //    try
        //    {
        //        userinfo = wxFO.BatchInviteUser(model.corpid, model.toUser, model.toParty, model.toTag, model.inviteTips
        //            , new Senparc.Weixin.QY.AdvancedAPIs.Asynchronous.Asynchronous_CallBack() 
        //            { 
        //                url = model.url, 
        //                token = model.token, 
        //                encodingaeskey = model.encodingaeskey 
        //            });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new
        //        {
        //            errorcode = "500",
        //            errormsg = ex.Message,
        //        });
        //    }
        //    #endregion

        //    return Json(userinfo);
        //}
        #endregion
        #endregion

        #region 消息信息
        #region 发送文本消息
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendTextMsg(QyTextMsgModel model)
        {
            LogWriter.Info("SendTextMsg1111111111");
            LogWriter.Info(string.Format("SendTextMsg-QyTextMsgModel={0}", JsonHelper.ToJson(model)));
            #region 校验
            var account = GetAccount(model.corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的SendTextMsg获取失败，原因：企业号不存在", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }

            if (!ValidateAccessToken(account, model.accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的SendTextMsg获取失败，原因：accesstoken错误", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            #region 发送文本消息
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            MassResult userinfo = null;

            try
            {
                userinfo = wxFO.SendTextMsg(model.corpid, model.toUser, model.toParty, model.toTag, model.agentId, model.content, model.safe);
                LogWriter.Info(string.Format("SendTextMsg-userinfo={0}", JsonHelper.ToJson(userinfo)));
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    errorcode = "500",
                    errormsg = ex.Message,
                });
            }
            #endregion

            return Json(userinfo);
        }
        #endregion

        #region 发送链接消息
        /// <summary>
        /// 发送链接消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendNewsMsg(QyNewsMsgModel model)
        {
            #region 校验
            var account = GetAccount(model.corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的SendNewsMsg获取失败，原因：企业号不存在", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }

            if (!ValidateAccessToken(account, model.accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的SendNewsMsg获取失败，原因：accesstoken错误", model.corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            #region 发送链接消息
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            MassResult userinfo = null;

            try
            {
                //LogWriter.Info(string.Format("QyNewsMsgModel={0}", JsonHelper.ToJson(model)));
                userinfo = wxFO.SendNewsMsg(model.corpid, model.toUser, model.toParty, model.toTag, model.agentId, model.news.articles, model.safe);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    errorcode = "500",
                    errormsg = ex.Message,
                });
            }
            #endregion

            return Json(userinfo);
        }
        #endregion
        #endregion

        #region OAuth2
        public ActionResult OAuth2(string corpid, string accesstoken, string reurl, string scope = "snsapi_base")
        {
            #region 校验
            if (string.IsNullOrEmpty(reurl))
            {
                LogWriter.Info(string.Format("qyid为“{0}”的OAuth2授权失败，原因：reurl为空", corpid));
                return Content("非法访问");
            }
            var account = GetAccount(corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("qyid为“{0}”的OAuth2授权失败，原因：企业号不存在", corpid));
                return Content("非法访问");
            }
            var passtoken = GetPassToken(account, accesstoken);
            if (passtoken == null)
            {
                LogWriter.Info(string.Format("qyid为“{0}”的OAuth2授权失败，原因：accesstoken错误", corpid));
                return Content("非法访问");
            }
            if (!ValidateOauth2Domain(passtoken, Base64Helper.DecodeBase64(reurl.Replace(" ", "+"))))
            {
                LogWriter.Info(string.Format("qyid为“{0}”的OAuth2授权失败，原因：reurl{1}错误", corpid, reurl));
                return Content("非法访问");
            }
            #endregion
            string CorpID = account.CorpID;
            var domain = Request.Url.Authority;
            var url = OAuth2Api.GetCode(CorpID, "http://" + domain + "/wechatservice/wxapi/OAuth2Callback?corpid=" + corpid + "&accesstoken=" + accesstoken + "&scope=" + scope + "&reurl=" + reurl, "JeffreySu", account.AgentId == null ? "" : account.AgentId.ToString(), scope: scope);
            return Redirect(url);
        }

        public ActionResult OAuth2Callback(string corpid, string accesstoken, string scope, string reurl, string code, string state)
        {
            //LogWriter.Info("OAuth2Callback:");
            string url = reurl ?? "";
            url = Base64Helper.DecodeBase64(url.Replace(" ", "+"));

            if (string.IsNullOrEmpty(code))
            {
                LogWriter.Info(string.Format("qyid为“{0}”的OAuth2授权失败，原因：拒绝了授权", corpid));
                return Content("您拒绝了授权！");
            }

            if (state != "JeffreySu" && state != "JeffreSu?10000skip=true")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                LogWriter.Info(string.Format("qyid为“{0}”的OAuth2授权失败，原因：验证失败", corpid));
                return Content("验证失败！请从正规途径进入！");
            }


            #region 校验
            if (string.IsNullOrEmpty(reurl))
            {
                LogWriter.Info(string.Format("qyid为“{0}”的OAuth2Base获取失败，原因：reurl为空", corpid));
                return Content("非法访问");
            }
            var account = GetAccount(corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("qyid为“{0}”的OAuth2Base获取失败，原因：企业号不存在", corpid));
                return Content("非法访问");
            }
            var passtoken = GetPassToken(account, accesstoken);
            if (passtoken == null)
            {
                LogWriter.Info(string.Format("qyid为“{0}”的OAuth2Base获取失败，原因：accesstoken错误", corpid));
                return Content("非法访问");
            }
            if (!ValidateOauth2Domain(passtoken, reurl))
            {
                LogWriter.Info(string.Format("qyid为“{0}”的OAuth2Base获取失败，原因：reurl{1}错误", corpid, reurl));
                return Content("非法访问");
            }
            #endregion

            //通过，用code换取access_token
            GetUserInfoResult result = null;
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();
            try
            {
                result = OAuth2Api.GetUserId(wxFO.GetAccessToken(corpid), code);
                //LogWriter.Info("OAuth2Callback:GetUserId" + JsonHelper.ToJson(result));
            }
            catch (Exception ex)
            {
                LogWriter.Error(ex, string.Format("qyid为{0}的静默授权在通过code获取token时异常", corpid));
                result = OAuth2Api.GetUserId(wxFO.GetAccessToken(corpid, true), code);
            }
            if (result.errcode != ReturnCode_QY.请求成功)
            {
                LogWriter.Info(string.Format("qyid为{0}的静默授权在通过code获取token时异常，原因：{1}", corpid, result.errmsg));
                return Content("错误：" + result.errmsg);
            }
            if (!string.IsNullOrEmpty(result.user_ticket) && !string.IsNullOrEmpty(result.UserId) && (scope == "snsapi_userinfo" || scope == "snsapi_privateinfo"))
            {
                GetUserDetailResult resultDetail = null;
                try
                {
                    resultDetail = CommonJsonSend.Send<GetUserDetailResult>(wxFO.GetAccessToken(corpid), "https://qyapi.weixin.qq.com/cgi-bin/user/getuserdetail?access_token={0}", 
                        new {
                            user_ticket= result.user_ticket,
                        });
                    //LogWriter.Info("OAuth2Callback:Send" + JsonHelper.ToJson(resultDetail));
                }
                catch (Exception ex)
                {
                    LogWriter.Error(ex, string.Format("qyid为{0}的认证授权在通过ticket获取详情时异常", corpid));
                }
                if (resultDetail != null && resultDetail.userid != null)
                {
                    url = string.Format("{0}{1}userinfo={2}"
                            , url, url.Contains('?') ? "&" : "?", Base64Helper.EncodeBase64(JsonHelper.ToJson(new {
                                userid = result.UserId,
                                openid = result.OpenId,
                                name=resultDetail.name,
                                department = resultDetail.department,
                                position = resultDetail.position,
                                mobile = resultDetail.mobile,
                                gender = resultDetail.gender,
                                email = resultDetail.email,
                                avatar = resultDetail.avatar,
                            })));
                    //LogWriter.Info("OAuth2Callback:url1:" + url);
                    return Redirect(url);
                }
            }

            url = string.Format("{0}{1}userinfo={2}"
                    , url, url.Contains('?') ? "&" : "?", Base64Helper.EncodeBase64(JsonHelper.ToJson(new {userid=result.UserId,openid=result.OpenId})));
            //LogWriter.Info("OAuth2Callback:url2:" + url);
            return Redirect(url);
        }
        #endregion

        #region JSSDK接口
        [HttpPost]
        public ActionResult Jssdk(string corpid, string accesstoken, string callurl)
        {
            #region 校验
            var account = GetAccount(corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的JSSDK获取失败，原因：企业号不存在", corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }
            if (GetPassToken(account, accesstoken) == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的JSSDK获取失败，原因：accesstoken错误", corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            var ticket = wxFO.GetJsApiTicket(corpid);
            var url = callurl ?? "";
            url = Base64Helper.DecodeBase64(url.Replace(" ", "+"));
            string timestamp = Convert.ToString(ConvertDateTimeInt(DateTime.Now));
            string nonceStr = createNonceStr();
            string rawstring = "jsapi_ticket=" + ticket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=" + url;
            string signature = SHA1_Hash(rawstring);

            return Json(new
            {
                errcode = "0",
                errormsg = "ok",
                appId = account.CorpID,
                nonceStr = nonceStr,
                timestamp = timestamp,
                url = url,
                signature = signature,
                rawString = rawstring,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JssdkJsonP(string corpid, string accesstoken, string callurl, string callback)
        {
            #region 校验
            var account = GetAccount(corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的JSSDK获取失败，原因：企业号不存在", corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "企业号不存在",
                });
            }
            if (GetPassToken(account, accesstoken) == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的JSSDK获取失败，原因：accesstoken错误", corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            #endregion

            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();

            var ticket = wxFO.GetJsApiTicket(corpid);
            var url = callurl ?? "";
            url = Base64Helper.DecodeBase64(url.Replace(" ", "+"));
            string timestamp = Convert.ToString(ConvertDateTimeInt(DateTime.Now));
            string nonceStr = createNonceStr();
            string rawstring = "jsapi_ticket=" + ticket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=" + url;
            string signature = SHA1_Hash(rawstring);

            return Content(string.IsNullOrEmpty(callback) ? "" : string.Format("{0}({1})", callback, new JavaScriptSerializer().Serialize(
                new
                {
                    errcode = "0",
                    errormsg = "ok",
                    appId = account.CorpID,
                    nonceStr = nonceStr,
                    timestamp = timestamp,
                    url = url,
                    signature = signature,
                    rawString = rawstring,
                })));
        }

        #region jssdk私有函数
        private string createNonceStr()
        {
            int length = 16;
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string str = "";
            Random rad = new Random();
            for (int i = 0; i < length; i++)
            {
                str += chars.Substring(rad.Next(0, chars.Length - 1), 1);
            }
            return str;
        }

        /// <summary> 
        /// 将c# DateTime时间格式转换为Unix时间戳格式 
        /// </summary> 
        /// <param name="time">时间</param> 
        /// <returns>double</returns> 
        public int ConvertDateTimeInt(System.DateTime time)
        {
            int intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = Convert.ToInt32((time - startTime).TotalSeconds);
            return intResult;
        }

        //SHA1哈希加密算法 
        public string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = System.Text.UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }
        #endregion
        #endregion

        #region 获取微信通行证
        /// <summary>
        /// 获取微信通行证
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="getnewtoken"></param>
        /// <returns></returns>
        public ActionResult GetAccessToken(string corpid, string accesstoken, int getnewtoken = 0)
        {
            var account = GetAccount(corpid);
            if (account == null)
            {
                LogWriter.Info(string.Format("corpid为“{0}”的accesstoken获取失败，原因：企业号不存在", corpid));
                return Content("企业号不存在");
            }

            if (!ValidateAccessToken(account, accesstoken))
            {
                LogWriter.Info(string.Format("corpid为“{0}”的accesstoken获取失败，原因：accesstoken错误", corpid));
                return Json(new
                {
                    errorcode = "500",
                    errormsg = "非法访问",
                });
            }
            var wxFO = Formula.FormulaHelper.CreateFO<WxFO>();
            return Json(new
            {
                access_token = wxFO.GetAccessToken(corpid, getnewtoken == 1),
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 校验通行证
        /// <summary>
        /// 校验通行证
        /// </summary>
        /// <param name="account"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        private bool ValidateAccessToken(QyAccount account, string accesstoken)
        {
            var passtoken = GetPassToken(account, accesstoken);
            if (passtoken != null)
            {
                if (passtoken.AllowIP != "*")
                {
                    var clientip = GetClientIp();
                    return passtoken.AllowIP.Split(',').Contains(clientip);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 校验通行证
        /// </summary>
        /// <param name="account"></param>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        private QyAccountPassToken GetPassToken(QyAccount account, string accesstoken)
        {
            var passtoken = CacheHelper.Get(string.Format("WxAccountToken{0}{1}", account.ID, accesstoken)) as QyAccountPassToken;
            if (passtoken == null)
            {
                passtoken = entities.Set<QyAccountPassToken>().Where(c => c.QyID == account.ID && c.PassToken == accesstoken).FirstOrDefault();
                if (passtoken != null)
                {
                    CacheHelper.Set(string.Format("WxAccountToken{0}{1}", account.ID, accesstoken), passtoken, cachesecond);
                }
            }
            return passtoken;
        }

        private bool ValidateOauth2Domain(QyAccountPassToken passtoken, string url)
        {
            if (passtoken.AllowIP != "*")
            {
                Uri ru = null;
                try
                {
                    ru = new Uri(url);
                }
                catch
                {
                    return false;
                }
                var redomain = ru.Authority.ToLower();
                return passtoken.OAuth2AllowDomain.Split(',').Contains(redomain);
            }
            return true;
        }
        #endregion

        #region 获取客户端Ip
        /// <summary>  
        /// 获取客户端Ip  
        /// </summary>  
        /// <returns></returns>  
        public string GetClientIp()
        {
            String clientIP = "";
            if (System.Web.HttpContext.Current != null)
            {
                clientIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(clientIP) || (clientIP.ToLower() == "unknown"))
                {
                    clientIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
                    if (string.IsNullOrEmpty(clientIP))
                    {
                        clientIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                else
                {
                    clientIP = clientIP.Split(',')[0];
                }
            }
            return clientIP;
        }
        #endregion

        public ActionResult Error()
        {
            return Json(new
            {
                errorcode = "500",
                errormsg = "非法访问",
            }, JsonRequestBehavior.AllowGet);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            LogWriter.Error(filterContext.Exception);
            Response.Redirect("/wechatservice/wxapi/error");
        }

        protected override void HandleUnknownAction(string actionName)
        {
            if (Request.HttpMethod == "POST")
                throw new Exception("没有Action:" + actionName);

            // 搜索文件是否存在
            var filePath = "";
            if (RouteData.DataTokens["area"] != null)
                filePath = string.Format("~/Areas/{2}/Views/{1}/{0}.cshtml", actionName, RouteData.Values["controller"], RouteData.DataTokens["area"]);
            else
                filePath = string.Format("~/Views/{1}/{0}.cshtml", actionName, RouteData.Values["controller"]);
            if (System.IO.File.Exists(Server.MapPath(filePath)))
            {
                View(filePath).ExecuteResult(ControllerContext);
            }
            else
            {
                LogWriter.Error(string.Format("找不到控制器：{0}", actionName));
                Response.Redirect("/wechatservice/wxapi/error");
            }
        }
    }
}
