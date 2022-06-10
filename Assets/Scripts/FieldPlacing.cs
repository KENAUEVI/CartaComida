using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPlacing : CardPlacing
{
    [SerializeField] private Transform spicePosition;
    [SerializeField] private Transform monsterPosition;

    private Card spiceCard;
    private Card monsterCard;

    public Card SpiceCard { get => spiceCard; }
    public Card MonsterCard { get => monsterCard; }

    // Start is called before the first frame update
    private void Start()
    {
        spiceCard = null;
        monsterCard = null;
    }

    override public void AddCard(Card card)
    {
        card.transform.SetParent(transform);
        card.transform.localEulerAngles = 0 * Vector3.forward;
        card.transform.localScale = cardScale * Vector2.one;

        if (card is Spice)
        {
            card.transform.localPosition = spicePosition.localPosition;
            spiceCard = card;
        }
        else if (card is Monster)
        {
            card.transform.localPosition = monsterPosition.localPosition;
            monsterCard = card;
        }

        if(spiceCard != null && monsterCard != null)
        {
            //COMECAR BATALHA?
        }
    }

    override public void RemoveCard(Card card)
    {
        if (card is Spice)
        {
            if (card == spiceCard) spiceCard = null;
        }
        else if (card is Monster)
        {
            if (card == monsterCard) monsterCard = null;
        }
    }

    override public void ReturnCardToPosition(Card card)
    {
        card.transform.SetParent(transform);
        card.transform.localEulerAngles = 0 * Vector3.forward;
        card.transform.localScale = cardScale * Vector2.one;

        if (card is Spice)
        {
            card.transform.localPosition = spicePosition.localPosition;
        }
        else if (card is Monster)
        {
            card.transform.localPosition = monsterPosition.localPosition;
        }
    }
}
