using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

using Persistence.Entity.Security;

namespace Persistence.Entity.RefObjectState
{
    public class RefObjectState
    {
        public long IdRefObjectState { get; set; }

        public string Name { get; set; }

        public string AliasName { get; set; }

        public string Description { get; set; }

        public bool IsDefault { get; set; }

        public bool IsDisplay { get; set; }

        public Guid? IdUserCreatedBy { get; set; }

        public Guid? IdUserUpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }


        #region for relationship

        #region Other

        public User User1 { get; set; }

        public User User2 { get; set; }

        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product.Product> Products1 { get; set; }

        #endregion


        public RefObjectState()
        {

            #region Others

            Users1 = new HashSet<User>();

            Products1 = new HashSet<Product.Product>();
            #endregion
        }
    }
}
