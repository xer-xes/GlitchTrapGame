using UnityEngine;
// TODO : Sort the continuous flipping of the sprite.
public class CreepRangeFinder : MonoBehaviour
{
    public bool isFacingForward = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Range")
        {       //------------------------------ FOR LEFT
            if (transform.parent.tag == "Left")
            {
                if (collision.gameObject.tag == "Right" ||
                     (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
                {
                    transform.parent.GetComponent<CreepsBehaviourScript>().isAttacking = true;

                    if (isFacingForward && transform.parent.position.x > collision.gameObject.transform.position.x)
                    {
                        transform.parent.localScale = new Vector3(-1 * transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
                        isFacingForward = false;
                    }
                    else if (!isFacingForward && transform.parent.position.x < collision.gameObject.transform.position.x)
                    {
                        transform.parent.localScale = new Vector3(-1 * transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
                        isFacingForward = true;
                    }
                }
            }           //-------------------------  FOR RIGHT
            else if(transform.parent.tag == "Right")
            {
                if (collision.gameObject.tag == "Left" ||
                    (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player1"))
                {
                    transform.parent.GetComponent<CreepsBehaviourScript>().isAttacking = true;

                    if (isFacingForward && transform.parent.position.x < collision.gameObject.transform.position.x)
                    {
                        transform.parent.localScale = new Vector3(-1 * transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
                        isFacingForward = false;
                    }
                    else if (!isFacingForward && transform.parent.position.x > collision.gameObject.transform.position.x)
                    {
                        transform.parent.localScale = new Vector3(-1 * transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
                        isFacingForward = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Range")
        {       //----------------------------  FOR LEFT
            if (transform.parent.tag == "Left")
            {
                if (collision.gameObject.tag == "Right" ||
                    (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
                {
                    transform.parent.GetComponent<CreepsBehaviourScript>().isAttacking = false;
                }
            }       //--------------------------- FOR RIGHT
            else if(transform.parent.tag == "Right")
            {
                if (collision.gameObject.tag == "Left" ||
                   (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player1"))
                {
                    transform.parent.GetComponent<CreepsBehaviourScript>().isAttacking = false;
                }
            }
            if (!isFacingForward)
            {
                transform.parent.localScale = new Vector3(-1 * transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
                isFacingForward = true;
            }
        }
    }
}
