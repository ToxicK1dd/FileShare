using FileShare.DataAccess.Base.Model.BaseEntity.Interface;
using Microsoft.AspNetCore.Identity;

namespace FileShare.DataAccess.Base.Model.BaseIdentityUser
{
    public class BaseIdentityUser : IdentityUser<Guid>, IBaseEntity
    {
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Changed { get; set; }

        public bool Deleted { get; set; }
    }
}