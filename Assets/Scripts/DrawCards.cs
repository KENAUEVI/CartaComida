using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    private enum ownerOptions { Player, Opponent }

    [SerializeField] private Deck deckReference;
    [SerializeField] private ownerOptions deckOwner;
    [SerializeField] private int numberOfCardsPerDraw;
    [SerializeField] private CardPlacing playerArea;
    [SerializeField] private HandPlacing opponentArea;

    private Deck deck;
    private Queue<Card> spiceDeck;
    private Queue<Card> monsterDeck;

    public delegate void DeckAction();
    static public event DeckAction OnDeckClicked;

    // Start is called before the first frame update
    private void Start()
    {
        deck = deckReference;
        Shuffle(deck);

        spiceDeck = new Queue<Card>(deck.spices);
        monsterDeck = new Queue<Card>(deck.monsters);

        OnDeckClicked += Draw;
    }

    public void CallDeckClickedAction()
    {
        OnDeckClicked();
    }

    public void Draw()
    {
        for (int i = 0; i < numberOfCardsPerDraw; i++)
        {
            Card spiceCard = Instantiate(spiceDeck.Dequeue(), transform.position, Quaternion.Euler(transform.eulerAngles));
            Card monsterCard = Instantiate(monsterDeck.Dequeue(), transform.position, Quaternion.Euler(transform.eulerAngles));

            if(deckOwner == ownerOptions.Player)
            {
                playerArea.AddCard(spiceCard);
                playerArea.AddCard(monsterCard);
            }
            else if(deckOwner == ownerOptions.Opponent)
            {
                opponentArea.AddCard(spiceCard);
                opponentArea.AddCard(monsterCard);
            }
        }
    }

    private void Shuffle(Deck deck)
    {
        ShuffleList(deck.spices);
        ShuffleList(deck.monsters);
    }

    private void ShuffleList(Card[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            Card temp = list[i];
            int randomIndex = Random.Range(i, list.Length);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
