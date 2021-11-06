using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            animator.SetTrigger("tr_ThrowGarbage");
        }
    }
}
