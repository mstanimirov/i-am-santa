using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Character")]
public class Card : ScriptableObject
{
    
    [Header("General Settings: ")]
    public string _name;

    public string question;
    public string positiveAnswer;
    public string negativeAnswer;

    [Header("Visual Settings: ")]
    public Sprite avatar;

    public Color border1;
    public Color border2;

    [Header("Stats Effect:")]
    public int[] positiveEffect = new int[3];
    public int[] negativeEffect = new int[3];

}
