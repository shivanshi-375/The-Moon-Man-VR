using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InWaterMoveProvider : LocomotionProvider
{
    public bool respawning = false;

    // Update is called once per frame
    void Update()
    {
        if (respawning)
        {
            locomotionPhase = LocomotionPhase.Moving;
        }
        else
        {
            locomotionPhase = LocomotionPhase.Done;
        }
    }
}
