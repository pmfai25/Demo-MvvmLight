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
    public class DefinePonter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //ListBoxItem a = new ListBoxItem();
            var PointerEvent = (PointerRoutedEventArgs)value;
            var Uielement = (FrameworkElement)parameter;
            var MousePoint = PointerEvent.GetCurrentPoint(Uielement);
            //return PointerEvent;
            return new Point(MousePoint.Position.X,MousePoint.Position.Y);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
