using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Vector2 input;
    public bool isMoving;

    public event Action onEncountered;

    protected CharacterAnimator animator;
    protected Character character;


    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    public void HandleUpdate() 
    {
        if (!character.IsMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
            }
        }
        character.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }

    void Interact()
    {
        var facingDir = new Vector3(
            character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(
            interactPos, 0.3f, GameLayers.i.InteractableLayer);

        if (collider != null)
            collider.GetComponent<Interactable>()?.Interact();

    }

    private void OnMoveOver()
    {
        var colliders = Physics2D.OverlapCircleAll(
            transform.position, 0.3f, GameLayers.i.TriggerableLayers);

        foreach( var collider in colliders)
        {
            var triggerable = collider.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {
                triggerable.OnPlayerTriggered(this);
                break;
            }
        }
    }


    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f,
            GameLayers.i.InteractableLayer) != null)
        {
            onEncountered();
        }
    }
}
