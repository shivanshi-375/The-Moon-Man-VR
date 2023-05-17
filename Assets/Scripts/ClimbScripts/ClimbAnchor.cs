using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Responsible for getting the velocity container from the controller to the climbing provider so it can be processed.
/// </summary>
public class ClimbAnchor : XRBaseInteractable
{ 

    [SerializeField] private ClimbingProvider climbingprov;

    private Rigidbody selfrb;
    public Rigidbody rb;


    protected void Start()
    {
       
      
    }
    protected override void Awake()
    {
        if (!base.colliders.Contains(this.GetComponent<Collider>()))
        {
            base.colliders.Add(this.GetComponent<Collider>());
        }

        base.Awake();
        FindClimbingProvider();
        selfrb = GetComponent<Rigidbody>();
    }

    private void FindClimbingProvider()
    {
        if (!climbingprov)
        {
            climbingprov = FindObjectOfType<ClimbingProvider>();
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        
        base.OnSelectEntered(args);
        TryAdd(args.interactorObject);
        
    }

    private void TryAdd(IXRSelectInteractor interactor)
    {
       
        if (interactor.transform.TryGetComponent(out VelocityContainer container))
            climbingprov.AddProvider(container);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        
        base.OnSelectExited(args);
        TryRemove(args.interactorObject);
    }

    private void TryRemove(IXRSelectInteractor interactor)
    {
        if (interactor.transform.TryGetComponent(out VelocityContainer container))
            climbingprov.RemoveProvider(container);
    }

    public override bool IsHoverableBy(IXRHoverInteractor interactor)
    {
        return base.IsHoverableBy(interactor) && interactor is XRDirectInteractor;

    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && interactor is XRDirectInteractor;
    }

    public void FallIcicle()
    {
        selfrb.useGravity = true;
        rb.useGravity = true;
    }
}
