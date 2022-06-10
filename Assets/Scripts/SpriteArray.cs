using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpriteArray", order = 1)]
public class SpriteArray : ScriptableObject
{
    public Sprite[] spriteArray;
}