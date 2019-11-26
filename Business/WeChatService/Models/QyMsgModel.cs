using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatService.Models
{
    public class QyMsgModel : QyBaseModel
    {
        public string toUser { get; set; }
        public string toParty { get; set; }
        public string toTag { get; set; }
        public string agentId { get; set; }
        public int safe { get; set; }
    }
}