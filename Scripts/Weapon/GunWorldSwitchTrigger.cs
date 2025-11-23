using UnityEngine;
using System;

public class GunWorldSwitchTrigger : MonoBehaviour
{
    public static Action forceWorldSwtich;
    
    // triggered in the start animation
    public void OnForceWorldSwitch()
    {
        forceWorldSwtich?.Invoke();
    }
}
