using System.Windows.Controls;
using System.Windows;
using ExamGenerator.ViewModels;

namespace ExamGenerator.Utilities
{
    public class OptionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RadioButtonTemplate { get; set; }
        public DataTemplate CheckBoxTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var mainVM = (MainWindowVM)Application.Current.MainWindow.DataContext;
            var question = mainVM.SelectedQuestion;
            if (question.IsMultiSelect)
            {
                return CheckBoxTemplate;
            }
            else
            {
                return RadioButtonTemplate;
            }
        }
    }
}



