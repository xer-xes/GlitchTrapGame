using UnityEngine;
// TODO : Sort the continuous flipping of the sprite.
public class CreepRangeFinder : MonoBehaviour
{
    public bool isFacingForward = true;
    private bool isPlayerInRange = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Range")
        {       //------------------------------ FOR LEFT
            if (transform.parent.tag == "Left")
            {
                if (collision.gameObject.tag == "Right" || collision.gameObject.tag == "RightTower" ||
                     (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
                {
                    if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2")
                        isPlayerInRange = true;
                    
                    transform.parent.GetComponent<CreepsBehaviourScript>().isAttacking = true;

                    if (isPlayerInRange && collision.gameObject.tag == "Right")
                        return;

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
                if (collision.gameObject.tag == "Left" || collision.gameObject.tag == "LeftTower" ||
                    (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player1"))
                {
                    if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player1")
                        isPlayerInRange = true;

                    transform.parent.GetComponent<CreepsBehaviourScript>().isAttacking = true;

                    if (isPlayerInRange && collision.gameObject.tag == "Left")
                        return;

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
                if (collision.gameObject.tag == "Right" || collision.gameObject.tag == "RightTower" ||
                    (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
                {
                    if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2")
                        isPlayerInRange = false;
                    transform.parent.GetComponent<CreepsBehaviourScript>().isAttacking = false;
                }
            }       //--------------------------- FOR RIGHT
            else if(transform.parent.tag == "Right")
            {
                if (collision.gameObject.tag == "Left" || collision.gameObject.tag == "leftTower" ||
                   (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player1"))
                {
                    if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player1")
                        isPlayerInRange = false;
                    transform.parent.GetComponent<CreepsBehaviourScript>().isAttacking = false;
                }
            }
            if (!isFacingForward && !isPlayerInRange)
            {
                transform.parent.localScale = new Vector3(-1 * transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
                isFacingForward = true;
            }
        }
    }
}
