using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class JSONDataReader : DataReader
{
    public override string DecryptFileContent(string content)
    {
        return content;
    }

    public override object[] InterpretFile(string type, string content)
    {
        List<object> result = new List<object>();
        var jsonContent = fsJsonParser.Parse(content);
        jsonContent.AsList.ForEach(entry => {
            Dictionary<string, object> dict = InterpretEntry(entry);
        });
        return result.ToArray();
    }

    private Dictionary<string, object> InterpretEntry(fsData entry) {
        Dictionary<string, object> result = new Dictionary<string, object>();
        entry.AsDictionary.Keys.ToList().ForEach(key => {
            result.Add(key, ConvertToType(entry.AsDictionary[key].AsString));
        });
        return result;
    }
}
