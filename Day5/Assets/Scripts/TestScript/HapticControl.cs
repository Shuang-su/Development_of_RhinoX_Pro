using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HapticControl : MonoBehaviour
{

    public void TouchHaptic()
    {
        
        var controllerlist = new List<InputDevice>();

        //bool triggerValue, gripValue;

        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, controllerlist);

        HapticCapabilities capabilities;

        if (controllerlist.Count == 1)
        {
            InputDevice rightcontroller = controllerlist[0];

                if (rightcontroller.TryGetHapticCapabilities(out capabilities))
                {
                    if (capabilities.supportsImpulse)
                    {
                        uint channel = 0;
                        float amplitude = 0.5f;
                        float duration = 0.5f;
                        rightcontroller.SendHapticImpulse(channel, amplitude, duration);
                    }
                }
            }
        
        }

}
