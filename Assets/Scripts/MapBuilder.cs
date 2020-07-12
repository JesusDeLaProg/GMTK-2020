using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    public float MapWidth;
    public float MapHeight;

    public float SamplingRateX;
    public float SamplingRateY;

    public float NoiseScale;

    public float PositionNoiseScale;
    public float InitialImpulse;

    public float Seed;

    public float[] AsteroidProbabilities;
    public GameObject[] AsteroidPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        BuildMap();
    }

    private void BuildMap()
    {
        for(var x = -MapWidth / 2; x < MapWidth / 2; x += SamplingRateX)
        {
            for(var y = -MapHeight / 2; y < MapHeight / 2; y += SamplingRateY)
            {
                var posX = (x + Seed + MapWidth / 2) / MapWidth;
                var posY = (y + Seed + MapHeight / 2) / MapHeight;
                var value = Mathf.PerlinNoise(posX * NoiseScale, posY * NoiseScale);
                GameObject objectToCreate = null;
                for(var i = 0; i < AsteroidProbabilities.Length; ++i)
                {
                    if(value > AsteroidProbabilities[i])
                    {
                        objectToCreate = AsteroidPrefabs[i];
                    }
                }
                if (objectToCreate)
                {
                    Debug.Log("Coordinate: " + new Vector2(x, y) + ". Noise Value : " + value + ". Creating: " + objectToCreate.name);
                    var asteroidPosition = new Vector3(x + (Random.value * 2 - 1) * PositionNoiseScale, y + (Random.value * 2 - 1) * PositionNoiseScale, 0);
                    if(-2 < asteroidPosition.x && asteroidPosition.x < 2 &&
                        -2 < asteroidPosition.y && asteroidPosition.y < 2)
                    {
                        continue;
                    }
                    var asteroid = Instantiate(objectToCreate, asteroidPosition, Quaternion.Euler(0, 0, 0));
                    asteroid.GetComponent<Rigidbody2D>().AddForce(Quaternion.AngleAxis(360 * Random.value, Vector3.up) * Vector3.forward * InitialImpulse, ForceMode2D.Impulse);
                }
            }
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach(var asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
            {
                Destroy(asteroid);
            }
            BuildMap();
        }
    }
}
