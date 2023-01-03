using System;
using System.IO;

namespace Kalkulator;

public partial class MainPage : ContentPage
{
    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "history.txt");
    public MainPage()
    {
        InitializeComponent();
        OnClear(this, null);
    }

    List<string> history = new List<string>();
    string currentEntry = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber = 0, secondNumber = 0;
    string decimalFormat = "N0";
    string textNumber1 = "";
    string textNumber2 = "";


    void OnSelectNumber(object sender, EventArgs e)
    {

        Button button = (Button)sender;
        string pressed = button.Text;

        currentEntry += pressed;

        if ((this.resultText.Text == "0" && pressed == "0")
            || (currentEntry.Length <= 1 && pressed != "0")
            || currentState < 0)
        {
            this.resultText.Text = "";
            if (currentState < 0)
                currentState *= -1;
        }

        if (pressed == "," && decimalFormat != "N2")
        {
            decimalFormat = "N2";
        }
            
        this.resultText.Text += pressed;
    }

    void OnSelectOperator(object sender, EventArgs e)
    {
        LockNumberValue(resultText.Text);

        currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        mathOperator = pressed;

        this.CurrentCalculation.Text = $"{firstNumber} {mathOperator}";
    }

    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }

            currentEntry = string.Empty;
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentState = 1;
        decimalFormat = "N0";
        this.resultText.Text = "0";
        currentEntry = string.Empty;
        this.CurrentCalculation.Text = "";
    }

    void OnCalculate(object sender, EventArgs e)
    {
        if (currentState == 2)
        {
            if (secondNumber == 0)
                LockNumberValue(resultText.Text);

            double result = Calculator.Calculate(firstNumber, secondNumber, mathOperator);

            string text = $"{firstNumber}{mathOperator}{secondNumber}={result} \n";

            this.CurrentCalculation.Text = $"{firstNumber} {mathOperator} {secondNumber}";

            File.AppendAllText(_fileName, text);

            this.resultText.Text = result.ToTrimmedString(decimalFormat);
            firstNumber = result;
            secondNumber = 0;
            currentState = 1;
            currentEntry = string.Empty;
        }
    }

}