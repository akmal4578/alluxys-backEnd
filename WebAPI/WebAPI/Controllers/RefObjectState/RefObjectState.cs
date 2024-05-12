//using Microsoft.AspNetCore.Mvc;

//namespace WebAPI.Controllers.RefObjectState
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RefObjectState : ControllerBase
//    {
//        private readonly ILogger<Services.RefObjectState.RefObjectState> _log;
//        private Services.RefObjectState.Contract.IRefObjectState _universalObjectState;

//        public RefObjectState(ILogger<Services.RefObjectState.RefObjectState> log, Services.RefObjectState.Contract.IRefObjectState universalObjectState)
//        {
//            this._log = log;
//            this._universalObjectState = universalObjectState;

//        }

//        [HttpGet]
//        public IEnumerable<Services.RefObjectState.DTO.RefObjectState> Get()
//        {
//            try
//            {
//                return this._universalObjectState.Get();

//            }
//            catch (Exception ex)
//            {
//                this._log.LogError(ex.ToString());
//                throw;
//            }
//        }

//        [HttpGet("{id}")]
//        public Services.RefObjectState.DTO.RefObjectState Get(int Id)
//        {
//            try
//            {
//                return this._universalObjectState.GetByKey(Id);

//            }
//            catch (Exception ex)
//            {
//                this._log.LogError(ex.ToString());
//                throw;
//            }
//        }

//        [HttpGet("GetByName/{name}")]
//        public IEnumerable<Services.RefObjectState.DTO.RefObjectState> GetByName(string name)
//        {
//            try
//            {
//                return this._universalObjectState.GetByName(name);

//            }
//            catch (Exception ex)
//            {
//                this._log.LogError(ex.ToString());
//                throw;
//            }
//        }

//        [HttpGet("ContainsName/{keyword}")]
//        public IEnumerable<Services.RefObjectState.DTO.RefObjectState> ContainsName(string keyword)
//        {
//            try
//            {
//                return this._universalObjectState.ContainsName(keyword);

//            }
//            catch (Exception ex)
//            {
//                this._log.LogError(ex.ToString());
//                throw;
//            }
//        }

//        [HttpGet("ContainsAliasName/{keyword}")]
//        public IEnumerable<Services.RefObjectState.DTO.RefObjectState> ContainsAliasName(string keyword)
//        {
//            try
//            {
//                return this._universalObjectState.ContainsAliasName(keyword);

//            }
//            catch (Exception ex)
//            {
//                this._log.LogError(ex.ToString());
//                throw;
//            }
//        }

//        [HttpGet("ContainsDescription/{keyword}")]
//        public IEnumerable<Services.RefObjectState.DTO.RefObjectState> ContainsDescription(string keyword)
//        {
//            try
//            {
//                return this._universalObjectState.ContainsDescription(keyword);

//            }
//            catch (Exception ex)
//            {
//                this._log.LogError(ex.ToString());
//                throw;
//            }
//        }

//        [HttpPost("{updater}")]
//        public void Post([FromBody] Services.RefObjectState.DTO.RefObjectState universalObjectState, Guid updater)
//        {
//            try
//            {
//                this._universalObjectState.Create(universalObjectState, updater, true);

//            }
//            catch (Exception ex)
//            {
//                this._log.LogError(ex.ToString());
//                throw;
//            }
//        }

//        [HttpPost("PostList/{updater}")]
//        public void Post([FromBody] IEnumerable<Services.RefObjectState.DTO.RefObjectState> universalObjectState, Guid updater)
//        {
//            try
//            {
//                this._universalObjectState.CreateInBulk(universalObjectState, updater, true);

//            }
//            catch (Exception ex)
//            {
//                this._log.LogError(ex.ToString());
//                throw;
//            }
//        }

//        [HttpPut("{updater}")]
//        public void Put([FromBody] Services.RefObjectState.DTO.RefObjectState universalObjectState, Guid updater)
//        {
//            try
//            {
//                this._universalObjectState.Update(universalObjectState, updater, true);

//            }
//            catch (Exception ex)
//            {
//                this._log.LogError(ex.ToString());
//                throw;
//            }
//        }

//        [HttpDelete("{id}/{updater}")]
//        public void Delete(long id, Guid updater)
//        {
//            try
//            {
//                this._universalObjectState.Delete(id, updater, true);

//            }
//            catch (Exception ex)
//            {
//                this._log.LogError(ex.ToString());
//                throw;
//            }
//        }
//    }
//}
