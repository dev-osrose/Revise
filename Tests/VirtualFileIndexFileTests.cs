#region License

/**
 * Copyright (C) 2011 Jack Wakefield
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#endregion

using System.IO;
using NUnit.Framework;
using Revise.IDX;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="VirtualFileIndexFile"/> class.
    /// </summary>
    [TestFixture]
    public class VirtualFileIndexFileTests {
        private const string TestFile = "Tests/Files/data.idx";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            const int FILE_SYSTEM_COUNT = 2;

            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            VirtualFileIndexFile virtualFileIndexFile = new VirtualFileIndexFile();
            virtualFileIndexFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
            Assert.That(FILE_SYSTEM_COUNT.Equals(virtualFileIndexFile.FileSystems.Count), "Incorrect file system count");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            VirtualFileIndexFile virtualFileIndexFile = new VirtualFileIndexFile();
            virtualFileIndexFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            virtualFileIndexFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            VirtualFileIndexFile savedVirtualFileIndexFile = new VirtualFileIndexFile();
            savedVirtualFileIndexFile.Load(savedStream);

            savedStream.Close();

            Assert.That(virtualFileIndexFile.BaseVersion.Equals(savedVirtualFileIndexFile.BaseVersion), "Base version values do not match");
            Assert.That(virtualFileIndexFile.CurrentVersion.Equals(savedVirtualFileIndexFile.CurrentVersion), "Current version values do not match");
            Assert.That(virtualFileIndexFile.FileSystems.Count.Equals(savedVirtualFileIndexFile.FileSystems.Count), "File system counts do not match");

            for (int i = 0; i < virtualFileIndexFile.FileSystems.Count; i++) {
                Assert.That(virtualFileIndexFile.FileSystems[i].FileName.Equals(savedVirtualFileIndexFile.FileSystems[i].FileName), "File names do not match");
                Assert.That(virtualFileIndexFile.FileSystems[i].Files.Count.Equals(savedVirtualFileIndexFile.FileSystems[i].Files.Count), "File counts do not match");

                for (int j = 0; j < virtualFileIndexFile.FileSystems[i].Files.Count; j++) {
                    Assert.That(virtualFileIndexFile.FileSystems[i].Files[j].FilePath.Equals(savedVirtualFileIndexFile.FileSystems[i].Files[j].FilePath), "File paths do not match");
                    Assert.That(virtualFileIndexFile.FileSystems[i].Files[j].Offset.Equals(savedVirtualFileIndexFile.FileSystems[i].Files[j].Offset), "Offset values do not match");
                    Assert.That(virtualFileIndexFile.FileSystems[i].Files[j].Size.Equals(savedVirtualFileIndexFile.FileSystems[i].Files[j].Size), "Size values do not match");
                    Assert.That(virtualFileIndexFile.FileSystems[i].Files[j].BlockSize.Equals(savedVirtualFileIndexFile.FileSystems[i].Files[j].BlockSize), "Block size values do not match");
                    Assert.That(virtualFileIndexFile.FileSystems[i].Files[j].IsDeleted.Equals(savedVirtualFileIndexFile.FileSystems[i].Files[j].IsDeleted), "Deleted values do not match");
                    Assert.That(virtualFileIndexFile.FileSystems[i].Files[j].IsCompressed.Equals(savedVirtualFileIndexFile.FileSystems[i].Files[j].IsCompressed), "Compresed values do not match");
                    Assert.That(virtualFileIndexFile.FileSystems[i].Files[j].IsEncrypted.Equals(savedVirtualFileIndexFile.FileSystems[i].Files[j].IsEncrypted), "Encrypted value sdo not match");
                    Assert.That(virtualFileIndexFile.FileSystems[i].Files[j].Version.Equals(savedVirtualFileIndexFile.FileSystems[i].Files[j].Version), "Version values do not match");
                    Assert.That(virtualFileIndexFile.FileSystems[i].Files[j].Checksum.Equals(savedVirtualFileIndexFile.FileSystems[i].Files[j].Checksum), "Checksum values do not match");
                }
            }
        }
    }
}