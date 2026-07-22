using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Forward Shot Settings (X-Axis)")]
    public float forwardSpeed = 15f;       
    
    [Header("Aiming Settings (Z-Axis)")]
    public float moveSpeed = 3f;           

    private Rigidbody rb;
    private bool isThrew = false;          
    private Vector3 startingPosition;       

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position; 
        if (rb != null) rb.useGravity = false;
    }

    void Update()
    {
        if (!isThrew)
        {
            float t = Mathf.PingPong(Time.time * moveSpeed, 1f);
            float currentZOffset = Mathf.Lerp(-0.65f, 0.1f, t);
            transform.position = new Vector3(startingPosition.x, startingPosition.y, startingPosition.z + currentZOffset);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ThrowBallForward();
            }
        }
    }

    void ThrowBallForward()
    {
        isThrew = true;
        if (rb != null)
        {
            rb.useGravity = true; 
            rb.linearVelocity = new Vector3(forwardSpeed, 0f, 0f);
        }
    }

    public void StopBallCompletely()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false; 
        }
    }

    public void ResetBall(Vector3 resetPos)
    {
        isThrew = false;
        transform.position = resetPos;
        startingPosition = resetPos; 

        if (rb != null)
        {
            rb.useGravity = false;             
            rb.linearVelocity = Vector3.zero;  
            rb.angularVelocity = Vector3.zero;
        }
    }
}