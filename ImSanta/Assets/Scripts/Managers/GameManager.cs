using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int cardsPlayedTotal;

    [Header("References: ")]
    public AudioManager audioManager;
    public SceneController sceneController;

    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;

    }

    private void Start()
    {

        sceneController.LoadScene("SampleScene");
        audioManager.PlaySound("Background");

    }

    public void RestartGame() {

        sceneController.LoadScene("SampleScene");

    }

}
