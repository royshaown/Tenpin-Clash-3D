using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Ball & UI Settings")]
    public BallController ball;
    public TextMeshProUGUI totalCoinText;
    
    [Header("Final Panel Settings")]
    public GameObject finalPanel;          
    public TextMeshProUGUI finalCoinText;  

    private List<GameObject> pins = new List<GameObject>();
    private List<Vector3> pinPositions = new List<Vector3>();
    private List<Quaternion> pinRotations = new List<Quaternion>();

    private Vector3 ballStartPosition;
    private int totalCoins = 0;
    private int currentRound = 0;
    private const int maxRounds = 10;
    private bool isRoundProcessing = false;

    void Start()
    {
        Pin[] foundPins = FindObjectsByType<Pin>(FindObjectsSortMode.None);
        foreach (Pin pinScript in foundPins)
        {
            GameObject pin = pinScript.gameObject;
            pins.Add(pin);
            pinPositions.Add(pin.transform.position);
            pinRotations.Add(pin.transform.localRotation);
        }

        if (ball != null)
        {
            ballStartPosition = ball.transform.position;
        }

        if (finalPanel != null) finalPanel.SetActive(false);
        UpdateCoinUI();
    }

    public void OnBallHitAnyPin()
    {
        if (!isRoundProcessing)
        {
            isRoundProcessing = true;
            Debug.Log("Ball hit a pin! Starting 5 seconds countdown...");
            StartCoroutine(CheckScoreAfterDelay(5f)); 
        }
    }

    IEnumerator CheckScoreAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (ball != null)
        {
            ball.StopBallCompletely();
            Debug.Log("Ball speed forced to 0.");
        }

        int fallenPins = 0;
        for (int i = 0; i < pins.Count; i++)
        {
            if (pins[i] != null)
            {
                float rotationDifference = Quaternion.Angle(pins[i].transform.localRotation, pinRotations[i]);
                if (rotationDifference > 5f) 
                {
                    fallenPins++;
                }
            }
        }

        totalCoins += fallenPins;
        currentRound++;

        Debug.Log($"রাউন্ড: {currentRound} | পিন পড়েছে: {fallenPins} | মোট কয়েন: {totalCoins}");

        if (currentRound >= maxRounds)
        {
            ShowFinalPanel();
        }
        else
        {
            UpdateCoinUI();
            ResetRound(); 
        }
    }

    void UpdateCoinUI()
    {
        if (totalCoinText != null)
        {
            totalCoinText.text = "Round: " + (currentRound + 1) + "/" + maxRounds + "\nTotal Coins: " + totalCoins;
        }
    }

    void ResetRound()
    {
        if (ball != null) ball.ResetBall(ballStartPosition);

        if (Camera.main != null)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, ballStartPosition.z - 4f);
        }

        for (int i = 0; i < pins.Count; i++)
        {
            if (pins[i] != null)
            {
                pins[i].SetActive(true); 
                pins[i].transform.position = pinPositions[i];
                pins[i].transform.localRotation = pinRotations[i];

                Rigidbody rb = pins[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.ResetInertiaTensor();
                    rb.Sleep();
                    rb.WakeUp();
                }
            }
        }

        isRoundProcessing = false;
        Debug.Log("Next Round Ready!");
    }

    void ShowFinalPanel()
    {
        if (ball != null) ball.gameObject.SetActive(false);
        foreach (GameObject pin in pins) if (pin != null) pin.SetActive(false);
        if (totalCoinText != null) totalCoinText.gameObject.SetActive(false);
        if (finalPanel != null) finalPanel.SetActive(true);
        if (finalCoinText != null) finalCoinText.text = "GAME OVER\n\nFinal Coins: " + totalCoins;
    }
}