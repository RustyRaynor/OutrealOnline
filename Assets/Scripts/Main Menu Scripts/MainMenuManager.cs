using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sfs2X.Core;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject loginUI;
    [SerializeField] RoomLister roomUI;

    [SerializeField] TMP_InputField nameField; 

    void Start()
    {
        NetworkManager.Instance.sfs.AddEventListener(SFSEvent.CONNECTION, ConnectedToServer);
    }

    private void ConnectedToServer(BaseEvent evt)
    {
        LeanTween.scale(loginUI, Vector3.one, 1f).setEaseSpring();
    }

    public void Login()
    {
        if(nameField.text.Length == 0)
        {
            return;
        }

        NetworkManager.Instance.sfs.AddEventListener(SFSEvent.ROOM_JOIN, LoadLobby);
        NetworkManager.Instance.LogIn(nameField.text);
    }

    void LoadLobby(BaseEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
