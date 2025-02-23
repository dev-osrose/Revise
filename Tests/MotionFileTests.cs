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
 * MERCHANTABILITY or FITNESS FOR AD:\Code\Revise\Revise.Files Tests\EFTTests.cs PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#endregion

using System.IO;
using NUnit.Framework;
using Revise.ZMO;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="MotionFile"/> class.
    /// </summary>
    [TestFixture]
    public class MotionFileTests {
        private const string TestFile = "Tests/Files/_WIND_01.ZMO";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            MotionFile motionFile = new MotionFile();
            motionFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            MotionFile motionFile = new MotionFile();
            motionFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            motionFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            MotionFile savedMotionFile = new MotionFile();
            savedMotionFile.Load(savedStream);

            savedStream.Close();

            Assert.That(motionFile.FramesPerSecond.Equals(savedMotionFile.FramesPerSecond), "Frames per second values do not match");
            Assert.That(motionFile.ChannelCount.Equals(savedMotionFile.ChannelCount), "Channel counts do not match");

            for (int i = 0; i < motionFile.ChannelCount; i++) {
                Assert.That(motionFile[i].Type.Equals(savedMotionFile[i].Type), "Channel types do not match");
                Assert.That(motionFile[i].Index.Equals(savedMotionFile[i].Index), "Channel index values do not match");
            }

            Assert.That(motionFile.FramesPerSecond.Equals(savedMotionFile.FramesPerSecond), "Frame counts do not match");
        }
    }
}