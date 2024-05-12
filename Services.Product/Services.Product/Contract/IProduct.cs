namespace Services.Product.Contract
{
    public interface IProduct
    {
        public IEnumerable<DTO.Product> Get();
        public DTO.Product GetByKey(Guid id);
        public bool Create(DTO.Product product, Guid updatedBy, bool CommitChanges = false);
        public bool CreateInBulk(IEnumerable<DTO.Product> product, Guid updatedBy, bool CommitChanges = false);
        public bool Update(DTO.Product product, Guid updatedBy, bool CommitChanges = false);
        public bool Delete(Guid id, Guid updatedBy, bool CommitChanges = false);
        public bool Delete(DTO.Product product, Guid updatedBy, bool CommitChanges = false);
        public IEnumerable<DTO.Product> GetActiveRefObjectState();
        public IEnumerable<DTO.Product> GetTerminatedRefObjectState();
        public IEnumerable<DTO.Product> ContainsName(string name);
        public IEnumerable<DTO.Product> GetByName(string name);
        public IEnumerable<DTO.Product> ContainsAliasName(string name);
        public IEnumerable<DTO.Product> ContainsDescription(string description);
        public bool Terminate(Guid key, Guid updatedBy, bool CommitChanges = false);
        public bool Reactivate(Guid key, Guid updatedBy, bool CommitChanges = false);
    }
}
