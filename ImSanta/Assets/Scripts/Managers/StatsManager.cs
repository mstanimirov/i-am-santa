using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{

    public static StatsManager instance;

    [Header("Stats: ")]
    public int believers;
    public int workers;
    public int money;

    [Header("Stats UI:")]
    public Stat believersUI;
    public Stat workersUI;
    public Stat moneyUI;

    #region Private Vars



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

    public void ShowImpacts(int[] impact) {

        believersUI.ShowImapct(impact[0]);
        workersUI.ShowImapct(impact[1]);
        moneyUI.ShowImapct(impact[2]);

    }

    public void HideImpacts() {

        believersUI.HideImpact();
        workersUI.HideImpact();
        moneyUI.HideImpact();

    }

    public void HandleBelievers(int amount) {

        if (amount != 0) {

            believers += amount;

            float statPercentage = believers / 100.0f;
            believersUI.HandleStatChanged(statPercentage);

        }

    }

    public void HandleWorkers(int amount) {

        if (amount != 0) {

            workers += amount;

            float statPercentage = workers / 100.0f;
            workersUI.HandleStatChanged(statPercentage);

        }

    }

    public void HandleMoney(int amount) {

        if (amount != 0) {

            money += amount;

            float statPercentage = money / 100.0f;
            moneyUI.HandleStatChanged(statPercentage);

        }

    }

}
