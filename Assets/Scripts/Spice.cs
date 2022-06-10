using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spice : Card
{
    override protected void Awake()
    {
        base.Awake();

        typeOfCard = cardTypes.spice;
    }
}
