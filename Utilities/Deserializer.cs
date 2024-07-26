using ExamGenerator.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace ExamGenerator.Utilities
{
    public class Deserializer
    {
        public ObservableCollection<Question> DeserializeQuestions(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            ObservableCollection<Question> questions = new();
            Question? currentQuestion = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("###"))
                {
                    if (currentQuestion != null)
                    {
                        currentQuestion.IsMultiSelect = currentQuestion.AnswerIndexes.Count > 1;

                        if (currentQuestion.Options.Count != 0)
                            questions.Add(currentQuestion);
                    }
                    currentQuestion = new Question
                    {
                        QuestionText = line.Substring(4).Trim(),
                        Options = new List<Option>(),
                        AnswerIndexes = new List<int>(),
                    };
                }
                else if (line.StartsWith("- ["))
                {
                    bool isAnswer = line[3] == 'x';
                    string optionText = line.Substring(6).Trim();
                    var option = new Option
                    {
                        IsChecked = false,
                        OptionText = optionText,
                        IsTrue = isAnswer
                    };
                    currentQuestion?.Options.Add(option);
                    if (isAnswer)
                    {
                        currentQuestion?.AnswerIndexes.Add(currentQuestion.Options.Count - 1);
                    }
                }
            }

            if (currentQuestion != null)
            {
                currentQuestion.IsMultiSelect = currentQuestion.AnswerIndexes.Count > 1;

                if (currentQuestion.Options.Count != 0)
                    questions.Add(currentQuestion);
            }

            return questions;
        }
    }
}
