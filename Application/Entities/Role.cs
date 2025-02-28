using Microsoft.AspNetCore.Identity;

namespace Application.Entities
{
    public  class Role : IdentityRole<Guid>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public string Description { get; set; }
    }
}
