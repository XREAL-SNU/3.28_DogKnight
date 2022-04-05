public interface Subject
{
    void RoundNotify();
    void TurnNotify();
    //void EndNotify();
    //public abstract void AddObserver(Observer o);
}

public interface Observer
{
    void TurnUpdate(int round, string turn);
    void FinishUpdate(bool isFinish);
}
