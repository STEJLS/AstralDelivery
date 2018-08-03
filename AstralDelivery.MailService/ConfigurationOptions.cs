using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AstralDelivery.MailService
{
    class ConfigurationOptions
    {
        public string ServiceName { get; set; }
        public string ServiceEmail { get; set; }
        public string ServicePassword { get; set; }
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public bool SMTPUseSSL { get; set; }

        public ConfigurationOptions()
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.Production.json");

            var config = builder.Build();

            ServiceName = config["Service:Name"];
            ServiceEmail = config["Service:Email"];
            ServicePassword = config["Service:Password"];
            SMTPHost = config["smtp:Host"];
            SMTPPort = int.Parse(config["smtp:Port"]);
            SMTPUseSSL = Boolean.Parse(config["smtp:UseSSL"]);
        }
    }
}
