using System;

public interface IGoldController
{
    event Action<int> GoldChanged;

    void ResetGold();
    void ChangeGold(int amount);
}
