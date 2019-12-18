using UnityEngine;

public class CardController : MonoBehaviour
{

    [HideInInspector]
    public CardState cardState;

    [Header("General Settings:")]
    public float speed;

    [Header("Object References")]
    public Transform graphic;

    public GameObject positiveAnsw;
    public GameObject negativeAnsw;

    public event System.Action OnSwipeEnd = delegate { };
    public event System.Action<int[]> OnSwipeLeft = delegate { };
    public event System.Action<int[]> OnSwipeRight = delegate { };

    #region Private Vars

    private Vector3 mousePos;
    private Vector3 targetPos;
    private Vector3 initialPos;

    private CardLoader cardLoader;
    private CardAnimator cardAnimator;

    #endregion

    #region Getters/Setters

    public Card GetCardVisuals { get; private set; }

    public CardData GetCardData { get; private set; }

    #endregion

    public enum CardState
    {

        Idle,
        Moving,
        Flipping,
        Dropping

    }

    private void Awake()
    {

        cardLoader = GetComponent<CardLoader>();
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

        switch (cardState)
        {

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

    public int CheckPosition()
    {

        if (graphic.position.x < -0.5f)
        {

            if (!positiveAnsw.activeSelf)
            {

                OnSwipeLeft?.Invoke(GetCardData.negativeEffects);
                positiveAnsw.SetActive(true);

            }

            return -1;

        }
        else if (graphic.position.x > 0.5f)
        {

            if (!negativeAnsw.activeSelf)
            {

                OnSwipeRight?.Invoke(GetCardData.positiveEffects);
                negativeAnsw.SetActive(true);
                
            }

            return 1;

        }

        if (positiveAnsw.activeSelf)
        {

            OnSwipeEnd?.Invoke();
            positiveAnsw.SetActive(false);

        }

        if (negativeAnsw.activeSelf)
        {

            OnSwipeEnd?.Invoke();
            negativeAnsw.SetActive(false);

        }

        return 0;

    }

    #region Card Interaction

    public void FlipCard()
    {

        cardAnimator.Flip();
        cardState = CardState.Flipping;

    }

    public void DropCard()
    {

        cardState = CardState.Dropping;

    }

    public void ResetCard()
    {

        positiveAnsw.SetActive(false);
        negativeAnsw.SetActive(false);

        cardState = CardState.Flipping;

        graphic.transform.position = initialPos;
        graphic.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

    }

    public void SetupCard(Card visuals, CardData cardData) {

        GetCardData = cardData;
        GetCardVisuals = visuals;

        cardLoader.LoadInfo(GetCardData);
        cardLoader.LoadVisuals(GetCardVisuals);

    }

    #endregion


}
