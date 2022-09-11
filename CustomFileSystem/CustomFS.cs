using DiskAccessLibrary.FileSystems.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CustomFileSystem
{
    public class CustomFS : DiskAccessLibrary.FileSystems.Abstractions.FileSystem
    {
        public string SharePath { get; }

        public CustomFS(string sharePath)
        {
            SharePath = sharePath;
        }

        public override string Name => throw new NotImplementedException();

        public override long Size => throw new NotImplementedException();

        public override long FreeSpace => throw new NotImplementedException();

        public override bool SupportsNamedStreams
        {
            get
            {
                return false;
            }
        }

        public override FileSystemEntry CreateDirectory(string path)
        {
            throw new NotImplementedException();
        }

        public override FileSystemEntry CreateFile(string path)
        {
            throw new NotImplementedException();
        }

        public override void Delete(string path)
        {
            throw new NotImplementedException();
        }


        /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        public override FileSystemEntry GetEntry(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new System.IO.IOException($@"Yol Boş Olamaz. {path}");
            }
            string FileName = path;
            if (FileName.StartsWith(@"\"))
            {
                FileName = FileName.Substring(1);
            }

            string FullName = Path.Combine(SharePath, FileName);

            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(FullName);

            //detect whether its a directory or file
            bool IsDirectory = (attr & FileAttributes.Directory) == FileAttributes.Directory;

            FileInfo fileInfo = new FileInfo(FullName);
            ulong FileSize = 0;
            if (false == IsDirectory)
            {                
                FileSize = (ulong)fileInfo.Length;
            }            

            DateTime CreationTime = fileInfo.CreationTime;
            DateTime LastWriteTime = fileInfo.LastWriteTime;
            DateTime LastAccessTime = fileInfo.LastAccessTime;
            bool IsHidden = (attr & FileAttributes.Hidden) == FileAttributes.Hidden;
            bool IsReadonly = (attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
            bool IsArchived = (attr & FileAttributes.Archive) == FileAttributes.Archive;

            var ret = new FileSystemEntry(FullName, FileName, IsDirectory, FileSize, CreationTime, LastWriteTime, LastAccessTime, IsHidden, IsReadonly, IsArchived);

            return ret;
        }

        public override List<FileSystemEntry> ListEntriesInDirectory(string path)
        {
            throw new NotImplementedException();
        }

        public override void Move(string source, string destination)
        {
            throw new NotImplementedException();
        }

        public override Stream OpenFile(string path, FileMode mode, FileAccess access, FileShare share, FileOptions options)
        {
            var fullPath = Path.Combine(SharePath, path.Substring(1));
            System.IO.FileStream fileStream = new FileStream(fullPath, mode, access, share, 4096 ,options );
            return fileStream;
        }

        public override void SetAttributes(string path, bool? isHidden, bool? isReadonly, bool? isArchived)
        {
            throw new NotImplementedException();
        }

        public override void SetDates(string path, DateTime? creationDT, DateTime? lastWriteDT, DateTime? lastAccessDT)
        {
            throw new NotImplementedException();
        }
    }
}
