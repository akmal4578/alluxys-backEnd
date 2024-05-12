using refObjectState = Persistence.Entity.RefObjectState;

namespace DataAccessLayer.Repository.RefObjectState
{
    public interface IRefObjectState : IRepo<refObjectState.RefObjectState>
    {
        IEnumerable<refObjectState.RefObjectState> GetByName(string name);
        IEnumerable<refObjectState.RefObjectState> ContainsName(string name);
        IEnumerable<refObjectState.RefObjectState> ContainsAliasName(string aliasName);
        IEnumerable<refObjectState.RefObjectState> ContainsDescription(string description);
        void Insert(refObjectState.RefObjectState entity, Guid updatedBy);
        void InsertRange(IEnumerable<refObjectState.RefObjectState> entities, Guid updatedBy);
        void Update(refObjectState.RefObjectState entity, Guid updatedBy);
    }
}
