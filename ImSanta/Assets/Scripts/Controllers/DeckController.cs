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
    public Card[] cardVisuals;

    [Header("Object References:")]
    public CardController[] cards;
    public Queue<CardController> cardsPool;

    public event System.Action OnCardReturn;

    #region Private Vars

    private int previousIndex = 0;

    private CardBags cardBags = new CardBags();

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

        for (int i = 0; i < cards.Length; i++)
        {

            cardsPool.Enqueue(cards[i]);

        }

        LoadCardsBag();

    }

    private void OnEnable()
    {

        foreach (CardController card in cards) {

            card.OnSwipeEnd += StatsManager.instance.HideImpacts;
            card.OnSwipeLeft += StatsManager.instance.HandleImpacts;
            card.OnSwipeRight += StatsManager.instance.HandleImpacts;

        }

    }

    private void OnDisable()
    {

        foreach (CardController card in cards)
        {

            card.OnSwipeEnd -= StatsManager.instance.HideImpacts;
            card.OnSwipeLeft -= StatsManager.instance.HandleImpacts;
            card.OnSwipeRight -= StatsManager.instance.HandleImpacts;

        }

    }

    public void ShowFirstCard()
    {

        GameController.instance.ShowNewCard();

    }

    public CardController GetCard(Vector3 position, Quaternion rotation)
    {

        CardController cardToSpawn = cardsPool.Dequeue();

        cardToSpawn.gameObject.SetActive(true);

        cardToSpawn.transform.rotation = rotation;
        cardToSpawn.transform.localPosition = position;

        cardToSpawn.SetupCard(GetVisuals(), GetInfo());

        Gameplay.instance.SetCardName(cardToSpawn.GetCardData.name);
        Gameplay.instance.SetQuestion(cardToSpawn.GetCardData.question);

        return cardToSpawn;

    }

    public void ReturnCard(CardController cardToReturn)
    {

        cardToReturn.gameObject.SetActive(false);

        cardToReturn.ResetCard();
        cardsPool.Enqueue(cardToReturn);

        OnCardReturn?.Invoke();

    }

    private CardData GetInfo() {

        switch (previousIndex) {

            case 0:

                return cardBags.ElfBag[0];

            case 1:

                return cardBags.DeerBag[0];

            case 2:

                return cardBags.GrinchBag[0];

            case 3:

                return cardBags.KrampusBag[0];


        }

        return null;   

    }

    private Card GetVisuals()
    {
        int newIndex = Random.Range(0, cardVisuals.Length);

        while (newIndex == previousIndex)
            newIndex = Random.Range(0, cardVisuals.Length);

        previousIndex = newIndex;

        return cardVisuals[previousIndex];

    }

    private void LoadCardsBag()
    {

        TextAsset asset = Resources.Load<TextAsset>("BagBalanced");

        if (asset != null)
            cardBags = JsonUtility.FromJson<CardBags>(asset.text);
        else
            Debug.Log("no asset loaded");

    }

}
