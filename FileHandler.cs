using System;
using System.Collections.Generic;
using System.IO;

namespace CleanCodeProject.C10;

public class FileHandler
{
    // 前置條件：path 不為空且檔案存在
    // 後置條件：返回檔案內容
    public virtual string ReadFile(string path)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentException("路徑不能為空");

        if (!File.Exists(path))
            throw new FileNotFoundException();

        return File.ReadAllText(path);
    }
}

// 符合 LSP 的子類 - 放寬前置條件
public class CachedFileHandler : FileHandler
{
    private Dictionary<string, string> cache = new Dictionary<string, string>();

    public override string ReadFile(string path)
    {
        // 符合 LSP：放寬前置條件（允許從快取讀取）
        if (cache.ContainsKey(path))
            return cache[path];

        string content = base.ReadFile(path);
        cache[path] = content;
        return content;
    }
}