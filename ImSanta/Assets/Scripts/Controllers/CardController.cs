using UnityEngine;

public class CardController : MonoBehaviour
{

    [Header("General Settings:")]
    public float speed;
    public float yOffset;

    [Header("Object References")]
    public Transform graphic;

    public GameObject positiveAnsw;
    public GameObject negativeAnsw;

    [HideInInspector]
    public CardState cardState;

    #region Private Vars

    private Vector3 mousePos;
    private Vector3 targetPos;
    private Vector3 initialPos;

    private InputMaster inputMaster;

    private CardAnimator cardAnimator;

    #endregion

    public enum CardState {

        Idle,
        Moving,
        Flipping,
        Dropping

    }

    private void Awake()
    {

        inputMaster = new InputMaster();
        cardAnimator = GetComponent<CardAnimator>();

    }

    private void Start()
    {

        initialPos = graphic.position;
        cardState = CardState.Flipping;

    }

    private void OnEnable()
    {

        inputMaster.Gameplay.Enable();
        inputMaster.Gameplay.ActionP.performed += ctx => CheckAnswer();

    }

    private void OnDisable()
    {

        inputMaster.Gameplay.ActionP.performed -= ctx => CheckAnswer();
        inputMaster.Gameplay.Disable();

    }

    private void OnMouseEnter()
    {

        if (cardState == CardState.Idle)
            cardState = CardState.Moving;

    }

    private void OnMouseExit()
    {

        if (cardState == CardState.Moving)
            cardState = CardState.Idle;

    }

    private void Update()
    {

        switch (cardState) {

            case CardState.Idle:

                if (graphic.position != initialPos)
                {

                    graphic.position = Vector3.MoveTowards(graphic.position, initialPos, speed / 2 * Time.deltaTime);
                    graphic.rotation = Quaternion.Lerp(graphic.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), speed / 2 * Time.deltaTime);

                }
                else
                {

                    graphic.position = initialPos;
                    graphic.rotation = Quaternion.Euler(Vector3.zero);

                }

                break;

            case CardState.Moving:

                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos = new Vector3(mousePos.normalized.x * 2.8f, (mousePos.normalized.y / 1.6f) - yOffset);

                graphic.position = Vector3.MoveTowards(graphic.position, targetPos, speed * Time.deltaTime);
                graphic.rotation = Quaternion.Lerp(graphic.rotation, Quaternion.Euler(0.0f, 0.0f, targetPos.x * -6), speed * Time.deltaTime);

                break;

            case CardState.Flipping:

                break;

            case CardState.Dropping:

                graphic.Translate(Vector3.down * speed * 2 * Time.deltaTime, Space.World);

                if (graphic.position.x < 0f)
                    graphic.Rotate(new Vector3(0.0f, 0.0f, 3f));
                else if (graphic.position.x > 0f)
                    graphic.Rotate(new Vector3(0.0f, 0.0f, -3f));

                if (graphic.position.y < -6)
                    gameObject.SetActive(false);

                break;

        }

        CheckPosition();

    }

    public void CheckAnswer() {

        switch (cardState) {

            case CardState.Idle:

                break;

            case CardState.Moving:

                if (graphic.position.x < -0.5f)
                    ChoosePositive();
                else if (graphic.position.x > 0.5f)
                    ChooseNegative();

                break;

            case CardState.Flipping:

                cardAnimator.Flip();

                break;

        }

        

    }

    public void ChoosePositive() {

        DropCard();

    }

    public void ChooseNegative() {

        DropCard();

    }

    private void DropCard() {

        cardState = CardState.Dropping;

    }

    private void CheckPosition() {

        if (graphic.position.x < -0.5f)
        {

            if (!positiveAnsw.activeSelf)
                positiveAnsw.SetActive(true);

        }
        else if (graphic.position.x > 0.5f)
        {

            if (!negativeAnsw.activeSelf)
                negativeAnsw.SetActive(true);

        }
        else
        {

            if (positiveAnsw.activeSelf)
                positiveAnsw.SetActive(false);

            if (negativeAnsw.activeSelf)
                negativeAnsw.SetActive(false);

        }

    }

}
