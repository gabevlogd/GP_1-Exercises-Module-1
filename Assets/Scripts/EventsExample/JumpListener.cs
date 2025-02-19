using UnityEngine;

public class JumpListener : MonoBehaviour
{
    private void OnEnable()
    {
        JumpComponent.OnJumpPerformed += ReproducePlayerJump; // mi iscrivo all evento di salto
    }

    private void OnDisable()
    {
        JumpComponent.OnJumpPerformed -= ReproducePlayerJump; // mi diiscrivo dall evento di salto
    }

    private void ReproducePlayerJump(Vector3 JumpForce)
    {
        GetComponent<Rigidbody>().AddForce(JumpForce);
    }

}
