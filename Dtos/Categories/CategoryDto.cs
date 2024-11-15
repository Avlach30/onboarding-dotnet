namespace onboarding_dotnet.Dtos.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}