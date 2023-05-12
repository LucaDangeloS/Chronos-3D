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
    private float grade;
    private int routine;
    private float timer;
    private Quaternion angle;
    public float minChaseD = 20;
    private float rotationSpeed = 100f;

  
    public float timeScale;


    void Start() {
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
        enemyBehavior();
    }

    void enemyBehavior() {
        if (Vector3.Distance(player.position, animator.transform.position) > minChaseD) {
            animator.SetBool("run", false);
            animator.SetBool("attack", false);
            timer += 1 * Time.deltaTime;
            if (timer >= 4) {
                routine = Random.Range(0,2);
                timer = 0;
            }

            switch (routine) {
                case 0: 
                    animator.SetBool("walk", false);
                    break;
                case 1:
                    grade = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, grade, 0);
                    routine++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, rotationSpeed * timeScale * Time.deltaTime);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime * timeScale);
                    animator.SetBool("walk", true);
                    break;
            }
        } else {
            if (Vector3.Distance(player.position, animator.transform.position) > 2) {
                var lookPos = player.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * timeScale * Time.deltaTime);
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                transform.Translate(Vector3.forward * 3 * Time.deltaTime * timeScale);
                animator.SetBool("attack", false);
            } else {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
                animator.SetBool("attack", true);
            }
        }
    }
}
