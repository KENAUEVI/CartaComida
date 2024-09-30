using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPlacing : CardPlacing
{
    [SerializeField] private OwnerOptions fieldOwner;
    [SerializeField] private Transform spicePosition;
    [SerializeField] private Transform monsterPosition;

    private Card spiceCard;
    private Card monsterCard;

    public Card SpiceCard { get => spiceCard; }
    public Card MonsterCard { get => monsterCard; }
    public OwnerOptions FieldOwner { get => fieldOwner; }

    // Start is called before the first frame update
    private void Start()
    {
        spiceCard = null;
        monsterCard = null;
    }

    public override void AddCard(Card card)
    {
        if (fieldOwner == OwnerOptions.Player)
        {
            card.CardIsInPlayerHandOrField();
        }
        else if (fieldOwner == OwnerOptions.Opponent)
        {
            card.CardIsInOpponentsHandOrField();
        }

        card.transform.SetParent(transform);
        card.transform.localEulerAngles = 0 * Vector3.forward;
        card.transform.localScale = Card.cardScale * Vector2.one;

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
    }

    public override void RemoveCard(Card card)
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

    public override void ReturnCardToPosition(Card card)
    {
        card.transform.SetParent(transform);
        card.transform.localEulerAngles = 0 * Vector3.forward;
        card.transform.localScale = Card.cardScale * Vector2.one;

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
