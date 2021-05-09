using UnityEngine;

public class InstantiateEnemies : MonoBehaviour
{
    public GameObject enemyPrefab, playerPrefab;
    public int numEnemies;
    public Vector3 position;
    public bool isFinished;

    public void InstantiateEnemy()
    {

        for (int i = 0; i < numEnemies; i++)
        {
            GameObject var = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);

            var.GetComponentInChildren<Enemy>().target = playerPrefab.transform.GetChild(1).gameObject;
            var.GetComponentInChildren<Enemy>().activeState = Enemy.States.chase;

            isFinished = false;
        }
    }
}
