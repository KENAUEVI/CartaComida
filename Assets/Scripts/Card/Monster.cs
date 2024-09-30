using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Monster : Card
{
    [SerializeField] private string foodmonName;
    [SerializeField] private FoodmonTypes originalTypeOfFoodmon;
    [SerializeField] private int originalPower;

    private FoodmonTypes typeOfFoodmon;
    private int power;

    public FoodmonTypes TypeOfFoodmon { get => typeOfFoodmon; set => typeOfFoodmon = value; }
    public int Power { get => power; set => power = value; }

    protected override void Awake()
    {
        base.Awake();

        typeOfFoodmon = originalTypeOfFoodmon;
        power = originalPower;
    }

    public override string GetCardName()
    {
        return TextColoredByType(foodmonName, typeOfFoodmon);
    }

    public override string GetCardExplanationText()
    {
        string explanationText = "";

        explanationText += "Uma estranha criatura alimentícia do " + TypeExplanation() + "\n\n";
        explanationText += PowerExplanation() + "\n";

        return explanationText;
    }

    private string TypeExplanation()
    {
        if (typeOfFoodmon == originalTypeOfFoodmon)
        {
            return "tipo " + TextColoredByType(NameOfType(typeOfFoodmon), typeOfFoodmon);
        }
        else
        {
            return "tipo " + TextColoredByType(NameOfType(typeOfFoodmon), typeOfFoodmon) + " ( " + TextColoredByType(NameOfType(originalTypeOfFoodmon), originalTypeOfFoodmon) + " )";
        }
    }

    private string PowerExplanation()
    {
        if (power == originalPower)
        {
             return "PODER: " + TextColoredByType(power.ToString(), typeOfFoodmon);
        }
        else
        {
            return "PODER: " + TextColoredByType(power.ToString(), typeOfFoodmon) + " ( " + TextColoredByType(originalPower.ToString(), typeOfFoodmon) + " )";
        }
    }
}
