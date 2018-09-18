using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float meteorRate;
    public int biomeNumber = 0;
    public Vector2 worldOffset;
    bool inWave = false;
    public int wave = 1;
    public float waveTimer;
    public float waveTime = 3f;
    public float meteorTime = 5f;
    public float meteorTimer;
    public int meteorCount = 1;
    public GameObject meteorTarget;
    public GameObject biome;
    public GameObject[] obstacles;
    public GameObject[] decor;
    public GameObject[] npcs;
    public GameObject meteor;
    public int worldSize = 3;
    public GameObject[] pathBlocks;
    public GameObject[] chunks;
    public GameObject[] grassDecor;
    public GameObject[] grassNpcs;
    public GameObject[] grassTrees;
    public GameObject[] desertDecor;
    public GameObject[] desertMids;
    public GameObject[] cityDecor;
    public GameObject[] cityMids;
    public Vector3 biomePos;
    public int lastBiomeType;
    public TouchManager touchManager;


    //public Vector3 refPos = new Vector3(0, 0, 0);

    void Start()
    {

    
        //SpawnGround();

        //Spawn3x3(Vector3.zero, 0,0);


        // SpawnNPC();
    }

    void Update()
    {

        if(!touchManager.inMenu){
            WaveTick();
        }

    }

    public void SpawnBiome()
    {
        biomePos = new Vector3(0, -biomeNumber, -6 * biomeNumber);

        Instantiate(biome, biomePos, Quaternion.identity);

    }


    public void MeteorTick(int _wave)
    {
        meteorTimer -= Time.deltaTime;

        if (meteorTimer <= 0f)
        {

            float spawnX = Random.Range(-worldSize, worldSize) + worldOffset.x;
            float spawnY = 6 + (-6 * biomeNumber) + Random.Range(-worldSize*2, worldSize) + worldOffset.y;
            float spawnZ = 25f - biomeNumber;
            Instantiate(meteor, new Vector3(spawnX, spawnZ, spawnY), Quaternion.Euler(0,Random.Range(0,360),0));
            Instantiate(meteorTarget, new Vector3(spawnX, -biomeNumber, spawnY), Quaternion.identity);
            meteorTimer = Random.Range(meteorTime * .5f, meteorTime * 2);
            meteorCount -= 1;
            if (meteorCount <= 0)
            {
                inWave = false;
            }
        }

    }
    public void WaveTick()
    {

        if (inWave)
        {
            MeteorTick(wave);
        }
        else
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0)
            {
                waveTimer = waveTime;
                wave += 1;
                meteorCount = 1;
                inWave = true;
            }
        }

    }

    public void Spawn3x3(Vector3 _refPos, int _type1, int _type2)
    {
        //TOP ROW
        Instantiate(chunks[_type1], _refPos + new Vector3(-6, 0, 6), Quaternion.identity);
        Instantiate(chunks[_type1], _refPos + new Vector3(0, 0, 6), Quaternion.identity);
        Instantiate(chunks[_type1], _refPos + new Vector3(6, 0, 6), Quaternion.identity);

        //MIDDLE ROW
        Instantiate(chunks[_type2], _refPos + new Vector3(-6, 0, 0), Quaternion.identity);
        Instantiate(chunks[_type2], _refPos + new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(chunks[_type2], _refPos + new Vector3(6, 0, 0), Quaternion.identity);

        //BOTTOM ROW_refPos 
        Instantiate(chunks[_type1], _refPos + new Vector3(-6, 0, -6), Quaternion.identity);
        Instantiate(chunks[_type1], _refPos + new Vector3(0, 0, -6), Quaternion.identity);
        Instantiate(chunks[_type1], _refPos + new Vector3(6, 0, -6), Quaternion.identity);

    }




}
