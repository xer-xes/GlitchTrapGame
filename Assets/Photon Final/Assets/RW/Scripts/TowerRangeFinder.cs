using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

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
    public int health = 500;
    public int maxHealth = 500;
    private bool dead = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(this.gameObject.tag == "Range")
        {       //------------------- FOR LEFT
            if(transform.parent.tag == "LeftTower")
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
            else if (transform.parent.tag == "RightTower")
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
            if (transform.parent.tag == "LeftTower")
            {
                if (collision.gameObject.tag == "Right" ||
                   (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
                {
                    if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2")
                        isPlayerInRange = false;
                    isAttacking = false;
                }
            }
            else if (transform.parent.tag == "RightTower")
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

    [PunRPC]
    public void TakeDamage(int damage)
    {
        this.health -= damage;
        foreach (var health in GetComponentsInChildren<SimpleHealthBar>())
        {
            if (health.type == "Health")
            {
                health.gameObject.GetComponent<Image>().fillAmount = (float)this.health / this.maxHealth;
            }
        }
        if (this.health <= 0)
            dead = true;
        if (dead)
            GetComponent<PhotonView>().RPC("RemoveTower", RpcTarget.All);
    }

    [PunRPC]
    public void RemoveTower()
    {
        PhotonNetwork.Destroy(transform.parent.gameObject);
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
