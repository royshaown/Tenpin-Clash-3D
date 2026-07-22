using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // এখানে আমাদের Bowling Ball থাকবে
    public Vector3 offset = new Vector3(0, 2f, -4f); // বল থেকে ক্যামেরার দূরত্ব
    public float laneEndZ = 12f; // লেনের শেষ প্রান্ত যেখানে ক্যামেরা আর এগোবে না

    void LateUpdate()
    {
        if (target == null) return;

        // ক্যামেরা শুধু Z অক্ষ বরাবর বলকে ফলো করবে, ডানে-বামে নড়বে না
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
        
        // বলটি লেনের বাইরে যাওয়ার আগেই ক্যামেরা থামিয়ে দেওয়ার জন্য
        if (target.position.z < laneEndZ)
        {
            transform.position = new Vector3(transform.position.x, target.position.y + offset.y, target.position.z + offset.z);
        }
    }
}