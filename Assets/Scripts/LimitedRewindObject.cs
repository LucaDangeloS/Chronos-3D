using LunarCatsStudio.SuperRewinder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class LimitedRewindObject : Rewind3DObject
{
    // This changes the base class to be able to control when an object should register keypoints
    // this allows to return the bridge in level1 without the risk of accidentally losing the time frame
    // the bridge can be rewinded to the point where it can be crossed

    // The flag that allows for the object position recording is controlled externally through other scripts (level1 scripts)

    public bool isRecording = false;

    /// <summary>
    /// Check and apply current action.
    /// </summary>
    void FixedUpdate()
    {
        if (m_isRewinding)
            Rewind();
        else if (isRecording)
        {
            Record();
        }
    }

    /// <summary>
    /// Rewind process.
    /// </summary>
    void Rewind()
    {
        for (int i = 0; i < m_rewindSpeed; i++)
        {
            // we have points in the list for rewind
            if (m_pointsInTime.Count > 0)
            {
                m_currentKeyPoint = m_pointsInTime[0]; // extract next point

                LoadKeyPoint();

                m_pointsInTime.RemoveAt(0); // delete used point
            }
            else
            {
                isRecording = true;
            }
        }
    }
}
