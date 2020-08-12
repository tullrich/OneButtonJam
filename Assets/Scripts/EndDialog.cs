using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndDialog : MonoBehaviour
{
    public TMP_Text guessValue;
    public TMP_Text actualValue;
    public GameObject correct;
    public GameObject incorrect;

    public void Show(int guess, int actual)
    {
        gameObject.SetActive(true);
        guessValue.text = guess.ToString();
        actualValue.text = actual.ToString();
        correct.SetActive(guess == actual);
        incorrect.SetActive(guess != actual);
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene("TallyScene");
    }
}
