using TMPro;
using UnityEngine;

public class CardLoader : MonoBehaviour
{

    [Header("Info Settings:")]
    public TextMeshProUGUI positiveTextUI;
    public TextMeshProUGUI negativeTextUI;
    
    [Header("Visuals Settings: ")]
    public SpriteRenderer avatar;

    public SpriteRenderer borderBgr;
    public SpriteRenderer borderFgr;

    public void LoadInfo(CardData data) {

        positiveTextUI.SetText(data.positiveAnswer);
        negativeTextUI.SetText(data.negativeAnswer);

    }

    public void LoadVisuals(Card visuals) {

        avatar.sprite = visuals.avatar;

        borderBgr.color = visuals.border1;
        borderFgr.color = visuals.border2;

    }

}
