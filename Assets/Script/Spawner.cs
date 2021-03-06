
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public Terrain terrain;

    private int terrainWidth; // terrain size (x)
    private int terrainLength; // terrain size (z)
    private int terrainPosX; // terrain position x
    private int terrainPosZ; // terrain position z
    public GameObject player;

    public GameObject prefabToSpawn;
    public float spawnTime;
    public float spawnTimeRandom;

    public float minSpawn;//khoảng cách nhỏ nhất cách người chơi
    public float maxSpawn;// khoảng cách dài nhất cách người chơi 
    // object sẽ được spawn trong khoảng từ minSpawn  đến maxSpawn

    public int maxNumberToSpawn;// số object tối đa để spawn xung quanh player

    private float spawnTimer;

    void Start()
    {
        // gán các giá trị max của terrain
        // terrain size x
        terrainWidth = (int)terrain.terrainData.size.x;
        // terrain size z
        terrainLength = (int)terrain.terrainData.size.z;
        // terrain x position
        terrainPosX = (int)terrain.transform.position.x;
        // terrain z position
        terrainPosZ = (int)terrain.transform.position.z;

        
        ResetSpawnTimer();
    }

    //Update is called once per frame
    void Update()
    {
        GameObject[] prefabs;
        prefabs = GameObject.FindGameObjectsWithTag(prefabToSpawn.gameObject.tag);// tìm số lượng object đã đc spawn trong map 
        //var position = new Vector3(player.transform.position.x + Random.Range(-30.0f, -15.0f), player.transform.position.y, player.transform.position.z + Random.Range(-30.0f, -15.0f));
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0.0f )// nếu hết thời gian đếm nguoiwcj 
        {
            float posx;
            float posz;
            float posy;
            for (int i = 1; i <= 4; i++)// vọng lặp spawn xung quanh 4 góc của người chơi
            {
                // 4 câu if ở dưới : nếu số lượng object trên map nhỏ hơn số object tối đa cần spawn thì spawn object ở 4 góc
                if(i == 1 && prefabs.Length < maxNumberToSpawn) 
                {
                    // generate random x position
                     posx = Random.Range(player.transform.position.x + minSpawn, player.transform.position.x + maxSpawn);
                    // generate random z position
                     posz = Random.Range(player.transform.position.z + minSpawn, player.transform.position.z + maxSpawn);
                    // get the terrain height at the random position
                     posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));
                    // create new gameObject on random position
                    Instantiate(prefabToSpawn, new Vector3(posx, posy, posz), Quaternion.identity);
                }
                if (i == 2 && prefabs.Length < maxNumberToSpawn)
                {
                    // generate random x position
                     posx = Random.Range(player.transform.position.x - minSpawn, player.transform.position.x - maxSpawn);
                    // generate random z position
                     posz = Random.Range(player.transform.position.z + minSpawn, player.transform.position.z + maxSpawn);
                    // get the terrain height at the random position
                     posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));
                    // create new gameObject on random position
                    Instantiate(prefabToSpawn, new Vector3(posx, posy , posz), Quaternion.identity);
                }
                if (i == 3 && prefabs.Length < maxNumberToSpawn)
                {
                    // generate random x position
                     posx = Random.Range(player.transform.position.x - minSpawn, player.transform.position.x - maxSpawn);
                    // generate random z position
                     posz = Random.Range(player.transform.position.z - minSpawn, player.transform.position.z - maxSpawn);
                    // get the terrain height at the random position
                     posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));
                    // create new gameObject on random position
                    Instantiate(prefabToSpawn, new Vector3(posx, posy , posz), Quaternion.identity);
                }
                if (i == 4 && prefabs.Length < maxNumberToSpawn)
                {
                    // generate random x position
                     posx = Random.Range(player.transform.position.x + minSpawn, player.transform.position.x + maxSpawn);
                    // generate random z position
                     posz = Random.Range(player.transform.position.z - minSpawn, player.transform.position.z - maxSpawn);
                    // get the terrain height at the random position
                     posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));
                    // create new gameObject on random position
                    Instantiate(prefabToSpawn, new Vector3(posx, posy , posz), Quaternion.identity);
                }
            }
            
            

            
            //if (Vector3.Distance(newObject.transform.position, player.transform.position) < 30f && Vector3.Distance(newObject.transform.position, player.transform.position) > minSpawnf)
            //{
            //    Destroy(newObject);
            //}
            ResetSpawnTimer();
        }
    }

    //Resets the spawn timer with a random offset
    void ResetSpawnTimer()// reset lại thời gian spawn object
    {
        spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom * 100) / 100.0);
    }
}