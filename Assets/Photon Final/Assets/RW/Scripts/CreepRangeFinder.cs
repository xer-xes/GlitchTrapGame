using UnityEngine;

public class CreepRangeFinder : MonoBehaviour
{
    public bool isFacingForward = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Range")
        {
            if (collision.gameObject.tag == "Right" ||
                 (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
            {
                transform.parent.GetComponent<CreepsBehaviourScript>().isAttacking = true;
                if (collision.gameObject.tag == "Right")
                    collision.gameObject.GetComponent<CreepsBehaviourScript>().isAttacking = true;
                
                if(isFacingForward && transform.parent.position.x > collision.gameObject.transform.position.x)
                {
                    transform.parent.localScale = new Vector3(-1 * transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
                    isFacingForward = false;
                }
                else if(!isFacingForward && transform.parent.position.x < collision.gameObject.transform.position.x)
                {
                    transform.parent.localScale = new Vector3(-1 * transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
                    isFacingForward = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Range")
        {
            if (collision.gameObject.tag == "Right" ||
                (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
            {
                transform.parent.GetComponent<CreepsBehaviourScript>().isAttacking = false;
                if (collision.gameObject.tag == "Right")
                    collision.gameObject.GetComponent<CreepsBehaviourScript>().isAttacking = false;
                
            }
            if (!isFacingForward)
            {
                transform.parent.localScale = new Vector3(-1 * transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
                isFacingForward = true;
            }
        }
    }
}
