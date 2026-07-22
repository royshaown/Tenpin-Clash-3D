using UnityEngine;

public class Pin : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        // সিনের মেইন GameManager-কে খুঁজে লিংক করে নেওয়া
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // পিনকে যখনই কোনো কিছু (যেমন বল) এসে ধাক্কা মারবে
    void OnCollisionEnter(Collision collision)
    {
        // ধাক্কা দেওয়া অবজেক্টটি যদি আমাদের বল হয়
        if (collision.gameObject.GetComponent<BallController>() != null)
        {
            if (gameManager != null)
            {
                // গেম ম্যানেজারকে সিগন্যাল দাও যে বল পিনকে হিট করেছে!
                gameManager.OnBallHitAnyPin();
            }
        }
    }
}