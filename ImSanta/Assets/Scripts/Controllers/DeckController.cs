using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{

    public static DeckController instance;

    [Header("General Settings:")]
    public Transform cardHolder;
    public CardController cardPrefab;

    public Queue<CardController> cards;

    public event System.Action OnCardReturn;

    #region Private Vars



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

    public CardController GetCard(Vector3 position, Quaternion rotation) {

        CardController cardToSpawn = cards.Dequeue();

        cardToSpawn.gameObject.SetActive(true);

        cardToSpawn.transform.localPosition = position;
        cardToSpawn.transform.rotation = rotation;

        return cardToSpawn;

    }

    public void ReturnCard(CardController cardToReturn) {

        cardToReturn.gameObject.SetActive(false);

        cardToReturn.ResetCard();
        cards.Enqueue(cardToReturn);

        OnCardReturn?.Invoke();

    }

    public void ShowFirstCard() {

        GameController.instance.ShowNewCard();

    }

    private void FillCardPool(int amount) {

        for (int i = 0; i < amount; i++) {

            CardController cardToAdd = Instantiate(cardPrefab);

            cardToAdd.transform.SetParent(cardHolder);
            cardToAdd.gameObject.SetActive(false);

            cards.Enqueue(cardToAdd);

        }

    }

}
