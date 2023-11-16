using UnityEngine;

public class ObjectUpDownMovement : MonoBehaviour
{
    
    public float moveSpeed = 0.0f; // Adjust the speed as needed
    public float moveDistance = 0.0f; // Adjust the distance to move up and down

    public MovementType currentMoveType;
    public enum MovementType
    {
        Verticle,
        Forward,
        Right,
        Left
    }

    private Vector3 initialPosition;
    
    private bool movingUp = true;
    private bool movingRight = true;
    private bool movingLeft = true;
    private bool movingForward = true;
    private bool movingBackwards = true;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {

        if (currentMoveType == MovementType.Verticle)
        {
            // Calculate the new position based on the current direction
            Vector3 newPosition = movingUp
                ? transform.position + Vector3.up * moveSpeed * Time.deltaTime
                : transform.position - Vector3.up * moveSpeed * Time.deltaTime;

            if(movingUp == true)
            {
                // Check if the new position is within the allowed moveDistance
                float distanceMoved = newPosition.y - initialPosition.y;
                if (Mathf.Abs(distanceMoved) <= moveDistance)
                {
                    // Apply the new position to the object's transform
                    transform.position = newPosition;
                }
                else
                {
                    // Change the direction when the move distance is reached
                    movingUp = !movingUp;
                }
            }
            else
            {
                // Calculate the distance to the initial position
                float distanceToInitial = Vector3.Distance(transform.position, initialPosition);

                if (distanceToInitial > 0.01f) // Check if it's not too close to the initial position
                {
                    // Apply the new position to the object's transform
                    transform.position = newPosition;
                }
                else
                {
                    // Change the direction when the move distance is reached
                    movingUp = !movingUp;
                }
            }
        }

        if (currentMoveType == MovementType.Forward)
        {
            // Calculate the new position based on the current direction
            Vector3 newPosition = movingForward
                ? transform.position + Vector3.forward * moveSpeed * Time.deltaTime
                : transform.position - Vector3.forward * moveSpeed * Time.deltaTime;

            if (movingForward == true)
            {
                // Check if the new position is within the allowed moveDistance
                float distanceMoved = newPosition.z - initialPosition.z;
                if (Mathf.Abs(distanceMoved) <= moveDistance)
                {
                    // Apply the new position to the object's transform
                    transform.position = newPosition;
                }
                else
                {
                    // Change the direction when the move distance is reached
                    movingForward = !movingForward;
                }
            }
            else
            {
                // Calculate the distance to the initial position
                float distanceToInitial = Vector3.Distance(transform.position, initialPosition);

                if (distanceToInitial > 0.01f) // Check if it's not too close to the initial position
                {
                    // Apply the new position to the object's transform
                    transform.position = newPosition;
                }
                else
                {
                    // Change the direction when the move distance is reached
                    movingForward = !movingForward;
                }
            }
        }

        if (currentMoveType == MovementType.Right)
        {
            // Calculate the new position based on the current direction
            Vector3 newPosition = movingRight
                ? transform.position + Vector3.right * moveSpeed * Time.deltaTime
                : transform.position - Vector3.right * moveSpeed * Time.deltaTime;

            if (movingRight == true)
            {
                // Check if the new position is within the allowed moveDistance
                float distanceMoved = newPosition.x - initialPosition.x;
                if (Mathf.Abs(distanceMoved) <= moveDistance)
                {
                    // Apply the new position to the object's transform
                    transform.position = newPosition;
                }
                else
                {
                    // Change the direction when the move distance is reached
                    movingRight = !movingRight;
                }
            }
            else
            {
                // Calculate the distance to the initial position
                float distanceToInitial = Vector3.Distance(transform.position, initialPosition);

                if (distanceToInitial > 0.01f) // Check if it's not too close to the initial position
                {
                    // Apply the new position to the object's transform
                    transform.position = newPosition;
                }
                else
                {
                    // Change the direction when the move distance is reached
                    movingRight = !movingRight;
                }
            }
        }

        if (currentMoveType == MovementType.Left)
        {
            // Calculate the new position based on the current direction
            Vector3 newPosition = movingLeft
                ? transform.position + Vector3.left * moveSpeed * Time.deltaTime
                : transform.position - Vector3.left * moveSpeed * Time.deltaTime;

            if (movingLeft == true)
            {
                // Check if the new position is within the allowed moveDistance
                float distanceMoved = newPosition.x - initialPosition.x;
                if (Mathf.Abs(distanceMoved) <= moveDistance)
                {
                    // Apply the new position to the object's transform
                    transform.position = newPosition;
                }
                else
                {
                    // Change the direction when the move distance is reached
                    movingLeft = !movingLeft;
                }
            }
            else
            {
                // Calculate the distance to the initial position
                float distanceToInitial = Vector3.Distance(transform.position, initialPosition);

                if (distanceToInitial > 0.01f) // Check if it's not too close to the initial position
                {
                    // Apply the new position to the object's transform
                    transform.position = newPosition;
                }
                else
                {
                    // Change the direction when the move distance is reached
                    movingLeft = !movingLeft;
                }
            }
        }
    }
}

