using Persistence.Entity;

namespace DataAccessLayer.Repository.Product
{
    public class Product : BaseRepo<Persistence.Entity.Product.Product>, IProduct
    {
        public Product(Entities dbContext) : base(dbContext)
        {

        }
        public Entities SRTCoreDbContext
        {
            get { return _dbContext as Entities; }
        }

        public IEnumerable<Persistence.Entity.Product.Product> GetByRefObjectState(Int64 refObjectState)
        {
            return this.SRTCoreDbContext.Products.Where(w => w.IdRefObjectState == refObjectState);
        }

        public IEnumerable<Persistence.Entity.Product.Product> ContainsName(string name)
        {
            return this.SRTCoreDbContext.Products.Where(w => w.Name.Contains(name));
        }

        public IEnumerable<Persistence.Entity.Product.Product> GetByName(string name)
        {
            return this.SRTCoreDbContext.Products.Where(w => w.Name == name);
        }

        public IEnumerable<Persistence.Entity.Product.Product> ContainsAliasName(string name)
        {
            return this.SRTCoreDbContext.Products.Where(w => w.AliasName.Contains(name));
        }

        public IEnumerable<Persistence.Entity.Product.Product> ContainsDescription(string description)
        {
            return this.SRTCoreDbContext.Products.Where(w => w.Description.Contains(description));
        }

        public void Insert(Persistence.Entity.Product.Product entity, Guid updatedBy)
        {
            entity.IdUserCreatedBy = updatedBy;
            entity.CreatedDate = DateTime.UtcNow;
            base.Insert(entity);
        }

        public void InsertRange(IEnumerable<Persistence.Entity.Product.Product> entities, Guid updatedBy)
        {
            foreach (Persistence.Entity.Product.Product t in entities)
            {
                t.IdUserCreatedBy = updatedBy;
                t.CreatedDate = DateTime.UtcNow;
            }
            base.InsertRange(entities);
        }

        public void Update(Persistence.Entity.Product.Product entity, Guid updatedBy)
        {
            entity.IdUserUpdatedBy = updatedBy;
            entity.UpdatedDate = DateTime.UtcNow;
            base.Update(entity);
        }
    }
}
