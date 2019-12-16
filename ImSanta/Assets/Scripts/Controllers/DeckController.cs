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

    public CardController[] cards;
    public Queue<CardController> cardsPool;

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

        cardsPool = new Queue<CardController>();

        for (int i = 0; i < cards.Length; i++) {
            
            cardsPool.Enqueue(cards[i]);
            
        }

    }

    private void OnEnable()
    {

        foreach (CardController card in cards) {

            card.OnSwipeEnd += StatsManager.instance.HideImpacts;
            card.OnSwipeLeft += StatsManager.instance.ShowImpacts;
            card.OnSwipeRight += StatsManager.instance.ShowImpacts;

        }

    }

    private void OnDisable()
    {

        foreach (CardController card in cards)
        {

            card.OnSwipeEnd -= StatsManager.instance.HideImpacts;
            card.OnSwipeLeft -= StatsManager.instance.ShowImpacts;
            card.OnSwipeRight -= StatsManager.instance.ShowImpacts;

        }

    }

    public void ShowFirstCard()
    {

        //Animation event function
        GameController.instance.ShowNewCard();

    }

    public CardController GetCard(Vector3 position, Quaternion rotation)
    {

        CardController cardToSpawn = cardsPool.Dequeue();

        cardToSpawn.gameObject.SetActive(true);

        cardToSpawn.transform.rotation = rotation;
        cardToSpawn.transform.localPosition = position;

        cardToSpawn.data.Set(GetData());

        Gameplay.instance.SetCardName(cardToSpawn.data.GetName());
        Gameplay.instance.SetQuestion(cardToSpawn.data.GetQuestion());

        return cardToSpawn;

    }

    public void ReturnCard(CardController cardToReturn)
    {

        cardToReturn.gameObject.SetActive(false);

        cardToReturn.ResetCard();
        cardsPool.Enqueue(cardToReturn);

        OnCardReturn?.Invoke();

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
