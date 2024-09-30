using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlacing : CardPlacing
{
    [SerializeField] private Transform playerSpicePosition;
    [SerializeField] private Transform playerMonsterPosition;
    [SerializeField] private Transform enemySpicePosition;
    [SerializeField] private Transform enemyMonsterPosition;

    private Card playerSpiceCard;
    private Card playerMonsterCard;
    private Card enemySpiceCard;
    private Card enemyMonsterCard;

    public Card PlayerSpiceCard { get => playerSpiceCard; }
    public Card PlayerMonsterCard { get => playerMonsterCard; }
    public Card EnemySpiceCard { get => enemySpiceCard; }
    public Card EnemyMonsterCard { get => enemyMonsterCard; }

    // Start is called before the first frame update
    private void Start()
    {
        playerSpiceCard = null;
        playerMonsterCard = null;
        enemySpiceCard = null;
        enemyMonsterCard = null;
    }

    public override void AddCard(Card card)
    {
        FieldPlacing previousField = card.transform.parent.GetComponent<FieldPlacing>();
        if (previousField == null)
        {
            Debug.Log("Cartas só podem ir para batalha a partir do campo!");
            return;
        }
        OwnerOptions cardOwner = previousField.FieldOwner;

        card.ShowCardForBattle();

        card.transform.SetParent(transform);
        card.transform.localEulerAngles = 0 * Vector3.forward;
        card.transform.localScale = Card.cardScaleBattling * Vector2.one;

        if (card is Spice)
        {
            if (cardOwner == OwnerOptions.Player)
            {
                card.transform.localPosition = playerSpicePosition.localPosition;
                playerSpiceCard = card;
            }
            else if (cardOwner == OwnerOptions.Opponent)
            {
                card.transform.localPosition = enemySpicePosition.localPosition;
                enemySpiceCard = card;
            }
        }
        else if (card is Monster)
        {
            if (cardOwner == OwnerOptions.Player)
            {
                card.transform.localPosition = playerMonsterPosition.localPosition;
                playerMonsterCard = card;
            }
            else if (cardOwner == OwnerOptions.Opponent)
            {
                card.transform.localPosition = enemyMonsterPosition.localPosition;
                enemyMonsterCard = card;
            }
        }
    }

    public override void RemoveCard(Card card)
    {
        if (card is Spice)
        {
            if (card == playerSpiceCard) playerSpiceCard = null;
            else if (card == enemySpiceCard) enemySpiceCard = null;
        }
        else if (card is Monster)
        {
            if (card == playerMonsterCard) playerMonsterCard = null;
            else if (card == enemyMonsterCard) enemyMonsterCard = null;
        }
    }

    public override void ReturnCardToPosition(Card card)
    {
        card.transform.SetParent(transform);
        card.transform.localEulerAngles = 0 * Vector3.forward;
        card.transform.localScale = Card.cardScaleBattling * Vector2.one;

        if (card is Spice)
        {
            if (card == playerSpiceCard)
            {
                card.transform.localPosition = playerSpicePosition.localPosition;
                playerSpiceCard = card;
            }
            else if (card == enemySpiceCard)
            {
                card.transform.localPosition = enemySpicePosition.localPosition;
                enemySpiceCard = card;
            }
        }
        else if (card is Monster)
        {
            if (card == playerMonsterCard)
            {
                card.transform.localPosition = playerMonsterPosition.localPosition;
                playerMonsterCard = card;
            }
            else if (card == enemyMonsterCard)
            {
                card.transform.localPosition = enemyMonsterPosition.localPosition;
                enemyMonsterCard = card;
            }
        }
    }
}
