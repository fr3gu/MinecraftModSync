using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows;
using ModSync.WPF.Models;
using Newtonsoft.Json;

namespace ModSync.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ListBox1.DataContext = GetModInfos(@"C:\temp\modsync_test");
        }

        private IEnumerable<ModArchiveInfo> GetModInfos(string path)
        {
            if (!DirectoryExists(path)) return null;
            var jars = GetDirContent(path);
            var retVal = new List<ModArchiveInfo>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var jar in jars)
            {
                var modArchiveInfo = GetMcModInfoFromPath(jar);
                if (modArchiveInfo == null) continue;
                retVal.Add(modArchiveInfo);
            }
            return retVal.AsEnumerable();

        }

        private ModArchiveInfo GetMcModInfoFromPath(string jar)
        {
            var archive = ZipFile.OpenRead(jar);
            var infoFile = archive.Entries.FirstOrDefault(f => f.Name == "mcmod.info");

            if (infoFile == null) return null;

            var modArchiveInfo = GetMcModArchiveInfo(infoFile, jar);
            archive.Dispose();
            return modArchiveInfo;
        }

        private ModArchiveInfo GetMcModArchiveInfo(ZipArchiveEntry infoFile, string jar)
        {
            var fileContent = GetMcModInfoFileContent(infoFile);
            var info = JsonConvert.DeserializeObject<McModInfo[]>(fileContent);
            var modArchiveInfo = new ModArchiveInfo(jar, new McModInfoRoot(info));
            return modArchiveInfo;
        }

        private string GetMcModInfoFileContent(ZipArchiveEntry infoFile)
        {
            var tempPathName = string.Format(@"{0}\mcmodsync\", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp"));
            Directory.CreateDirectory(tempPathName);
            var tempFileName = Path.Combine(tempPathName, string.Format("{0}mcmod.info", Guid.NewGuid()));
            infoFile.ExtractToFile(tempFileName);
            var fileContent = GetFileContent(tempFileName);
            Directory.Delete(tempPathName, true);
            return fileContent;
        }

        private string GetFileContent(string tempFileName)
        {
            var sr = new StreamReader(tempFileName);
            var fileContent = sr.ReadToEnd();
            sr.Close();
            return fileContent;
        }

        private IEnumerable<string> GetDirContent(string path)
        {
            var jars = Directory.GetFiles(path, "*.jar", SearchOption.TopDirectoryOnly);

            return jars.Any() ? jars.AsEnumerable() : null;
        }

        private bool DirectoryExists(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }

        private void Button1_OnClick(object sender, RoutedEventArgs e)
        {
            ListBox1.DataContext = GetModInfos(@"C:\temp\modsync_test");
        }
    }
}
