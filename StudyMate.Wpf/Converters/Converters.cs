using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StudyMate.Wpf.Converters
{
    /// <summary>
    /// Converts null values to Visibility.Collapsed and non-null values to Visibility.Visible.
    /// Useful for hiding UI elements when bound data is null.
    /// 
    /// Conversion behavior:
    /// - null input -> Visibility.Collapsed (element hidden)
    /// - Non-null input -> Visibility.Visible (element shown)
    /// ConvertBack is not implemented (one-way converter).
    /// </summary>
    public class NullToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a null value to Visibility.Collapsed.
        /// </summary>
        /// <param name="value">The value to check for null.</param>
        /// <param name="targetType">The target type (must be Visibility).</param>
        /// <param name="parameter">Converter parameter (not used).</param>
        /// <param name="culture">Culture info (not used).</param>
        /// <returns>Visibility.Collapsed if value is null; otherwise Visibility.Visible.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// ConvertBack is not implemented. Throws NotImplementedException.
        /// </summary>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts integer count values to Visibility based on whether count is greater than zero.
    /// Shows elements when count is 0, hides them when count > 0.
    /// Commonly used to show "empty state" messages when collections have no items.
    /// 
    /// Conversion behavior:
    /// - count <= 0 -> Visibility.Visible
    /// - count > 0 -> Visibility.Collapsed
    /// ConvertBack is not implemented (one-way converter).
    /// </summary>
    public class CountToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts an integer count to Visibility.Collapsed if count > 0.
        /// </summary>
        /// <param name="value">The integer count value.</param>
        /// <param name="targetType">The target type (must be Visibility).</param>
        /// <param name="parameter">Converter parameter (not used).</param>
        /// <param name="culture">Culture info (not used).</param>
        /// <returns>Visibility.Collapsed if count > 0; otherwise Visibility.Visible.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
            {
                return count > 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Visible;
        }

        /// <summary>
        /// ConvertBack is not implemented. Throws NotImplementedException.
        /// </summary>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts boolean values to Visibility with optional inversion support.
    /// Supports a converter parameter "Inverted" to reverse the logic.
    /// 
    /// Standard conversion (no parameter):
    /// - true -> Visibility.Visible
    /// - false -> Visibility.Collapsed
    /// 
    /// Inverted conversion (parameter="Inverted"):
    /// - true -> Visibility.Collapsed
    /// - false -> Visibility.Visible
    /// 
    /// ConvertBack is not implemented (one-way converter).
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value to Visibility with optional inversion.
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The target type (must be Visibility).</param>
        /// <param name="parameter">Use "Inverted" to reverse visibility logic.</param>
        /// <param name="culture">Culture info (not used).</param>
        /// <returns>Visibility based on boolean value and optional inversion.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                if (parameter?.ToString() == "Inverted")
                {
                    return boolValue ? Visibility.Collapsed : Visibility.Visible;
                }
                
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        /// <summary>
        /// ConvertBack is not implemented. Throws NotImplementedException.
        /// </summary>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Multi-value converter that displays quiz result text based on submission and correctness states.
    /// Combines two boolean inputs (isSubmitted and isCorrect) to produce result feedback text.
    /// 
    /// Conversion logic:
    /// - !isSubmitted -> string.Empty (no result shown yet)
    /// - isSubmitted && isCorrect -> "Correct!"
    /// - isSubmitted && !isCorrect -> "Incorrect"
    /// 
    /// Used in quiz UI to display feedback after user selects an answer.
    /// ConvertBack is not implemented (one-way converter).
    /// </summary>
    public class ResultTextConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts multiple boolean values to result feedback text.
        /// </summary>
        /// <param name="values">Array where values[0]=isSubmitted (bool), values[1]=isCorrect (bool).</param>
        /// <param name="targetType">The target type (must be string).</param>
        /// <param name="parameter">Converter parameter (not used).</param>
        /// <param name="culture">Culture info (not used).</param>
        /// <returns>"Correct!", "Incorrect", or empty string based on input values.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 &&
                values[0] is bool isSubmitted &&
                values[1] is bool isCorrect)
            {
                if (!isSubmitted)
                {
                    return string.Empty;
                }

                return isCorrect ? "Correct!" : "Incorrect";
            }

            return string.Empty;
        }

        /// <summary>
        /// ConvertBack is not implemented. Throws NotImplementedException.
        /// </summary>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
