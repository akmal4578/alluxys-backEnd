using Persistence.Entity;
using refObjectState = Persistence.Entity.RefObjectState;

namespace DataAccessLayer.Repository.RefObjectState
{
    public class RefObjectState : BaseRepo<refObjectState.RefObjectState>, IRefObjectState
    {
        public RefObjectState(Entities dbContext) : base(dbContext)
        {

        }

        public Entities DbContext
        {
            get { return _dbContext as Entities; }
        }

        public IEnumerable<refObjectState.RefObjectState> ContainsAliasName(string aliasName)
        {
            return this.DbContext.RefObjectStates.Where(w => w.AliasName.Contains(aliasName));
        }

        public IEnumerable<refObjectState.RefObjectState> ContainsDescription(string description)
        {
            return this.DbContext.RefObjectStates.Where(w => w.Description.Contains(description));
        }

        public IEnumerable<refObjectState.RefObjectState> ContainsName(string name)
        {
            return this.DbContext.RefObjectStates.Where(w => w.Name.Contains(name));
        }

        public IEnumerable<refObjectState.RefObjectState> GetByName(string name)
        {
            return this.DbContext.RefObjectStates.Where(w => w.Name == name);
        }

        public void Insert(refObjectState.RefObjectState entity, Guid updatedBy)
        {
            entity.IdUserCreatedBy = updatedBy;
            entity.CreatedDate = DateTime.UtcNow;
            base.Insert(entity);
        }

        public void InsertRange(IEnumerable<refObjectState.RefObjectState> entities, Guid updatedBy)
        {
            foreach (refObjectState.RefObjectState t in entities)
            {
                t.IdUserCreatedBy = updatedBy;
                t.CreatedDate = DateTime.UtcNow;
            }
            base.InsertRange(entities);
        }

        public void Update(refObjectState.RefObjectState entity, Guid updatedBy)
        {
            entity.IdUserUpdatedBy = updatedBy;
            entity.UpdatedDate = DateTime.UtcNow;
            base.Update(entity);
        }
    }
}
