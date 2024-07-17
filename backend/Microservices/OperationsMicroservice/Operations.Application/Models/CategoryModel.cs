namespace Operations.Application.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public CategoryTypeModel? CategoryType { get; set; }
    }
}
