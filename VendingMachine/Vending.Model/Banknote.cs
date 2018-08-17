using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace Vending.Model
{
    //структура, а не класс, чтобы сравнение была сразу по значению, а не по ссылке
    public struct Banknote {
        //представим, что список пришел из базы данных 
        public static readonly IReadOnlyList<Banknote> Banknotes = new[] {
                new Banknote("Рубль", 1, true),
                new Banknote("Два рубля", 2, true),
                new Banknote("Пять рублей", 5, true),
                new Banknote("Десять рублей", 10, false),
                new Banknote("Пятьдесят рублей", 50, false),
                new Banknote("Сто рублей", 100, false),
            };
        private Banknote(string name, int nominal, bool isCoin) {
            Name = name;
            Nominal = nominal;
            IsCoin = isCoin;  //монета ли это. Нужно для красоты
        }
        public string Name { get; }
        public int Nominal { get; }
        public bool IsCoin { get; }
    }
}
