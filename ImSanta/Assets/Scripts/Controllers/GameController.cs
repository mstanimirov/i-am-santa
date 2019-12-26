using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public GameState gameState;

    public enum GameState {

        Gameplay,
        Gameover

    }

    #region Private Vars

    private int effectCode;
    private int[] statsToApply = new int[3];

    private InputMaster inputManager;

    private CardController activeCard;
    private DeckController deckController;

    #endregion

    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;

        inputManager = new InputMaster();
        deckController = FindObjectOfType<DeckController>();
        
    }

    private void Start()
    {



    }

    private void OnEnable()
    {

        inputManager.Gameplay.Enable();
        inputManager.Gameplay.ActionP.performed += ctx => ChooseAnswer();

        deckController.OnCardReturn += ShowNewCard;

    }

    private void OnDisable()
    {

        inputManager.Gameplay.ActionP.performed -= ctx => ChooseAnswer();
        inputManager.Gameplay.Disable();

        deckController.OnCardReturn -= ShowNewCard;

    }

    public void ChooseAnswer()
    {

        if (gameState == GameState.Gameover) {

            if (GameManager.instance)
                GameManager.instance.RestartGame();

        }

        if (activeCard.cardState != CardController.CardState.Moving)
            return;

        switch (activeCard.CheckPosition())
        {

            case 0:

                break;

            case 1:

                activeCard.DropCard();

                statsToApply = activeCard.GetCardData.negativeEffects;
                
                break;

            case -1:

                activeCard.DropCard();

                statsToApply = activeCard.GetCardData.positiveEffects;

                break;

        }

        switch (activeCard.GetCardData.specialEffect) {

            case 0:


                break;

            case 1:

                gameState = GameState.Gameover;

                break;

        }

        StatsManager.instance.HideImpacts();

        StatsManager.instance.HandleBelievers(statsToApply[0]);
        StatsManager.instance.HandleWorkers(statsToApply[1]);
        StatsManager.instance.HandleMoney(statsToApply[2]);

    }

    public void ShowNewCard()
    {

        if (gameState != GameState.Gameplay)
            return;

        activeCard = deckController.GetCard(Vector3.zero, Quaternion.identity);
        activeCard.FlipCard();

    }

}
