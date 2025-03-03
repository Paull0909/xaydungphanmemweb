using Microsoft.AspNetCore.Identity;

namespace Application.Entities
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public virtual User User { get; set; }
    }
}
