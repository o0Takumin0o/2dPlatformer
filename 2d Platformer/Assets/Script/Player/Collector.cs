using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collector : MonoBehaviour
{
    Rigidbody2D rb2d;
    public GameCtrl GameCtrl;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            GameCtrl.UpdateCoinCount();
            Destroy(other.gameObject);
        }
        
    }
}
