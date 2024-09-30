using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardPlacing : MonoBehaviour
{
    public enum OwnerOptions { Player, Opponent };

    public abstract void AddCard(Card card);
    public abstract void RemoveCard(Card card);
    public abstract void ReturnCardToPosition(Card card);
}
