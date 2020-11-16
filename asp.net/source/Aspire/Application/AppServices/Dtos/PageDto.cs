namespace Aspire.Application.AppServices.Dtos
{
    public class PageDto<TOutputDto>
    {
        public int Total { get; set; }

        public TOutputDto[] Data { get; set; }
    }
}
