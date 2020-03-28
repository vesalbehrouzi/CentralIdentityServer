using System;
using System.Collections.Generic;

namespace CentralIdentityServer.Model
{
    public abstract class EntityBase
    {
        #region public auto-implemented property
        public Guid Id { get; set; }
        #endregion
    }
}
