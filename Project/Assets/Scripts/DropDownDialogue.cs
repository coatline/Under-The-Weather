using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropDownDialogue : MonoBehaviour
{
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] Animator animator;

    public void Say(string message)
    {
        animator.Play("Slide");
        dialogueText.text = message;
    }
}
