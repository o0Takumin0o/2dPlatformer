using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUP_PressTo : MonoBehaviour
{
    
    public GameObject GuideTxt;
    private void Start()
    {
        GuideTxt.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GuideTxt.gameObject.SetActive(true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GuideTxt.gameObject.SetActive(false);
        }
    }
}
