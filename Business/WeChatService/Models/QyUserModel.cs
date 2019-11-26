using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatService.Models
{
    public class QyUserModel : QyBaseModel
    {
        public string userid { get; set; }
        public string name { get; set; }
        public int[] department { get; set; }
        public string position { get; set; }
        public string mobile { get; set; }
        public int gender { get; set; }
        public string email { get; set; }
        public string weixinid { get; set; }
        public string avatar_mediaid { get; set; }
        public string avatar { get; set; }
        public int enable { get; set; }
        public Extattr extattr { get; set; }
    }
}