public interface Subject
{
    void RoundNotify();
    void TurnNotify();
    void EndNotify();
}

public interface Observer
{
    void TurnUpdate(int round, string turn);
    void FinishUpdate(bool isFinish);
}
