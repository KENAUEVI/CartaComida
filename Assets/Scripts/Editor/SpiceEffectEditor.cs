using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpiceEffect))]
public class SpiceEffectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        bool isSwitcheroo = false;
        bool isTypeSetting = false;

        SpiceEffect effect = target as SpiceEffect;

        effect.effectAbility = (SpiceEffect.EffectAbilities)EditorGUILayout.EnumPopup("Effect Ability", effect.effectAbility);

        switch (effect.effectAbility)
        {
            case SpiceEffect.EffectAbilities.ChangePower:
                effect.powerChangeFormat = (SpiceEffect.PowerChangeFormats) EditorGUILayout.EnumPopup("Change Format", effect.powerChangeFormat);
                switch (effect.powerChangeFormat)
                {
                    case SpiceEffect.PowerChangeFormats.Increase:
                        effect.value = EditorGUILayout.IntField("Increment", effect.value);
                        break;

                    case SpiceEffect.PowerChangeFormats.Decrease:
                        effect.value = EditorGUILayout.IntField("Decrement", effect.value);
                        break;

                    case SpiceEffect.PowerChangeFormats.Set:
                        effect.value = EditorGUILayout.IntField("Value", effect.value);
                        break;

                    case SpiceEffect.PowerChangeFormats.Switcheroo:
                        isSwitcheroo = true;
                        break;
                }
                break;

            case SpiceEffect.EffectAbilities.ChangeType:
                effect.typeChangeFormat = (SpiceEffect.TypeChangeFormats) EditorGUILayout.EnumPopup("Change Format", effect.typeChangeFormat);
                switch (effect.typeChangeFormat)
                {
                    case SpiceEffect.TypeChangeFormats.NextInOrder: case SpiceEffect.TypeChangeFormats.PreviousInOrder:
                        break;

                    case SpiceEffect.TypeChangeFormats.Set:
                        effect.type = (Card.FoodmonTypes) EditorGUILayout.EnumPopup("Type", effect.type);
                        isTypeSetting = true;
                        break;

                    case SpiceEffect.TypeChangeFormats.Switcheroo:
                        isSwitcheroo = true;
                        break;
                }
                break;
        }

        if (!isSwitcheroo)
        {
            effect.targetOwner = (SpiceEffect.TargetOwners) EditorGUILayout.EnumPopup("Target Owner", effect.targetOwner);

            effect.targetType = (SpiceEffect.TargetTypes) EditorGUILayout.EnumPopup("Target Type", effect.targetType);

            if (isTypeSetting)
            {
                switch (effect.targetType)
                {
                    case SpiceEffect.TargetTypes.OnlyDesserts:
                        if (effect.type == Card.FoodmonTypes.Dessert)
                        {
                            EditorGUILayout.HelpBox("Effect will change desserts into desserts", MessageType.Warning);
                        }
                        break;

                    case SpiceEffect.TargetTypes.OnlyVegetables:
                        if (effect.type == Card.FoodmonTypes.Vegetable)
                        {
                            EditorGUILayout.HelpBox("Effect will change vegetables into vegetables", MessageType.Warning);
                        }
                        break;

                    case SpiceEffect.TargetTypes.OnlyMeals:
                        if (effect.type == Card.FoodmonTypes.Meal)
                        {
                            EditorGUILayout.HelpBox("Effect will change meals into meals", MessageType.Warning);
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
