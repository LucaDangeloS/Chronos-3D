using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
  
    public bool isAttack;
  
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Enemies" && isAttack)
        {
            other.GetComponent<Damage>().TakeDamage(20);
        }
        isAttack = false;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAttack = true;
        }
        
    }
}
