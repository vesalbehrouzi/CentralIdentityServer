using System;
using System.Collections.Generic;

namespace CentralIdentityServer.Model
{
    public class User : EntityBase
    {
        #region public auto-implemented property
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public IList<UserGroup> UserGroups { get; set; }
        #endregion
    }
}
