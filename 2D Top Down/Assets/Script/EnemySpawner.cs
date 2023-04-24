using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField]
    private GameObject Enemy;
    [SerializeField]
    private float Interval = 5f;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(spawnEnemy(Interval, Enemy));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) { 
        yield return new WaitForSeconds(interval);
        Vector3 spawnPos = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y), 0);
        if (spawnPos != Camera.main.WorldToScreenPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)))
        {
            GameObject newEnemy = Instantiate(enemy, spawnPos, Quaternion.identity);
            StartCoroutine(spawnEnemy(interval, enemy));
        }
        
    }
}
