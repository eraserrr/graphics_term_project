using UnityEngine;
using System.Collections;

public class Energizer : MonoBehaviour
{

    private GameManager gm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gm == null) Debug.Log("Energizer did not find Game Manager!");
    }
    /*
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("hi");
        if (col.name == "thanos")
        {
            Destroy(gameObject);
        }
    }
    */
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        if (col.name == "thanos")
        {
            gm.ScareGhosts();
            Destroy(gameObject);
        }
    }
}
