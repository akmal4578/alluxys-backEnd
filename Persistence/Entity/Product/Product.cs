using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Persistence.Entity.Security;
using refObjectState = Persistence.Entity.RefObjectState;

namespace Persistence.Entity.Product
{
    public class Product
    {
        public Guid IdProduct { get; set; }
        public string Name { get; set; }
        public string AliasName { get; set; }
        public string Description { get; set; }
        public Guid? IdUserCreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? IdUserUpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long IdRefObjectState { get; set; }

        #region For Relationship

        public User User1 { get; set; }
        public User User2 { get; set; }

        public refObjectState.RefObjectState RefObjectState1 { get; set; }

        #endregion

        public Product()
        {
            
        }
    }
}
