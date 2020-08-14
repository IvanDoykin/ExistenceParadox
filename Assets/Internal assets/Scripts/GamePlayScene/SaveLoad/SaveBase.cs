public abstract class SaveBase<SavingData> : InputOutputBase where SavingData : Saveable
{
    //Save all cached data and clear dictionary-cache
    public abstract void SaveAll();

    //Write data at the dictionary-cache
    public abstract void WriteData(SavingData someData, string key);
}
