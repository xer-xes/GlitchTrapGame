using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
   
    public string player;
    private bool isFacingForward = true;
    public int health = 100;
    public int maxHealth = 100;
    private float horizontal;
    public bool dead = false;
    public int damage;
    private int experience = 0;
    private int newExperienceLevel = 50;
    private int level = 1;

    private void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            this.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
            GetComponent<PhotonView>().RPC("SetPlayerName", RpcTarget.All);
        }
    }

    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            if (!GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack") && !dead)
                horizontal = Input.GetAxisRaw("Horizontal");
            else
                horizontal = 0;

            if (player == "Player1")
            {       //----------------------------- FOR PLAYER 1
                if (horizontal < 0 && isFacingForward)
                {
                    transform.localScale = new Vector3(-1 * transform.localScale.x , transform.localScale.y, transform.localScale.z);
                    isFacingForward = false;
                }
                else if (horizontal > 0 && !isFacingForward)
                {
                    transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    isFacingForward = true;
                }
            }
            else if(player == "Player2")
            {       //----------------------------- FOR PLAYER2
                if (horizontal > 0 && isFacingForward)
                {
                    transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    isFacingForward = false;
                }
                else if (horizontal < 0 && !isFacingForward)
                {
                    transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    isFacingForward = true;
                }
            }

            if(!GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                transform.position += new Vector3(horizontal, 0, 0) * Time.deltaTime * movementSpeed;
            GetComponentInChildren<AttackManager>().GetComponent<PhotonView>().RPC("Walking", RpcTarget.All, (int)horizontal);

            if (Input.GetKeyDown(KeyCode.Space))
                GetComponentInChildren<AttackManager>().GetComponent<PhotonView>().RPC("Attack", RpcTarget.All);

            if (isFacingForward)
                GetComponentInChildren<TextMesh>().transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            else
                GetComponentInChildren<TextMesh>().transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        this.health -= damage;
        foreach (var health in GetComponentsInChildren<SimpleHealthBar>())
            if (health.type == "Health")
                health.gameObject.GetComponent<Image>().fillAmount = (float)this.health / this.maxHealth;
        if (this.health <= 0)
        {
            dead = true;
            GetComponentInChildren<AttackManager>().GetComponent<PhotonView>().RPC("Dead", RpcTarget.All);
        }
    }

    [PunRPC]
    public void GainExperience(int experience)
    {
        Debug.Log("Experience Gained" + experience);
        this.experience += experience;
        if (this.experience >= newExperienceLevel)        //--------------------- Level Up
        {
            level++;
            this.experience -= newExperienceLevel;
            newExperienceLevel *= 2;
            foreach (TextMesh name in GetComponentsInChildren<TextMesh>())
                if (name.gameObject.tag != "NameText")
                    name.text = level.ToString();
        }
        foreach (var exp in GetComponentsInChildren<SimpleHealthBar>())
            if (exp.type == "Exp")
                exp.gameObject.GetComponent<Image>().fillAmount = (float)this.experience / this.newExperienceLevel;
        Debug.Log("Experience Level : " + level);
    }

    [PunRPC]
    private void SetPlayerName()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            foreach (TextMesh name in GetComponentsInChildren<TextMesh>())
                if (name.gameObject.tag == "NameText")
                    name.text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            foreach (TextMesh name in GetComponentsInChildren<TextMesh>())
                if (name.gameObject.tag == "NameText")
                    name.text = PhotonNetwork.PlayerListOthers[0].NickName;
        }
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
