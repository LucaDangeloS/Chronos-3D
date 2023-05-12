using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRewindable
{
    float recordedTime { get; set; }
    bool isRewinding { get; set; }
    float maxRecordTime { get; set; }

    void Rewind();
    void StopRewind();
    // Method to update the recordedTime variable
    public void UpdateCooldown();
}
