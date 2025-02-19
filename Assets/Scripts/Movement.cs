using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 2f; //default movement speed
    private bool movingRight;

    void Start()
    {
        //randomly choose the starting direction
        movingRight = Random.value > 0.5f;
    }

    void Update()
    {
        //calculate the new position
        float moveDirection = movingRight ? 1 : -1;
        Vector3 newPosition = transform.position + Vector3.right * moveDirection * speed * Time.deltaTime;

        //calculate screen boundry in world coordinates
        float screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        //check if new position is within the screen boundries
        if (newPosition.x > screenRight || newPosition.x < screenLeft)
        {
            //change the enemy direction if out of bounds
            movingRight = !movingRight;
        }
        else
        {
            //update position if within bounds
            transform.position = newPosition;
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
