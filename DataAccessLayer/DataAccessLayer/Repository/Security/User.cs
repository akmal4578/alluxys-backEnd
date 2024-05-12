using Persistence.Entity;

namespace DataAccessLayer.Repository.Security
{
    public class User : BaseRepo<Persistence.Entity.Security.User>, IUser
    {
        public User(Entities dbContext) : base(dbContext)
        {

        }

        public Entities SRTCoreDbContext
        {
            get { return _dbContext as Entities; }
        }

        public IEnumerable<Persistence.Entity.Security.User> GetByRefObjectState(Int64 refObjectState)
        {
            return this.SRTCoreDbContext.Users.Where(w => w.IdRefObjectState == refObjectState);
        }

        public IEnumerable<Persistence.Entity.Security.User> ContainsName(string name)
        {
            return this.SRTCoreDbContext.Users.Where(w => w.Name.Contains(name));
        }

        public IEnumerable<Persistence.Entity.Security.User> GetByName(string name)
        {
            return this.SRTCoreDbContext.Users.Where(w => w.Name == name);
        }

        public IEnumerable<Persistence.Entity.Security.User> ContainsAliasName(string name)
        {
            return this.SRTCoreDbContext.Users.Where(w => w.AliasName.Contains(name));
        }

        public IEnumerable<Persistence.Entity.Security.User> ContainsDescription(string description)
        {
            return this.SRTCoreDbContext.Users.Where(w => w.Description.Contains(description));
        }

        public void Insert(Persistence.Entity.Security.User entity, Guid updatedBy)
        {
            entity.IdUserCreatedBy = updatedBy;
            entity.CreatedDate = DateTime.UtcNow;
            base.Insert(entity);
        }

        public void InsertRange(IEnumerable<Persistence.Entity.Security.User> entities, Guid updatedBy)
        {
            foreach (Persistence.Entity.Security.User t in entities)
            {
                t.IdUserCreatedBy = updatedBy;
                t.CreatedDate = DateTime.UtcNow;
            }
            base.InsertRange(entities);
        }

        public void Update(Persistence.Entity.Security.User entity, Guid updatedBy)
        {
            entity.IdUserUpdatedBy = updatedBy;
            entity.UpdatedDate = DateTime.UtcNow;
            base.Update(entity);
        }

        //public void Terminate(Persistence.Entity.Security.User entity, Guid updatedBy)
        //{
        //    //entity.IdRefObjectState = 0;
        //    entity.IdUserUpdatedBy = updatedBy;
        //    entity.UpdatedDate = DateTime.UtcNow;

        //    this.SRTCoreDbContext.Users.Update(entity);
        //}

        //public void Reactivate(Persistence.Entity.Security.User entity, Guid updatedBy)
        //{
        //    //entity.IdRefObjectState = 1;
        //    entity.IdUserUpdatedBy = updatedBy;
        //    entity.UpdatedDate = DateTime.UtcNow;

        //    this.SRTCoreDbContext.Users.Update(entity);
        //}

    }
}
