using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimsController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private PlayerController pc;

    [SerializeField]
    private TreeController treecontroller;

    private Coroutine coroutine;
    public bool isPlayerIn = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree" && other.GetComponent<TreeController>().treeHealt > 0)
        {
            animator.SetBool("isTree", true);
            treecontroller = other.GetComponent<TreeController>();
            Debug.Log(treecontroller);
            isPlayerIn = true;
            coroutine = StartCoroutine(treecontroller.createWood(this));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree") && other.GetComponent<TreeController>().treeHealt != 0)
        {
            isPlayerIn = false;
            animator.SetBool("isTree", false);
            StopCoroutine(coroutine);
        }
    }
    void Update()
    {
        //Debug.Log(pc.rb.velocity.magnitude);
        if (pc.rb.velocity.magnitude > 0.2f)
        {
            animator.SetBool("isWalk", true);
        }
        else if (pc.rb.velocity.magnitude == 0)
        {
            animator.SetBool("isWalk", false);
        }
    }
}
