using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int catsToSpawn;
    public int catsHerded;
    public EdgeCollider2D spawnCollider;
    public Sprite[] sprites;

    [SerializeField]
    Canvas LevelCompleteCanvas;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if(LevelCompleteCanvas)
        {
            LevelCompleteCanvas.enabled = false;
        }
        catsToSpawn = 5;
        catsHerded = 0;
        sprites = Resources.LoadAll<Sprite>("Cats");
        SpawnCatsFromPool();
        Cat_Ai.CatHerded += OnCatHerded;
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

    private void DeactivateCats()
    {
        for(int i = 0; i < catsToSpawn; i++)
        {
            if(ObjectPool.SharedInstance.pooledObjects[i].activeInHierarchy == true)
            {
                ObjectPool.SharedInstance.pooledObjects[i].SetActive(false);
            }
        }
    }

    private Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));
    }

    private void LevelComplete()
    {
        audioSource.Play();
        LevelCompleteCanvas.enabled = true;
        Invoke("NewLevel", 5);
    }

    public void NewLevel()
    {
        LevelCompleteCanvas.enabled = false;
        DeactivateCats();
        if(catsToSpawn + 1 <= 50)
        {
            catsToSpawn++;
        }
        catsHerded = 0;
        SpawnCatsFromPool();
    }

    public void OnCatHerded()
    {
        catsHerded++;

        if(catsHerded == catsToSpawn)
        {
            LevelComplete();
        }
    }
}
