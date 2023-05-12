using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorDNightMare : Enemies
{
    private float grade;
    private int routine;
    private float timer;
    private Quaternion angle;
    private Enemies enemy;
    public float minChaseD = 20;

    protected override void Start() {
        base.Start();
        enemy = GetComponent<Enemies>();
    }

    protected override void enemyBehavior(Transform player, Animator animator) {
    
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
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                transform.Translate(Vector3.forward * 1 * Time.deltaTime * enemy.timeScale);
                animator.SetBool("walk", true);
                break;
        }
        } else {
            if (Vector3.Distance(player.position, animator.transform.position) > 2) {
                var lookPos = player.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                transform.Translate(Vector3.forward * 3 * Time.deltaTime * enemy.timeScale);
                animator.SetBool("attack", false);
            } else {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
                animator.SetBool("attack", true);
            }
        }
    }
}

