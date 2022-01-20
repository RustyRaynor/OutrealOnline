using Sfs2X.Entities;
using Sfs2X.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Lobby lobby;
    bool finished;
    private void Start()
    {
  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !finished)
        {
            finished = true;
            JoinGameRoom();
        }
    }


    void JoinGameRoom()
    {
        lobby.LeaveLobby();
    }
}
