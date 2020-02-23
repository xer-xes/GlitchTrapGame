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

        public GameObject ballSpawnTransform;

        private GameObject ball;
        private GameObject player1;
        private GameObject player2;

        // Start Method
        void Start()
        {
            if (!PhotonNetwork.IsConnected) // 1
            {
                SceneManager.LoadScene("Launcher");
                return;
            }

            if (PlayerManager.LocalPlayerInstance == null)
            {
                if (PhotonNetwork.IsMasterClient) // 2
                {
                    Debug.Log("Instantiating Player 1");
                    // 3
                    player1 = PhotonNetwork.Instantiate("Car", player1SpawnPosition.transform.position, player1SpawnPosition.transform.rotation, 0);
                    // 4
                    ball = PhotonNetwork.Instantiate("Ball", ballSpawnTransform.transform.position, ballSpawnTransform.transform.rotation, 0);
                    ball.name = "Ball";
                }
                else // 5
                {
                    player2 = PhotonNetwork.Instantiate("Car", player2SpawnPosition.transform.position, player2SpawnPosition.transform.rotation, 0);
                }
            }
        }

        // Update Method
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //1
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
