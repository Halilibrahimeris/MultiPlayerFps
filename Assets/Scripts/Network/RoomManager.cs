using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject Player;

    [Space]
    public Transform SpawnPoint;

    void Start()
    {
        Debug.Log("Baðlanýyor...");

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Servera baðlandý");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);

        Debug.Log("Lobiye baðlanýldý ve katýldýk");

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("Odaya girildi");

        GameObject _player = PhotonNetwork.Instantiate(Player.name, SpawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetUp>().IsLocalPlayer();
        Debug.Log("Spawn Atýldý");
    }
}
