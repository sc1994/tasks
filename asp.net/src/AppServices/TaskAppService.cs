using Aspire.Application.AppServices;
using Aspire.Application.AppServices.Dtos;
using Aspire.Domain.Repositories;
using Aspire.Map;

using AutoMapper;

using TaskService.Entities;

namespace TaskService.AppServices
{
    public class TaskAppService : CrudAppService<TaskEntity, TaskOutputDto, long, TaskInputDto>
    {
        public TaskAppService(IRepository<TaskEntity, long> repository, IAspireMapper mapper) : base(repository, mapper)
        {
        }
    }

    public class TaskInputDto : InputDto
    {
    }

    public class TaskOutputDto : OutputDto
    {
    }

    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskOutputDto>();
            CreateMap<TaskInputDto, TaskEntity>();
        }
    }
}
