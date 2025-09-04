using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Attackable")
        {
            var characterController = collision.gameObject
                .GetComponent<CharacterController>();
            characterController?.RunHitAnimation();

            Destroy(gameObject);
        }
    }
}
