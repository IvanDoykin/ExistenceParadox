public abstract class LoadBase<LoadingData> : InputOutputBase where LoadingData : Saveable
{
    public abstract bool TryLoadData(ref LoadingData loadingData, string key);
    protected abstract LoadingData GetData(string key);
    protected abstract bool TryGetFromDictionary(out LoadingData loadingData, string key);
}
