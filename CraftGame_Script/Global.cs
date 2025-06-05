using UnityEngine;

public class Global : SingletonMonoBehaviour<Global>
{
    private void Awake()
    {
        if(this!= Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public int SaveNumber;
    public bool Load;
   

    
}
