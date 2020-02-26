using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public GameObject winnerUI;
        public GameObject player1SpawnPosition;
        public GameObject player2SpawnPosition;
        public GameObject player1;
        public GameObject player2;

        void Start()
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Launcher");
                return;
            }

            if (PlayerManager.LocalPlayerInstance == null)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    player1 = PhotonNetwork.Instantiate("Player1", player1SpawnPosition.transform.position, player1SpawnPosition.transform.rotation, 0);
                }
                else
                {
                    player2 = PhotonNetwork.Instantiate("Player2", player2SpawnPosition.transform.position, player2SpawnPosition.transform.rotation, 0);
                }
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        // Photon Methods
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Launcher");
            }
        }

        // Helper Methods
        public void QuitRoom()
        {
            Application.Quit();
        }
    }
}
