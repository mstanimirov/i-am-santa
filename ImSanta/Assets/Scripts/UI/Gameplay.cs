using TMPro;
using UnityEngine;

public class Gameplay : MonoBehaviour
{

    public static Gameplay instance;

    [Header("Object References")]
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI question;

    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;

    }

    public void SetCardName(string value)
    {

        cardName.SetText(value);

    }

    public void SetQuestion(string value)
    {

        question.SetText(value);

    }

}
