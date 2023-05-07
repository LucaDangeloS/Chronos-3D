using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : MonoBehaviour
{

    public bool rotate;
    public bool move;

    public float speed = 15f;
    public float moveSpeed = 4f;
    private float startPositionX;
    // Start is called before the first frame update
    void Start()
    {
        float startPositionX = transform.position[2];

    }

    // Update is called once per frame
    void Update()
    {
        if (rotate) {
        transform.Rotate(0, speed * Time.deltaTime, 0);
        return;
        }

        if (move) {
            // if (startPositionX > transform.position[2]+2 && startPositionX < transform.position[2]-2) {
            //     Debug.Log("atpc");
            // }

            if (transform.position[2] < startPositionX + 2 && transform.position[2] > startPositionX - 2) {
                moveSpeed = -moveSpeed;
            } else {
                Debug.Log("in");
            }


            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            // Debug.Log("move");
        }
    }
}
