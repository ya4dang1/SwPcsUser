using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructures
{
    public class EmailConfig
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string FromName { get; set; }
        public string From { get; set; }
    }
}
