using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; //assign object prefab
    public Transform spawnPoint;    //set a spawn position
    public Slider speedSlider;       //UI slider to modify speed
    public Text objectCountText;    //UI text to show how many objects exist
    public Button spawnButton;      //button to spawn objects
    public Button deleteButton;     //button to delete last object

    private List<GameObject> spawnedObjects = new List<GameObject>(); //stores spawned objects
    private float timer = 0f;       //timer variable

    void Start()
    {
        //button click events
        spawnButton.onClick.AddListener(SpawnObject);
        deleteButton.onClick.AddListener(DeleteLastObject);

        speedSlider.onValueChanged.AddListener(ChangeObjectSize);
        UpdateObjectCount();
    }

    void Update()
    {

    }

    void SpawnObject()
    {
        //instantiate an object at a random position
        Vector2 randomPosition = new Vector2(
            Random.Range(-0f, 1180f),  // Random X position in world space
            Random.Range(-0f, 561f)     // Random Y position in world space
        );

        GameObject newObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity);

        //store the object in the list
        spawnedObjects.Add(newObject);

        //update UI text
        UpdateObjectCount();
    }

    void DeleteLastObject()
    {
        if (spawnedObjects.Count > 0)
        {
            GameObject lastObject = spawnedObjects[spawnedObjects.Count - 1];
            spawnedObjects.Remove(lastObject);
            Destroy(lastObject);
            UpdateObjectCount();
        }
    }

    void ChangeObjectSize(float newSize)
    {
        //change the speed of the last spawned object
        if (spawnedObjects.Count > 0)
        {
            GameObject lastObject = spawnedObjects[spawnedObjects.Count - 1];
            lastObject.transform.localScale = Vector3.one * newSize;
        }
    }

    void UpdateObjectCount()
    {
        //update UI Text with object count
        objectCountText.text = "Enemies: " + spawnedObjects.Count;
    }
}
