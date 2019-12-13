using UnityEngine;

public class CardController : MonoBehaviour
{

    [Header("General Settings:")]
    public float speed;

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

                    CheckPosition();
                    
                }
                else
                {

                    graphic.position = initialPos;
                    graphic.rotation = Quaternion.Euler(Vector3.zero);

                }

                break;

            case CardState.Moving:

                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos = new Vector3(mousePos.normalized.x * 2.8f, (mousePos.normalized.y / 1.6f) + initialPos.y);

                graphic.position = Vector3.MoveTowards(graphic.position, targetPos, speed * Time.deltaTime);
                graphic.rotation = Quaternion.Lerp(graphic.rotation, Quaternion.Euler(0.0f, 0.0f, targetPos.x * -6), speed * Time.deltaTime);

                CheckPosition();
                
                break;

            case CardState.Flipping:

                break;

            case CardState.Dropping:


                if (graphic.position.x < 0f)
                {

                    graphic.Rotate(new Vector3(0.0f, 0.0f, 4f));
                    graphic.Translate((Vector3.down + (Vector3.left * 0.5f)) * speed * 2.2f * Time.deltaTime, Space.World);

                }
                else if (graphic.position.x > 0f)
                {

                    graphic.Rotate(new Vector3(0.0f, 0.0f, -4f));
                    graphic.Translate((Vector3.down + (Vector3.right * 0.5f)) * speed * 2.2f * Time.deltaTime, Space.World);
                    
                }

                if (graphic.position.y < -8)
                    DeckController.instance.ReturnCard(this);

                break;

        }

    }

    private void CheckPosition()
    {

        switch (Direction()) {

            case 0:

                if (positiveAnsw.activeSelf)
                    positiveAnsw.SetActive(false);

                if (negativeAnsw.activeSelf)
                    negativeAnsw.SetActive(false);

                break;

            case 1:

                if (!negativeAnsw.activeSelf)
                    negativeAnsw.SetActive(true);

                break;

            case -1:

                if (!positiveAnsw.activeSelf)
                    positiveAnsw.SetActive(true);

                break;

        }

    }

    #region Card Interaction

    public int Direction() {

        if (graphic.transform.position.x < -0.5f)
            return -1;
        else if (graphic.transform.position.x > 0.5f)
            return 1;

        return 0;

    }

    public void FlipCard()
    {

        cardAnimator.Flip();
        cardState = CardState.Flipping;

    }

    public void DropCard() {

        cardState = CardState.Dropping;

    }

    public void ResetCard() {

        positiveAnsw.SetActive(false);
        negativeAnsw.SetActive(false);

        graphic.transform.position = initialPos;
        graphic.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

    }

    #endregion
    

}
