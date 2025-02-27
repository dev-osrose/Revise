﻿#region License

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
using Revise.HLP;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="HelpFile"/> class.
    /// </summary>
    [TestFixture]
    public class HelpFileTests {
        private const string TestFile = "Tests/Files/HELP.HLP";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            const int PAGE_COUNT = 69;

            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            HelpFile hlp = new HelpFile();
            hlp.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
            Assert.That(PAGE_COUNT.Equals(hlp.Pages.Count), "Incorrect page count");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            HelpFile helpFile = new HelpFile();
            helpFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            helpFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            HelpFile savedHelpFile = new HelpFile();
            savedHelpFile.Load(savedStream);

            savedStream.Close();

            Assert.That(helpFile.Pages.Count.Equals(savedHelpFile.Pages.Count), "Page counts do not match");

            TestNodes(helpFile.RootNode, savedHelpFile.RootNode);

            for (int i = 0; i < helpFile.Pages.Count; i++) {
                Assert.That(helpFile.Pages[i].Title.Equals(savedHelpFile.Pages[i].Title), "Page title values do not match");
                Assert.That(helpFile.Pages[i].Content.Equals(savedHelpFile.Pages[i].Content), "Page content values do not match");
            }
        }

        /// <summary>
        /// Tests the two nodes by comparing the names and the children.
        /// </summary>
        /// <param name="nodeA">The first node.</param>
        /// <param name="nodeB">The second node.</param>
        private void TestNodes(HelpNode nodeA, HelpNode nodeB) {
            Assert.That(nodeA.Name.Equals(nodeB.Name), "Node name values do not match");
            Assert.That(nodeA.Children.Count.Equals(nodeB.Children.Count), "Child counts do not match");

            for (int i = 0; i < nodeA.Children.Count; i++) {
                TestNodes(nodeA.Children[i], nodeA.Children[i]);
            }
        }
    }
}