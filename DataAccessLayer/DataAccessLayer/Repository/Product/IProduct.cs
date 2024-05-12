using product = Persistence.Entity.Product;

namespace DataAccessLayer.Repository.Product
{
    public interface IProduct : IRepo<product.Product>
    {
        IEnumerable<product.Product> GetByRefObjectState(Int64 refObjectState);
        IEnumerable<product.Product> GetByName(string name);
        IEnumerable<product.Product> ContainsName(string name);
        IEnumerable<product.Product> ContainsAliasName(string name);
        IEnumerable<product.Product> ContainsDescription(string description);
        void Insert(product.Product entity, Guid updatedBy);
        void InsertRange(IEnumerable<product.Product> entities, Guid updatedBy);
        void Update(product.Product entity, Guid updatedBy);
    }
}
