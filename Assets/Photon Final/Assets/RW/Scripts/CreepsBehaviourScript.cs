using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CreepsBehaviourScript : MonoBehaviour
{
    public string type;
    public bool isAttacking = false;
    private GameObject leftRangeBullet;
    [SerializeField] private Transform bulletSpawnPoint;
    public int damage;
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth = 40;
    [SerializeField] private int health = 40;
    private bool dead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("Attack", isAttacking);
        if (!isAttacking && !dead)
            transform.position += Vector3.right * speed * Time.deltaTime;
    }

    public void LaunchBullet()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            leftRangeBullet = PhotonNetwork.Instantiate("LeftRangeBullet", bulletSpawnPoint.position, this.transform.GetComponentInChildren<CreepRangeFinder>().transform.rotation, 0);
            leftRangeBullet.GetComponent<BulletScript>().isForward = this.transform.GetComponentInChildren<CreepRangeFinder>().isFacingForward;
            leftRangeBullet.GetComponent<BulletScript>().damage = this.damage;
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
}
