using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class GameplayUI : MonoBehaviour
{
    [Header("Question UI")]
    [SerializeField] private QuestionUI questionUI;
    [Space(10), Header("Time UI")]
    [SerializeField] private TextMeshProUGUI timeText;
    [Space(10), Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [Space(10), Header("Round Result")]
    public GameObject nextQuestionButton;

    public void OpenMainMenuScene()
    {
        SceneLoader.OpenMainMenu();
    }
    public void GenerateNewQuestion(QuestionData questionData)
    {
        questionUI.SetQuestionChoices(questionData);
    }

    public void ShowAnswer(int answerIndex, int trueIndex, RoundState roundState)
    {
        questionUI.ShowAnswer(answerIndex, trueIndex, roundState);
    }

    public void SetTimerText(float spendedTime)
    {
        timeText.text = spendedTime.ToString("F0");
    }

    public void ToggleRoundEnd(bool isVisible)
    {
        nextQuestionButton.SetActive(isVisible);
    }
    public void UpdateScore(float score)
    {
        scoreText.text = score.ToString();
    }
}
