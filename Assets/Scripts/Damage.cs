using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private int life = 100;
    public Animator animator;
    private Animator player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }


    public void TakeDamage(int dAmount) {
        life -= dAmount;
        if(life <= 0) {
            animator.SetTrigger("die");
             if (animator != player) { 
                Destroy(gameObject,1.5f);
            }
        } else {
            animator.SetTrigger("damage");
        }
    }


}
