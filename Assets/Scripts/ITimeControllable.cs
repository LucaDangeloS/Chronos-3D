using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeControllable
{
    public void SetTimeScale(float newTime, bool firstSet = true);
}
