using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoppac : MonoBehaviour
{
    private GameManager gm;
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (other.name == "thanos")
        {
            GameObject[] pacdots = GameObject.FindGameObjectsWithTag("pacdot");
            this.gameObject.tag = "Blinky";
            Debug.Log(this.gameObject.tag);
            if (pacdots.Length == 1)
            {
                GameObject.FindObjectOfType<GameGUINavigation>().LoadLevel();
            }
            Destroy(this.gameObject);
        }
    }
}
