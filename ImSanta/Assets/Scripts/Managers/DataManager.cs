using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    public Card[] cardVisuals;

    #region Constants

    private readonly string bagStart = "BagStart";

    #endregion

    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;

    }

    private void Start()
    {
        


    }

    public void GetInfo() {



    }

    public CardBags LoadCardBag(string bagName) {

        TextAsset asset = Resources.Load<TextAsset>(bagName);

        if (asset != null) {

            CardBags cardBags = JsonUtility.FromJson<CardBags>(asset.text);
            return cardBags;

        }
        else
            Debug.Log("No Bag Loaded!");

        return null;

    }

    public void FillCardData(CardController card)
    {



    }

}
