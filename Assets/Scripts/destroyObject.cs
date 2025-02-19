using UnityEngine;


//when the mouse click = DESTROY object
public class DestroyOnClick : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("Enemy clicked!"); // Debug log to confirm click detection
        ObjectSpawner.RemoveAndDestroyObject(gameObject);
    }
}
