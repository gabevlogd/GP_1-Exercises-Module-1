using System;
using UnityEngine;

public class JumpComponent : MonoBehaviour
{
    public static event Action<Vector3> OnJumpPerformed;


    public void Jump()
    {
        Vector3 jumpForce = new Vector3(0, 500f, 0);
        GetComponent<Rigidbody>().AddForce(jumpForce);
        OnJumpPerformed?.Invoke(jumpForce);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
}
