using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemies : MonoBehaviour
{
   
    private Animator animator;
    private Tween movementTween;
    private Tween rotationTween;
    public Transform player;

  
    public float timeScale;


    protected virtual void  Start() {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void AttackPlayer() {
        player.GetComponent<Damage>().TakeDamage(20);
    }


    void Update(){
        animator.speed = timeScale;
        if (rotationTween != null)
            rotationTween.timeScale = timeScale;
        if (movementTween != null)
            movementTween.timeScale = timeScale;
        enemyBehavior(player, animator);
    }

    protected virtual void enemyBehavior(Transform player, Animator animator) {
        
    }
}
