[System.Serializable]
public class Page
{
    public int page;
    public bool is_last;
    public Data[] data;

    public Page(int page, bool is_last, Data[] data)
    {
        this.page = page;
        this.is_last = is_last;
        this.data = data;
    }
    public bool IsLastDataItem(Data data)
    {
        if (this.data[this.data.Length-1].rank==data.rank)
        {
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class Data
{
    public int rank;
    public string nickname;
    public int score;
}