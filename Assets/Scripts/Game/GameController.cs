using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private EnemyController enemy;
    [SerializeField] private GameObject battlefield;
    [SerializeField] private GameObject battleButton;
    // [SerializeField] private GameObject chooseUI;
    [SerializeField] private FieldPlacing playerZone;
    [SerializeField] private FieldPlacing enemyZone;
    [SerializeField] private Health playerHealth;
    [SerializeField] private Health enemyHealth;

    private BattlePlacing battlefieldPlacing;
    // private ChooseCardOption chooseScript;
    private Spice[] spicesInOrder = new Spice[2];
    private Monster[] monstersInOrder = new Monster[2];
    private Card chosenCard;
    private List<Monster> effectTargets;

    private enum GameState { WaitingBattleStart, BattlePreparation, SpiceActivations, Battle, BattleEnded }

    private GameState gameState;
    private bool battlePaused;
    private bool enemySetCards;
    private bool choosing;
    private int spiceIndex;
    private int effectIndex;

    // Start is called before the first frame update
    private void Start()
    {
        battlefieldPlacing = battlefield.GetComponent<BattlePlacing>();
        // chooseScript = chooseUI.GetComponent<ChooseCardOption>();

        battleButton.GetComponent<BattleButton>().OnButtonPressed += StartBattle;
        battlefield.GetComponent<BattleInteractor>().OnInteractorClicked += ContinueBattle;
        // COLOCAR EVENTOS DO CHOOSE

        InitializeConstants();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!battlePaused)
        {
            switch (gameState)
            {
                case GameState.WaitingBattleStart:
                    if (AllZonesOccupied())
                    {
                        battleButton.SetActive(true);
                    }
                    else
                    {
                        battleButton.SetActive(false);

                        if (!enemySetCards && !NoZonesOccupied())
                        {
                            enemy.SetCards();
                            enemySetCards = true;
                        }
                    }
                    break;

                case GameState.BattlePreparation:
                    PrepareCardsForBattle();
                    DetermineSpiceOrder();

                    gameState = GameState.SpiceActivations;
                    PauseBattle();
                    break;

                case GameState.SpiceActivations:
                    if (spiceIndex >= 2)
                    {
                        spiceIndex = 0;
                        effectIndex = 0;
                        gameState = GameState.Battle;
                        break;
                    }
                    
                    if (effectIndex >= spicesInOrder[spiceIndex].Effects.Count)
                    {
                        spiceIndex++;
                        effectIndex = 0;
                        break;
                    }

                    GetTargetsAndApplyEffects();

                    PauseBattle();
                    break;

                case GameState.Battle:
                    CalculateBattleDamage();

                    gameState = GameState.BattleEnded;
                    PauseBattle();
                    break;

                case GameState.BattleEnded:
                    RemoveCardsFromBattle();

                    InitializeConstants();
                    break;
            }
        }
        else if (choosing)
        {
            if (chosenCard != null)
            {
                // BRILHO NA CHOSEN CARD
            }
        }
    }

    private void InitializeConstants()
    {
        chosenCard = null;
        effectTargets = null;
        battlePaused = false;
        enemySetCards = false;
        choosing = false;
        spiceIndex = 0;
        effectIndex = 0;
        gameState = GameState.WaitingBattleStart;
    }

    private void PrepareCardsForBattle()
    {
        playerZone.SpiceCard.ShowCardForBattle();
        playerZone.MonsterCard.ShowCardForBattle();
        enemyZone.SpiceCard.ShowCardForBattle();
        enemyZone.MonsterCard.ShowCardForBattle();

        battlefield.SetActive(true);

        Card playerSpiceTemp = playerZone.SpiceCard;
        Card playerMonsterTemp = playerZone.MonsterCard;
        Card enemySpiceTemp = enemyZone.SpiceCard;
        Card enemyMonsterTemp = enemyZone.MonsterCard;

        playerZone.RemoveCard(playerSpiceTemp);
        battlefieldPlacing.AddCard(playerSpiceTemp);
        playerZone.RemoveCard(playerMonsterTemp);
        battlefieldPlacing.AddCard(playerMonsterTemp);
        enemyZone.RemoveCard(enemySpiceTemp);
        battlefieldPlacing.AddCard(enemySpiceTemp);
        enemyZone.RemoveCard(enemyMonsterTemp);
        battlefieldPlacing.AddCard(enemyMonsterTemp);
    }

    private void RemoveCardsFromBattle()
    {
        Card playerSpiceTemp = battlefieldPlacing.PlayerSpiceCard;
        Card playerMonsterTemp = battlefieldPlacing.PlayerMonsterCard;
        Card enemySpiceTemp = battlefieldPlacing.EnemySpiceCard;
        Card enemyMonsterTemp = battlefieldPlacing.EnemyMonsterCard;

        battlefieldPlacing.RemoveCard(playerSpiceTemp);
        Destroy(playerSpiceTemp.gameObject);
        battlefieldPlacing.RemoveCard(playerMonsterTemp);
        Destroy(playerMonsterTemp.gameObject);
        battlefieldPlacing.RemoveCard(enemySpiceTemp);
        Destroy(enemySpiceTemp.gameObject);
        battlefieldPlacing.RemoveCard(enemyMonsterTemp);
        Destroy(enemyMonsterTemp.gameObject);

        battlefield.SetActive(false);
    }

    private void DetermineSpiceOrder()
    {
        if (((Spice) battlefieldPlacing.PlayerSpiceCard).Speed >= ((Spice) battlefieldPlacing.EnemySpiceCard).Speed)
        {
            spicesInOrder[0] = (Spice) battlefieldPlacing.PlayerSpiceCard;
            spicesInOrder[1] = (Spice) battlefieldPlacing.EnemySpiceCard;

            monstersInOrder[0] = (Monster) battlefieldPlacing.PlayerMonsterCard;
            monstersInOrder[1] = (Monster) battlefieldPlacing.EnemyMonsterCard;
        }
        else
        {
            spicesInOrder[0] = (Spice) battlefieldPlacing.EnemySpiceCard;
            spicesInOrder[1] = (Spice) battlefieldPlacing.PlayerSpiceCard;

            monstersInOrder[0] = (Monster) battlefieldPlacing.EnemyMonsterCard;
            monstersInOrder[1] = (Monster) battlefieldPlacing.PlayerMonsterCard;
        }
    }

    private void GetTargetsAndApplyEffects()
    {
        SpiceEffect effect = spicesInOrder[spiceIndex].Effects[effectIndex];
        if (effectTargets == null)
        {
            effectTargets = effect.GetTargetedMonsters(monstersInOrder[spiceIndex], monstersInOrder[(spiceIndex + 1) % 2]);
            if (effectTargets.Count == 0)
            {
                choosing = true;
                // chooseUI.SetActive(true);
            }
            else
            {
                // BRILHO NAS CARTAS SELECIONADAS
            }
        }
        else
        {
            effect.ApplyEffect(effectTargets);
            effectTargets = null;
        }
    }

    private void CalculateBattleDamage()
    {
        int playerPower = ((Monster)battlefieldPlacing.PlayerMonsterCard).Power;
        int enemyPower = ((Monster)battlefieldPlacing.EnemyMonsterCard).Power;
        Card.FoodmonTypes playerType = ((Monster)battlefieldPlacing.PlayerMonsterCard).TypeOfFoodmon;
        Card.FoodmonTypes enemyType = ((Monster)battlefieldPlacing.EnemyMonsterCard).TypeOfFoodmon;

        if (playerType == enemyType)
        {
            ShowBattleResults(playerPower, enemyPower, playerPower >= enemyPower);
        }
        else
        {
            ShowBattleResults(playerPower, enemyPower, PlayerTypeWon(playerType, enemyType));
        }
    }

    private void ShowBattleResults(int playerPower, int enemyPower, bool playerWon)
    {
        if (playerWon)
        {
            // ANIMACAO DE BATALHA
            enemyHealth.TakeDamage(Mathf.Abs(playerPower - enemyPower));
        }
        else
        {
            // ANIMACAO DE BATALHA
            playerHealth.TakeDamage(Mathf.Abs(enemyPower - playerPower));
        }
    }

    private void StartBattle()
    {
        gameState = GameState.BattlePreparation;
        battleButton.SetActive(false);
    }

    private void PauseBattle()
    {
        battlePaused = true;
    }

    private void ContinueBattle()
    {
        if (choosing)
        {
            if (chosenCard == null)
            {
                return;
            }
            else
            {
                effectTargets.Add((Monster) chosenCard);

                chosenCard = null;
                choosing = false;
            }
        }

        battlePaused = false;
    }

    private bool AllZonesOccupied()
    {
        return playerZone.SpiceCard != null && playerZone.MonsterCard != null && enemyZone.SpiceCard != null && enemyZone.MonsterCard != null;
    }

    private bool NoZonesOccupied()
    {
        return playerZone.SpiceCard == null && playerZone.MonsterCard == null && enemyZone.SpiceCard == null && enemyZone.MonsterCard == null;
    }

    private bool PlayerTypeWon(Card.FoodmonTypes playerType, Card.FoodmonTypes enemyType)
    {
        switch (playerType)
        {
            case Card.FoodmonTypes.Dessert:
                return enemyType == Card.FoodmonTypes.Vegetable;

            case Card.FoodmonTypes.Vegetable:
                return enemyType == Card.FoodmonTypes.Meal;

            case Card.FoodmonTypes.Meal:
                return enemyType == Card.FoodmonTypes.Dessert;

            default:
                Debug.Log("Tipo inesperado!!");
                return false;
        }
    }
}
