using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidLayer;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask portalLayer;

    public static GameLayers i { get; set; }

    private void Awake(){ i = this; }

    public LayerMask InteractableLayer { get => interactableLayer; }

    public LayerMask SolidLayer { get => solidLayer;  }

    public LayerMask PortalLayer {get => portalLayer;}

    public LayerMask TriggerableLayers { get => portalLayer; }

}
