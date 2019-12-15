using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{

    [Header("General Settings: ")]
    public Image foreground;

    public GameObject imapctBig;
    public GameObject imapctSmall;

    public void HideImpact()
    {

        imapctBig.SetActive(false);
        imapctSmall.SetActive(false);

    }

    public void ShowImapct(bool isBig) {

        if (isBig)
            imapctBig.SetActive(true);
        else
            imapctSmall.SetActive(true);

    }
    

}
