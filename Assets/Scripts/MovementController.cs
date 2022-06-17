using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private CharacterController _charController;


    public void Move(Vector2 moveVector)
    {
        moveVector *= _moveSpeed;
        var deltaPosition = new Vector3(moveVector.x, 0, moveVector.y);
        deltaPosition *= Time.deltaTime;
        _charController.Move(deltaPosition);
    }
}
