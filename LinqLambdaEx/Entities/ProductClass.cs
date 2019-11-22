using System.Globalization;

namespace LinqLambdaEx.Entities
{
    public class ProductClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public CategoryClass Category { get; set; }

        public override string ToString()
        {
            return Id
                + ", "
                + Name
                + ", "
                + Price.ToString("F2", CultureInfo.InvariantCulture)
                + ", "
                + Category.Name
                + ", "
                + Category.Tier;
        }
    }
}
