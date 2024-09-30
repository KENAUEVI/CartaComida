using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spice : Card
{
    [SerializeField] private string spiceName;
    [SerializeField] private int speed;
    [SerializeField] private List<SpiceEffect> effects;

    public int Speed { get => speed; }
    public List<SpiceEffect> Effects { get => effects; }

    protected override void Awake()
    {
        base.Awake();
    }

    public override string GetCardName()
    {
        return TextColoredAsSpice(spiceName);
    }

    public override string GetCardExplanationText()
    {
        string explanationText = "";

        if (effects.Count == 1)
        {
            explanationText += "Um tempero mágico que " + EffectExplanation(0) + "\n";
        }
        else
        {
            explanationText += "Um tempero mágico que:" + "\n";
            for (int i = 0; i < effects.Count; i++)
            {
                explanationText += "- " + EffectExplanation(i) + "\n";
            }
            explanationText += "em ordem" + "\n";
        }

        explanationText += SpeedExplanation() + "\n";

        return explanationText;
    }

    private string EffectExplanation(int effectIndex)
    {
        switch (effects[effectIndex].effectAbility)
        {
            case SpiceEffect.EffectAbilities.ChangePower:
                return ChangePowerExplanation(effectIndex);

            case SpiceEffect.EffectAbilities.ChangeType:
                return ChangeTypeExplanation(effectIndex);

            default:
                Debug.Log("Efeito inesperado!!");
                return null;
        }
    }

    private string ChangePowerExplanation(int effectIndex)
    {
        switch (effects[effectIndex].powerChangeFormat)
        {
            case SpiceEffect.PowerChangeFormats.Increase:
                return TextColoredAsSpice("aumenta o poder ") + TargetExplanation(effectIndex) + " em " + TextColoredAsSpice(effects[effectIndex].value.ToString());

            case SpiceEffect.PowerChangeFormats.Decrease:
                return TextColoredAsSpice("diminui o poder ") + TargetExplanation(effectIndex) + " em " + TextColoredAsSpice(effects[effectIndex].value.ToString());

            case SpiceEffect.PowerChangeFormats.Set:
                return TextColoredAsSpice("torna " + effects[effectIndex].value + " o poder ") + TargetExplanation(effectIndex);

            case SpiceEffect.PowerChangeFormats.Switcheroo:
                return TextColoredAsSpice("troca o poder ") + "da sua criatura com o da criatura do oponente";

            default:
                Debug.Log("Efeito inesperado!!");
                return null;
        }
    }

    private string ChangeTypeExplanation(int effectIndex)
    {
        switch (effects[effectIndex].typeChangeFormat)
        {
            case SpiceEffect.TypeChangeFormats.NextInOrder:
                return TextColoredAsSpice("muda o tipo ") + TargetExplanation(effectIndex) + " para o próximo tipo, segundo a ordem " + TextColoredByType(NameOfType(FoodmonTypes.Dessert), FoodmonTypes.Dessert) + "->" + TextColoredByType(NameOfType(FoodmonTypes.Vegetable), FoodmonTypes.Vegetable) + "->" + TextColoredByType(NameOfType(FoodmonTypes.Meal), FoodmonTypes.Meal);

            case SpiceEffect.TypeChangeFormats.PreviousInOrder:
                return TextColoredAsSpice("muda o tipo ") + TargetExplanation(effectIndex) + " para o tipo anterior, segundo a ordem " + TextColoredByType(NameOfType(FoodmonTypes.Dessert), FoodmonTypes.Dessert) + "->" + TextColoredByType(NameOfType(FoodmonTypes.Vegetable), FoodmonTypes.Vegetable) + "->" + TextColoredByType(NameOfType(FoodmonTypes.Meal), FoodmonTypes.Meal);

            case SpiceEffect.TypeChangeFormats.Set:
                return TextColoredAsSpice("muda o tipo ") + TargetExplanation(effectIndex) + " para " + TextColoredByType(NameOfType(effects[effectIndex].type), effects[effectIndex].type);

            case SpiceEffect.TypeChangeFormats.Switcheroo:
                return TextColoredAsSpice("troca o tipo ") + "da sua criatura com o da criatura do oponente";

            default:
                Debug.Log("Efeito inesperado!!");
                return null;
        }
    }

    private string TargetExplanation(int effectIndex)
    {
        FoodmonTypes? typeTargeted;
        switch (effects[effectIndex].targetType)
        {
            case SpiceEffect.TargetTypes.OnlyDesserts:
                typeTargeted = FoodmonTypes.Dessert;
                break;

            case SpiceEffect.TargetTypes.OnlyVegetables:
                typeTargeted = FoodmonTypes.Vegetable;
                break;

            case SpiceEffect.TargetTypes.OnlyMeals:
                typeTargeted = FoodmonTypes.Meal;
                break;

            default:
                typeTargeted = null;
                break;
        }

        string targetText = "";

        switch (effects[effectIndex].targetOwner)
        {
            case SpiceEffect.TargetOwners.OnlyPlayer:
                targetText += "da sua criatura";
                if (effects[effectIndex].targetType != SpiceEffect.TargetTypes.All)
                {
                    targetText += ", caso seja " + TextColoredByType(NameOfType(typeTargeted), typeTargeted) + ",";
                }
                return targetText;

            case SpiceEffect.TargetOwners.OnlyOpponent:
                targetText += "da criatura do oponente";
                if (effects[effectIndex].targetType != SpiceEffect.TargetTypes.All)
                {
                    targetText += ", caso seja " + TextColoredByType(NameOfType(typeTargeted), typeTargeted) + ",";
                }
                return targetText;

            case SpiceEffect.TargetOwners.All:
                targetText += "de todas as criaturas em campo";
                if (effects[effectIndex].targetType != SpiceEffect.TargetTypes.All)
                {
                    targetText += " que sejam " + TextColoredByType(NameOfType(typeTargeted), typeTargeted);
                }
                return targetText;

            case SpiceEffect.TargetOwners.Choose:
                targetText += "de uma criatura em campo";
                if (effects[effectIndex].targetType != SpiceEffect.TargetTypes.All)
                {
                    targetText += " que seja " + TextColoredByType(NameOfType(typeTargeted), typeTargeted);
                }
                return targetText;

            default:
                Debug.Log("Alvo inesperado!!");
                return null;
        }
    }

    private string SpeedExplanation()
    {
        return "VELOCIDADE: " + TextColoredAsSpice(speed.ToString());
    }
}
