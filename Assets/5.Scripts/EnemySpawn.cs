using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab_1;
    [SerializeField]
    private GameObject EnemyPrefab_2;
    [SerializeField]
    private GameObject EnemyPrefab_3;
    private MemoryPool memoryPool_1;
    private MemoryPool memoryPool_2;
    private MemoryPool memoryPool_3;

    int count = 0;
    public int maxcount = 10;
    
    void Awake()
    {
        memoryPool_1 = new MemoryPool(EnemyPrefab_1);
        memoryPool_2 = new MemoryPool(EnemyPrefab_2);
        memoryPool_3 = new MemoryPool(EnemyPrefab_3);
    }

    // Update is called once per frame
    void Start()
    {
        Invoke("Spawning", Random.Range(5, 10));
    }

    public void Spawning()
    {
        if (count > maxcount)
            return; 

        switch (Random.Range(0, 3))
        {
            case 0:
                GameObject item_1 = memoryPool_1.ActivatePoolItem();
                item_1.transform.position = new Vector3(transform.position.x, 8.09126f, transform.position.z);
                item_1.GetComponent<Enemy>().Setup(memoryPool_1);
                break;
            case 1:
                GameObject item_2 = memoryPool_2.ActivatePoolItem();
                item_2.transform.position = new Vector3(transform.position.x, 8.09126f, transform.position.z);
                item_2.GetComponent<Enemy>().Setup(memoryPool_2);
                break;
            case 2:
                GameObject item_3 = memoryPool_3.ActivatePoolItem();
                item_3.transform.position = new Vector3(transform.position.x, 8.09126f, transform.position.z);
                item_3.GetComponent<Enemy>().Setup(memoryPool_3);
                break;
        }
        count++;

        
        Invoke("Spawning", Random.Range(3, 7));
    }
}
