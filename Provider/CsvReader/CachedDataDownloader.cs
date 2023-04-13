using System.Net;

namespace Provider.CsvReader;

public sealed class CachedDataDownloader
{
    private readonly string _dataFolder;

    public CachedDataDownloader()
    {
        _dataFolder = Path.Join(Path.GetTempPath(), "cached_data_downloader");
        Directory.CreateDirectory(_dataFolder);
        Console.WriteLine($"CachedDataDownloader -> {_dataFolder}");
    }

    public FileStream GetReaderFromUrl(string url)
    {
        var fileName = GetCacheFileName(url);
        if (!File.Exists(fileName))
        {
            fileName = DownloadFileFromUrl(url);
        }
        else
        {
            Console.WriteLine($"GetReaderFromUrl({url}) FROM CACHE {fileName}");
        }

        return File.OpenRead(fileName);
    }

    private string DownloadFileFromUrl(string url)
    {
        Console.WriteLine($"Downloading file from {url}");
        var filename = GetCacheFileName(url);

        using var wc = new WebClient();
        wc.DownloadFile(url, filename);
        Console.WriteLine($"File saved to {filename}");
        return filename;
    }

    private string GetCacheFileName(string url)
    {
        var p = new Uri(url).LocalPath;
        return Path.Join(_dataFolder, p.Replace('/', '_'));
    }
}
