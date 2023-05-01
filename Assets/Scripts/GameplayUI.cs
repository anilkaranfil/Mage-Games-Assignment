using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class GameplayUI : MonoBehaviour
{
    [Header("Header")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI questionText;
    [Space(10),Header("Body")]
    [SerializeField] private QuestionUI questionUI;
    [Space(10), Header("Footer")]
    public GameObject nextQuestionButton;
    public GameObject restartButton;

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
    public void SetQuestionText(int currentQuestion)
    {
        questionText.text = "Question\n"+currentQuestion.ToString();
    }
    public void UpdateScore(float score)
    {
        scoreText.text = score.ToString();
    }
    public void ToggleVisualRestartButton(bool isVisible)
    {
        if (isVisible)
        {
            questionText.text = "";
            ScaleAnimation(restartButton.transform, .25f);
        }
        restartButton.SetActive(isVisible);
    }
    public void ToggleVisualNextQuestionButton(bool isVisible)
    {
        if (isVisible)
        {
            ScaleAnimation(nextQuestionButton.transform,.25f);
        }
        nextQuestionButton.SetActive(isVisible);
    }
    public void OpenGameplayScene()
    {
        SceneLoader.OpenGameplay();
    }
    private void ScaleAnimation(Transform animatedTransform,float time)
    {
        DOTween.Kill(animatedTransform.transform);
        animatedTransform.transform.localScale = Vector3.zero;
        animatedTransform.transform.DOScale(Vector3.one, time);
    }
}
