using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        double defaultValue = Double.MinValue;
        double firstNumber;
        double secondNumber;
        double result;
        string operation = "";
        bool isCleared = false;
        bool isEvaluated = false;
        public Form1()
        {
            InitializeComponent();
            firstNumber = defaultValue;
            secondNumber = defaultValue;
            result = defaultValue;
        }

        public void Handle_DigitClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int digit = int.Parse(btn.Text);
            bool flag = true;
            if (operation == "")
            {
                if (digit.ToString() == textBoxResult.Text)
                {
                    textBoxResult.Text += digit.ToString();
                    flag = false;
                }
                else if (textBoxResult.Text == "0" || isEvaluated)
                {
                    if (!textBoxResult.Text.Contains(","))
                    {
                        textBoxResult.Clear();
                    }
                    
                }
                if (flag)
                {
                    textBoxResult.Text += digit.ToString();
                }
                firstNumber = double.Parse(textBoxResult.Text);
                labelResult.Text = "";
                isEvaluated = false;
            }
            else
            {
                if (textBoxResult.Text[0] == '0' && textBoxResult.Text[1] != ',')
                {
                    textBoxResult.Clear();
                    isCleared = true;
                }
                if (secondNumber == firstNumber && !isCleared)
                {
                    textBoxResult.Clear();
                    isCleared = true;
                }
                textBoxResult.Text += digit.ToString();
                secondNumber = double.Parse(textBoxResult.Text);
                if (isEvaluated)
                {
                    textBoxResult.Text = digit.ToString();
                    firstNumber = result;
                    secondNumber = double.Parse(textBoxResult.Text);
                    isEvaluated = false;
                }
            }
        }
        public void Handle_OperationClick(object sender, EventArgs e)
        {
            if (firstNumber == defaultValue)
            {
                firstNumber = 0;
            }
            Button btn = (Button)sender;
            operation = btn.Text;
            labelResult.Text = $"{firstNumber} {operation} ";
            if (textBoxResult.Text == "Cannot divide by 0")
            {
                secondNumber = defaultValue;
                operation = "";
                isCleared = false;
                isEvaluated = false;
                textBoxResult.Text = "0";
                labelResult.Text = "";
            }
            else
            {
                secondNumber = double.Parse(textBoxResult.Text);
            }
            
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            if (operation != "")
            {
                if (operation == "÷" && secondNumber == 0)
                {
                    labelResult.Text = $"{firstNumber} {operation} ";
                }
                else
                {
                    labelResult.Text = $"{firstNumber} {operation} {secondNumber} =";
                }
                
                isEvaluated = true;
                isCleared = false;
                switch (operation)
                {
                    case "+":
                        result = (firstNumber + secondNumber);
                        break;
                    case "-":
                        result = (firstNumber - secondNumber);
                        break;
                    case "×":
                        result = (firstNumber * secondNumber);
                        break;
                    case "÷":
                        if (secondNumber == 0)
                        {
                            textBoxResult.Text = "Cannot divide by 0";
                            operation = "";
                            firstNumber = defaultValue;
                            secondNumber = defaultValue;
                            result = defaultValue;
                        }
                        else
                        {
                            result = Math.Round(firstNumber / secondNumber, 4);
                        }
                        break;
                    default:
                        break;
                }
                if (result != defaultValue)
                {
                    textBoxResult.Text = result.ToString();
                    firstNumber = result;
                    operation = "";
                }
            }
        }
        private void buttonClearEntry_Click(object sender, EventArgs e)
        {
            if (textBoxResult.Text == "Cannot divide by 0")
            {
                buttonClearAll_Click(sender, e);
            }
            if (secondNumber != defaultValue && firstNumber != defaultValue)
            {
                
                if (isEvaluated)
                {
                    buttonClearAll_Click(sender, e);
                }
                else
                {
                    secondNumber = defaultValue;
                    textBoxResult.Text = "0";
                    isCleared = false;
                }
            }
            else if (secondNumber == defaultValue)
            {
                firstNumber = defaultValue;
                textBoxResult.Text = "0";
                isCleared = false;
            }
        }
        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            firstNumber = defaultValue;
            secondNumber = defaultValue;
            operation = "";
            isCleared = false;
            isEvaluated = false;
            textBoxResult.Text = "0";
            labelResult.Text = "";
        }
        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (textBoxResult.Text.Length >= 1)
            {
                if (textBoxResult.Text == "Cannot divide by 0")
                {
                    buttonClearAll_Click(sender, e);
                }
                if (double.Parse(textBoxResult.Text) >= -9 && double.Parse(textBoxResult.Text) < 0)
                {
                    textBoxResult.Text = "0";
                    firstNumber = defaultValue;
                    secondNumber = defaultValue;
                    operation = "";
                    isEvaluated = false;
                    labelResult.Text = "";
                }
                
                if (isEvaluated)
                {
                    labelResult.Text = "";
                    firstNumber = double.Parse(textBoxResult.Text);
                    secondNumber = defaultValue;
                    isEvaluated = false;
                }
                else
                {
                    if (textBoxResult.Text.Length == 1)
                    {
                        textBoxResult.Text = "0";
                        secondNumber = defaultValue;
                        isCleared = false;
                    }
                    else
                    {
                        textBoxResult.Text = textBoxResult.Text.Substring(0, textBoxResult.Text.Length - 1);
                    }
                }
            }
            else
            {
                textBoxResult.Text = textBoxResult.Text.Substring(0, textBoxResult.Text.Length - 1);
            }

            if (firstNumber != defaultValue && secondNumber == defaultValue && operation == "")
            {
                firstNumber = double.Parse(textBoxResult.Text);
            }
            else if (firstNumber != defaultValue && secondNumber != defaultValue)
            {
                secondNumber = double.Parse(textBoxResult.Text);
            }
        }

        private void buttonPeriod_Click(object sender, EventArgs e)
        {
            if (textBoxResult.Text == "Cannot divide by 0")
            {
                buttonClearAll_Click(sender, e);
            }
            if (!textBoxResult.Text.Contains(","))
            {
                if (operation != "" && firstNumber == secondNumber)
                {
                    textBoxResult.Text = "0,";
                    secondNumber = 0;
                }
                else
                {
                    textBoxResult.Text += ",";
                }
                
            }
            if (labelResult.Text != String.Empty && isEvaluated)
            {
                isEvaluated = false;
                if (operation != "" && firstNumber != defaultValue)
                {
                    textBoxResult.Text = "0,";
                    secondNumber = 0;
                }
                else
                {
                    labelResult.Text = "";
                }
               
            }
        }

        private void buttonNegativeSign_Click(object sender, EventArgs e)
        {
            if (textBoxResult.Text == "Cannot divide by 0")
            {
                buttonClearAll_Click(sender, e);
            }
            if (textBoxResult.Text != "0")
            {
                if (double.Parse(textBoxResult.Text) < 0)
                {
                    textBoxResult.Text = (Math.Abs(double.Parse(textBoxResult.Text))).ToString();
                }
                else
                {
                    textBoxResult.Text = "-" + (double.Parse(textBoxResult.Text)).ToString();
                }
                if (firstNumber != defaultValue && secondNumber == defaultValue && operation == "")
                {
                    firstNumber = double.Parse(textBoxResult.Text);
                }
                else if (firstNumber != defaultValue && secondNumber != defaultValue)
                {
                    secondNumber = double.Parse(textBoxResult.Text);
                }
                if (isEvaluated)
                {
                    firstNumber = double.Parse(textBoxResult.Text);
                    isEvaluated = false;
                }
            }
        }
    }
}
