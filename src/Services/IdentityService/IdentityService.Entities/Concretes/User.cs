using IdentityService.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Entities.Concretes
{
    public class User : BaseEntity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpires { get; set; }
        public Role Role { get; set; }
    }
}
