using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TallyGameManager : MonoBehaviour
{
    public Image roundTimer;
    public TMP_Text roundText;
    public TMP_Text counterText;
    public float roundLength;
    public WalkerSpawner[] spawners;
    public Button startButton;
    public Button counterButton;
    public TMP_Text instructionsText;
    public EndDialog endDialog;
    float roundTimeRemaining;
    bool gameplayRunning;
    int counter;

    // Start is called before the first frame update
    void Start()
    {
        roundTimeRemaining = roundLength;
        counterButton.interactable = false;
        UpdateRoundTimerGUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameplayRunning)
        {
            // Update remaining round time
            roundTimeRemaining = Mathf.Max(roundTimeRemaining - Time.deltaTime, 0);
            UpdateRoundTimerGUI();

            // Detect end of round
            if (roundTimeRemaining <= 0 && gameplayRunning)
            {
                OnRoundEnded();
            }
        }
    }

    void UpdateRoundTimerGUI()
    {
        roundTimer.fillAmount = roundTimeRemaining / roundLength;
        roundText.text = $"{(int)roundTimeRemaining / 60}:{((int)roundTimeRemaining % 60):D2}";
    }

    void OnRoundEnded()
    {
        gameplayRunning = false;
        counterButton.interactable = false;
        int actual = spawners.Aggregate(0, (accum, spawner) => accum + spawner.maxSpawnCount);
        endDialog.Show(counter, actual);
    }

    public void OnStartGameClicked()
    {
        gameplayRunning = true;
        instructionsText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        counterButton.interactable = true;
        foreach (WalkerSpawner s in spawners)
        {
            s.OnGameplayStart();
        }
    }

    public void OnCounterClicked()
    {
        if (gameplayRunning)
        {
            counter++;
            counterText.text = $"Count: {counter}";
        }
    }
}
