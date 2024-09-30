using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpiceEffect", order = 1)]
public class SpiceEffect : ScriptableObject
{
    public enum EffectAbilities { ChangePower, ChangeType }
    public enum PowerChangeFormats { Increase, Decrease, Set, Switcheroo }
    public enum TypeChangeFormats { NextInOrder, PreviousInOrder, Set, Switcheroo }
    public enum TargetOwners { OnlyPlayer, OnlyOpponent, All, Choose }
    public enum TargetTypes { OnlyDesserts, OnlyVegetables, OnlyMeals, All }

    public EffectAbilities effectAbility;

    public PowerChangeFormats powerChangeFormat;
    public int value;

    public TypeChangeFormats typeChangeFormat;
    public Card.FoodmonTypes type;

    public TargetOwners targetOwner;
    public TargetTypes targetType;

    public List<Monster> GetTargetedMonsters(Monster ownerMonster, Monster opponentMonster)
    {
        List<Monster> targetedMonsters = new List<Monster>();
        
        if (IsSwitcheroo())
        {
            targetedMonsters.Add(ownerMonster);
            targetedMonsters.Add(opponentMonster);
        }
        else
        {
            switch (targetOwner)
            {
                case TargetOwners.OnlyPlayer:
                    targetedMonsters.Add(ownerMonster);
                    break;

                case TargetOwners.OnlyOpponent:
                    targetedMonsters.Add(opponentMonster);
                    break;

                case TargetOwners.All:
                    targetedMonsters.Add(ownerMonster);
                    targetedMonsters.Add(opponentMonster);
                    break;

                case TargetOwners.Choose:
                    break;

                default:
                    Debug.Log("Alvo inesperado!!");
                    return null;
            }
        }

        return targetedMonsters;
    }

    public void ApplyEffect(List<Monster> targetedMonsters)
    {
        if (IsSwitcheroo())
        {
            ApplySwitcherooEffects(targetedMonsters);

            targetedMonsters[0].ShowCardForBattle();
            targetedMonsters[1].ShowCardForBattle();
        }
        else
        {
            RemoveNonAffectedMonsters(targetedMonsters);

            foreach (Monster monster in targetedMonsters)
            {
                switch (effectAbility)
                {
                    case EffectAbilities.ChangePower:
                        ApplyChangePowerEffect(monster);
                        break;

                    case EffectAbilities.ChangeType:
                        ApplyChangeTypeEffect(monster);
                        break;

                    default:
                        Debug.Log("Efeito inesperado!!");
                        break;
                }

                monster.ShowCardForBattle();
            }
        }
    }

    private void ApplySwitcherooEffects(List<Monster> targetedMonsters)
    {
        int powerTemp;
        Card.FoodmonTypes typeTemp;

        switch (effectAbility)
        {
            case EffectAbilities.ChangePower:
                powerTemp = targetedMonsters[0].Power;
                targetedMonsters[0].Power = targetedMonsters[1].Power;
                targetedMonsters[1].Power = powerTemp;
                return;

            case EffectAbilities.ChangeType:
                typeTemp = targetedMonsters[0].TypeOfFoodmon;
                targetedMonsters[0].TypeOfFoodmon = targetedMonsters[1].TypeOfFoodmon;
                targetedMonsters[1].TypeOfFoodmon = typeTemp;
                return;

            default:
                Debug.Log("Efeito inesperado!!");
                return;
        }
    }

    private void ApplyChangePowerEffect(Monster monster)
    {
        switch (powerChangeFormat)
        {
            case PowerChangeFormats.Increase:
                monster.Power += value;
                return;

            case PowerChangeFormats.Decrease:
                monster.Power -= value;
                return;

            case PowerChangeFormats.Set:
                monster.Power = value;
                return;

            default:
                Debug.Log("Efeito inesperado!!");
                return;
        }
    }

    private void ApplyChangeTypeEffect(Monster monster)
    {
        switch (typeChangeFormat)
        {
            case TypeChangeFormats.NextInOrder:
                switch (monster.TypeOfFoodmon)
                {
                    case Card.FoodmonTypes.Dessert:
                        monster.TypeOfFoodmon = Card.FoodmonTypes.Vegetable;
                        break;

                    case Card.FoodmonTypes.Vegetable:
                        monster.TypeOfFoodmon = Card.FoodmonTypes.Meal;
                        break;

                    case Card.FoodmonTypes.Meal:
                        monster.TypeOfFoodmon = Card.FoodmonTypes.Dessert;
                        break;

                    default:
                        Debug.Log("Tipo inesperado!!");
                        break;
                }
                return;

            case TypeChangeFormats.PreviousInOrder:
                switch (monster.TypeOfFoodmon)
                {
                    case Card.FoodmonTypes.Dessert:
                        monster.TypeOfFoodmon = Card.FoodmonTypes.Meal;
                        break;

                    case Card.FoodmonTypes.Vegetable:
                        monster.TypeOfFoodmon = Card.FoodmonTypes.Dessert;
                        break;

                    case Card.FoodmonTypes.Meal:
                        monster.TypeOfFoodmon = Card.FoodmonTypes.Vegetable;
                        break;

                    default:
                        Debug.Log("Tipo inesperado!!");
                        break;
                }
                return;

            case TypeChangeFormats.Set:
                monster.TypeOfFoodmon = type;
                return;

            default:
                Debug.Log("Efeito inesperado!!");
                return;
        }
    }

    private void RemoveNonAffectedMonsters(List<Monster> targetedMonsters)
    {
        switch (targetType)
        {
            case TargetTypes.OnlyDesserts:
                targetedMonsters.RemoveAll(monster => monster.TypeOfFoodmon != Card.FoodmonTypes.Dessert);
                return;

            case TargetTypes.OnlyVegetables:
                targetedMonsters.RemoveAll(monster => monster.TypeOfFoodmon != Card.FoodmonTypes.Vegetable);
                return;

            case TargetTypes.OnlyMeals:
                targetedMonsters.RemoveAll(monster => monster.TypeOfFoodmon != Card.FoodmonTypes.Meal);
                return;

            default:
                return;
        }
    }

    private bool IsSwitcheroo()
    {
        return (effectAbility == EffectAbilities.ChangePower && powerChangeFormat == PowerChangeFormats.Switcheroo) || (effectAbility == EffectAbilities.ChangeType && typeChangeFormat == TypeChangeFormats.Switcheroo);
    }
}
