using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float _jumpForce;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            CharacterManager.Instance.Player._controller._rb.AddForce(Vector2.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
