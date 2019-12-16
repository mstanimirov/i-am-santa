using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimator : MonoBehaviour
{

    #region Private Vars

    private Animator animator;
    private CardController cardController;

    #endregion

    #region Getters/Setters

    public bool IsFlipped { get; private set; }

    #endregion

    private void Awake()
    {

        animator = GetComponent<Animator>();
        cardController = GetComponent<CardController>();

    }

    private void Start()
    {

        IsFlipped = false;

    }

    public void Flip()
    {

        animator.SetTrigger("Flip");

    }

    public void OnFlipAnimationOver()
    {

        IsFlipped = true;
        cardController.cardState = CardController.CardState.Idle;

    }

}
