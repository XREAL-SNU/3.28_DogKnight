public interface Subject
{
    void RoundNotify();
    void TurnNotify();
}

public interface Observer
{
    void TurnUpdate(int round, string turn, Character character);
    void FinishUpdate(bool isFinish);
}
