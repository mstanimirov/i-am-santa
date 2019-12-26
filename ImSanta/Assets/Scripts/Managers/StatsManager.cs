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

    public void HideImpacts() {

        believersUI.HideImpact();
        workersUI.HideImpact();
        moneyUI.HideImpact();

    }

    public void HandleImpacts(int[] impact)
    {

        believersUI.ShowImapct(impact[0]);
        workersUI.ShowImapct(impact[1]);
        moneyUI.ShowImapct(impact[2]);

    }

    public void HandleBelievers(int amount) {

        if (amount != 0) {

            if (believers + amount < 100)
                believers += amount;
            else
                believers = 100;

            float statPercentage = believers / 100.0f;
            believersUI.HandleStatChanged(statPercentage);

        }

    }

    public void HandleWorkers(int amount) {

        if (amount != 0) {

            if (workers + amount < 100)
                workers += amount;
            else
                workers = 100;

            float statPercentage = workers / 100.0f;
            workersUI.HandleStatChanged(statPercentage);

        }

    }

    public void HandleMoney(int amount) {

        if (amount != 0) {

            if (money + amount < 100)
                money += amount;
            else
                money = 100;

            float statPercentage = money / 100.0f;
            moneyUI.HandleStatChanged(statPercentage);

        }

    }

}
