using UnityEngine;
using Photon.Pun;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool isForward = true;
    public int damage;

	private void Awake()
	{
        StartCoroutine("KillBullet");
	}

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (isForward)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                Debug.Log("Is this running anyway ? " + isForward);
            }
            else
                transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (this.gameObject.tag == "LeftBullet")
            {
                if (collision.gameObject.tag == "Right" ||
                     (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
                {
                    collision.gameObject.GetComponent<PlayerMovement>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, this.damage);
                    GetComponent<PhotonView>().RPC("DeleteBullet", RpcTarget.All);
                }
            }
        }
    }

    private IEnumerator KillBullet()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            yield return new WaitForSecondsRealtime(5);
            GetComponent<PhotonView>().RPC("DeleteBullet", RpcTarget.All);
        }
    }

    [PunRPC]
    public void DeleteBullet()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }
}
