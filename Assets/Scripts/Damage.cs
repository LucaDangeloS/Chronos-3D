using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private int life = 100;
    public Animator animator;
    private Animator player;

    public HealthBar healthBar;

    public GameObject gameOverScreen; 
    public StarterAssetsInputs inputController;

    void Start()
    {
        Time.timeScale = 1f;
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Animator>();

        if (healthBar != null) {
            healthBar.SetMaxHealth(life);
        }
    }


    public void TakeDamage(int dAmount) {
        life -= dAmount;
        
        if (healthBar != null) {
            healthBar.SetHealth(life);
        }
        if(life <= 0) {
            animator.SetTrigger("die");
            if (animator != player) { 
                Destroy(gameObject,1.5f);
                return;
            }

            StartCoroutine(DelayGameOverScreen(3f));
            inputController.setGamePaused(true);


        } else {
            animator.SetTrigger("damage");
        }
    }


    private void gameOver() {
        gameOverScreen.SetActive(true);
    }


    IEnumerator DelayGameOverScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f;
        gameOver();
    }

}
