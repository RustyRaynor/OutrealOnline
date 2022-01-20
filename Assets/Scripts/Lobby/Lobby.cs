using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Lobby : MonoBehaviour
{
    [SerializeField] Transform startingPosition;
    [SerializeField] PlayerMovement playerPrefab;
    [SerializeField] OtherPlayer otherPlayerPrefab;

    [SerializeField] List<string> scenes;

    List<OtherPlayer> otherPlayers;

    PlayerMovement player;

    void Start()
    {
        NetworkManager.Instance.sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoomInLobby);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.ROOM_ADD, OnRoomAdded);
       NetworkManager.Instance.sfs.AddEventListener(SFSEvent.PUBLIC_MESSAGE, MessageRecieved);

        otherPlayers = new List<OtherPlayer>();
        List<User> playerIDs = NetworkManager.Instance.sfs.LastJoinedRoom.UserList;

        for (int i = 0; i < playerIDs.Count; i++)
        {
            if(playerIDs[i].Id != NetworkManager.Instance.sfs.MySelf.Id)
            {
                otherPlayers.Add(Instantiate(otherPlayerPrefab, startingPosition));
                otherPlayers[otherPlayers.Count - 1].id = playerIDs[i].Id;
                otherPlayers[otherPlayers.Count - 1].nameText.text = playerIDs[i].Name;
            }
        }

        player = Instantiate(playerPrefab, startingPosition);
    }

    void OnConnectionLost(BaseEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void OnRoomJoin(BaseEvent evt)
    {
        if (NetworkManager.Instance.isHost)
        {
            Debug.Log("Hosting and loading scene");

            NetworkManager.Instance.sfs.RemoveAllEventListeners();
            int sceneSelected = Random.Range(0, scenes.Count);

            NetworkManager.Instance.loadedGameScene = sceneSelected;
            Addressables.LoadSceneAsync(scenes[sceneSelected], UnityEngine.SceneManagement.LoadSceneMode.Single).Completed += (handle) => { Debug.Log("SceneLoaded"); };
        }
    }

    void OnRoomJoinError(BaseEvent evt)
    {
        Debug.Log("Failed To Join Game Room");
    }

    void OnUserEnterRoom(BaseEvent evt)
    {
        User newUser = (User)evt.Params["user"];
        otherPlayers.Add(Instantiate(otherPlayerPrefab, startingPosition));
        otherPlayers[otherPlayers.Count - 1].id = newUser.Id;
        otherPlayers[otherPlayers.Count - 1].nameText.text = newUser.Name;
    }

    void OnUserExitRoomInLobby(BaseEvent evt)
    {
        User newUser = (User)evt.Params["user"];

        for (int i = 0; i < otherPlayers.Count; i++)
        {
            if(otherPlayers[i].id == newUser.Id)
            {
                Destroy(otherPlayers[i].gameObject);
                otherPlayers.RemoveAt(i);
                return;
            }
        }
    }

    void OnUserExitRoomAfterLobby(BaseEvent evt)
    {
        User newUser = (User)evt.Params["user"];

        if(newUser.Id == NetworkManager.Instance.sfs.MySelf.Id)
        {
            List<Room> rooms = NetworkManager.Instance.GetListOfRooms();

            for (int i = 1; i < rooms.Count; i++)
            {
                if (rooms[i].UserCount != 6)
                {
                    NetworkManager.Instance.isHost = false;
                    NetworkManager.Instance.JoinRoom(rooms[i].Name);
                    return;
                }
            }

            NetworkManager.Instance.isHost = true;

            RoomSettings settings = new RoomSettings(NetworkManager.Instance.sfs.MySelf.Name + "'s game");
            settings.GroupId = "games";
            settings.IsGame = true;
            settings.MaxUsers = 6;

            NetworkManager.Instance.CreateRoom(settings);
        }
    }

    void MessageRecieved(BaseEvent evt)
    {
        SFSObject data = (SFSObject)evt.Params["data"];

        if(data.GetInt("id") != NetworkManager.Instance.sfs.MySelf.Id)
        {
            switch ((string)evt.Params["message"])
            {
                case "Player Location Sent":
                    for (int i = 0; i < otherPlayers.Count; i++)
                    {
                        if (otherPlayers[i].id == data.GetInt("id"))
                        {
                            otherPlayers[i].transform.position = new Vector3(data.GetFloat("posX"), data.GetFloat("posY"), data.GetFloat("posZ"));
                            otherPlayers[i].transform.rotation = new Quaternion(data.GetFloat("rotX"), data.GetFloat("rotY"), data.GetFloat("rotZ"), data.GetFloat("rotW"));
                            return;
                        }
                    }
                    break;
                case "SceneInfo":
                    Debug.Log("Got scene info and loading scene");
                    NetworkManager.Instance.sfs.RemoveAllEventListeners();
                    NetworkManager.Instance.loadedGameScene = data.GetInt("sceneNumber");
                    Addressables.LoadSceneAsync(scenes[data.GetInt("sceneNumber")], UnityEngine.SceneManagement.LoadSceneMode.Single).Completed += (handle) => { Debug.Log("SceneLoaded"); };
                    break;
            }
        }
    }

    void OnRoomAdded(BaseEvent evt)
    {

    }

    public void LeaveLobby()
    {
        player.cantMove = true;
        player.enabled = false;

        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoomInLobby);
        NetworkManager.Instance.sfs.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoomAfterLobby);
        NetworkManager.Instance.sfs.Send(new LeaveRoomRequest());
    }

    private void OnDestroy()
    {
        Debug.Log("Lobby Callbacks Removed");
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoomAfterLobby);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoomInLobby);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.ROOM_ADD, OnRoomAdded);
        NetworkManager.Instance.sfs.RemoveEventListener(SFSEvent.PUBLIC_MESSAGE, MessageRecieved);
    }
}