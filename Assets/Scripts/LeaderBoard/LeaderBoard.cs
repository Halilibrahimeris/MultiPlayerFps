using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;
public class LeaderBoard : MonoBehaviour
{
    public GameObject playersHolder;
    public GameObject Head;

    [Header("Opttions")]
    public float refreshRate = 1.0f;

    [Header("UI")]
    public GameObject[] slots;
    [Space]
    public TextMeshProUGUI[] KillscoreTexts;
    public TextMeshProUGUI[] DeathscoreTexts;
    public TextMeshProUGUI[] nameTexts;

    private void Start()
    {
        InvokeRepeating(nameof(Refresh), 1f, refreshRate);
    }

    public void Refresh()
    {
        foreach (var slot in slots) 
        {
            slot.SetActive(false);
        }

        var sortedPlayerList = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

        int i = 0;
        foreach (var player in sortedPlayerList) 
        {
            slots[i].SetActive(true);

            if (player.NickName == "")
                player.NickName = "unnamed";

            nameTexts[i].text = player.NickName;
            KillscoreTexts[i].text = player.GetScore().ToString();
            DeathscoreTexts[i].text = player.GetDeathScore().ToString();

            i++;
        }
    }

    private void Update()
    {
        playersHolder.SetActive(Input.GetKey(KeyCode.Tab));
        Head.SetActive(Input.GetKey(KeyCode.Tab));
    }
}
