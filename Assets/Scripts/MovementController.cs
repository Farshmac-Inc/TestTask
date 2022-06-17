using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private CharacterController _charController;
    [SerializeField] private Animation _animation;


    public void Move(Vector2 moveVector)
    {
        if (moveVector == Vector2.zero)
        {
            _animation.Play("Idle");
            return;
        }
        _animation.Play("Run");
        Rotate(moveVector);
        moveVector *= _moveSpeed;
        var deltaPosition = new Vector3(moveVector.x, 0, moveVector.y);
        deltaPosition *= Time.deltaTime;
        _charController.Move(deltaPosition);
    }

    private void Rotate(Vector2 moveVector)
    {
        moveVector = moveVector.normalized;
        float angle = 0;
        if (moveVector.x >= 0) angle = Mathf.Acos(moveVector.x) * Mathf.Rad2Deg;
        else angle = 180 - Mathf.Acos(-moveVector.x) * Mathf.Rad2Deg;
        angle = (moveVector.y <= 0 ? angle : -angle) + 90;
        transform.Rotate(0, angle - transform.rotation.eulerAngles.y, 0);
    }
}
