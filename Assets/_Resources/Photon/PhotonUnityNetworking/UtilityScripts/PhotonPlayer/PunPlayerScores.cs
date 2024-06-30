// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PunPlayerScores.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Utilities,
// </copyright>
// <summary>
//  Scoring system for PhotonPlayer
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Photon.Pun.UtilityScripts
{
    /// <summary>
    /// Scoring system for PhotonPlayer
    /// </summary>
    public class PunPlayerScores : MonoBehaviour
    {
        public const string PlayerScoreProp = "score";
        public const string PlayerDeathScoreProp = "deathscore";
    }

    public static class ScoreExtensions
    {
        public static void SetScore(this Player player, int newScore)
        {
            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[PunPlayerScores.PlayerScoreProp] = newScore;

            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        }

        public static void AddScore(this Player player, int scoreToAddToCurrent)
        {
            int current = player.GetScore();
            Debug.Log($"Current Score: {current}");
            current = current + scoreToAddToCurrent;

            Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
            score[PunPlayerScores.PlayerScoreProp] = current;

            Debug.Log($"New Score: {current}");
            player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.

            // Optional: Log the player's custom properties to see if they are being set correctly
            Debug.Log($"Player Custom Properties after SetCustomProperties: {player.CustomProperties[PunPlayerScores.PlayerScoreProp]}");
        }

        public static void SetDeathScore(this Player player, int newDeathScore)
        {
            Hashtable deathScore = new Hashtable();  // using PUN's implementation of Hashtable
            deathScore[PunPlayerScores.PlayerDeathScoreProp] = newDeathScore;

            player.SetCustomProperties(deathScore);  // this locally sets the death score and will sync it in-game asap.
        }

        public static void AddDeathScore(this Player player, int deathScoreToAddToCurrent)
        {
            int current = player.GetDeathScore();
            current = current + deathScoreToAddToCurrent;

            Hashtable deathScore = new Hashtable();  // using PUN's implementation of Hashtable
            deathScore[PunPlayerScores.PlayerDeathScoreProp] = current;

            player.SetCustomProperties(deathScore);  // this locally sets the death score and will sync it in-game asap.
        }

        public static int GetScore(this Player player)
        {
            object score;
            if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerScoreProp, out score))
            {
                return (int)score;
            }

            return 0;
        }

        public static int GetDeathScore(this Player player)
        {
            object deathScore;
            if (player.CustomProperties.TryGetValue(PunPlayerScores.PlayerDeathScoreProp, out deathScore))
            {
                return (int)deathScore;
            }

            return 0;
        }
    }
}
