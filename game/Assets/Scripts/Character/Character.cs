using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed;
    CharacterAnimator animator;
    public bool IsMoving;

    public CharacterAnimator Animator { get => animator; }
    
    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
        SetPositionAndSnapToTile(transform.position);
    }

    public void SetPositionAndSnapToTile(Vector2 pos)
    {
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.8f;

        transform.position = pos;
    }


    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }

    public IEnumerator Move(Vector3 moveVec, Action OnMoveOver=null)
    {
        animator.MoveX = moveVec.x;
        animator.MoveY = moveVec.y;

        Vector3 targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        if (!IsWalkable(targetPos))
            yield break;

        IsMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        IsMoving = false;

        OnMoveOver?.Invoke();
    }
    private bool IsWalkable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(
            targetPos, 0.2f, GameLayers.i.SolidLayer | GameLayers.i.InteractableLayer) == null;
    }
}
