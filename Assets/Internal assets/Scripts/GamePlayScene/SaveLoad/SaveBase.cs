public abstract class SaveBase<SavingData> : InputOutputBase where SavingData : Saveable
{
    public abstract void SaveAll();
    public abstract void WriteData(SavingData someData, string key);
}
