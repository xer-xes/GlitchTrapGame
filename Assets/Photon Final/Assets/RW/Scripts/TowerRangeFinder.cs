using UnityEngine;
using Photon.Pun;

public class TowerRangeFinder : MonoBehaviour
{
    private bool isShootingForward = true;
    private bool isAttacking = false;
    private float shootCooldown = 3;
    private float time;
    public bool isLeft;
    public int damage;
    private GameObject towerBullet;
    private bool isPlayerInRange = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(this.gameObject.tag == "Range")
        {       //------------------- FOR LEFT
            if(transform.parent.tag == "Left")
            {
                if (collision.gameObject.tag == "Right" ||
                    (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
                {
                    if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2")
                        isPlayerInRange = true;

                    isAttacking = true;

                    if (isPlayerInRange && collision.gameObject.tag == "Right")
                        return;

                    if (isShootingForward && collision.gameObject.transform.position.x < transform.position.x)
                        isShootingForward = false;
                    else if (!isShootingForward && collision.gameObject.transform.position.x > transform.position.x)
                        isShootingForward = true;
                }
            }
            else if (transform.parent.tag == "Right")
            {       //------------------------- FOR RIGHT
                if (collision.gameObject.tag == "Left" ||
                    (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player1"))
                {
                    if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player1")
                        isPlayerInRange = true;

                    isAttacking = true;

                    if (isPlayerInRange && collision.gameObject.tag == "Left")
                        return;

                    if (isShootingForward && collision.gameObject.transform.position.x > transform.position.x)
                        isShootingForward = false;
                    else if (!isShootingForward && collision.gameObject.transform.position.x < transform.position.x)
                        isShootingForward = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(this.gameObject.tag == "Range")
        {        //---------------------- FOR LEFT
            if (transform.parent.tag == "Left")
            {
                if (collision.gameObject.tag == "Right" ||
                   (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
                {
                    if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2")
                        isPlayerInRange = false;
                    isAttacking = false;
                }
            }
            else if (transform.parent.tag == "Right")
            {       //--------------------------- FOR RIGHT
                if (collision.gameObject.tag == "Left" ||
                   (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player1"))
                {
                    if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player1")
                        isPlayerInRange = false;
                    isAttacking = false;
                }
            }
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (isAttacking && time > shootCooldown)
            Shoot();
    }

    private void Shoot()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            time = 0;
            if(isLeft)
            {       //------------------------ FOR LEFT
                towerBullet = PhotonNetwork.Instantiate("TowerLeftBullet", transform.position, transform.rotation, 0);
                towerBullet.GetComponent<BulletScript>().damage = this.damage;
                towerBullet.GetComponent<BulletScript>().isLeft = true;
                towerBullet.GetComponent<BulletScript>().isForward = isShootingForward;
            }
            else
            {
                towerBullet = PhotonNetwork.Instantiate("TowerRightBullet", transform.position, transform.rotation, 0);
                towerBullet.GetComponent<BulletScript>().damage = this.damage;
                towerBullet.GetComponent<BulletScript>().isLeft = false;
                towerBullet.GetComponent<BulletScript>().isForward = isShootingForward;
            }
        }
    }
}
