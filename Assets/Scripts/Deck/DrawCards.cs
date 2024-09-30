using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    [SerializeField] private Deck deckReference;
    [SerializeField] private int numberOfCardsPerDraw;
    [SerializeField] private float delayBetweenCards;
    [SerializeField] private CardPlacing areaToPutCards;

    private Deck deck;
    private Stack<Card> spiceDeck;
    private Stack<Card> monsterDeck;

    public delegate void DeckAction();
    public static event DeckAction OnDeckClicked;

    // Start is called before the first frame update
    private void Start()
    {
        SpriteLoader.LoadCardBackgrounds();

        deck = deckReference;
        Shuffle(deck);

        spiceDeck = new Stack<Card>(deck.spices);
        monsterDeck = new Stack<Card>(deck.monsters);

        OnDeckClicked += Draw;
    }

    public void CallDeckClickedAction()
    {
        OnDeckClicked();
    }

    public void Draw()
    {
        StartCoroutine(DrawRoutine());
    }

    private IEnumerator DrawRoutine()
    {
        for (int i = 0; i < numberOfCardsPerDraw; i++)
        {
            Card spiceCard = Instantiate(spiceDeck.Pop(), transform.position, Quaternion.Euler(transform.eulerAngles));
            areaToPutCards.AddCard(spiceCard);
            yield return new WaitForSeconds(delayBetweenCards);

            Card monsterCard = Instantiate(monsterDeck.Pop(), transform.position, Quaternion.Euler(transform.eulerAngles));
            areaToPutCards.AddCard(monsterCard);
            yield return new WaitForSeconds(delayBetweenCards);
        }

        yield return null;
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
