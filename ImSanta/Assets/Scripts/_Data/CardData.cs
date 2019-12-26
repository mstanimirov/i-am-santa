using UnityEngine;
using TMPro;

[System.Serializable]
public class CardData
{

    public int index;
    public int lastPlayedAt;
    public int turnToShowAgain;

    public string name;
    public string question;

    public string positiveAnswer;
    public string negativeAnswer;

    public int specialEffect;
    public int[] positiveEffects;
    public int[] negativeEffects;

}
