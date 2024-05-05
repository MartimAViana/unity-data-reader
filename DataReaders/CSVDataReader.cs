using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.UIElements;

public class CSVDataReader : DataReader
{
    private static string rowSeparator = "\n";
    private string colSeparator;

    public CSVDataReader(string colSeparator) {
        this.colSeparator = colSeparator;
    }

    public override string DecryptFileContent(string content)
    {
        return content;
    }

    public override object[] InterpretFile(string type, string content)
    {
        List<object> result = new List<object>();
        string[] rows = content.Split(rowSeparator);
        string[] headers = rows[0].Split(colSeparator);
        rows = rows.Skip(1).Take(rows.Length - 2).ToArray();
        foreach (string row in rows) {
            Dictionary<string, object> rowDict = GetRowDictionary(headers, row.Split(colSeparator));
            result.Add(GetFactory(type).Create(rowDict));
        }
        return result.ToArray();
    }

    private Dictionary<string, object> GetRowDictionary(string[] headers, string[] cells) {
        Dictionary<string, object> result = new Dictionary<string, object>();
        for (int i = 0; i < cells.Length; i++) {
            result.Add(headers[i], ConvertToType(cells[i]));
        }
        return result;
    }
}
