using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using static feleves_feladat.Shop;

namespace feleves_feladat
{
    public class ShopTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Shop.ShopType shopType)
            {
                return shopType switch
                {
                    Shop.ShopType.Hygene => Brushes.Aqua,
                    Shop.ShopType.Food => Brushes.Fuchsia,
                    Shop.ShopType.Games => Brushes.DarkSalmon,
                    Shop.ShopType.Drinks => Brushes.Khaki,
                    _ => Brushes.Transparent,
                };
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
