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
using Revise.EFT;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="EffectFile"/> class.
    /// </summary>
    [TestFixture]
    public class EffectFileTests {
        private const string TestFile = "Tests/Files/_RUNASTON_01.EFT";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            EffectFile effectFile = new EffectFile();
            effectFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            EffectFile effectFile = new EffectFile();
            effectFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            effectFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            EffectFile savedEffectFile = new EffectFile();
            savedEffectFile.Load(savedStream);

            savedStream.Close();

            Assert.That(effectFile.Name.Equals(savedEffectFile.Name), "Name values do not match");
            Assert.That(effectFile.SoundEnabled.Equals(savedEffectFile.SoundEnabled), "Sound enable values do not match");
            Assert.That(effectFile.SoundFilePath.Equals(savedEffectFile.SoundFilePath), "Sound file path values do not match");
            Assert.That(effectFile.LoopCount.Equals(savedEffectFile.LoopCount), "Loop count values do not match");

            Assert.That(effectFile.Particles.Count.Equals(savedEffectFile.Particles.Count), "Particle count values do not match");

            for (int i = 0; i < effectFile.Particles.Count; i++) {
                Assert.That(effectFile.Particles[i].Name.Equals(savedEffectFile.Particles[i].Name), "Particle name values do not match");
                Assert.That(effectFile.Particles[i].UniqueIdentifier.Equals(savedEffectFile.Particles[i].UniqueIdentifier), "Particle unique identifier values do not match");
                Assert.That(effectFile.Particles[i].ParticleIndex.Equals(savedEffectFile.Particles[i].ParticleIndex), "Particle particle index values do not match");
                Assert.That(effectFile.Particles[i].FilePath.Equals(savedEffectFile.Particles[i].FilePath), "Particle file path values do not match");
                Assert.That(effectFile.Particles[i].AnimationEnabled.Equals(savedEffectFile.Particles[i].AnimationEnabled), "Particle animation enabled values do not match");
                Assert.That(effectFile.Particles[i].AnimationName.Equals(savedEffectFile.Particles[i].AnimationName), "Particle animation name values do not match");
                Assert.That(effectFile.Particles[i].AnimationLoopCount.Equals(savedEffectFile.Particles[i].AnimationLoopCount), "Particle animation loop count values do not match");
                Assert.That(effectFile.Particles[i].AnimationIndex.Equals(savedEffectFile.Particles[i].AnimationIndex), "Particle animation index values do not match");
                Assert.That(effectFile.Particles[i].Position.Equals(savedEffectFile.Particles[i].Position), "Particle position values do not match");
                Assert.That(effectFile.Particles[i].Rotation.Equals(savedEffectFile.Particles[i].Rotation), "Particle rotation values do not match");
                Assert.That(effectFile.Particles[i].Delay.Equals(savedEffectFile.Particles[i].Delay), "Particle delay values do not match");
                Assert.That(effectFile.Particles[i].LinkedToRoot.Equals(savedEffectFile.Particles[i].LinkedToRoot), "Particle link to root values do not match");
            }

            Assert.That(effectFile.Animations.Count.Equals(savedEffectFile.Animations.Count), "Animation count values do not match");

            for (int i = 0; i < effectFile.Animations.Count; i++) {
                Assert.That(effectFile.Animations[i].EffectName.Equals(savedEffectFile.Animations[i].EffectName), "Animation effect name values do not match");
                Assert.That(effectFile.Animations[i].MeshName.Equals(savedEffectFile.Animations[i].MeshName), "Animation mesh name values do not match");
                Assert.That(effectFile.Animations[i].MeshIndex.Equals(savedEffectFile.Animations[i].MeshIndex), "Animation mesh index values do not match");
                Assert.That(effectFile.Animations[i].MeshFilePath.Equals(savedEffectFile.Animations[i].MeshFilePath), "Animation mesh file path values do not match");
                Assert.That(effectFile.Animations[i].AnimationFilePath.Equals(savedEffectFile.Animations[i].AnimationFilePath), "Animation animation file path values do not match");
                Assert.That(effectFile.Animations[i].TextureFilePath.Equals(savedEffectFile.Animations[i].TextureFilePath), "Animation texture file path values do not match");
                Assert.That(effectFile.Animations[i].AlphaEnabled.Equals(savedEffectFile.Animations[i].AlphaEnabled), "Animation alpha enabled values do not match");
                Assert.That(effectFile.Animations[i].TwoSidedEnabled.Equals(savedEffectFile.Animations[i].TwoSidedEnabled), "Animation two sided enabled values do not match");
                Assert.That(effectFile.Animations[i].AlphaTestEnabled.Equals(savedEffectFile.Animations[i].AlphaTestEnabled), "Animation alpha test enabled values do not match");
                Assert.That(effectFile.Animations[i].DepthTestEnabled.Equals(savedEffectFile.Animations[i].DepthTestEnabled), "Animation depth test enabled values do not match");
                Assert.That(effectFile.Animations[i].DepthWriteEnabled.Equals(savedEffectFile.Animations[i].DepthWriteEnabled), "Animation depth write enabled values do not match");
                Assert.That(effectFile.Animations[i].SourceBlend.Equals(savedEffectFile.Animations[i].SourceBlend), "Animation source blend values do not match");
                Assert.That(effectFile.Animations[i].DestinationBlend.Equals(savedEffectFile.Animations[i].DestinationBlend), "Animation destination blend values do not match");
                Assert.That(effectFile.Animations[i].BlendOperation.Equals(savedEffectFile.Animations[i].BlendOperation), "Animation blend operation values do not match");
                Assert.That(effectFile.Animations[i].AnimationEnabled.Equals(savedEffectFile.Animations[i].AnimationEnabled), "Animation animation enabled values do not match");
                Assert.That(effectFile.Animations[i].AnimationName.Equals(savedEffectFile.Animations[i].AnimationName), "Animation animation name values do not match");
                Assert.That(effectFile.Animations[i].AnimationLoopCount.Equals(savedEffectFile.Animations[i].AnimationLoopCount), "Animation animation loop count values do not match");
                Assert.That(effectFile.Animations[i].AnimationIndex.Equals(savedEffectFile.Animations[i].AnimationIndex), "Animation animation index values do not match");
                Assert.That(effectFile.Animations[i].Position.Equals(savedEffectFile.Animations[i].Position), "Animation position values do not match");
                Assert.That(effectFile.Animations[i].Rotation.Equals(savedEffectFile.Animations[i].Rotation), "Animation rotation values do not match");
                Assert.That(effectFile.Animations[i].Delay.Equals(savedEffectFile.Animations[i].Delay), "Animation delay values do not match");
                Assert.That(effectFile.Animations[i].LoopCount.Equals(savedEffectFile.Animations[i].LoopCount), "Animation loop count values do not match");
                Assert.That(effectFile.Animations[i].LinkedToRoot.Equals(savedEffectFile.Animations[i].LinkedToRoot), "Animation link to root values do not match");
            }
        }   
    }
}