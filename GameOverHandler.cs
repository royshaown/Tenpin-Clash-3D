using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // সিন রিস্টার্ট করার জন্য এটি লাগবে

public class GameOverHandler : MonoBehaviour
{
    // অন্য যেকোনো স্ক্রিপ্ট থেকে সহজে কল করার জন্য এই স্ট্যাটিক ইন্সট্যান্স
    public static GameOverHandler Instance;

    [Header("UI Settings")]
    public GameObject gameOverPanel; // আপনার গেম ওভার প্যানেলটি এখানে দেবেন

    void Awake()
    {
        // সিঙ্গেলটন প্যাটার্ন সেটআপ
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // গেম শুরু হওয়ার সময় প্যানেলটি নিজে থেকেই লুকিয়ে (Hide) যাবে
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // গেম ওভার করার মেইন ফাংশন (যেকোনো জায়গা থেকে এটি কল করা যাবে)
    public void TriggerGameOver()
    {
        Debug.Log("গেম ওভার হয়েছে! ৪ সেকেন্ড কাউন্টডাউন শুরু...");

        // ১. স্ক্রিনে গেম ওভার প্যানেলটি অন করা হলো
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // ২. ৪ সেকেন্ড অপেক্ষা করার টাইমার বা কোরুটিন চালু করা হলো
        StartCoroutine(WaitAndRestartCoroutine());
    }

    IEnumerator WaitAndRestartCoroutine()
    {
        // ঠিক ৪ সেকেন্ড অপেক্ষা করবে
        yield return new WaitForSeconds(4f);

        Debug.Log("৪ সেকেন্ড শেষ! গেম রিস্টার্ট হচ্ছে...");

        // ৩. বর্তমান সিনটি একদম প্রথম থেকে রিলোড/রিস্টার্ট হবে
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}