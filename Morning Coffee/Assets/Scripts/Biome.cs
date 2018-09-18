using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{

    public Spawner spawner;
    public int biomeType;
    public GameObject[] biomes;

    void OnEnable()
    {

        if (spawner.lastBiomeType == 3)
        { //CHECKING IF PATH BIOME
            biomeType = Random.Range(0, biomes.Length - 1);
            spawner.lastBiomeType = biomeType;
            biomes[biomeType].SetActive(true);
        }
        else
        {
            biomeType = Random.Range(0, biomes.Length);
            spawner.lastBiomeType = biomeType;
            biomes[biomeType].SetActive(true);


        }
        Populate(biomeType);

    }

    public void Populate(int _biomeType)
    {

        if (_biomeType == 3) // (OR ANY OTHER PATH BIOME)
        { //IF PATH BIOME

            CreatePath(_biomeType);

        }
        else
        {
            SpawnNPC(_biomeType);
            SpawnDecor(_biomeType);
            SpawnTrees(_biomeType);

        }
    }

    void SpawnPathStep(GameObject _type, float _x, float _y) //FOR USE IN: CreatePath() function
    {
        Instantiate(_type, new Vector3(transform.position.x + _x, transform.position.y, transform.position.z + _y), Quaternion.identity);
    }

    public void CreatePath(int _biomeType)
    {

        switch (_biomeType)
        {

            //DETERMINE SPECIFIC PATH TYPE (etc clouds, lava, water)
            case 3: //CLOUDS

                Vector2 lastPos;
                bool freeLeft = true;
                bool freeRight = true;

                lastPos = new Vector2(Random.Range(-2, 3), 3);

                SpawnPathStep(spawner.pathBlocks[0], lastPos.x, 3); //FIRST BLOCK

                do
                {
                    //DETERMINE OPTIONS FIRST

                    if (lastPos.x == -2) //LEFT EDGE
                    {
                        freeLeft = false;
                    }

                    if (lastPos.x == 3) //RIGHT EDGE
                    {

                        freeRight = false;
                    }

                    if (freeLeft && freeRight)
                    {
                        int rand = Random.Range(0, 3);

                        if (rand == 0)
                        {
                            lastPos.x -= 1;
                            SpawnPathStep(spawner.pathBlocks[0], lastPos.x, lastPos.y);
                            freeRight = false;
                        }
                        else if (rand == 1)
                        {
                            lastPos.x += 1;
                            SpawnPathStep(spawner.pathBlocks[0], lastPos.x, lastPos.y);
                            freeLeft = false;
                        }
                        else
                        {
                            lastPos.y -= 1;
                            SpawnPathStep(spawner.pathBlocks[0], lastPos.x, lastPos.y);
                            freeLeft = freeRight = true;
                        }

                    }
                    else if (freeLeft)
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            lastPos.x -= 1;
                            SpawnPathStep(spawner.pathBlocks[0], lastPos.x, lastPos.y);
                            freeRight = false;
                        }
                        else
                        {
                            lastPos.y -= 1;
                            SpawnPathStep(spawner.pathBlocks[0], lastPos.x, lastPos.y);
                            freeLeft = freeRight = true;
                        }
                    }
                    else if (freeRight)
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            lastPos.x += 1;
                            SpawnPathStep(spawner.pathBlocks[0], lastPos.x, lastPos.y);
                            freeLeft = false;

                        }
                        else
                        {
                            lastPos.y -= 1;
                            SpawnPathStep(spawner.pathBlocks[0], lastPos.x, lastPos.y);
                            freeLeft = freeRight = true;
                        }
                    }
                    else
                    {
                        lastPos.y -= 1;
                        SpawnPathStep(spawner.pathBlocks[0], lastPos.x, lastPos.y);
                        freeLeft = freeRight = true;
                    }

                } while (lastPos.y > -2);
                break;

            default:
                break;
        }
    }

    void SpawnNPC(int _biomeType)
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(spawner.npcs[Random.Range(0, spawner.npcs.Length)], new Vector3(Random.Range(-spawner.worldSize, spawner.worldSize) + spawner.worldOffset.x, transform.position.y, transform.position.z + Random.Range(-spawner.worldSize, spawner.worldSize) + spawner.worldOffset.y), Quaternion.identity);
        }
    }


    public void SpawnTrees(int _biomeType)
    {
        switch (_biomeType)
        {
            
            case 0: //GRASS BIOME
                for (int i = 0; i < 5; i++)
                {
                    int rand = Random.Range(0, spawner.grassTrees.Length);
                    Instantiate(spawner.grassTrees[rand], new Vector3(Random.Range(-spawner.worldSize + 1, spawner.worldSize - 1) + spawner.worldOffset.x, transform.position.y, transform.position.z + Random.Range(-spawner.worldSize + 1, spawner.worldSize - 1) + spawner.worldOffset.y), Quaternion.identity); //??
                }
                break;
            case 1: //DESERT BIOME

                for (int i = 0; i < 5; i++)
                {
                    int rand = Random.Range(0, spawner.desertMids.Length);
                    Instantiate(spawner.desertMids[rand], new Vector3(Random.Range(-spawner.worldSize + 1, spawner.worldSize - 1) + spawner.worldOffset.x, transform.position.y, transform.position.z + Random.Range(-spawner.worldSize + 1, spawner.worldSize - 1) + spawner.worldOffset.y), Quaternion.identity); //??
                }

                break;

                case 2: //DESERT BIOME

                for (int i = 0; i < 5; i++)
                {
                    int rand = Random.Range(0, spawner.cityMids.Length);
                    Instantiate(spawner.cityMids[rand], new Vector3(Random.Range(-spawner.worldSize + 1, spawner.worldSize - 1) + spawner.worldOffset.x, transform.position.y, transform.position.z + Random.Range(-spawner.worldSize + 1, spawner.worldSize - 1) + spawner.worldOffset.y), Quaternion.identity); //??
                }

                break;

            default:
                break;
        }
    }


    public void SpawnDecor(int _biomeType)
    {

        switch (_biomeType)
        {
            case 0: //GRASS BIOME

                SpawnSpecificDecor(spawner.grassDecor, 5);

                break;
            case 1: //DESERT BIOME

                SpawnSpecificDecor(spawner.desertDecor, 5);

                break;

            case 2:
                SpawnSpecificDecor(spawner.cityDecor, 5);
                break;

            default:
                break;
        }

    }

    public void SpawnSpecificDecor(GameObject[] _decorArray, int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            int rand = Random.Range(0, _decorArray.Length);
            Instantiate(_decorArray[rand], new Vector3(Random.Range(-spawner.worldSize, spawner.worldSize) + spawner.worldOffset.x, transform.position.y, transform.position.z + Random.Range(-spawner.worldSize, spawner.worldSize) + spawner.worldOffset.y), Quaternion.identity); //??
        }
    }

}

