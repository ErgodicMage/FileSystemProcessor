using System.Collections.Generic;
using System.IO;

using FileSystem;

namespace FileSystemUnitTests
{
    public static class TestUtilities
    {
        public static IList<string> RunEnumerationToList(IFindFileSystem ffs)
        {
            IList<string> values = new List<string>();
            foreach (var f in ffs.Enumerate())
                values.Add(f.FullName);
            return values;
        }

        // Filters
        public static bool FileIsPDF(FileSystemInfo info) => info.Name.EndsWith(".pdf");
        public static bool FileGreaterThan1MB(FileSystemInfo info) => ((FileInfo)info).Length > 1048576;
        public static bool FileIsPDFGreaterThan1MB(FileSystemInfo info) => FileIsPDF(info) && FileGreaterThan1MB(info);
        public static bool DirectoryHasQRCodes(FileSystemInfo info) => info.Name.Equals("QRCodes");
        public static bool IsCServeWS(FileSystemInfo info) => info.FullName.Contains(@"\CServeWS\");
        public static bool IsCServeWSOrDDPEA(FileSystemInfo info) => IsCServeWS(info) || info.FullName.Contains(@"\DDPEA\");
        public static void WriteToFile(string path, IList<string> values)
        {
            if (File.Exists(path))
                File.Delete(path);

            using (StreamWriter writer = new StreamWriter(path))
            {
                WriteToStreamWriter(writer, values);
            }
        }
        public static void WriteToStreamWriter(StreamWriter writer, IList<string> values)
        {
            foreach (var v in values)
                writer.WriteLine(v);
        }
    }
}
