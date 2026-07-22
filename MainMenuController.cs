using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // সিন পরিবর্তনের জন্য এই নেমস্পেসটি অত্যন্ত প্রয়োজনীয়

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("আপনার মেইন গেম সিনের সঠিক নাম এখানে লিখুন")]
    public string gameSceneName = "MainGameScene"; 

    [Header("Delay Settings")]
    [Tooltip("কত সেকেন্ড অপেক্ষা করে গেম চালু হবে")]
    public float delayBeforeLoad = 2f;

    void Start()
    {
        // মেনু সিন চালু হওয়ার সাথে সাথেই কোরুটিনটি রান হবে
        StartCoroutine(LoadGameWithDelay());
    }

    IEnumerator LoadGameWithDelay()
    {
        // নির্ধারিত ২ সেকেন্ড অপেক্ষা করবে
        yield return new WaitForSeconds(delayBeforeLoad);

        // ২ সেকেন্ড পার হওয়ার পর মেইন গেমের সিনটি লোড হবে
        SceneManager.LoadScene(gameSceneName);
    }
}