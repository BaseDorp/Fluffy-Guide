using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int catsToSpawn;
    public EdgeCollider2D spawnCollider;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        catsToSpawn = 5;
        sprites = Resources.LoadAll<Sprite>("Cats");
        SpawnCatsFromPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnCatsFromPool()
    {
        for(int i = 0; i < catsToSpawn; i++)
        {
            Vector2 position = RandomPointInBounds(spawnCollider.bounds);

            var cat = ObjectPool.SharedInstance.GetPooledObject();

            cat.SetActive(true);
            cat.transform.position = position;
            cat.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[i];
        }
    }

    private Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));
    }
}
