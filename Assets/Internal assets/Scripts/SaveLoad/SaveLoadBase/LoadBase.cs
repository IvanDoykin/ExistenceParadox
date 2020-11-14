public abstract class LoadBase<LoadingData> : InputOutputBase where LoadingData : Saveable
{
    //point of the enter with setting up of load-path
    public abstract bool TryLoadData(ref LoadingData loadingData, string key);

    //try get from dictionary OR from hard-drive (if data is there)
    protected abstract bool GetData(ref LoadingData loadingData, string key);

    //try find data in dictionary-cache
    protected abstract bool TryGetFromDictionary(ref LoadingData loadingData, string key);
}
