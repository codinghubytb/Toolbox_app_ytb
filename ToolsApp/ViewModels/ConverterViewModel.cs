using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;

namespace ToolsApp.ViewModels
{
    public class ConverterViewModel : BaseViewModel
    {
        #region Porperties

        ConvertUnit[] allUnits = (ConvertUnit[])Enum.GetValues(typeof(ConvertUnit));
        UnitConvert unitConvert = new UnitConvert();
        Dictionary<Unit, string> unit = new Dictionary<Unit, string>();

        string enter;
        string result;

        string enterUnit;
        string enterParameter;

        string resultUnit;
        string resultParameter;

        string textConverter;

        public string TextConverter
        {
            get => textConverter;
            set => SetProperty(ref textConverter, value);
        }

        public string Enter
        {
            get => enter;
            set => SetProperty(ref enter, value);
        }
        public string EnterUnit
        {
            get => enterUnit;
            set => SetProperty(ref enterUnit, value);
        }
        public string EnterParameter
        {
            get => enterParameter;
            set => SetProperty(ref enterParameter, value);
        }
        public string Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }
        public string ResultUnit
        {
            get => resultUnit;
            set => SetProperty(ref resultUnit, value);
        }
        public string ResultParameter
        {
            get => resultParameter;
            set => SetProperty(ref resultParameter, value);
        }

        #endregion

        #region Command

        public Command<string> BtnNumberClick { get; set; }
        public Command<string> BtnEnterUnitClick { get; set; }
        public Command<string> BtnResultUnitClick { get; set; }
        public Command OpenConversionCommand { get; set; }

        #endregion

        #region Constructeur

        public ConverterViewModel()
        {
            BtnNumberClick = new Command<string>(OnBtnClicked);
            BtnEnterUnitClick = new Command<string>(OnBtnEnterUnit);
            BtnResultUnitClick = new Command<string>(OnBtnResultUnit);
            OpenConversionCommand = new Command(OnOpenConversion);
            ChangeConverter(allUnits[0].ToString());
        }

        #endregion

        #region Methods

        void ChangeConverter(string value)
        {
            TextConverter = value;
            unit = unitConvert.GetUnit((ConvertUnit)Enum.Parse(typeof(ConvertUnit), value));

            EnterUnit = unit.FirstOrDefault().Value;
            EnterParameter = unit.FirstOrDefault().Key.ToString();

            ResultUnit = unit.LastOrDefault().Value;
            ResultParameter = unit.LastOrDefault().Key.ToString();

            Enter = string.Empty;
            Result = string.Empty;
        }

        private async void OnOpenConversion(object obj)
        {
            string[] result = new string[allUnits.Length];

            for(int i=0; i< allUnits.Length; i++)
            {
                result[i] = allUnits[i].ToString();
            }
            var action = await Shell.Current.DisplayActionSheet("Conversion", null, null, result);

            if (!string.IsNullOrEmpty(action))
            {
                ChangeConverter(action);
            }
        }

        private async void OnBtnUnitSelected(string obj, bool isEnterUnit)
        {
            string[] result = unit.Values.ToArray();

            var selectedUnit = await Shell.Current.DisplayActionSheet("Unit", null, null, result);

            if (!string.IsNullOrEmpty(selectedUnit))
            {
                if (isEnterUnit)
                {
                    EnterUnit = selectedUnit;
                    EnterParameter = unit.FirstOrDefault(x => x.Value == selectedUnit).Key.ToString();
                }
                else
                {
                    ResultUnit = selectedUnit;
                    ResultParameter = unit.FirstOrDefault(x => x.Value == selectedUnit).Key.ToString();
                }
                string nb = Enter;
                Enter = string.Empty;
                OnBtnClicked(nb);
            }
        }

        private void OnBtnResultUnit(string obj)
        {
            OnBtnUnitSelected(obj, false);
        }

        private void OnBtnEnterUnit(string obj)
        {
            OnBtnUnitSelected(obj, true);
        }

        private void OnBtnClicked(string obj)
        {

            if (obj == "retour")
            {
                if(!string.IsNullOrEmpty(Enter))
                    Enter = Enter.Substring(0, Enter.Length - 1);
                else
                    return;
            }
            else
                Enter += obj;

            if (double.TryParse(Enter, out double inputValue))
            {
                try
                {
                    var inputUnit = Enum.Parse<Unit>(EnterParameter);
                    var resultUnit = Enum.Parse<Unit>(ResultParameter);
                    Result = unitConvert.GetResultConvert(inputValue, inputUnit, resultUnit).ToString();
                }
                catch
                {
                    Result = "Error";
                }
            }
            else
            {
                Result = "Error";
            }
        }


        #endregion
    }
}
