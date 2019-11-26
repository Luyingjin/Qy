/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：OAuth2Result.cs
    文件功能描述：获取成员信息返回结果
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
 
    修改标识：Senparc - 20150316
    修改描述：添加DeviceId字段
 
    修改标识：Senparc - 20150316
    修改描述：GetUserIdResult变更为GetUserInfoResult，增加OpenId字段
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.QY.AdvancedAPIs.OAuth2
{
    /// <summary>
    /// 获取成员信息返回结果
    /// </summary>
    public class GetUserInfoResult : QyJsonResult
    {
        /// <summary>
        /// 员工UserID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 非企业成员的OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 手机设备号(由微信在安装时随机生成)
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 凭证
        /// </summary>
        public string user_ticket { get; set; }
    }

    /// <summary>
    /// 获取成员信息返回结果
    /// </summary>
    public class GetUserDetailResult : QyJsonResult
    {
        /// <summary>
        /// 员工UserID
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public List<int> department { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }
    }
}
