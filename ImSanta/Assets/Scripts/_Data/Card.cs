using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Character")]
public class Card : ScriptableObject
{

    [Header("Visuals: ")]
    public Sprite avatar;

    public Color border1;
    public Color border2;

}
