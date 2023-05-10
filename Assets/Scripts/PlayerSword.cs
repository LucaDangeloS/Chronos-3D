using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other) 
    {

        if(other.tag == "Enemies")
        {
            other.GetComponent<Enemies>().TakeDamage(20);
        }

    }
    
}
