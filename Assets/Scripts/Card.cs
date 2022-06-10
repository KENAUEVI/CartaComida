using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[RequireComponent(typeof(DragDrop), typeof(SeeInDetail))]
public abstract class Card : MonoBehaviour
{
    private static Sprite cardBackSprite = null;
    private static Sprite dessertBackgroundSprite = null;
    private static Sprite vegetableBackgroundSprite = null;
    private static Sprite mealBackgroundSprite = null;
    private static Sprite spiceBackgroundSprite = null;
    private static bool hasDoneIt = false;

    private Image backgroundImage;
    private GameObject foodmonStamp;
    protected enum cardTypes { dessert, vegetable, meal, spice };
    protected cardTypes typeOfCard;

    private bool interactive;
    public bool Interactive { get => interactive; }

    virtual protected void Awake()
    {
        backgroundImage = GetComponent<Image>();
        foodmonStamp = transform.Find("Foodmon").gameObject;
        interactive = false;
    }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        if (cardBackSprite == null && !hasDoneIt)
        {
            hasDoneIt = true;
            AsyncOperationHandle<SpriteArray> spriteHandle = Addressables.LoadAssetAsync<SpriteArray>("Assets/Sprites/Card/CardAppearance.asset");
            spriteHandle.Completed += LoadSpritesWhenReady;
        }
    }

    private void LoadSpritesWhenReady(AsyncOperationHandle<SpriteArray> handleToCheck)
    {
        if (handleToCheck.Status == AsyncOperationStatus.Succeeded)
        {
            cardBackSprite = handleToCheck.Result.spriteArray[0];
            dessertBackgroundSprite = handleToCheck.Result.spriteArray[1];
            vegetableBackgroundSprite = handleToCheck.Result.spriteArray[2];
            mealBackgroundSprite = handleToCheck.Result.spriteArray[3];
            spiceBackgroundSprite = handleToCheck.Result.spriteArray[4];
        }
    }

    public void CardIsInPlayerHand()
    {
        interactive = true;
        backgroundImage.sprite = ChooseSpriteForCard();
        foodmonStamp.SetActive(true);
    }

    public void CardIsInOpponentsHand()
    {
        interactive = false;
        backgroundImage.sprite = cardBackSprite;
        foodmonStamp.SetActive(false);
    }

    public void ShowCardForBattle()
    {
        interactive = false;
        backgroundImage.sprite = ChooseSpriteForCard();
        foodmonStamp.SetActive(true);
    }

    private Sprite ChooseSpriteForCard()
    {
        if (typeOfCard == cardTypes.dessert)
        {
            return dessertBackgroundSprite;
        }
        else if (typeOfCard == cardTypes.vegetable)
        {
            return vegetableBackgroundSprite;
        }
        else if (typeOfCard == cardTypes.meal)
        {
            return mealBackgroundSprite;
        }
        else if (typeOfCard == cardTypes.spice)
        {
            return spiceBackgroundSprite;
        }
        else
        {
            return null;
        }
    }
}
