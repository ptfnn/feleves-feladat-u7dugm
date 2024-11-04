using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static feleves_feladat.Shop;

namespace feleves_feladat
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Shop> _items;
        private ObservableCollection<Shop> _order;
        private Shop _selectedItem;
        private Shop _selectedOrderItem;
        private bool _isFilterEnabled;
        private Shop.ShopType? _selectedType;
        public List<ShopType> AvailableTypes { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _items = new ObservableCollection<Shop>
            {
                new Shop("Sampon", ShopType.Hygene, 990),
                new Shop("Coca-Cola", ShopType.Drinks, 690),
                new Shop("Uno", ShopType.Games, 2190),
                new Shop("Almalé", ShopType.Drinks, 490),
                new Shop("Monopoly", ShopType.Games, 12890),
                new Shop("Kenyér", ShopType.Food, 790),
                new Shop("Szappan", ShopType.Hygene, 890),
                new Shop("Virsli", ShopType.Food, 390),
                new Shop("Bor", ShopType.Drinks, 1190),
                new Shop("Heads Up", ShopType.Games, 4890),
                new Shop("Burgonya (kg)", ShopType.Food, 290),
                new Shop("WC papír", ShopType.Hygene, 1690)
            };

            _order = new ObservableCollection<Shop>();

            _isFilterEnabled = false;
            _selectedType = null;

            AvailableTypes = Enum.GetValues(typeof(Shop.ShopType)).Cast<Shop.ShopType>().ToList();

            AddToOrderCommand = new RelayCommand(AddToOrder, CanAddToOrder);
            RemoveFromOrderCommand = new RelayCommand(RemoveFromOrder, CanRemoveFromOrder);
        }

        public ObservableCollection<Shop> Items => _items;

        public ObservableCollection<Shop> FilteredItems
        {
            get
            {
                if (_isFilterEnabled && _selectedType.HasValue)
                {
                    return new ObservableCollection<Shop>(_items.Where(f => f.Type == _selectedType.Value));
                }
                return _items;
            }
        }

        public ObservableCollection<Shop> Order => _order;

        public Shop SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                (AddToOrderCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }

        public Shop SelectedOrderItem
        {
            get => _selectedOrderItem;
            set
            {
                _selectedOrderItem = value;
                OnPropertyChanged(nameof(SelectedOrderItem));
                (RemoveFromOrderCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }

        public bool IsFilterEnabled
        {
            get => _isFilterEnabled;
            set
            {
                _isFilterEnabled = value;
                OnPropertyChanged(nameof(IsFilterEnabled));
                OnPropertyChanged(nameof(FilteredItems));
            }
        }

        public Shop.ShopType? SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
                OnPropertyChanged(nameof(FilteredItems));
            }
        }

        public int TotalPrice => _order.Sum(f => f.Price);

        public ICommand AddToOrderCommand { get; }
        public ICommand RemoveFromOrderCommand { get; }

        private void AddToOrder()
        {
            if (_selectedItem != null)
            {
                _order.Add(_selectedItem);
                OnPropertyChanged(nameof(Order));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private void FoodListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedItem != null)
            {
                ItemEditorWindow editor = new ItemEditorWindow(SelectedItem);
                if (editor.ShowDialog() == true)
                {
                    OnPropertyChanged(nameof(FilteredItems));
                }
            }
        }

        private void OrderListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedOrderItem != null)
            {
                ItemEditorWindow editorWindow = new ItemEditorWindow(SelectedOrderItem);
                editorWindow.ShowDialog();

                OnPropertyChanged(nameof(Order));
            }
        }

        private bool CanAddToOrder()
        {
            return _selectedItem != null;
        }

        private void RemoveFromOrder()
        {
            if (_selectedOrderItem != null)
            {
                _order.Remove(_selectedOrderItem);
                OnPropertyChanged(nameof(Order));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private bool CanRemoveFromOrder()
        {
            return _selectedOrderItem != null;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenPaymentWindow(object sender, RoutedEventArgs e)
        {
            if (_selectedItem != null)
            {
                PaymentWindow paymentWindow = new PaymentWindow();
                paymentWindow.ShowDialog();
            }
        }
    }
}