using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collector : MonoBehaviour
{
    Rigidbody2D rb2d;
    public SaveData saveData;
    public SoundFx soundFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            saveData.UpdateCoinCount();
            Destroy(other.gameObject);
            soundFX.Pickup();
        }
        
    }
}
