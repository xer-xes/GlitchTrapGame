using UnityEngine;
using Photon.Pun;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        this.transform.localScale = new Vector3(transform.parent.transform.localScale.x * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.parent.gameObject.GetComponent<PhotonView>().IsMine)
        {
            if (this.gameObject.tag == "LeftBullet")
            {
                if (collision.gameObject.tag == "Right" ||
                     (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
                {
                    Debug.Log("Calling function");
                    collision.gameObject.GetComponent<PlayerMovement>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
