namespace Aspire.Application.AppServices.Dtos
{
    public class InputDto : InputDto<long>
    {
    }

    public class InputDto<TId> : UpdateDto<TId>
    {
    }
}
