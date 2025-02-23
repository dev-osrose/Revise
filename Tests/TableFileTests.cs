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
using Revise.TBL;

namespace Revise.Tests {
    /// <summary>
    /// Provides testing for the <see cref="TableFile"/> class.
    /// </summary>
    [TestFixture]
    public class TableFileTests {
        private const string TestFile = "Tests/Files/O_RANGE.TBL";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            const int MAXIMUM_RANGE = 42;

            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            TableFile tableFile = new TableFile();
            tableFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
            Assert.That(MAXIMUM_RANGE.Equals(tableFile.MaximumRange), "Incorrect maximum range");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            TableFile tableFile = new TableFile();
            tableFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            tableFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            TableFile savedTableFile = new TableFile();
            savedTableFile.Load(savedStream);

            savedStream.Close();

            Assert.That(tableFile.MaximumRange.Equals(savedTableFile.MaximumRange), "Maximum range does not match");

            for (int i = 0; i < tableFile.MaximumRange; i++) {
                Assert.That(tableFile.StartIndices[i].Equals(tableFile.StartIndices[i]), "Start index does not match");
                Assert.That(tableFile.IndexCounts[i].Equals(tableFile.IndexCounts[i]), "Index count does not match");
            }

            Assert.That(tableFile.Points.Count.Equals(savedTableFile.Points.Count), "Points count does not match");

            for (int i = 0; i < tableFile.Points.Count; i++) {
                Assert.That(tableFile.Points[i].X.Equals(tableFile.Points[i].X), "Points do not match");
                Assert.That(tableFile.Points[i].Y.Equals(tableFile.Points[i].Y), "Points do not match");
            }
        }
    }
}