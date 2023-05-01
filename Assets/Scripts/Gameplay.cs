using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public GameplayUI gameplayUI;

    private Questions questions;
    private int currentQuestionIndex;
    [Space(20), Header("Question Round")]
    [SerializeField] private float roundTime = 20f;
    private float waitedRoundTime = 0;
    private bool roundStarted;

    [Space(20), Header("Score")]
    [SerializeField] private int score = 0;

    private void Start()
    {
        LoadData();
        GetNewQuestion();
    }

    private void FixedUpdate()
    {
        if (roundStarted)
        {
            waitedRoundTime += Time.fixedDeltaTime;
            if (waitedRoundTime >= roundTime)
            {
                EndRound();
                CheckAnswer(-1);
                waitedRoundTime = 0;
                UpdateScore(-3);
            }
            gameplayUI.SetTimerText(roundTime - waitedRoundTime);
        }
    }
    public void GetNewQuestion()
    {
        if (currentQuestionIndex > questions.questions.Length - 1)
        {
            return;
        }
        gameplayUI.ToggleVisualRestartButton(false);
        gameplayUI.ToggleVisualNextQuestionButton(false);
        QuestionData newQuestionData = questions.questions[currentQuestionIndex];
        gameplayUI.GenerateNewQuestion(newQuestionData);
        StartRound();
    }
    private void StartRound()
    {
        waitedRoundTime = 0;
        roundStarted = true;
    }
    private void EndRound()
    {
        roundStarted = false;
        gameplayUI.ToggleVisualNextQuestionButton(true);
    }
    private void LoadData()
    {
        string data = DataLoader.LoadedData("questions");
        if (!string.IsNullOrEmpty(data))
        {
            questions = JsonUtility.FromJson<Questions>(data);
        }
        else
        {
            Debug.LogError("Empty");
        }
    }
    private int GetAnswerIndex()
    {
        EndRound();
        roundStarted = false;
        int index = -1;
        for (int i = 0; i < questions.questions[currentQuestionIndex].choices.Length; i++)
        {
            if (questions.questions[currentQuestionIndex].choices[i].Substring(0, 1) == questions.questions[currentQuestionIndex].answer)
            {
                index = i;
                break;
            }
        }
        return index;
    }
    public void CheckAnswer(int index)
    {
        RoundState roundState = RoundState.Wrong;
        int trueIndex = GetAnswerIndex();
        currentQuestionIndex++;
        CheckGameplayFinish();
        if (trueIndex == -1)
        {
            return;
        }
        if (index == -1)
        {
            roundState = RoundState.Pass;
        }
        //true answer
        if (trueIndex == index)
        {
            roundState = RoundState.Correct;
            UpdateScore(10);
        }
        //false answer
        else
        {
            UpdateScore(-5);
        }
        gameplayUI.ShowAnswer(index, trueIndex, roundState);
    }

    private void UpdateScore(int point)
    {
        score += point;
        gameplayUI.UpdateScore(score);
    }
    private void CheckGameplayFinish()
    {
        if (currentQuestionIndex > questions.questions.Length - 1)
        {
            gameplayUI.ToggleVisualRestartButton(true);
            return;
        }
    }
}
public enum RoundState
{
    Correct,
    Wrong,
    Pass
}