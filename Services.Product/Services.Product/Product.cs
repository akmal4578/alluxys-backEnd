using AutoMapper;
using System.Data;
using Services.Product.Contract;
using DataAccessLayer.UnitOfWork;

namespace Services.Product
{
    public class Product : IProduct
    {
        private IUnitOfWork _uow;
        private IMapper _mapper = null;
        private MapperConfiguration _cfgOrder = null;

        public Product(IUnitOfWork uow)
        {
            this._uow = uow;

            this._cfgOrder = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Persistence.Entity.Product.Product, DTO.Product>().ReverseMap();
            });
            this._mapper = _cfgOrder.CreateMapper();
        }

        public IEnumerable<DTO.Product> Get()
        {
            return this._mapper.Map<IEnumerable<Persistence.Entity.Product.Product>, IEnumerable<DTO.Product>>(this._uow.Products.Get());
        }

        public DTO.Product GetByKey(Guid id)
        {
            Persistence.Entity.Product.Product entity = this._uow.Products.GetByPKey(id);
            if (entity != null)
            {
                return this._mapper.Map<Persistence.Entity.Product.Product, DTO.Product>(entity);
            }
            else
            {
                return null;
            }
        }

        public bool Create(DTO.Product product, Guid updatedBy, bool CommitChanges = false)
        {
            DTO.Product obj = this.GetByName(product.Name).SingleOrDefault();
            if (obj != null)
            {
                throw new Exception();
            }

            product.IdRefObjectState = 1;
            this._uow.Products.Insert(this._mapper.Map<DTO.Product, Persistence.Entity.Product.Product>(product), updatedBy);

            if (CommitChanges) this._uow.Complete();

            return true;
        }

        public bool CreateInBulk(IEnumerable<DTO.Product> products, Guid updatedBy, bool CommitChanges = false)
        {
            foreach (DTO.Product t in products)
            {
                Create(t, updatedBy);
            }

            if (CommitChanges) this._uow.Complete();

            return true;

        }

        public bool Update(DTO.Product product, Guid updatedBy, bool CommitChanges = false)
        {
            DTO.Product obj = this.GetByKey(product.IdProduct);
            if (obj == null)
            {
                return false;
            }

            this._uow.Products.Update(this._mapper.Map<DTO.Product, Persistence.Entity.Product.Product>(product), updatedBy);

            if (CommitChanges) this._uow.Complete();
            return true;
        }

        public bool Delete(Guid id, Guid updatedBy, bool CommitChanges = false)
        {
            DTO.Product obj = this.GetByKey(id);
            if (obj == null)
            {
                return false;
            }
            this._uow.Products.Delete(id);

            if (CommitChanges) this._uow.Complete();
            return true;
        }

        public bool Delete(DTO.Product product, Guid updatedBy, bool CommitChanges = false)
        {
            Persistence.Entity.Product.Product entity = this._mapper.Map<DTO.Product, Persistence.Entity.Product.Product>(product);
            this._uow.Products.Delete(entity);

            if (CommitChanges) this._uow.Complete();

            return true;
        }

        public IEnumerable<DTO.Product> GetActiveRefObjectState()
        {
            IEnumerable<Persistence.Entity.Product.Product> products = this._uow.Products.GetByRefObjectState(1);

            if (products != null)
            {
                return _mapper.Map<IEnumerable<Persistence.Entity.Product.Product>, IEnumerable<DTO.Product>>(products);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<DTO.Product> GetTerminatedRefObjectState()
        {
            IEnumerable<Persistence.Entity.Product.Product> products = this._uow.Products.GetByRefObjectState(2);

            if (products != null)
            {
                return _mapper.Map<IEnumerable<Persistence.Entity.Product.Product>, IEnumerable<DTO.Product>>(products);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<DTO.Product> GetByName(string name)
        {
            IEnumerable<Persistence.Entity.Product.Product> products = this._uow.Products.GetByName(name);

            if (products != null)
            {
                return _mapper.Map<IEnumerable<Persistence.Entity.Product.Product>, IEnumerable<DTO.Product>>(products);
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<DTO.Product> ContainsName(string name)
        {
            IEnumerable<Persistence.Entity.Product.Product> products = this._uow.Products.ContainsName(name);

            if (products != null)
            {
                return _mapper.Map<IEnumerable<Persistence.Entity.Product.Product>, IEnumerable<DTO.Product>>(products);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<DTO.Product> ContainsAliasName(string name)
        {
            IEnumerable<Persistence.Entity.Product.Product> products = this._uow.Products.ContainsAliasName(name);

            if (products != null)
            {
                return _mapper.Map<IEnumerable<Persistence.Entity.Product.Product>, IEnumerable<DTO.Product>>(products);
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<DTO.Product> ContainsDescription(string description)
        {
            IEnumerable<Persistence.Entity.Product.Product> products = this._uow.Products.ContainsDescription(description);

            if (products != null)
            {
                return _mapper.Map<IEnumerable<Persistence.Entity.Product.Product>, IEnumerable<DTO.Product>>(products);
            }
            else
            {
                return null;
            }
        }
        public bool Terminate(Guid key, Guid updatedBy, bool CommitChanges = false)
        {
            Persistence.Entity.Product.Product product = this._uow.Products.GetByPKey(key);

            if (product == null)
            {
                throw new Exception();
            }

            product.IdRefObjectState = 2;
            this._uow.Products.Update(product, updatedBy);

            if (CommitChanges) this._uow.Complete();

            return true;

        }
        public bool Reactivate(Guid key, Guid updatedBy, bool CommitChanges = false)
        {
            Persistence.Entity.Product.Product product = this._uow.Products.GetByPKey(key);

            if (product == null)
            {
                throw new Exception();
            }

            product.IdRefObjectState = 1;
            this._uow.Products.Update(product, updatedBy);

            if (CommitChanges) this._uow.Complete();

            return true;
        }
        /*
        //get associated modules
        public IEnumerable<DTO.GetAreaByQuery> GetAreaWithExecuteReader()
        {
            var objects = this._uow.Areas.GetAreaWithExecuteReader((Int64)Enumeration.Enum.RefObjectState.Active);

            if (objects != null)
            {
                Extension.MapObjectToDTOExt extensionService = new Extension.MapObjectToDTOExt();

                IEnumerable<DTO.GetAreaByQuery> GetAreaByQueries = extensionService.MapObjectToDTO<DTO.GetAreaByQuery>(objects);

                return GetAreaByQueries;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<DTO.GetResourceByQuery> GetResourceWithExecuteReader()
        {
            var objects = this._uow.Resources.GetResourceWithExecuteReader((Int64)Enumeration.Enum.RefObjectState.Active);

            if (objects != null)
            {
                Extension.MapObjectToDTOExt extensionService = new Extension.MapObjectToDTOExt();

                IEnumerable<DTO.GetResourceByQuery> GetResourceByQueries = extensionService.MapObjectToDTO<DTO.GetResourceByQuery>(objects);

                return GetResourceByQueries;
            }
            else
            {
                return null;
            }
        }*/

        //public Int64 ExecuteNonQuery(string query, CommandType commandType)
        //{
        //    return this._uow.Products.ExecuteNonQuery(query, commandType);
        //}
    }
}
