using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using KingOfBurgers.Core;

namespace KingOfBurgers.App.Converters;

public class IngredientTypeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not IngredientType type)
            return Brushes.Gray;

        return type switch
        {
            IngredientType.Bun => new SolidColorBrush(Color.FromRgb(217, 155, 82)), // булочка
            IngredientType.Cheese => new SolidColorBrush(Color.FromRgb(250, 204, 21)), //сыр
            IngredientType.Tomato => new SolidColorBrush(Color.FromRgb(239, 68, 68)), // помидор
            IngredientType.Letuce => new SolidColorBrush(Color.FromRgb(74, 222, 128)), // зелёный салат
            IngredientType.Onion => new SolidColorBrush(Color.FromRgb(168, 85, 247)), // лук (фиолетовый)
            IngredientType.Pickle => new SolidColorBrush(Color.FromRgb(21, 128, 61)), // огурчик (тёмно-зелёный)
            IngredientType.Patty => new SolidColorBrush(Color.FromRgb(120, 63, 35)), // котлета
            _ => Brushes.Gray
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}