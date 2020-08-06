public abstract class LoadBase<LoadingData> : InputOutputBase where LoadingData : Saveable
{
    public abstract bool TryLoadData(ref LoadingData loadingData, string key);
    protected abstract bool GetData(ref LoadingData loadingData, string key);
    protected abstract bool TryGetFromDictionary(ref LoadingData loadingData, string key);
}
