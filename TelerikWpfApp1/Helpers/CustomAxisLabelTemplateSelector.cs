using System.Windows;
using System.Windows.Controls;
using Telerik.Charting;


namespace TelerikWpfApp1.Helpers
{
    public class CustomAxisLabelTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var dp = (AxisLabelModel)item;
           
            return base.SelectTemplate(item, container);
        }
    }
}
