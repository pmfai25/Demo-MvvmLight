using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Demo_MvvmLight.Converters
{
    public class RightTapEvent:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var e = (RightTappedRoutedEventArgs)value;
            var sender = parameter;
           
            Tuple<object, RightTappedRoutedEventArgs> tuple = new Tuple<object, RightTappedRoutedEventArgs>(sender, e);
            
            return  tuple;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
