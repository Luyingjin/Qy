using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatService.Models
{
    public class QyBatchInviteUserModel : QyAsynchronousModel
    {
        public string toUser { get; set; }
        public string toParty { get; set; }
        public string toTag { get; set; }
        public string inviteTips { get; set; }
    }
}