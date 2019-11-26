using Senparc.Weixin.QY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatService.Models
{
    public class QyNewsMsgModel : QyMsgModel
    {
        public QyNewsSubModel news { get; set; }
    }

    public class QyNewsSubModel
    {
        public List<Article> articles { get; set; }
    }
}