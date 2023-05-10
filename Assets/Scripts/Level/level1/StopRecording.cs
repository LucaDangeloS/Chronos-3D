using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopRecording : MonoBehaviour
{
    public List<LimitedRewind3DObject> rewindControllers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Rock"))
        {
            rewindControllers.ForEach(rewindController => rewindController.isRecording = false);
        }
    }
}
