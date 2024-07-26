using ExamGenerator.ViewModels;

namespace ExamGenerator.Models
{
    public class Question : ViewModelBase
    {
        public bool IsMultiSelect { get; set; }
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<Option> Options { get; set; }
        public List<int> AnswerIndexes { get; set; }

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

        public bool CheckAnswer()
        {
            bool isCorrect = true;

            var selectionCounter = 0;
            foreach (var option in Options)
            {
                if (option.IsChecked)
                {
                    selectionCounter++;
                }
            }

            if (selectionCounter != AnswerIndexes.Count)
            {
                return false;
            }

            foreach (var index in AnswerIndexes)
            {
                if (!Options[index].IsChecked)
                {
                    isCorrect = false;
                    break;
                }
            }

            IsTrue = isCorrect;

            return isCorrect;
        }
    }
}
