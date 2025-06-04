using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public SpawnEnemy SE;
    // Start is called before the first frame update
    void Start()
    {
        SE.Destoroyenemy++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
