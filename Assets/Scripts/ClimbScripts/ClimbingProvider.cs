using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// We average the velocity of the controllers and move the origin via a Character Controller or transform.
/// </summary>
public class ClimbingProvider : LocomotionProvider
{

   

    private bool isClimbing = false;
    private Rigidbody rb;

    private List<VelocityContainer> activeVelocities = new List<VelocityContainer>();

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    private void FindCharacterController()
    {

    }

    public void AddProvider(VelocityContainer provider)
    {
        if (!activeVelocities.Contains(provider))
            activeVelocities.Add(provider);
    }

    public void RemoveProvider(VelocityContainer provider)
    {
        if (activeVelocities.Contains(provider))
            activeVelocities.Remove(provider);
    }

    private void Update()
    {
        TryBeginClimb();

        if (isClimbing)
            ApplyVelocity();

        TryEndClimb();
    }

    private void TryBeginClimb()
    {
        
        
        if (CanClimb() && BeginLocomotion())
        {
            
            isClimbing = true; 
         rb.useGravity = false;
        }

    }

    private void TryEndClimb()
    {
        if (!CanClimb() && EndLocomotion())
        { isClimbing = false;
            rb.useGravity = true;
        }
    }

    private bool CanClimb()
    {
        if(activeVelocities.Count > 0)
        {
            
        }
        
        return activeVelocities.Count != 0;
        
    }

    private void ApplyVelocity()
    {
        
        Vector3 velocity = CollectControllerVelocity();
        Transform origin = system.xrOrigin.transform;

        velocity = origin.TransformDirection(velocity);

        velocity *= Time.deltaTime;

        origin.position -= velocity;
    }

    private Vector3 CollectControllerVelocity()
    {

        Vector3 totalVelocity = Vector3.zero;

        foreach (VelocityContainer container in activeVelocities)
            totalVelocity += container.Velocity;
        return totalVelocity;
    }
}
