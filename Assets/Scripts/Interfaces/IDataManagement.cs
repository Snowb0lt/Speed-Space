public interface IDataMangement
{
    bool SaveData<T>(string RelativePath, T data, bool Encrypted);

    T LoadData<T>(string RelativePath, bool Encrypted);


}
