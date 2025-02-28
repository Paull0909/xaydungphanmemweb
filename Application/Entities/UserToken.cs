using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public virtual User User { get; set; }
    }
}
