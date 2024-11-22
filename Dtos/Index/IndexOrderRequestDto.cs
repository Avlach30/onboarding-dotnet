namespace onboarding_dotnet.Dtos.Index
{
    public class IndexOrderRequestDto: IndexRequestDto
    {
        public int TotalPriceMin { get; set; } = 0;
        public int TotalPriceMax { get; set; } = 0;
    }
}