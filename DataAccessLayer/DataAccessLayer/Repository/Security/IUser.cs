using user = Persistence.Entity.Security;

namespace DataAccessLayer.Repository.Security
{
    public interface IUser : IRepo<user.User>
    {
        IEnumerable<user.User> GetByRefObjectState(Int64 refObjectState);
        IEnumerable<user.User> GetByName(string name);
        IEnumerable<user.User> ContainsName(string name);
        IEnumerable<user.User> ContainsAliasName(string name);
        IEnumerable<user.User> ContainsDescription(string description);
        void Insert(user.User entity, Guid updatedBy);
        void InsertRange(IEnumerable<user.User> entities, Guid updatedBy);
        void Update(user.User entity, Guid updatedBy);
        //void Terminate(user.User entity, Guid updatedBy);
        //void Reactivate(user.User entity, Guid updatedBy);//changes

    }
}
