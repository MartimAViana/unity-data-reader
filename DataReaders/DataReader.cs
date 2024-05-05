using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.UIElements;

public abstract class DataReader
{
    private Dictionary<string, Factory> factories = new Dictionary<string, Factory>();

    public DataReader() {}

    public abstract Object[] InterpretFile(string type, string content);
    public abstract string DecryptFileContent(string content);

    public Object[] ReadFile(string type, string filepath) {
        string content = File.ReadAllText(filepath);
        content = DecryptFileContent(content);
        return InterpretFile(type, content);
    }

    public Boolean CanConvert(string value, Type type) {
        TypeConverter converter = TypeDescriptor.GetConverter(type);
        return converter.IsValid(value);
    }

    public Object ConvertToType(string value) {
        if (CanConvert(value, typeof(int))) {
            return int.Parse(value);
        }
        if (CanConvert(value, typeof(float))) {
            return float.Parse(value);
        }
        if (CanConvert(value, typeof(double))) {
            return double.Parse(value);
        }
        if (CanConvert(value, typeof(bool))) {
            return bool.Parse(value);
        }
        if (value.Contains("[") && value.Contains("]")) {
            string[] numbers = value.Split(",");
            List<object> result = new List<object>();
            numbers.ToList().ForEach(number => {
                result.Add(ConvertToType(number));
            });
            return result.ToArray();
        }
        else {
            return value;
        }
    }

    // Setters
    public void AddFactory(string id, Factory factory) {
        factories.Add(id, factory);
    }

    // Getters
    public Factory GetFactory(string type) {
        return factories[type];
    }
}
