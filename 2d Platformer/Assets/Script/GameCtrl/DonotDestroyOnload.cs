using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonotDestroyOnload : MonoBehaviour
{
    
    private static DonotDestroyOnload instance = null;
    private static DonotDestroyOnload Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if ( instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }


}
