using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Vending.Model;

namespace Vending.Client.Main
{
    public class MainViewVM : BindableBase
    {
        public MainViewVM()
        {
            _user = _manager.User;
            _automata = _manager.Automata;
            _user.PropertyChanged += (s, a) => { RaisePropertyChanged(nameof(UserSumm)); };
            _automata.PropertyChanged += (s, a) => { RaisePropertyChanged(nameof(Credit)); };

            //Кошелек пользователя
            UserWallet = new ObservableCollection<MoneyVM>(_user.UserWallet.Select(ms => new MoneyVM(ms)));
            Watch(_user.UserWallet, UserWallet, um => um.MoneyStack);
            //покупки пользователя
            UserBuyings = new ObservableCollection<ProductVM>(_user.UserBuyings.Select(ub => new ProductVM(ub)));
            Watch(_user.UserBuyings, UserBuyings, ub => ub.ProductStack);
            
            //деньги автомата
            AutomataBank = new ObservableCollection<MoneyVM>(_automata.AutomataBank.Select(a => new MoneyVM(a, _manager)));
            Watch(_automata.AutomataBank, AutomataBank, a => a.MoneyStack);
            //товары автомата
            ProductsInAutomata = new ObservableCollection<ProductVM>(_automata.ProductsInAutomata.Select(ap => new ProductVM(ap, _manager)));
            Watch(_automata.ProductsInAutomata, ProductsInAutomata, p => p.ProductStack);

            GetChange = new DelegateCommand(() => _manager.GetChange());
        }
        public int UserSumm => _user.UserSumm;
        public ObservableCollection<MoneyVM> UserWallet { get; }
        public ObservableCollection<ProductVM> UserBuyings { get; }
        public DelegateCommand GetChange { get; }
        public int Credit => _automata.Credit;
        public ObservableCollection<MoneyVM> AutomataBank { get; }
        public ObservableCollection<ProductVM> ProductsInAutomata { get; }
        private User _user;
        private Automata _automata;
        private PurchaseManager _manager = new PurchaseManager();

        //функция синхронизации ReadOnly коллекции элементов модели и соответствующей коллекции VM, 
        //в конструкторы которых передается эти экземпляры модели, указываемые в делегате
        private static void Watch<T, T2> (ReadOnlyObservableCollection<T> collToWatch, ObservableCollection<T2> collToUpdate,
            Func<T2, object> modelProperty)
        {
            ((INotifyCollectionChanged) collToWatch).CollectionChanged += (s, a) =>
            {
                if (a.NewItems?.Count == 1) collToUpdate.Add((T2) Activator.CreateInstance(typeof(T2), (T)a.NewItems[0], null));
                if (a.OldItems?.Count == 1) collToUpdate.Remove(collToUpdate.First(mv => modelProperty(mv) == a.NewItems[0]));
            };
        }
    }
    public class ProductVM : BindableBase {
        public ProductStack ProductStack { get; }
        public ProductVM(ProductStack productStack, PurchaseManager manager = null)
        {
            ProductStack = productStack;
            productStack.PropertyChanged += (s, a) => { RaisePropertyChanged(nameof(Amount)); };

            if (manager != null)
                BuyCommand = new DelegateCommand(() => {
                    manager.BuyProduct(ProductStack.Product);
                });
        }
        public Visibility IsBuyVisible => BuyCommand == null ? Visibility.Collapsed : Visibility.Visible;
        public DelegateCommand BuyCommand { get; }
        public string Name => ProductStack.Product.Name;
        public string Price => $"({ProductStack.Product.Price} руб.)";
        public int Amount => ProductStack.Amount;
    }
    public class MoneyVM : BindableBase {
        public MoneyStack MoneyStack { get; }        

        public MoneyVM(MoneyStack moneyStack, PurchaseManager manager = null)
        {
            MoneyStack = moneyStack;
            moneyStack.PropertyChanged += (s, a) => { RaisePropertyChanged(nameof(Amount)); };
            
            if (manager != null)
                InsertCommand = new DelegateCommand(()=>{
                    manager.InsertMoney(MoneyStack.Banknote);
                });
        }
        public Visibility IsInsertVisible => InsertCommand == null ? Visibility.Collapsed : Visibility.Visible;
        public DelegateCommand InsertCommand { get; }
        public string Icon => MoneyStack.Banknote.IsCoin ? "..\\Images\\coin.jpg" : "..\\Images\\banknote.png";
        public string Name => MoneyStack.Banknote.Name;
        public int Amount => MoneyStack.Amount;        
    }
}
