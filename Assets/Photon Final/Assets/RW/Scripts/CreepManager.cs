using UnityEngine;
using Photon.Realtime;
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
        Invoke("SpawnCreeps", 10);
    }

    private void SpawnCreeps()
    {
        GameObject rangeLeft = PhotonNetwork.Instantiate("RangeLeftCreep", leftRangeSpawn.transform.position, Quaternion.identity, 1);
    }
}
