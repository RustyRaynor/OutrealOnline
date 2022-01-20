using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sfs2X;
using Sfs2X.Logging;
using Sfs2X.Util;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    [SerializeField] string IP;
    [SerializeField] int port;

    public SmartFox sfs;

    public int loadedGameScene;

    public bool isHost;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        sfs = new SmartFox(UseWebSocket.WS_BIN);

        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);

        StartCoroutine(Connect());
    }

    void OnApplicationQuit()
    {
        if (sfs != null && sfs.IsConnected)
            sfs.Disconnect();
    }

    IEnumerator Connect()
    {
        yield return new WaitForSeconds(0.1f);

        ConfigData cfg = new ConfigData();
        cfg.Host = IP;
        cfg.Port = port;
        cfg.Zone = "BasicExamples";

        sfs.Connect(cfg);
    }

    void Update()
    {
        if (sfs != null)
            sfs.ProcessEvents();
    }

    public void LogIn(string name)
    {
        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
        sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);

        sfs.Send(new Sfs2X.Requests.LoginRequest(name));
    }

    void OnConnection(BaseEvent evt)
    {
        if ((bool)evt.Params["success"])
        {
            Debug.Log("Connected");        }
        else
        {
            Debug.Log("Failed to connect");
        }
    }

    void OnConnectionLost(BaseEvent evt)
    {
        Debug.Log("Connection Lost " + evt.Type);
    }

    void OnLogin(BaseEvent evt)
    {
        Debug.Log("Log In");
        sfs.RemoveEventListener(SFSEvent.LOGIN, OnLogin);
        JoinRoom(sfs.RoomList[0].Name);
    }

    void OnLoginError(BaseEvent evt)
    {
        Debug.Log("Login Failed");
    }

    public List<Room> GetListOfRooms()
    {
        return sfs.RoomList;
    }

    public List<User> GetListOfUserID()
    {
        return sfs.LastJoinedRoom.UserList;
    }

    public void JoinRoom(string name)
    {
        sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);

        sfs.Send(new JoinRoomRequest(name));
    }

    void OnRoomJoinError(BaseEvent evt)
    {
        Debug.Log("Room Join Failed");
    }

    public void SendPlayerLocation(Vector3 playerLocation, Quaternion playerRot)
    {
        if(sfs.LastJoinedRoom != null)
        {
            SFSObject data = new SFSObject();
            data.PutInt("id", sfs.MySelf.Id);

            data.PutFloat("posX", playerLocation.x);
            data.PutFloat("posY", playerLocation.y);
            data.PutFloat("posZ", playerLocation.z);

            data.PutFloat("rotX", playerRot.x);
            data.PutFloat("rotY", playerRot.y);
            data.PutFloat("rotZ", playerRot.z);
            data.PutFloat("rotW", playerRot.w);

            sfs.Send(new PublicMessageRequest("Player Location Sent", data, sfs.LastJoinedRoom));
        }
    }

    public void CreateRoom(RoomSettings roomSettings)
    {
        sfs.Send(new CreateRoomRequest(roomSettings, true));
    }

    public void StartGame()
    {
        if(sfs.LastJoinedRoom!= null)
        {
            SFSObject data = new SFSObject();
            sfs.Send(new PublicMessageRequest("StartGame", data, sfs.LastJoinedRoom));
        }
    }

    public void SendSceneInfo()
    {
        if(sfs.LastJoinedRoom != null)
        {
            SFSObject data = new SFSObject();
            data.PutInt("sceneNumber", loadedGameScene);
            sfs.Send(new PublicMessageRequest("SceneInfo", data, sfs.LastJoinedRoom));
        }
    }
}
