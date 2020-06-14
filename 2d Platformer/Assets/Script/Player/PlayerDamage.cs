using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    //waiting for damage and helth system
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trap")
        {
            //for now reload scene whan player hit trap
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
