using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class SpriteLoader
{
    private static Sprite cardBackSprite = null;
    private static Sprite dessertBackgroundSprite = null;
    private static Sprite vegetableBackgroundSprite = null;
    private static Sprite mealBackgroundSprite = null;
    private static Sprite spiceBackgroundSprite = null;

    public static Sprite CardBackSprite { get => cardBackSprite; }
    public static Sprite DessertBackgroundSprite { get => dessertBackgroundSprite; }
    public static Sprite VegetableBackgroundSprite { get => vegetableBackgroundSprite; }
    public static Sprite MealBackgroundSprite { get => mealBackgroundSprite; }
    public static Sprite SpiceBackgroundSprite { get => spiceBackgroundSprite; }

    private static bool hasLoadedCardBackgrounds = false;

    public static void LoadCardBackgrounds()
    {
        if (!hasLoadedCardBackgrounds)
        {
            hasLoadedCardBackgrounds = true;
            AsyncOperationHandle<SpriteArray> spriteHandle = Addressables.LoadAssetAsync<SpriteArray>("Assets/Sprites/Card/CardAppearance.asset");
            spriteHandle.Completed += LoadCardBackgroundsWhenReady;
        }
    }

    private static void LoadCardBackgroundsWhenReady(AsyncOperationHandle<SpriteArray> handleToCheck)
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
}
