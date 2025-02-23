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

using System;
using System.IO;
using NUnit.Framework;
using Revise.STB;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="DataFile"/> class.
    /// </summary>
    [TestFixture]
    public class DataFileTests {
        private const string TestFile = "Tests/Files/LIST_QUEST.STB";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            const int ROW_COUNT = 5501;
            const int COLUMN_COUNT = 6;

            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            DataFile dataFile = new DataFile();
            dataFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
            Assert.That(ROW_COUNT.Equals(dataFile.RowCount), "Incorrect row count");
            Assert.That(COLUMN_COUNT.Equals(dataFile.ColumnCount), "Incorrect column count");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            DataFile dataFile = new DataFile();
            dataFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            dataFile.Save(savedStream);
            savedStream.Seek(0, SeekOrigin.Begin);

            DataFile savedDataFile = new DataFile();
            savedDataFile.Load(savedStream);
            savedStream.Close();

            Assert.That(dataFile.RowCount.Equals(savedDataFile.RowCount), "Row counts do not match");
            Assert.That(dataFile.ColumnCount.Equals(savedDataFile.ColumnCount), "Column counts do not match");

            for (int i = 0; i < dataFile.RowCount; i++) {
                for (int j = 0; j < dataFile.ColumnCount; j++) {
                    Assert.That(dataFile[i][j].Equals(savedDataFile[i][j]), "Cell values do not match");
                }
            }
        }

        /// <summary>
        /// Tests the column and row methods.
        /// </summary>
        [Test]
        public void TestColumnAndRowMethods() {
            const string COLUMN_HEADER = "Test Column";
            const int COLUMN_WIDTH = 101;
            const string CELL_VALUE = "Test Value";

            DataFile dataFile = new DataFile();
            dataFile.AddColumn(COLUMN_HEADER, COLUMN_WIDTH);
            dataFile.AddRow();

            dataFile[0][0] = CELL_VALUE;

            Assert.That(dataFile.GetColumnName(0).Equals(COLUMN_HEADER), "Incorrect column header");
            Assert.That(dataFile.GetColumnWidth(0).Equals(COLUMN_WIDTH), "Incorrect column width");
            Assert.That(dataFile[0][0].Equals(CELL_VALUE), "Incorrect cell value");

            dataFile.RemoveColumn(0);

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => {
                dataFile.GetColumnName(0);
            }, "Column not removed");

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => {
                dataFile[0][0] = CELL_VALUE;
            }, "Cell not removed");

            dataFile.RemoveRow(0);

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => {
                dataFile[0][0] = CELL_VALUE;
            }, "Row not removed");
        }
    }
}