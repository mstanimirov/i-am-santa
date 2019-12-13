using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{


    #region Private Vars

    private Animator animator;

    #endregion

    private void Awake()
    {

        animator = GetComponent<Animator>();

    }

    public void Hide() {

        animator.SetTrigger("Hide");

    }

    public void OnHideComplete() {

        gameObject.SetActive(false);

    }

}
