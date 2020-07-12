using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Left,
    Right,
    Down
}

public class MapBuilder : MonoBehaviour
{
    private class MapChunks
    {
        public MapChunk[] Chunks = new MapChunk[9];

        public MapChunk[] Line(int i)
        {
            return new MapChunk[]
            {
                Chunks[0 + 3 * i],
                Chunks[1 + 3 * i],
                Chunks[2 + 3 * i],
            };
        }

        public MapChunk[] Column(int j)
        {
            return new MapChunk[]
            {
                Chunks[0 + j],
                Chunks[3 + j],
                Chunks[6 + j],
            };
        }
    }

    private class MapChunk
    {
        public Vector3 center;
        public Transform container;

        public void Destroy()
        {
            foreach(Transform obj in container)
            {
                GameObject.Destroy(obj.gameObject);
            }
            GameObject.Destroy(container.gameObject);
        }
    }

    public GameObject GasStationPrefab;
    public float GasStationSpawnDistance;

    public Transform AsteroidContainer;

    public float ChunkWidth = 20;
    public float ChunkHeight = 20;

    public float SamplingRateX = 6;
    public float SamplingRateY = 6;

    public float NoiseScale = 3;

    public float PositionNoiseScale = 2;
    public float InitialImpulse = 2;

    public float Seed;

    public float[] AsteroidProbabilities = new float[]
    {
        0.2f, 0.3f, 0.6f, 0.8f
    };
    public GameObject[] AsteroidPrefabs;

