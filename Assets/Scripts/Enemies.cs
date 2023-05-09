using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int routine;
    public float timer;
    public Animator animator;
    public Quaternion angle;
    public float grade;
    public float minChaseD = 20;
    public int  enemyLife = 100;

    public Transform player;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void enemyBehavior(){
    
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
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
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
                transform.Translate(Vector3.forward * 3 * Time.deltaTime);
                animator.SetBool("attack", false);
            } else {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
                animator.SetBool("attack", true);
              
            }
        }
    }

    public void TakeDamage(int dAmount) {
        enemyLife -= dAmount;
        if(enemyLife <= 0) {
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
        } else {
            animator.SetTrigger("damage");
        }
    }

    void Update(){
        enemyBehavior();
    }
}
