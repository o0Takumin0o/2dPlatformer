using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collector : MonoBehaviour
{//Update point when player hit "Gem" object.  
    Rigidbody2D rb2d;
    public SaveData saveData;
    public SoundFx soundFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            saveData.UpdatePointCount();
            Destroy(other.gameObject);
            soundFX.Pickup();
        }   
    }
}
