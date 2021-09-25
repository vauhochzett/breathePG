using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public bool isMoving;
    public Vector2 input;

    public event Action onEncountered;

    private Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void HandleUpdate() 
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                Vector3 targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveY", input.y);
            }
        }
        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(
            interactPos, 0.3f, GameLayers.i.InteractableLayer);

        if (collider != null)
            collider.GetComponent<Interactable>()?.Interact();

    }

    private void OnMoveOver()
    {
        // (Physics2D.OverlapCircle(transform.position, 0.3f, interactableLayer))

    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(
            targetPos, 0.2f, GameLayers.i.SolidLayer | GameLayers.i.InteractableLayer) == null;
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
