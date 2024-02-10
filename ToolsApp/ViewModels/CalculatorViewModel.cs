using CommunityToolkit.Maui.Alerts;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsApp.ViewModels
{
    public class CalculatorViewModel : BaseViewModel
    {
        #region Properties

        private Grid _grid;
        private string _lblEnter;
        private string _lblResult;
        char[,] characters;

        public string LblEnter
        {
            get => _lblEnter;
            set => SetProperty(ref _lblEnter, value);
        }

        public string LblResult
        {
            get => _lblResult;
            set => SetProperty(ref _lblResult, value);
        }

        public Grid GridElement
        {
            get => _grid;
            set => SetProperty(ref _grid, value);
        }

        #endregion

        #region Command

        public Command ClearCommand { get; }
        public Command RemoveOneCharacterCommand { get; }

        #endregion

        #region Constructeur

        public CalculatorViewModel(Grid grid)
        {
            GridElement = grid;
            characters = new char[,]
            {
                    { 'C', 'R', '(', ')' },
                    { '7', '8', '9', '/' },
                    { '4', '5', '6', '*' },
                    { '1', '2', '3', '+' },
                    { '0', '.', '=', '-' }
            };
            ClearCommand = new Command(OnClear);
            RemoveOneCharacterCommand = new Command(OnRemoveOneCharacter);
            Initialize();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            try
            {
                RowDefinitionCollection rowDefinitions = new RowDefinitionCollection();
                ColumnDefinitionCollection columnDefinitions = new ColumnDefinitionCollection();

                for(int i=0; i < characters.GetLength(0); i++)
                {
                    rowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
                }

                for(int i=0; i<characters.GetLength(1); i++)
                {
                    columnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                }

                GridElement.RowDefinitions = rowDefinitions;
                GridElement.ColumnDefinitions = columnDefinitions;

                for (int i = 0; i < rowDefinitions.Count; i++)
                {
                    for (int j = 0; j < columnDefinitions.Count; j++)
                    {
                        Button button = new Button
                        {
                            Text = characters[i, j].ToString(),
                            FontSize = 25,
                            FontAttributes = FontAttributes.Bold,
                            CornerRadius = 0
                        };
                        button.SetAppThemeColor(Button.BackgroundColorProperty, Colors.White, Colors.Black);
                        button.SetAppThemeColor(Button.TextColorProperty, Colors.Black, Colors.White);

                        if (characters[i, j].Equals('='))
                        {
                            button.BackgroundColor = Colors.Orange;
                            button.TextColor = Colors.White;
                        }

                        button.Command = new Command<Button>(OnClickBtn);
                        button.CommandParameter = button;

                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);

                        GridElement.Children.Add(button);
                    }
                }
            }
            finally
            {
            }
        }

        void OnClickBtn(Button button)
        {
            if (button.Text.Equals("="))
                LblResult = $"= {Evaluate(LblEnter)}";
            else if (button.Text.Equals("C"))
                OnClear();
            else if (button.Text.Equals("R"))
                OnRemoveOneCharacter();
            else
            {
                LblEnter += button.Text;
            }
        }

        private string Evaluate(string expression)
        {
            try
            {
                return Convert.ToDouble(new System.Data.DataTable().Compute(expression, string.Empty)).ToString();
            }
            catch
            {
                return "Error";
            }

        }

        private void OnClear()
        {
            LblEnter = string.Empty;
            LblResult = string.Empty;
        }
        private void OnRemoveOneCharacter()
        {
            if (!string.IsNullOrEmpty(LblEnter))
            {
                LblEnter = LblEnter.Remove(LblEnter.Length - 1);
                if (!string.IsNullOrEmpty(LblEnter))
                    LblResult = $"= {Evaluate(LblEnter)}";
                else
                    LblResult = "= 0";
            }
        }

        #endregion
    }
}
