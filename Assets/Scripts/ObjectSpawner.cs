using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; //assign object prefab
    public Transform spawnArea; //empty GameObject defining spawn bounds
    public Slider speedSlider; //UI slider to modify speed
    public Text objectCountText; //UI text to show how many objects exist
    public Button spawnButton; //button to spawn objects
    public Button deleteButton; //button to delete last object
    public float spawnZPosition = -9f; //Z position for spawning objects

    private static List<GameObject> spawnedObjects = new List<GameObject>(); //list for all the spawned enemies
    private BoxCollider2D spawnAreaCollider;

    void Start()
    {
        //button click events
        spawnButton.onClick.AddListener(SpawnObject);
        //deleteButton.onClick.AddListener(DeleteLastObject); //REMOVED
        speedSlider.onValueChanged.AddListener(ChangeSpeedOfAllObjects);
        UpdateObjectCount();

        //make sure spawn area is good - if not let me know cuz its broken :|
        if (spawnArea != null)
        {
            spawnAreaCollider = spawnArea.GetComponent<BoxCollider2D>();
            if (spawnAreaCollider == null)
            {
                Debug.LogError("Spawn Area does not have a BoxCollider2D component!");
            }
        }
        else
        {
            Debug.LogError("Spawn Area is not assigned!");
        }
    }

    void SpawnObject()
    {
        if (spawnAreaCollider == null)
        {
            Debug.LogError("Spawn Area Collider is not assigned!");
            return;
        }

        //bounds for the spawn area
        Bounds bounds = spawnAreaCollider.bounds;

        //make random position inside the spawn area
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        //set the Z position 
        Vector3 randomPosition = new Vector3(randomX, randomY, spawnZPosition);

        //instantiate the object
        GameObject newObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity);
        spawnedObjects.Add(newObject);

        //set speed based on the slider value
        EnemyMovement enemyMovement = newObject.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetSpeed(speedSlider.value);
        }

        UpdateObjectCount();
    }

    //delete objects when button clicked
    // REMOVED
    //void DeleteLastObject()
    //{
      //  if (spawnedObjects.Count > 0)
        //{
          //  GameObject lastObject = spawnedObjects[spawnedObjects.Count - 1];
            //RemoveAndDestroyObject(lastObject);
        //}
    //}

    //new version of this ^
    public static void RemoveAndDestroyObject(GameObject obj)
    {
        spawnedObjects.Remove(obj);
        Destroy(obj);
        UpdateObjectCountStatic();
    }

    //count the objects on the screen
    public static void UpdateObjectCountStatic()
    {
        //ensure the UI text is updated
        ObjectSpawner instance = FindObjectOfType<ObjectSpawner>();
        if (instance != null)
        {
            instance.objectCountText.text = "Enemies: " + spawnedObjects.Count;
        }
    }

    //modify speed of the objects - attach to the slider
    void ChangeSpeedOfAllObjects(float newSpeed)
    {
        foreach (GameObject obj in spawnedObjects)
        {
            EnemyMovement enemyMovement = obj.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.SetSpeed(newSpeed);
            }
        }
    }

    //update the actual text
    void UpdateObjectCount()
    {
        objectCountText.text = "Enemies: " + spawnedObjects.Count;
    }
}
