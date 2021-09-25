using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IPlayerTriggerable
{
    public void onPlayerTriggered(PlayerController player)
    {
        Debug.Log("onPlayerTrigger");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
