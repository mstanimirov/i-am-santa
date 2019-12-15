using UnityEngine;
using TMPro;

public class CardData : MonoBehaviour
{

    [HideInInspector]
    public Card cardData;

    public SpriteRenderer avatar;

    public SpriteRenderer borderSprite1;
    public SpriteRenderer borderSprite2;

    public TextMeshProUGUI positiveAnswer;
    public TextMeshProUGUI negativeAnswer;

    #region Getters/Setters

    public string GetName() => cardData._name;

    public string GetQuestion() => cardData.question;

    #endregion

    private void Start()
    {

        

    }

    public void Set(Card data) {

        cardData = data;
        OnDataChanged();

    }

    private void OnDataChanged() {

        avatar.sprite = cardData.avatar;

        borderSprite1.color = cardData.border1;
        borderSprite2.color = cardData.border2;

        positiveAnswer.SetText(cardData.positiveAnswer);
        negativeAnswer.SetText(cardData.negativeAnswer);

    }

}
