using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0,25,0);
  
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
