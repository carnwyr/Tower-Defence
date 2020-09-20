using System;

public interface IGoldController
{
    event Action<int> GoldChanged;

    int GetGold();
    void ResetGold();
    void ChangeGold(int amount);
}
