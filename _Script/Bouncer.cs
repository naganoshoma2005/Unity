using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float bounceForce = 10f; // 反発の強さ（調整可能）

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = collision.rigidbody;
            if (rb != null)
            {
                // 法線方向に強い反発力を加える
                Vector3 bounceDirection = collision.contacts[0].normal;
                rb.AddForce(-bounceDirection * bounceForce, ForceMode.Impulse);
            }
        }
    }
}
