using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject GameplayUI;
    private void Awake()
    {
        Time.timeScale = 1f;
    }
    void OnTriggerEnter(Collider hitCollider)
    {
        if (hitCollider.tag == "Finish")
        {
            WinScreen.SetActive(true);
            GameplayUI.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}
