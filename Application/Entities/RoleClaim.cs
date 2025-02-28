using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        // Khóa ngoại cho bảng Role
 
    }
}
