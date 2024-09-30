using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private FieldPlacing dropZone;
    [SerializeField] private HandPlacing enemyHand;

    private GameObject canvas;
    private Card chosenSpiceCard;
    private Card chosenMonsterCard;

    // Start is called before the first frame update
    private void Start()
    {
        canvas = GameObject.Find("Game Scene");
        chosenSpiceCard = null;
        chosenMonsterCard = null;
    }

    public void SetCards()
    {
        ChooseCardsToPlay();
        StartCoroutine(SetCardsRoutine());
    }

    private IEnumerator SetCardsRoutine()
    {
        Vector2 initialPosition;

        yield return new WaitForSeconds(2f);

        chosenSpiceCard.transform.SetParent(canvas.transform);
        initialPosition = chosenSpiceCard.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            chosenSpiceCard.transform.localPosition = Vector2.Lerp(initialPosition, dropZone.transform.localPosition, i / 5);
            yield return new WaitForSeconds(0.05f);
        }
        enemyHand.RemoveCard(chosenSpiceCard);
        dropZone.AddCard(chosenSpiceCard);
        chosenSpiceCard = null;

        yield return new WaitForSeconds(1f);

        chosenMonsterCard.transform.SetParent(canvas.transform);
        initialPosition = chosenMonsterCard.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            chosenMonsterCard.transform.localPosition = Vector2.Lerp(initialPosition, dropZone.transform.localPosition, i / 5);
            yield return new WaitForSeconds(0.05f);
        }
        enemyHand.RemoveCard(chosenMonsterCard);
        dropZone.AddCard(chosenMonsterCard);
        chosenMonsterCard = null;

        yield return null;
    }

    private void ChooseCardsToPlay()
    {
        List<Card> spicesInHand = enemyHand.SpiceCards;
        chosenSpiceCard = spicesInHand[Random.Range(0, spicesInHand.Count)];

        List<Card> monstersInHand = enemyHand.MonsterCards;
        chosenMonsterCard = monstersInHand[Random.Range(0, monstersInHand.Count)];
    }
}
