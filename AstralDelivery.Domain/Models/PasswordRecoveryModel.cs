using System;
using System.Collections.Generic;
using System.Text;

namespace AstralDelivery.Domain.Models
{
    public class PasswordRecoveryModel
    {
        public Guid Token{ get; set; }
        public string NewPassword { get; set; }
    }
}
