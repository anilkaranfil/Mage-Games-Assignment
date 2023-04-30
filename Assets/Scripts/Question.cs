[System.Serializable]
public class QuestionData
{
    public string category;
    public string question;
    public string[] choices;
    public string answer;

    public QuestionData(string category, string question, string[] choices, string answer)
    {
        this.category = category;
        this.question = question;
        this.choices = choices;
        this.answer = answer;
    }
}
[System.Serializable]
public class Questions
{
    public QuestionData[] questions;
}