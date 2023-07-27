using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Element
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string movename;
    [SerializeField]
    private bool isDead = false;
    protected override void Start()
    {
        if (isDead)
        {

            for (int i = 0; i < dead.Count; i++)
                dead[i].SetActive(true);

            if (alive.Count > 0)
            {
                for (int i = 0; i < alive.Count; i++)
                    alive[i].SetActive(false);
            }
        }
        else
        {
            if (dead.Count > 0)
            {
                for (int i = 0; i < dead.Count; i++)
                    dead[i].SetActive(false);
            }
            if (alive.Count > 0)
            {
                for (int i = 0; i < alive.Count; i++)
                    alive[i].SetActive(true);
            }

        }
        animator = GetComponent<Animator>();
        button.Instance.append(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetTrigger(movename);
        }
    }

    public override void OnButtonPress()
    {
        base.OnButtonPress();
        if (isDead)
            isDead = false;
        else
            isDead = true;
    }
}
