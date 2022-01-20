using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    bool finished;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !finished)
        {
            Debug.Log("PlayerDead");
            finished = true;
            FindObjectOfType<Game>().PlayerDead();
        }
    }
}
