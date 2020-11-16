using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Aspire.Application.AppServices.Dtos;
using Aspire.Domain.Entities;
using Aspire.Domain.Repositories;
using Aspire.Map;

using Microsoft.AspNetCore.Mvc;

namespace Aspire.Application.AppServices
{
    public abstract class CrudAppService<TEntity, TCommonDto> : CrudAppService<TEntity, TCommonDto, long>
        where TEntity : BaseEntity<long>
        where TCommonDto : CommonDto
    {
        protected CrudAppService(IRepository<TEntity, long> repository, IAspireMapper mapper) : base(repository, mapper)
        {
        }
    }

    public abstract class CrudAppService<TEntity, TCommonDto, TId> : CrudAppService<TEntity, TCommonDto, TId, TCommonDto>
        where TEntity : BaseEntity<TId>
        where TCommonDto : CommonDto<TId>
    {
        protected CrudAppService(IRepository<TEntity, TId> repository, IAspireMapper mapper) : base(repository, mapper)
        {
        }
    }

    public abstract class CrudAppService<TEntity, TOutputDto, TId, TInputDto> : CrudAppService<TEntity, TOutputDto, TId, TInputDto, TInputDto>
        where TEntity : BaseEntity<TId>
        where TOutputDto : OutputDto<TId>
        where TInputDto : InputDto<TId>
    {
        protected CrudAppService(IRepository<TEntity, TId> repository, IAspireMapper mapper) : base(repository, mapper)
        {
        }

        [HttpPost("/api/[controller]/create-or-update/{id?}")]
        public virtual async Task<TOutputDto> CreateOrUpdateAsync(TInputDto input, TId id = default(TId))
        {
            if (id?.Equals(default(TId)) ?? true)
                return await base.CreateAsync(input);
            return await base.UpdateAsync(id, input);
        }
    }

    public abstract class CrudAppService<TEntity, TOutputDto, TId, TCreateDto, TUpdateDto, TSearchDto> : CrudAppService<TEntity, TOutputDto, TId, TCreateDto, TUpdateDto>
        where TEntity : BaseEntity<TId>
        where TOutputDto : OutputDto<TId>
        where TUpdateDto : UpdateDto<TId>

    {
        protected CrudAppService(IRepository<TEntity, TId> repository, IAspireMapper mapper) : base(repository, mapper)
        {
        }
    }

    public abstract class CrudAppService<TEntity, TOutputDto, TId, TCreateDto, TUpdateDto> : AppService
        where TEntity : BaseEntity<TId>
        where TOutputDto : OutputDto<TId>
        where TUpdateDto : UpdateDto<TId>
    {
        private readonly IRepository<TEntity, TId> _repository;
        private readonly IAspireMapper _mapper;

        protected CrudAppService(IRepository<TEntity, TId> repository, IAspireMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("/api/[controller]")]
        public virtual async Task<TOutputDto> CreateAsync([FromBody] TCreateDto input)
        {
            var r = await _repository.AddThenEntityAsync(_mapper.To<TEntity>(input));
            return _mapper.To<TOutputDto>(r);
        }

        [HttpDelete("/api/[controller]/{id}")]
        public virtual async Task<bool> DeleteAsync(TId id)
           => await _repository.DeleteAsync(id);

        [HttpPut("/api/[controller]/{id}")]
        public virtual async Task<TOutputDto> UpdateAsync(TId id, [FromBody] TUpdateDto input)
        {
            var e = await _repository.GetByIdAsync(id);
            _mapper.To(input, e);
            e.Id = id;
            var r = await _repository.UpdateThenEntityAsync(e);
            return _mapper.To<TOutputDto>(r);
        }

        [HttpGet("/api/[controller]/{id}")]
        public virtual async Task<TOutputDto> GetAsync(TId id)
        {
            var r = await _repository.GetByIdAsync(id);
            return _mapper.To<TOutputDto>(r);
        }

        [HttpGet("/api/[controller]/{take:int}/{skip:int}")]
        public virtual async Task<PageDto<TOutputDto>> PageAsync(int take, int skip)
        {
            var (total, data) = await _repository.PageAsync(skip, take);
            return new PageDto<TOutputDto>
            {
                Data = _mapper.To<TOutputDto[]>(data ?? (IList<TEntity>)new TEntity[0]),
                Total = total
            };
        }

        [HttpGet("/api/[controller]/from/{type}")]
        public virtual object GetFrom(string type)
        {
            IEnumerable<Dictionary<string, object>> r = null;
            if (type == "create")
            {
                var properties = typeof(TCreateDto).GetProperties();
                r = properties.Select(x =>
                {
                    var rule = (IAspireForm)x.GetCustomAttributes()
                        .FirstOrDefault(f =>
                            f.GetType().GetInterface(nameof(IAspireForm)) == typeof(IAspireForm));

                    return rule?.GetFormRule(x);
                }).Where(x => x != null);
            }
            else if (type == "table")
            {
                var properties = typeof(TOutputDto).GetProperties();
                r = properties.Select(x =>
                {
                    var rule = (IAspireTable)x.GetCustomAttributes()
                        .FirstOrDefault(f =>
                            f.GetType().GetInterface(nameof(IAspireTable)) == typeof(IAspireTable));
                    return rule?.GetTableRule(x);
                }).Where(x => x != null);
            }

            return r;
        }

        [HttpPost("/api/[controller]/list")]
        public virtual async Task<IEnumerable<TOutputDto>> GetListByIds(IEnumerable<TId> ids)
        {
            IList<object> list = await _repository.GetByIdsAsync(ids.ToArray());
            return _mapper.To<TOutputDto>(list);
        }
    }

    public interface IAspireForm
    {
        Dictionary<string, object> GetFormRule(PropertyInfo property);
    }

    public interface IAspireTable
    {
        Dictionary<string, object> GetTableRule(PropertyInfo property);
    }
}
