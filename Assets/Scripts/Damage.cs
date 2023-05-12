using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Clase para gestionar la vida, la barra de vida y el daño recibido de jugadores y de enemigos.
public class Damage : MonoBehaviour
{
    private int life = 100;
    public Animator animator;
    private Animator player;

    public HealthBar healthBar;

    public GameObject gameOverScreen; 
    public StarterAssetsInputs inputController;
    public AudioSource hitSound;

    void Start()
    {
        Time.timeScale = 1f;
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Animator>();

        if (healthBar != null) {
            healthBar.SetMaxHealth(life);
        }
    }


    //Recibir daño
    public void TakeDamage(int dAmount) {
        if (hitSound != null) {
        hitSound.Play();
        }
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
            //Cuando muere el jugador, damos 3 segundos de "margen" para poder ver la animación de muerte y luego se activa la pantalla de pausa.
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