    private MapChunks Chunks = new MapChunks();
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Spaceship").transform;
        BuildMap();
    }

    private void Update()
    {
        if(Mathf.Abs(Chunks.Chunks[1].center.y - player.position.y) < ChunkHeight / 2)
        {
            AddChunks(Direction.Up);
        }
        if (Mathf.Abs(Chunks.Chunks[3].center.x - player.position.x) < ChunkWidth / 2)
        {
            AddChunks(Direction.Left);
        }
        if (Mathf.Abs(Chunks.Chunks[5].center.x - player.position.x) < ChunkWidth / 2)
        {
            AddChunks(Direction.Right);
        }
        if (Mathf.Abs(Chunks.Chunks[7].center.y - player.position.y) < ChunkHeight / 2)
        {
            AddChunks(Direction.Down);
        }
    }

    private void SpawnGasStation()
    {
        Instantiate(GasStationPrefab, Quaternion.AngleAxis(360 * Random.value, Vector3.forward) * Vector3.up * GasStationSpawnDistance, Quaternion.Euler(0, 0, 0));
    }

    public void BuildMap()
    {
        int i = 0;
        for(var y = 1; y >= -1; --y)
        {
            for(var x = -1; x <= 1; ++x)
            {
                Chunks.Chunks[i] = BuildChunk(new Vector3(x * ChunkWidth, y * ChunkHeight), x != 0 || y != 0);
                ++i;
            }
        }
        SpawnGasStation();
    }

    private MapChunk BuildChunk(Vector3 chunkCenter, bool addImpulse)
    {
        var container = new GameObject();
        container.transform.SetParent(AsteroidContainer);

        for (var x = chunkCenter.x - ChunkWidth / 2; x < chunkCenter.x + ChunkWidth / 2; x += SamplingRateX)
        {
            for (var y = chunkCenter.y - ChunkHeight / 2; y < chunkCenter.y + ChunkHeight / 2; y += SamplingRateY)
            {
                var posX = (x + Seed + ChunkWidth / 2) / ChunkWidth;
                var posY = (y + Seed + ChunkHeight / 2) / ChunkHeight;
                var value = Mathf.PerlinNoise(posX * NoiseScale, posY * NoiseScale);
                Debug.Log(value);
                GameObject objectToCreate = null;
                for (var i = 0; i < AsteroidProbabilities.Length; ++i)
                {
                    if (value > AsteroidProbabilities[i])
                    {
                        objectToCreate = AsteroidPrefabs[i];
                    }
                }
                if (objectToCreate)
                {
                    Debug.Log("Creating : " + objectToCreate);
                    var asteroidPosition = new Vector3(x + (Random.value * 2 - 1) * PositionNoiseScale, y + (Random.value * 2 - 1) * PositionNoiseScale, 0);
                    if ((player.position - asteroidPosition).magnitude < 3)
                    {
                        continue;
                    }
                    var asteroid = Instantiate(objectToCreate, asteroidPosition, Quaternion.Euler(0, 0, 0), container.transform);
                    asteroid.GetComponent<Rigidbody2D>().AddForce(Quaternion.AngleAxis(360 * Random.value, Vector3.up) * Vector3.forward * InitialImpulse, ForceMode2D.Impulse);
                }
            }
        }

        return new MapChunk
        {
            center = chunkCenter,
            container = container.transform
        };
    }

    private void AddChunks(Direction direction)
    {
        switch(direction)
        {
            case Direction.Up:
                Debug.Log("Move UP");
                DestroyChunks(Chunks.Line(2));
                var vUp = new Vector3(0,ChunkHeight,0);
                Chunks.Chunks = new MapChunk[]
                {
                    BuildChunk(Chunks.Chunks[0].center + vUp, true),BuildChunk(Chunks.Chunks[1].center + vUp, true),BuildChunk(Chunks.Chunks[2].center + vUp, true),
                    Chunks.Chunks[0],Chunks.Chunks[1],Chunks.Chunks[2],
                    Chunks.Chunks[3],Chunks.Chunks[4],Chunks.Chunks[5]
                };
                break;
            case Direction.Left:
                Debug.Log("Move LEFT");
                DestroyChunks(Chunks.Column(2));
                var vLeft = new Vector3(-ChunkWidth, 0, 0);
                Chunks.Chunks = new MapChunk[]
                {
                    BuildChunk(Chunks.Chunks[0].center + vLeft, true),Chunks.Chunks[0],Chunks.Chunks[1],
                    BuildChunk(Chunks.Chunks[3].center + vLeft, true),Chunks.Chunks[3],Chunks.Chunks[4],
                    BuildChunk(Chunks.Chunks[6].center + vLeft, true),Chunks.Chunks[6],Chunks.Chunks[7],
                };
                break;
            case Direction.Right:
                Debug.Log("Move RIGHT");
                DestroyChunks(Chunks.Column(0));
                var vRight = new Vector3(ChunkWidth, 0, 0);
                Chunks.Chunks = new MapChunk[]
                {
                    Chunks.Chunks[1],Chunks.Chunks[2],BuildChunk(Chunks.Chunks[2].center + vRight, true),
                    Chunks.Chunks[4],Chunks.Chunks[5],BuildChunk(Chunks.Chunks[5].center + vRight, true),
                    Chunks.Chunks[7],Chunks.Chunks[8],BuildChunk(Chunks.Chunks[8].center + vRight, true),
                };
                break;
            case Direction.Down:
                Debug.Log("Move DOWN");
                DestroyChunks(Chunks.Line(0));
                var vDown = new Vector3(0, -ChunkHeight, 0);
                Chunks.Chunks = new MapChunk[]
                {
                    Chunks.Chunks[3],Chunks.Chunks[4],Chunks.Chunks[5],
                    Chunks.Chunks[6],Chunks.Chunks[7],Chunks.Chunks[8],
                    BuildChunk(Chunks.Chunks[6].center + vDown, true),BuildChunk(Chunks.Chunks[7].center + vDown, true),BuildChunk(Chunks.Chunks[8].center + vDown, true)
                };
                break;
        }
    }

    private void DestroyChunks(MapChunk[] chunks)
    {
        foreach(var c in chunks)
        {
            c.Destroy();
        }
    }
}
