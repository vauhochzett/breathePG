using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningStage : PlayerController
{
    public void HandleUpdate()
    {
        input.x = 1;
        input.y = Input.GetAxisRaw("Vertical");

        StartCoroutine(character.Move(input));
        character.HandleUpdate();
    }
}
