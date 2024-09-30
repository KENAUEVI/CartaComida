using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DragDrop), typeof(SeeInDetail))]
public abstract class Card : MonoBehaviour
{
    public static float cardScale = 1.2f;
    public static float cardScaleIncreased = 1.4f;
    public static float cardScaleBattling = 2.8f;
    public static float cardScaleBattlingIncreased = 3.2f;
    public static float cardScaleZoom = 6f;

    public static string dessertColorHex = "#D67FFFFF";
    public static string vegetableColorHex = "#54CC2CFF";
    public static string mealColorHex = "#F25C00FF";
    public static string spiceColorHex = "#00B8FFFF";

    public enum FoodmonTypes { Dessert, Vegetable, Meal };

    private Image backgroundImage;
    private GameObject foodmonStamp;

    private bool interactive;
    private bool grabbable;
    private bool battling;

    public bool Interactive { get => interactive; }
    public bool Grabbable { get => grabbable; }
    public bool Battling { get => battling; }

    protected virtual void Awake()
    {
        backgroundImage = GetComponent<Image>();
        foodmonStamp = transform.Find("Foodmon").gameObject;
        interactive = false;
        grabbable = false;
        battling = false;
    }

    public abstract string GetCardName();
    public abstract string GetCardExplanationText();

    public void CardIsInPlayerHandOrField()
    {
        interactive = true;
        grabbable = true;
        battling = false;
        backgroundImage.sprite = ChooseSpriteForCard();
        foodmonStamp.SetActive(true);
    }

    public void CardIsInOpponentsHandOrField()
    {
        interactive = false;
        grabbable = false;
        battling = false;
        backgroundImage.sprite = SpriteLoader.CardBackSprite;
        foodmonStamp.SetActive(false);
    }

    public void ShowCardForBattle()
    {
        interactive = true;
        grabbable = false;
        battling = true;
        backgroundImage.sprite = ChooseSpriteForCard();
        foodmonStamp.SetActive(true);
    }

    private Sprite ChooseSpriteForCard()
    {
        if (this is Spice)
        {
            return SpriteLoader.SpiceBackgroundSprite;
        }
        else if (this is Monster)
        {
            switch (((Monster)this).TypeOfFoodmon)
            {
                case FoodmonTypes.Dessert:
                    return SpriteLoader.DessertBackgroundSprite;

                case FoodmonTypes.Vegetable:
                    return SpriteLoader.VegetableBackgroundSprite;

                case FoodmonTypes.Meal:
                    return SpriteLoader.MealBackgroundSprite;

                default:
                    Debug.Log("Tipo inesperado!!");
                    return null;
            }
        }
        else
        {
            Debug.Log("Tipo inesperado!!");
            return null;
        }
    }

    protected string NameOfType(FoodmonTypes? type)
    {
        switch (type)
        {
            case FoodmonTypes.Dessert:
                return "SOBREMESA";

            case FoodmonTypes.Vegetable:
                return "HORTALIÇA";

            case FoodmonTypes.Meal:
                return "SALGADO";

            default:
                Debug.Log("Tipo inesperado!!");
                return null;
        }
    }

    protected string TextColoredAsSpice(string text)
    {
        return "<color=" + spiceColorHex + ">" + text + "</color>";
    }

    protected string TextColoredByType(string text, FoodmonTypes? type)
    {
        switch (type)
        {
            case FoodmonTypes.Dessert:
                return "<color=" + dessertColorHex + ">" + text + "</color>";

            case FoodmonTypes.Vegetable:
                return "<color=" + vegetableColorHex + ">" + text + "</color>";

            case FoodmonTypes.Meal:
                return "<color=" + mealColorHex + ">" + text + "</color>";

            default:
                Debug.Log("Tipo inesperado!!");
                return null;
        }
    }
}
