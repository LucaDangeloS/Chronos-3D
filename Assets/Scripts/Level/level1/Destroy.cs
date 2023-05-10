using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroy : MonoBehaviour
{
    public FixedJoint joint;
    public List<LimitedRewind3DObject> rewindControllers;

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Destroy(joint);
            rewindControllers.ForEach(controller => { controller.enabled = true; controller.isRecording = true; });
        }
    }
}
