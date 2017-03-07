using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour 
{
    private RoomInfo[] roomsList;

    public GameObject ourSnakeHead;
    public string _gameVersion;

	void Start () 
	{
        ourSnakeHead.SetActive(false);
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
	}

    void OnGUI()
    {
        if(!PhotonNetwork.connected)
        {
            GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        }
        else if(PhotonNetwork.room == null)
        {
            if(GUI.Button(new Rect(100,100,250,100), "Start Server"))
            {
                PhotonNetwork.CreateRoom("GirlzCamp", new RoomOptions() { MaxPlayers = 2 }, null);
            }
            if(roomsList != null)
            {
                for(int i =0; i < roomsList.Length; i++)
                {
                    if(GUI.Button(new Rect(100,250 +(110*i),250,100),"Join this room"))
                    {
                        PhotonNetwork.JoinRoom(roomsList[i].Name);
                    }
                }
            }
        }
    }

    void Update () 
	{
		
	}

    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();
    }

    void OnJoinedRoom()
    {
        //connected
        ourSnakeHead.SetActive(true);
    }
}
