using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatService.Models
{
    public class QyDeptUserModel : QyBaseModel
    {
        public int department_id { get; set; }
        public int fetch_child { get; set; }
        public int status { get; set; }
    }
}