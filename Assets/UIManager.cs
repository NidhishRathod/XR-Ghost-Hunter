using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject scorePrefab; // Assign the score prefab
    public GameObject heartPrefab; // Assign the heart prefab
    public Transform heartContainer; // Assign the HeartContainer Transform

    private TextMeshProUGUI scoreText; // Reference to the score text component
    private int score = 0; // Player's score
    private int maxHearts = 5; // Maximum hearts
    private int currentHearts; // Current hearts
    private List<GameObject> hearts = new List<GameObject>(); // List to hold heart instances

    void Start()
    {
        GameObject scoreObj = Instantiate(scorePrefab, transform);
        scoreText = scoreObj.GetComponent<TextMeshProUGUI>();

        currentHearts = maxHearts;
        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            hearts.Add(heart);
        }
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void DecreaseHeart()
    {
        if (currentHearts > 0)
        {
            Destroy(hearts[currentHearts - 1]);
            hearts.RemoveAt(currentHearts - 1);
            currentHearts--;
        }
    }
}
