using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class NumberSystem : MonoBehaviour
{
    // display text
    public Text calcResult;
    float currentInput = 0, previousInput = 0, result = 0;
    string mathSymbol = "";
    // checks if user inputed a new number or not
    bool isNewNum = true;


    public void NumberButtonClick(string num)
    {
        // stores first letter of number (eg. 3)

        if (isNewNum == true)
        {
            calcResult.text = num;
            isNewNum = false;
        }
        // stores all letters after first number (eg . 345 - three is the first letter from before 45 gets added here)

        else
        {
            calcResult.text += num;
        }
    }

    public void HoldsPreviousNum(string opreator)
    {
        // stores the first number
        if (opreator == "+")
        {
            previousInput = float.Parse(calcResult.text);
            mathSymbol = "+";
            calcResult.text = "";
        }
        if (opreator == "*")
        {
            previousInput = float.Parse(calcResult.text);
            mathSymbol = "*";
            calcResult.text = "";
        }
        if (opreator == "/")
        {
            previousInput = float.Parse(calcResult.text);
            mathSymbol = "/";
            calcResult.text = "";
        }
        if (opreator == "-")
        {
            previousInput = float.Parse(calcResult.text);
            mathSymbol = "-";
            calcResult.text = "";
        }
        if (opreator == "C")
        {
            previousInput = 0;
            currentInput = 0;
            calcResult.text = "";
            isNewNum = true;
        }
    }

    public void EqualsButtonClick(string opreator)
    {
        if (opreator == "=")
        {
            currentInput = float.Parse(calcResult.text);
            // depneds on which opreator was pressed before does calculation

            if (mathSymbol == "+")
            {
                result = previousInput + currentInput;
                calcResult.text = result.ToString();
            }
            if (mathSymbol == "/" && currentInput != 0)
            {
                result = previousInput / currentInput;
                calcResult.text = result.ToString();
            }
            else if (currentInput == 0)
            {
                calcResult.text = "UNDEFINED";
            }
            if (mathSymbol == "-")
            {
                result = previousInput - currentInput;
                calcResult.text = result.ToString();
            }
            if (mathSymbol == "*")
            {
                result = previousInput * currentInput;
                calcResult.text = result.ToString();
            }
        }


    }
}







