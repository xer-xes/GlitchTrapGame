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
    [SerializeField] private GameObject towerLeft2Spawn;
    [SerializeField] private GameObject towerRight2Spawn;

    [SerializeField] private GameObject castleLeftSpawn;
    [SerializeField] private GameObject castleRightSpawn;

    [SerializeField] private GameObject castleLeftPrefab;
    [SerializeField] private GameObject castleRightPrefab;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnTowers();
            InvokeRepeating("SpawnCreeps", 10, 30);             //---------------------- TODO : Need to change it to 30,30
        }
    }

    private void SpawnCreeps()
    {
        GameObject meleeLeft = PhotonNetwork.Instantiate("MeleeLeftCreep", leftMeleeSpawn.transform.position, Quaternion.identity, 0);
        GameObject rangeLeft = PhotonNetwork.Instantiate("RangeLeftCreep", leftRangeSpawn.transform.position, Quaternion.identity, 0);
        GameObject meleeRight = PhotonNetwork.Instantiate("MeleeRightCreep", rightMeleeSpawn.transform.position, Quaternion.identity, 0);
        GameObject rangeRight = PhotonNetwork.Instantiate("RangeRightCreep", rightRangeSpawn.transform.position, Quaternion.identity, 0);
    }

    private void SpawnTowers()
    {
        GameObject towerLeft1 = PhotonNetwork.Instantiate("TowerLeft1", towerLeft1Spawn.transform.position, Quaternion.identity, 0);
        GameObject towerRight1 = PhotonNetwork.Instantiate("TowerRight1", towerRight1Spawn.transform.position, Quaternion.identity, 0);

        GameObject towerLeft2 = PhotonNetwork.Instantiate("TowerLeft1", towerLeft2Spawn.transform.position, Quaternion.identity, 0);
        towerLeft2.GetComponentInChildren<TowerRangeFinder>().damage = 30;
        towerLeft2.GetComponentInChildren<TowerRangeFinder>().health = 1000;
        towerLeft2.GetComponentInChildren<TowerRangeFinder>().maxHealth = 1000;

        GameObject towerRight2 = PhotonNetwork.Instantiate("TowerRight1", towerRight2Spawn.transform.position, Quaternion.identity, 0);
        towerRight2.GetComponentInChildren<TowerRangeFinder>().damage = 30;
        towerRight2.GetComponentInChildren<TowerRangeFinder>().health = 1000;
        towerRight2.GetComponentInChildren<TowerRangeFinder>().maxHealth = 1000;

        GameObject castleLeft = PhotonNetwork.Instantiate("CastleLeft", castleLeftSpawn.transform.position, Quaternion.identity, 0);
        GameObject castleRight = PhotonNetwork.Instantiate("CastleRight", castleRightSpawn.transform.position, Quaternion.identity, 0);
    }
}
