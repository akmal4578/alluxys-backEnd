using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product
    {
        private readonly ILogger<Services.Product.Product> _log;
        private Services.Product.Contract.IProduct _product;


        public Product(ILogger<Services.Product.Product> log, Services.Product.Contract.IProduct product)
        {
            this._log = log;
            this._product = product;
        }

        [HttpGet]
        public IEnumerable<Services.Product.DTO.Product> Get()
        {
            try
            {
                return this._product.Get();

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpGet("{id}")]
        public Services.Product.DTO.Product Get(Guid Id)
        {
            try
            {
                return this._product.GetByKey(Id);

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpGet("GetActiveRefObjectState")]
        public IEnumerable<Services.Product.DTO.Product> GetActiveRefObjectState()
        {
            try
            {
                return this._product.GetActiveRefObjectState();

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpGet("GetTerminatedRefObjectState")]
        public IEnumerable<Services.Product.DTO.Product> GetTerminatedRefObjectState()
        {
            try
            {
                return this._product.GetTerminatedRefObjectState();

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpGet("GetByName/{name}")]
        public IEnumerable<Services.Product.DTO.Product> GetByName(string name)
        {
            try
            {
                return this._product.GetByName(name);

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpGet("ContainsName/{keyword}")]
        public IEnumerable<Services.Product.DTO.Product> ContainsName(string keyword)
        {
            try
            {
                return this._product.ContainsName(keyword);

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpGet("ContainsAliasName/{keyword}")]
        public IEnumerable<Services.Product.DTO.Product> ContainsAliasName(string keyword)
        {
            try
            {
                return this._product.ContainsAliasName(keyword);

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpGet("ContainsDescription/{keyword}")]
        public IEnumerable<Services.Product.DTO.Product> ContainsDescription(string keyword)
        {
            try
            {
                return this._product.ContainsDescription(keyword);

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpPost("{updater}")]
        public void Post([FromBody] Services.Product.DTO.Product product, Guid updater)
        {
            try
            {
                this._product.Create(product, updater, true);

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpPost("PostList/{updater}")]
        public void Post([FromBody] IEnumerable<Services.Product.DTO.Product> product, Guid updater)
        {
            try
            {
                this._product.CreateInBulk(product, updater, true);

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpPut("{updater}")]
        public void Put([FromBody] Services.Product.DTO.Product product, Guid updater)
        {
            try
            {
                this._product.Update(product, updater, true);

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpPut("Terminate/{id}/{updater}")]
        public void Terminate([FromBody] Services.Product.DTO.Product product, Guid Id, Guid updater)
        {
            try
            {
                this._product.Terminate(Id, updater, true);

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpPut("Reactivate/{id}/{updater}")]
        public void Reactivate([FromBody] Services.Product.DTO.Product product, Guid Id, Guid updater)
        {
            try
            {
                this._product.Reactivate(Id, updater, true);

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }

        [HttpDelete("{id}/{updater}")]
        public void Delete(Guid id, Guid updater)
        {
            try
            {
                this._product.Delete(id, updater, true);

            }
            catch (Exception ex)
            {
                this._log.LogError(ex.ToString());
                throw;
            }
        }
    }
}
