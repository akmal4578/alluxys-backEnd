using DataAccessLayer.Repository.Product;
using DataAccessLayer.Repository.RefObjectState;
using DataAccessLayer.Repository.Security;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRefObjectState RefObjectStates { get; }

        IUser Users { get; }

        IProduct Products { get; }

        #region Method

        void CreateIOTagArchivedTable(string tableName);

        void CreateEntityTypeNameAttribute(string tableName);

        int Complete();

        //int Complete(Guid userId, bool enableChangeTracker = true);

        void Dispose();

        #endregion
    }
}
