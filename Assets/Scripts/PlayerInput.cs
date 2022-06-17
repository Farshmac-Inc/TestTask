using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{ 
    public Action<Vector2> MoveEvent;
    public Action DroppingBombEvent;

    private void Update()
    {
        var moveVector = new Vector2(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
        MoveEvent?.Invoke(moveVector);
        if (Input.GetKeyDown(KeyCode.Space)) 
            DroppingBombEvent?.Invoke();
    }
}
