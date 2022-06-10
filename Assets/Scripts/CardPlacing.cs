using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CardPlacing : MonoBehaviour
{
    protected float cardScale = 1.2f;

    abstract public void AddCard(Card card);
    abstract public void RemoveCard(Card card);
    abstract public void ReturnCardToPosition(Card card);
}
