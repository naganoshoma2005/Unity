using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedtime : MonoBehaviour
{
    public int timescale = 1;
    public Text timetext;
    // Start is called before the first frame update
    void Start()
    {
        timescale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timescale;
        timetext.text = "×" + timescale;
    }
    public void OnTapSpeedChange()
    {
        timescale++;
        if(timescale ==4)
        {
            timescale = 1;
        }
    }

   /* public void OnTapSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timescale=1;
        }
    }*/
}
