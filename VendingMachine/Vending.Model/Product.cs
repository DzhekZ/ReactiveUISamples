using System.Collections.Generic;

namespace Vending.Model
{
    public class Product
    {
        public static IReadOnlyList<Product> Products = new List<Product>()
        {
            new Product("Кофе",12),
            new Product("Кофе подороже", 25),
            new Product("Чай",6),
            new Product("Чипсы",23),
            new Product("Батончик",19),
            new Product("Нечто",670),
        };
        private Product(string name, int price)
        {
            Name = name;
            Price = price;
        }
        public string Name { get; }
        public int Price { get; }
    }
}