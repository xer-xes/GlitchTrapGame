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

    [SerializeField] private GameObject towerLeft1Prefab;
    [SerializeField] private GameObject towerRight1Prefab;

    [SerializeField] private GameObject towerLeft1Spawn;
    [SerializeField] private GameObject towerRight1Spawn;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnTowers();
            InvokeRepeating("SpawnCreeps", 30, 30);
        }
    }

    private void SpawnCreeps()
    {
        GameObject meleeLeft = PhotonNetwork.Instantiate("MeleeLeftCreep", leftMeleeSpawn.transform.position, Quaternion.identity, 0);
        GameObject rangeLeft = PhotonNetwork.Instantiate("RangeLeftCreep", leftRangeSpawn.transform.position, Quaternion.identity, 0);
        GameObject rangeRight = PhotonNetwork.Instantiate("RangeRightCreep", rightRangeSpawn.transform.position, Quaternion.identity, 0);
    }

    private void SpawnTowers()
    {
        GameObject towerLeft1 = PhotonNetwork.Instantiate("TowerLeft1", towerLeft1Spawn.transform.position, Quaternion.identity, 0);
        GameObject towerRight1 = PhotonNetwork.Instantiate("TowerRight1", towerRight1Spawn.transform.position, Quaternion.identity, 0);
    }
}
