using UnityEngine;
using Photon.Pun;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool isForward = true;
    public int damage;
    public bool isLeft = true;

	private void Awake()
	{
        StartCoroutine("KillBullet");
	}

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {       //----------------------------- FOR LEFT
            if (isLeft)
            {
                if (isForward)
                    transform.position += Vector3.right * speed * Time.deltaTime;
                else
                    transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {       //------------------------------ FOR RIGHT
                if (isForward)
                    transform.position += Vector3.left * speed * Time.deltaTime;
                else
                    transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {           //----------------------------------- FOR LEFT
            if (this.gameObject.tag == "LeftBullet")
            {
                if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2")
                {
                    collision.gameObject.GetComponent<PlayerMovement>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, this.damage);
                    GetComponent<PhotonView>().RPC("DeleteBullet", RpcTarget.All);
                }
                else if (collision.gameObject.tag == "Right")
                {
                    collision.gameObject.GetComponent<CreepsBehaviourScript>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, this.damage);
                    GetComponent<PhotonView>().RPC("DeleteBullet", RpcTarget.All);
                }
            }
            else if (this.gameObject.tag == "RightArrow")
            {       //------------------------------------ FOR RIGHT  
                if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player1")
                {
                    collision.gameObject.GetComponent<PlayerMovement>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, this.damage);
                    GetComponent<PhotonView>().RPC("DeleteBullet", RpcTarget.All);
                }
                else if (collision.gameObject.tag == "Left")
                {
                    collision.gameObject.GetComponent<CreepsBehaviourScript>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, this.damage);
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
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(this.gameObject);
    }
}
