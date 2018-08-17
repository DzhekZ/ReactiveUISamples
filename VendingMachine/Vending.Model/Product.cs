using System.Collections.Generic;

namespace Vending.Model
{
    public class Product
    {
        public static IReadOnlyList<Product> Products = new List<Product>()
        {
            new Product("����",12),
            new Product("���� ��������", 25),
            new Product("���",6),
            new Product("�����",23),
            new Product("��������",19),
            new Product("�����",670),
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