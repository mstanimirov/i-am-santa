using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{

    [Header("General Settings: ")]
    public float updateTime;

    public Image foreground;

    public GameObject impactBig;
    public GameObject impactSmall;

    public void HideImpact()
    {

        impactBig.SetActive(false);
        impactSmall.SetActive(false);

    }

    public void ShowImapct(int amount)
    {

        if (amount > 5 || amount < -5)
            impactBig.SetActive(true);
        else if (amount == 5 || amount == -5)
            impactSmall.SetActive(true);
        else {

            impactBig.SetActive(false);
            impactSmall.SetActive(false);

        }

    }

    public void HandleStatChanged(float percent)
    {

        StopAllCoroutines();
        StartCoroutine(ChangeToPercent(percent));

    }

    private IEnumerator ChangeToPercent(float percent)
    {

        float preChangePercent = foreground.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < updateTime * 2)
        {

            elapsedTime += Time.deltaTime;
            foreground.fillAmount = Mathf.Lerp(preChangePercent, percent, elapsedTime / updateTime);

            yield return null;

        }

        foreground.fillAmount = percent;

    }

}
