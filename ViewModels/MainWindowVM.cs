using ExamGenerator.Models;
using ExamGenerator.Utilities;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace ExamGenerator.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private bool _isImported = false;
        private int _questionNumber = 1;
        private Deserializer _deserializer;
        private ObservableCollection<Question> _questions;
        private Question _selectedQuestion;
        private bool _isFinished;
        private int _correctCounter;
        private int _wrongCounter;

        public ObservableCollection<Question> Questions
        {
            get => _questions;
            set
            {
                _questions = value;
                OnPropertyChanged(nameof(Questions));
            }
        }

        public Question SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged(nameof(SelectedQuestion));
            }
        }

        public int QuestionNumber
        {
            get => _questionNumber;
            set
            {
                _questionNumber = value;
                OnPropertyChanged(nameof(QuestionNumber));
            }
        }

        public bool IsImported
        {
            get => _isImported;
            set
            {
                _isImported = value;
                OnPropertyChanged(nameof(IsImported));
            }
        }

        public bool IsFinished
        {
            get => _isFinished;
            set
            {
                _isFinished = value;
                OnPropertyChanged(nameof(IsFinished));
            }
        }

        public int CorrectCounter
        {
            get => _correctCounter;
            set
            {
                _correctCounter = value;
                OnPropertyChanged(nameof(CorrectCounter));
            }
        }

        public int WrongCounter
        {
            get => _wrongCounter;
            set
            {
                _wrongCounter = value;
                OnPropertyChanged(nameof(WrongCounter));
            }
        }

        public ICommand ImportButton { get; set; }
        public ICommand BackButton { get; set; }
        public ICommand NextButton { get; set; }
        public ICommand FinishButton { get; set; }
        public ICommand QuestionButton { get; set; }
        public ICommand HideAnswersButton { get; set; }

        public MainWindowVM()
        {
            _deserializer = new Deserializer();
            FinishButton = new RelayCommand(OnFinish, (o) => { return true; });
            QuestionButton = new RelayCommand(OnQuestionButton, (o) => { return true; });
            ImportButton = new RelayCommand(OnImport, (o) => { return true; });
            HideAnswersButton = new RelayCommand(OnHide, (o) => { return true; });
            BackButton = new RelayCommand(OnBack, (o) => { return SelectedQuestion != null && SelectedQuestion.Id - 1 != 0; });
            NextButton = new RelayCommand(OnNext, (o) => { return SelectedQuestion != null && Questions != null && SelectedQuestion.Id - 1 != Questions.Count - 1; });
        }

        private void OnHide(object obj)
        {
            IsFinished = false;

            foreach (var question in Questions)
                question.IsTrue = false;
        }

        private void OnQuestionButton(object obj)
        {
            if (obj is Question question)
            {
                SelectedQuestion = question;
                QuestionNumber = Questions.IndexOf(question) + 1;
            }
        }

        private void OnFinish(object obj)
        {
            CorrectCounter = 0;
            WrongCounter = 0;

            IsFinished = true;

            foreach (var question in Questions)
            {
                if (question.CheckAnswer())
                {
                    CorrectCounter++;
                }
                else
                {
                    WrongCounter++;
                }
            }
        }

        private void OnNext(object obj)
        {
            QuestionNumber++;
            var indexWillBeSelected = Questions.IndexOf(SelectedQuestion) + 1;
            SelectedQuestion = Questions[indexWillBeSelected];
        }

        private void OnBack(object obj)
        {
            QuestionNumber--;
            var indexWillBeSelected = Questions.IndexOf(SelectedQuestion) - 1;
            SelectedQuestion = Questions[indexWillBeSelected];
        }

        private void OnImport(object obj)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Md files (*.md)|*.md|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;

                if (File.Exists(filePath))
                    Questions = _deserializer.DeserializeQuestions(filePath);

                if (Questions != null && Questions.Count > 0)
                {
                    Questions.Shuffle();
                    SelectedQuestion = Questions.FirstOrDefault();
                    QuestionNumber = 1;
                    IsImported = true;
                    IsFinished = false;
                    SetId();
                }
            }
        }

        private void SetId()
        {
            foreach (var question in Questions)
            {
                question.Id = Questions.IndexOf(question) + 1;
            }
        }
    }
}
