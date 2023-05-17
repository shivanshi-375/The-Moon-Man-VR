using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FallMoveProvider : LocomotionProvider
{ 
    bool player_is_falling()
    {
        return Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(player_is_falling())
        {
            locomotionPhase = LocomotionPhase.Moving;
        } else
        {
            locomotionPhase = LocomotionPhase.Done;
        }
    }
}
