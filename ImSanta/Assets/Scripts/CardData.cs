using UnityEngine;
using TMPro;

public class CardData : MonoBehaviour
{

    public Card cardData;

    public SpriteRenderer avatar;

    public SpriteRenderer borderSprite1;
    public SpriteRenderer borderSprite2;

    public TextMeshProUGUI positiveAnswer;
    public TextMeshProUGUI negativeAnswer;

    private void Start()
    {

        avatar.sprite = cardData.avatar;

        borderSprite1.color = cardData.border1;
        borderSprite2.color = cardData.border2;

        positiveAnswer.SetText(cardData.positiveAnswer);
        negativeAnswer.SetText(cardData.negativeAnswer);

    }

}
