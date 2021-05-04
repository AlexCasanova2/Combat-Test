using UnityEngine;

public class InstantiateEnemyBoss : MonoBehaviour
{
    public GameObject enemyBoss, playerPrefab;
    public int numEnemies;
    public bool canInstantiate;


    void Update()
    {
        if (canInstantiate){
            Instantiate();
        }
        if (canInstantiate) return;
    }

    public void Instantiate()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            GameObject var = Instantiate(enemyBoss, gameObject.transform.position, Quaternion.identity);

            var.GetComponentInChildren<Enemy>().target = playerPrefab.transform.GetChild(1).gameObject;
            var.GetComponentInChildren<Enemy>().activeState = Enemy.States.chase;
        }
    }
}
