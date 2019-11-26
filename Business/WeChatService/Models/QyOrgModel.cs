using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatService.Models
{
    public class QyOrgModel : QyBaseModel
    {
        public string name { get; set; }
        public int parentid { get; set; }
        public int order { get; set; }
        public int? id { get; set; }
    }
}