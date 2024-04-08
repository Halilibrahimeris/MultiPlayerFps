using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    public GameObject Player;

    [Space]
    public Transform SpawnPoint;

    [Space]
    public GameObject RoomCam;

    private string nickname = "Unknown";

    private void Awake()
    {
        instance = this;

    }

    public void JoinRoomButton()
    {
        Debug.Log("Ba�lan�yor...");

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Servera ba�land�");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);

        Debug.Log("Lobiye ba�lan�ld� ve kat�ld�k");

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("Odaya girildi");
        RoomCam.SetActive(false);
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(Player.name, SpawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetUp>().IsLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
        Debug.Log("Spawn At�ld�");

        _player.GetComponent<PhotonView>().RPC("SetNickName", RpcTarget.AllBuffered, nickname);
    }

    public void ChangeNickName(string _name)
    {
        nickname = _name;
    }
}
