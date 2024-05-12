using System;
using System.Collections.Generic;
using System.Text;

using refObjectState = Persistence.Entity.RefObjectState;

namespace Persistence.Entity.Security
{
    public class User
    {
        public Guid IdUser { get; set; }

        public string Name { get; set; }

        public string AliasName { get; set; }

        public string Description { get; set; }

        public long IdRefObjectState { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public Guid? IdUserCreatedBy { get; set; }

        public Guid? IdUserUpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        #region For Relationship


        public User User1 { get; set; }

        public User User2 { get; set; }

        public refObjectState.RefObjectState RefObjectState1 { get; set; }

        #region Others
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<refObjectState.RefObjectState> RefObjectStates1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<refObjectState.RefObjectState> RefObjectStates2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product.Product> Products1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product.Product> Products2 { get; set; }

        #endregion

        #endregion


        public User()
        {

            #region Others

            Users1 = new HashSet<User>();
            Users2 = new HashSet<User>();

            RefObjectStates1 = new HashSet<refObjectState.RefObjectState>();
            RefObjectStates2 = new HashSet<refObjectState.RefObjectState>();

            Products1 = new HashSet<Product.Product>();
            Products2 = new HashSet<Product.Product>();

            #endregion
        }

    }
}