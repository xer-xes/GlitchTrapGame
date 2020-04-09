using UnityEngine;
using Photon.Pun;

public class CreepManager : MonoBehaviour
{
    [SerializeField] private GameObject rightMeleeSpawn;
    [SerializeField] private GameObject rightRangeSpawn;
    [SerializeField] private GameObject leftMeleeSpawn;
    [SerializeField] private GameObject leftRangeSpawn;

    [SerializeField] private GameObject meleeRightCreep;
    [SerializeField] private GameObject rangeRightCreep;
    [SerializeField] private GameObject meleeLeftCreep;
    [SerializeField] private GameObject rangeLeftCreep;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            InvokeRepeating("SpawnCreeps", 30, 30);
    }

    private void SpawnCreeps()
    {
        GameObject rangeLeft = PhotonNetwork.Instantiate("RangeLeftCreep", leftRangeSpawn.transform.position, Quaternion.identity, 0);
        GameObject rangeRight = PhotonNetwork.Instantiate("RangeRightCreep", rightRangeSpawn.transform.position, Quaternion.identity, 0);
    }
}
