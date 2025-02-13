using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using OllamaSharp;

namespace wpfOllama
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void askQuestion_Click(object sender, RoutedEventArgs e)
        {
            await InitModel(question.Text);
        }

        private async Task InitModel(string message)
        {
            try
            {
                var uri = new Uri("http://localhost:11434");
                var ollama = new OllamaApiClient(uri);
                ollama.SelectedModel = "llama2";

                var chat = new Chat(ollama);
                string systemMessage = "Ты специалист наводного транспорта всяких лодок и судов ";

                if (string.IsNullOrWhiteSpace(message))
                {
                    MessageBox.Show("Введите вопрос!");
                    return;
                }

                List<string> answer = new List<string>();

                await foreach (var answerToken in chat.SendAsync($"{systemMessage} {message}"))
                {
                    answer.Add(answerToken.ToString());
                    
                    Dispatcher.Invoke(() => Answer.Text = string.Join("", answer.ToArray()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async void askQuestion_Click_1(object sender, RoutedEventArgs e)
        {
            await InitModel(question.Text);

        }
    }
}