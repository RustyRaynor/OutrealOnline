using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPositions;
    [SerializeField] PlayerMovement playerPrefab;
    [SerializeField] OtherPlayer otherPlayerPrefab;
    [SerializeField] TMPro.TextMeshProUGUI preGameText;
    [SerializeField] GameObject lava;
    [SerializeField] GameObject winUI;
    [SerializeField] float maxLavaHeight;

    List<OtherPlayer> otherPlayers;

    int currentPos;

    enum GameState { preGame, inGame}
    GameState gameState;

    bool dead;

    PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoomInGame);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.ROOM_ADD, OnRoomAdded);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.PUBLIC_MESSAGE, MessageRecieved);
      
       otherPlayers = new List<OtherPlayer>();
       List<User> playerIDs = NetworkManager.Instance.sfs.LastJoinedRoom.UserList;
      
       for (int i = 0; i < playerIDs.Count; i++)
       {
           if (playerIDs[i].Id != NetworkManager.Instance.sfs.MySelf.Id)
           {
               otherPlayers.Add(Instantiate(otherPlayerPrefab, spawnPositions[i]));
               otherPlayers[otherPlayers.Count - 1].id = playerIDs[i].Id;
               otherPlayers[otherPlayers.Count - 1].nameText.text = playerIDs[i].Name;
           }
           else
           {
               player = Instantiate(playerPrefab, spawnPositions[i]);
           }
      
           currentPos = i;
       }

        player.cantMove = true;
      
       preGameText.text = "Waiting for players... \n \n" + playerIDs.Count + " / 3";
      
       if(playerIDs.Count == 3)
       {      
           NetworkManager.Instance.StartGame();
       }
    }


    IEnumerator CountDown()
    {
        float seconds = 5;

        while(seconds != 0)
        {
            preGameText.text = "Game starts in \n \n" + seconds;
            yield return new WaitForSeconds(1);
            seconds--;
        }

        StartGame();
    }

    void StartGame()
    {
        gameState = GameState.inGame;

        player.cantMove = false;
        preGameText.enabled = false;

        LeanTween.moveLocalY(lava, maxLavaHeight, 25);

        StartCoroutine(GameWin());
    }

    IEnumerator GameWin()
    {
        yield return new WaitForSeconds(30);

        LeanTween.scale(winUI, Vector3.one, 1);

        yield return new WaitForSeconds(3);

        PlayerDead();
    }

    void OnConnectionLost(BaseEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void OnRoomJoin(BaseEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }

    void OnRoomJoinError(BaseEvent evt)
    {
        Debug.Log("Failed To Join Lobby Room");
    }

    void OnUserEnterRoom(BaseEvent evt)
    {
        if(NetworkManager.Instance.isHost)
        {
            Debug.Log("Sending scene info");
            NetworkManager.Instance.SendSceneInfo();
        }

        currentPos++;

        User newUser = (User)evt.Params["user"];
        otherPlayers.Add(Instantiate(otherPlayerPrefab, spawnPositions[currentPos]));
        otherPlayers[otherPlayers.Count - 1].id = newUser.Id;
        otherPlayers[otherPlayers.Count - 1].nameText.text = newUser.Name;

        NetworkManager.Instance.SendPlayerLocation(player.transform.position, player.transform.rotation);

        preGameText.text = "Waiting for players... \n \n" + NetworkManager.Instance.sfs.LastJoinedRoom.UserList.Count + " / 3";
    }

    void OnUserExitRoomInGame(BaseEvent evt)
    {
        List<User> playerIDs = NetworkManager.Instance.sfs.LastJoinedRoom.UserList;

        if(playerIDs[0].Id == NetworkManager.Instance.sfs.MySelf.Id)
        {
            NetworkManager.Instance.isHost = true;
        }

        currentPos--;
        User newUser = (User)evt.Params["user"];

        for (int i = 0; i < otherPlayers.Count; i++)
        {
            if (otherPlayers[i].id == newUser.Id)
            {
                Destroy(otherPlayers[i].gameObject);
                otherPlayers.RemoveAt(i);
                return;
            }
        }

        switch (gameState)
        {
            case GameState.preGame:
                for (int i = 0; i < playerIDs.Count; i++)
                {
                    if(playerIDs[i].Id == NetworkManager.Instance.sfs.MySelf.Id)
                    {
                        playerPrefab.transform.position = spawnPositions[i].position;
                        return;
                    }
                }

                preGameText.text = "Waiting for players... \n \n" + playerIDs.Count + " / 3";
                break;
        }
    }

    void OnUserExitRoomAfterGame(BaseEvent evt)
    {
        User newUser = (User)evt.Params["user"];
        if(newUser.Id == NetworkManager.Instance.sfs.MySelf.Id)
        {
            NetworkManager.Instance.JoinRoom("The Lobby");
        }
    }

    public void PlayerDead()
    {
        StopAllCoroutines();

        LeanTween.cancel(lava);

        player.cantMove = true;
        player.enabled = false;

        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoomInGame);
        NetworkManager.Instance.sfs.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoomAfterGame);
        NetworkManager.Instance.sfs.Send(new Sfs2X.Requests.LeaveRoomRequest());
    }

    void MessageRecieved(BaseEvent evt)
    {
        SFSObject data = (SFSObject)evt.Params["data"];

        switch ((string)evt.Params["message"])
            {
                case "StartGame":
                    StartCoroutine(CountDown());
                    break;
                case "Player Location Sent":
                    if(data.GetInt("id") != NetworkManager.Instance.sfs.MySelf.Id)
                    {
                        for (int i = 0; i < otherPlayers.Count; i++)
                        {
                            if (otherPlayers[i].id == data.GetInt("id"))
                            {
                                otherPlayers[i].transform.position = new Vector3(data.GetFloat("posX"), data.GetFloat("posY"), data.GetFloat("posZ"));
                                otherPlayers[i].transform.rotation = new Quaternion(data.GetFloat("rotX"), data.GetFloat("rotY"), data.GetFloat("rotZ"), data.GetFloat("rotW"));
                                return;
                            }
                        }
                    }
                   break;
            }
    }

    void OnRoomAdded(BaseEvent evt)
    {

    }

    private void OnDestroy() 
    {
        Debug.Log("Game Callbacks Removed");
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoomAfterGame);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoomInGame);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.ROOM_ADD, OnRoomAdded);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.PUBLIC_MESSAGE, MessageRecieved);
    }
}
