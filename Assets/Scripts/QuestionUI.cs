using DG.Tweening;
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
            if (interactable)
            {
                ScaleAnimation(choices[i].choicesButton.transform, .2f, i * .2f);
            }
            choices[i].choicesButton.interactable = interactable;
        }
    }
    public void ShowAnswer(int playerAnswer, int trueAnswer, RoundState roundState)
    {
        ButtonInteraction(false);
        if (roundState == RoundState.Correct)
        {
            ChangeColorButton(choices[playerAnswer].choicesButton,correctColor);
        }
        else if (roundState == RoundState.Wrong)
        {
            ChangeColorButton(choices[playerAnswer].choicesButton, wrongColor);
            ChangeColorButton(choices[trueAnswer].choicesButton, correctColor);
        }
        else if (roundState == RoundState.Pass)
        {
            ChangeColorButton(choices[trueAnswer].choicesButton, correctColor);
        }
    }
    private void ChangeColorButton(Button button,Color color)
    {
        var colors = button.colors;
        colors.disabledColor = color;
        button.colors = colors;
    }
    private void PunchScaleAnimation(Transform animatedTransform, float time)
    {
        DOTween.Kill(animatedTransform.transform);
        animatedTransform.transform.localScale = Vector3.one;
        animatedTransform.transform.DOPunchScale(Vector3.one * 1.1f, time);
    }
    private void ScaleAnimation(Transform animatedTransform, float time,float delay)
    {
        DOTween.Kill(animatedTransform.transform);
        animatedTransform.transform.localScale = Vector3.zero;
        animatedTransform.transform.DOScale(Vector3.one, time).SetDelay(delay);
    }
}
[System.Serializable]
public struct QuestionButton
{
    public TextMeshProUGUI choicesText;
    public Button choicesButton;
}