using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour 
{
    public RoomInfo[] roomsList;
    private int number =1;
    public GameObject ourSnakeHead;
    const string _gameVersion = "v0.0.1";

	void Start () 
	{
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
        Debug.Log("start");
	}

    void OnJoinedLobby()
    {
        Debug.Log("joined lobby");
        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("GirlzCamp", roomOptions, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        Debug.Log("joined room");
        //connected
        if (PhotonNetwork.countOfPlayersInRooms == 1)
        {
            number = 2;
        }
        PhotonNetwork.Instantiate(ourSnakeHead.transform.name, new Vector2(0, PhotonNetwork.playerList.Length*2), Quaternion.identity, 0);
        
    }
}
