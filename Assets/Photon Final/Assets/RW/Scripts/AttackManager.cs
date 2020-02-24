using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    bool isAttacking = false;

    public void CheckHit()
    {
        isAttacking = true;
    }

    public void UnCheckHit()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.gameObject != this.transform.parent.gameObject &&
            (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Opponent"))
            Debug.Log("Doing Damage to : " + collision.gameObject.name + " by : " + this.transform.parent.gameObject.name);
    }
}
