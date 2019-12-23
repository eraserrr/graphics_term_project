using UnityEngine;
using System.Collections;

public class Pac : MonoBehaviour
{
    private GameManager gm;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("thanos");
        if (other.name == "thanos")
        {
            GameManager.score += 50;
            GameObject[] pacdots = GameObject.FindGameObjectsWithTag("pacdot");
            Destroy(gameObject);

            if (pacdots.Length == 1)
            {
                GameObject.FindObjectOfType<GameGUINavigation>().LoadLevel();
            }
        }
    }
}
