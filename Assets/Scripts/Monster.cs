using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Card
{
    private enum foodmonTypes { Dessert, Vegetable, Meal }

    [SerializeField] private foodmonTypes typeOfFoodmon;
    [SerializeField] private int power;

    override protected void Awake()
    {
        base.Awake();

        if(typeOfFoodmon == foodmonTypes.Dessert)
        {
            typeOfCard = cardTypes.dessert;
        }
        else if(typeOfFoodmon == foodmonTypes.Vegetable)
        {
            typeOfCard = cardTypes.vegetable;
        }
        else if(typeOfFoodmon == foodmonTypes.Meal)
        {
            typeOfCard = cardTypes.meal;
        }
        else
        {
            Debug.Log("Tipo inesperado!!");
        }
    }
}
