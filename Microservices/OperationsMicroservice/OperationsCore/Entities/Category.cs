namespace Operations.Core.Entities
{
    public class Category :BaseEntity
    {    
        public int CategoryTypeId { get; set; }
        public CategoryType CategoryType { get; set; }
    }
}
