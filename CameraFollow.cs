using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset = new Vector3(0, 2f, -4f); 
    public float laneEndZ = 12f; 

    void LateUpdate()
    {
        if (target == null) return;

        
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
        
       
        if (target.position.z < laneEndZ)
        {
            transform.position = new Vector3(transform.position.x, target.position.y + offset.y, target.position.z + offset.z);
        }
    }
}
