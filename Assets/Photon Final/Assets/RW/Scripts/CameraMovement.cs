using UnityEngine;
using Photon.Pun;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject camera;

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
            camera.transform.position = new Vector3(transform.position.x, 5, -10);
    }
}
