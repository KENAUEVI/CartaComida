using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandPlacing : CardPlacing
{
    [SerializeField] private OwnerOptions handOwner;
    [SerializeField] private float fanAngle;
    [SerializeField] private float distanceBetweenCards;
    [SerializeField] private float fanUplift;

    private List<Card> spiceCards;
    private List<Card> monsterCards;

    public List<Card> SpiceCards { get => spiceCards; }
    public List<Card> MonsterCards { get => monsterCards; }

    // Start is called before the first frame update
    private void Start()
    {
        spiceCards = new List<Card>();
        monsterCards = new List<Card>();
    }

    public override void AddCard(Card card)
    {
        if(handOwner == OwnerOptions.Player)
        {
            card.CardIsInPlayerHandOrField();
        }
        else if(handOwner == OwnerOptions.Opponent)
        {
            card.CardIsInOpponentsHandOrField();
        }

        card.transform.SetParent(transform);
        if(card is Spice)
        {
            spiceCards.Add(card);
        }
        else if(card is Monster)
        {
            monsterCards.Add(card);
        }

        MakeCardFan();
    }

    public override void RemoveCard(Card card)
    {
        if (card is Spice)
        {
            spiceCards.Remove(card);
        }
        else if (card is Monster)
        {
            monsterCards.Remove(card);
        }

        MakeCardFan();
    }

    public override void ReturnCardToPosition(Card card)
    {
        card.transform.SetParent(transform);
        MakeCardFan();
    }

    private void MakeCardFan()
    {
        List<Card> allCards = spiceCards.Concat(monsterCards).ToList();

        if (allCards.Count == 0)
        {
            return;
        }
        else if (allCards.Count == 1)
        {
            allCards[0].transform.localPosition = (fanUplift / 2) * Vector2.up;
            allCards[0].transform.localEulerAngles = 0 * Vector3.forward;
            allCards[0].transform.localScale = Card.cardScale * Vector2.one;
        }
        else
        {
            for (int i = 0; i < allCards.Count; i++)
            {
                allCards[i].transform.SetSiblingIndex(i);

                float percent = ((float) i) / ((float) allCards.Count - 1);
                float xCoord = Mathf.Lerp(-distanceBetweenCards * allCards.Count / 2, distanceBetweenCards * allCards.Count / 2, percent);
                float yCoord = Mathf.Lerp(-fanUplift / 2, fanUplift / 2, -4 * Mathf.Pow(percent, 2) + 4 * percent);
                float angle = Mathf.Lerp(fanAngle / 2, -fanAngle / 2, percent);
                if (handOwner == OwnerOptions.Opponent)
                {
                    yCoord = -yCoord;
                    angle = 180 - angle;
                }

                allCards[i].transform.localPosition = new Vector2(xCoord, yCoord);
                allCards[i].transform.localEulerAngles = angle * Vector3.forward;
                allCards[i].transform.localScale = Card.cardScale * Vector2.one;
            }
        }
    }
}
