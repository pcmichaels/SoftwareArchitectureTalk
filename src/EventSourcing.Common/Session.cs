namespace EventSourcing.Common;

using System.Text.Json;

public class Session<T>
{
    private string _file;
    private IList<T> _sessions;

    public void Initialise(string file) =>  
        _file = file;    

    public void StartSession()
    {
        var existingData = Read();
        if (existingData == null)
            _sessions = new List<T>();
        else
            _sessions = existingData;
    }

    public void Store(T value)
    {
        _sessions.Add(value);
    }

    public IList<T>? Read()
    {
        if (!File.Exists(_file)) return null;

        string json = File.ReadAllText(_file);
        IList<T>? values = JsonSerializer.Deserialize<IList<T>>(json);
        if (values?.Any() ?? false)
        {
            return values;
        }

        return null;
    }

    public void Commit()
    {
        string json = JsonSerializer.Serialize(_sessions);
        File.WriteAllText(_file, json);

    }
}