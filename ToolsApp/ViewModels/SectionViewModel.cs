using System;
using ToolsApp.Models;

namespace ToolsApp.ViewModels
{
    public class SectionViewModel
    {
        public Command TappedCommand { get; }
        public ICollection<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category("Calculator", "icon_calculator.svg", "calculator"),
                new Category("Compass", "icon_compass.svg", "compass"),
                new Category("Converter", "icon_convert.svg", "converter"),
                new Category("Text To Speech", "icon_text_to_speech.svg", "textToSpeech"),
                new Category("Speech To Text", "icon_speech_to_text.svg", "speechToText")
            };
        }
        public SectionViewModel()
        {
            TappedCommand = new Command(OnTapped);
            this.Sections = GetCategories();
        }

        private async void OnTapped(object obj)
        {
            string path = (string)obj;
            await Shell.Current.GoToAsync(path);
        }

        public ICollection<Category> Sections { get; set; }
    }
}

