using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CreepsBehaviourScript : MonoBehaviour
{
    public string type;
    public bool isAttacking = false;
    private GameObject RangeBullet;
    [SerializeField] private Transform bulletSpawnPoint;
    public int damage;
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth = 40;
    [SerializeField] private int health = 40;
    public bool dead = false;
    public bool isLeft = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null || collision.gameObject.GetComponent<CreepsBehaviourScript>() != null) 
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("Attack", isAttacking);
        if (!isAttacking && !dead)
        {
            if(isLeft)
                transform.position += Vector3.right * speed * Time.deltaTime;
            else
                transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    public void LaunchBullet()
    {                                               //------------------------  Called through ranged creep attack animation event
        if (PhotonNetwork.IsMasterClient)
        {       //-------------------------- FOR LEFT BULLET
            if (isLeft)
            {
                RangeBullet = PhotonNetwork.Instantiate("LeftRangeBullet", bulletSpawnPoint.position, 
                    this.transform.GetComponentInChildren<CreepRangeFinder>().transform.rotation, 0);
                RangeBullet.GetComponent<BulletScript>().isForward = this.transform.GetComponentInChildren<CreepRangeFinder>().isFacingForward;
                RangeBullet.GetComponent<BulletScript>().damage = this.damage;
                RangeBullet.GetComponent<BulletScript>().isLeft = true;
            }
            else
            {       //--------------------------- FOR RIGHT BULLET
                RangeBullet = PhotonNetwork.Instantiate("RightRangeArrow", bulletSpawnPoint.position,
                    this.transform.GetComponentInChildren<CreepRangeFinder>().transform.rotation, 0);
                RangeBullet.transform.Rotate(0, 0, -15);
                RangeBullet.GetComponent<BulletScript>().isForward = this.transform.GetComponentInChildren<CreepRangeFinder>().isFacingForward;
                RangeBullet.GetComponent<BulletScript>().damage = this.damage;
                RangeBullet.GetComponent<BulletScript>().isLeft = false;
            }
        }
    }

    public void AttackDamage()
    {                                                   //-------------------- Called through melee creep attack animation event
        if (PhotonNetwork.IsMasterClient)
        {
            if (gameObject.tag == "Left")
            {
                if (GetComponentInChildren<CreepRangeFinder>().target == "Player")
                    foreach (PlayerMovement player in FindObjectsOfType<PlayerMovement>())
                        if (player.player == "Player2")
                            player.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, this.damage);
            }
            if (gameObject.tag == "Right")
            {
                if (GetComponentInChildren<CreepRangeFinder>().target == "Player")
                    foreach (PlayerMovement player in FindObjectsOfType<PlayerMovement>())
                        if (player.player == "Player1")
                            player.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, this.damage);
            }
            if (GetComponentInChildren<CreepRangeFinder>().target == "Creep")
                GetComponentInChildren<CreepRangeFinder>().hitCollider.GetComponent<CreepsBehaviourScript>().
                    GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, this.damage);
            if (GetComponentInChildren<CreepRangeFinder>().target == "Tower")
                GetComponentInChildren<CreepRangeFinder>().hitCollider.GetComponentInChildren<TowerRangeFinder>().
                    GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, this.damage);
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
        {
            dead = true;
            GetComponent<PhotonView>().RPC("Dead", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Dead()
    {
        GetComponent<Animator>().SetTrigger("Dead");
    }

    [PunRPC]
    public void RemoveCreep()
    {
        PhotonNetwork.Destroy(this.gameObject);
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
