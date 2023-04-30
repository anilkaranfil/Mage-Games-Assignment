using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour
{
    [Header("Question Components")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private QuestionButton[] choices;

    [Space(20),Header("Button Colors")]
    [SerializeField] private Color correctColor;
    [SerializeField] private Color wrongColor;
    [SerializeField] private Color baseColor;

    public void SetQuestionChoices(QuestionData questionData)
    {
        this.questionText.text = questionData.question;
        for (int i = 0; i < choices.Length; i++)
        {
            this.choices[i].choicesText.text = questionData.choices[i];
            var colors = choices[i].choicesButton.colors;
            colors.disabledColor = baseColor;
            this.choices[i].choicesButton.colors = colors;
        }
        ButtonInteraction(true);
    }
    private void ButtonInteraction(bool interactable)
    {
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].choicesButton.interactable = interactable;
        }
    }
    public void ShowAnswer(int playerAnswer, int trueAnswer, RoundState roundState)
    {
        ButtonInteraction(false);
        if (roundState == RoundState.Correct)
        {
            var colors = choices[playerAnswer].choicesButton.colors;
            colors.disabledColor = correctColor;
            choices[playerAnswer].choicesButton.colors = colors;
        }
        else if (roundState == RoundState.Wrong)
        {
            var colors = choices[playerAnswer].choicesButton.colors;
            colors.disabledColor = wrongColor;
            choices[playerAnswer].choicesButton.colors = colors;
            colors.disabledColor = correctColor;
            choices[trueAnswer].choicesButton.colors = colors;
        }
        else if (roundState == RoundState.Pass)
        {
            var colors = choices[trueAnswer].choicesButton.colors;
            colors.disabledColor = correctColor;
            choices[trueAnswer].choicesButton.colors = colors;
        }
    }
}
[System.Serializable]
public struct QuestionButton
{
    public TextMeshProUGUI choicesText;
    public Button choicesButton;
}