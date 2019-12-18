using UnityEngine;
using TMPro;

[System.Serializable]
public class CardData
{

    public string name;
    public string question;

    public string positiveAnswer;
    public string negativeAnswer;

    public int[] positiveEffects;
    public int[] negativeEffects;

}
