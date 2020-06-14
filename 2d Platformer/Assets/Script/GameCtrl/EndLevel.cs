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
        WinScreen.SetActive(false);
        GameplayUI.SetActive(true);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            WinScreen.SetActive(true);
            GameplayUI.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}
