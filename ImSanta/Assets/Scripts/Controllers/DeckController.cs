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
    private int cardsPlayedTotal = 0;

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

        LoadCardsBag("BagStart");
        GameManager.instance.audioManager.PlaySound("CardDeal");

    }

    private void OnEnable()
    {

        foreach (CardController card in cards)
        {

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

        CardData cardData = GetInfo();
        cardToSpawn.SetupCard(cardVisuals[cardData.index], cardData);

        Gameplay.instance.SetCardName(cardToSpawn.GetCardData.name);
        Gameplay.instance.SetQuestion(cardToSpawn.GetCardData.question);

        return cardToSpawn;

    }

    public void ReturnCard(CardController cardToReturn)
    {

        cardToReturn.gameObject.SetActive(false);

        cardToReturn.ResetCard();
        cardsPool.Enqueue(cardToReturn);

        cardsPlayedTotal++;

        OnCardReturn?.Invoke();

    }

    private CardData GetInfo()
    {

        List<CardData> currentList = new List<CardData>();

        if (StatsManager.instance.believers <= 0)
        {

            LoadCardsBag("BagDeaths");
            currentList = cardBags.ManagersBag;

            return currentList[0];

        }
        else if (StatsManager.instance.believers >= 100) {

            LoadCardsBag("BagDeaths");
            currentList = cardBags.ManagersBag;

            return currentList[1];

        }
        if (StatsManager.instance.workers <= 0)
        {

            LoadCardsBag("BagDeaths");
            currentList = cardBags.ManagersBag;

            return currentList[2];

        }
        else if (StatsManager.instance.workers >= 100)
        {

            LoadCardsBag("BagDeaths");
            currentList = cardBags.ManagersBag;

            return currentList[3];

        }
        if (StatsManager.instance.money <= 0)
        {

            LoadCardsBag("BagDeaths");
            currentList = cardBags.ManagersBag;

            return currentList[4];

        }
        else if (StatsManager.instance.money >= 100)
        {

            LoadCardsBag("BagDeaths");
            currentList = cardBags.ManagersBag;

            return currentList[5];

        }

        if (cardsPlayedTotal % 3 == 0 && cardsPlayedTotal != 0)
            currentList = cardBags.KidsBag;
        else
            currentList = cardBags.ManagersBag;

        if (currentList != null)
            return currentList[Random.Range(0, currentList.Count)];

        return null;

    }

    private void LoadCardsBag(string bagName)
    {

        TextAsset asset = Resources.Load<TextAsset>(bagName);

        if (asset != null)
            cardBags = JsonUtility.FromJson<CardBags>(asset.text);
        else
            Debug.Log("no asset loaded");

    }

}
