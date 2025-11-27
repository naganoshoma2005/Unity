using UnityEngine;
using System.Collections.Generic; 

public class BouncerWall : MonoBehaviour
{
    public float bounceForce = 10f; 
    public float requiredContactTime = 2f; 

    private Dictionary<GameObject, float> contactingBalls = new Dictionary<GameObject, float>();
    private Dictionary<GameObject, Vector3> initialContactNormals = new Dictionary<GameObject, Vector3>(); 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (!contactingBalls.ContainsKey(collision.gameObject))
            {
                contactingBalls.Add(collision.gameObject, Time.time);
               
                initialContactNormals.Add(collision.gameObject, collision.contacts[0].normal);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (contactingBalls.ContainsKey(collision.gameObject))
            {
                contactingBalls.Remove(collision.gameObject);
                initialContactNormals.Remove(collision.gameObject);
            }
        }
    }

    private void Update()
    {
        
        List<GameObject> ballsToBounce = new List<GameObject>();

        foreach (var entry in contactingBalls)
        {
            GameObject ball = entry.Key;
            float contactStartTime = entry.Value;

            if (Time.time - contactStartTime >= requiredContactTime)
            {
                Rigidbody rb = ball.GetComponent<Rigidbody>();
                if (rb != null && initialContactNormals.ContainsKey(ball))
                {
                    Vector3 bounceDirection = initialContactNormals[ball];
                    rb.AddForce(-bounceDirection * bounceForce, ForceMode.Impulse);
                    ballsToBounce.Add(ball); 
                }
            }
        }

        
        foreach (GameObject ball in ballsToBounce)
        {
            contactingBalls.Remove(ball);
            initialContactNormals.Remove(ball);
        }
    }
}