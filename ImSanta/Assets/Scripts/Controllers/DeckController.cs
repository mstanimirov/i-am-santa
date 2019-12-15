using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{

    public static DeckController instance;

    [Header("General Settings:")]
    public Transform cardHolder;
    public CardController cardPrefab;

    [Header("Card Settings: ")]
    public Card[] testCards;
    public Queue<CardController> cards;

    public event System.Action OnCardReturn;

    #region Private Vars

    private int previousIndex = 0;

    #endregion

    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;

    }

    private void Start()
    {
        cards = new Queue<CardController>();

        FillCardPool(2);

    }

    public void ShowFirstCard()
    {

        //Animation event function
        GameController.instance.ShowNewCard();

    }

    public CardController GetCard(Vector3 position, Quaternion rotation) {

        CardController cardToSpawn = cards.Dequeue();

        cardToSpawn.gameObject.SetActive(true);

        cardToSpawn.transform.rotation = rotation;
        cardToSpawn.transform.localPosition = position;

        cardToSpawn.data.Set(GetData());

        Gameplay.instance.SetCardName(cardToSpawn.data.GetName());
        Gameplay.instance.SetQuestion(cardToSpawn.data.GetQuestion());

        return cardToSpawn;

    }

    public void ReturnCard(CardController cardToReturn) {

        cardToReturn.gameObject.SetActive(false);

        cardToReturn.ResetCard();
        cards.Enqueue(cardToReturn);

        OnCardReturn?.Invoke();

    }    

    private void FillCardPool(int amount) {

        for (int i = 0; i < amount; i++) {

            CardController cardToAdd = Instantiate(cardPrefab);

            cardToAdd.transform.SetParent(cardHolder);
            cardToAdd.gameObject.SetActive(false);

            cards.Enqueue(cardToAdd);

        }

    }

    private Card GetData()
    {

        int newIndex = Random.Range(0, testCards.Length);

        while (newIndex == previousIndex)
            newIndex = Random.Range(0, testCards.Length);

        previousIndex = newIndex;

        return testCards[previousIndex];

    }

}
