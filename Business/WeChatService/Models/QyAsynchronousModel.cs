using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatService.Models
{
    public class QyAsynchronousModel : QyBaseModel
    {
        public string url { get; set; }
        public string token { get; set; }
        public string encodingaeskey { get; set; }
    }
}