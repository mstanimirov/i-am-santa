using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    #region Private Vars

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

    public void ChooseAnswer() {

        switch (activeCard.Direction()) {

            case 0:

                break;

            case 1:

                activeCard.DropCard();

                break;

            case -1:

                activeCard.DropCard();

                break;

        }

    }

    public void ShowNewCard() {

        activeCard = DeckController.instance.GetCard(Vector3.zero, Quaternion.identity);

        //Fill data

        activeCard.FlipCard();

    }

}
