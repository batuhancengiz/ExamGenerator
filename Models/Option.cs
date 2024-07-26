using ExamGenerator.ViewModels;

namespace ExamGenerator.Models
{
    public class Option : ViewModelBase
    {
        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        private string _optionText;

        public string OptionText
        {
            get => _optionText;
            set
            {
                _optionText = value;
                OnPropertyChanged(nameof(OptionText));
            }
        }

        private bool _isTrue;

        public bool IsTrue
        {
            get => _isTrue;
            set
            {
                _isTrue = value; 
                OnPropertyChanged(nameof(IsTrue));
            }
        }
    }
}
