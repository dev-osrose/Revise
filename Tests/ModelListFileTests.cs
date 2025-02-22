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
using Revise.ZSC;

namespace Revise.Files.Tests {
    /// <summary>
    /// Provides testing for the <see cref="ModelListFile"/> class.
    /// </summary>
    [TestFixture]
    public class ModelListFileTests {
        private const string TestFile = "Tests/Files/LIST_DECO_JPT.ZSC";

        /// <summary>
        /// Tests the load method.
        /// </summary>
        [Test]
        public void TestLoadMethod() {
            Stream stream = File.OpenRead(TestFile);

            stream.Seek(0, SeekOrigin.End);
            long fileSize = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);

            ModelListFile modelListFile = new ModelListFile();
            modelListFile.Load(stream);

            long streamPosition = stream.Position;
            stream.Close();

            Assert.That(fileSize.Equals(streamPosition), "Not all of the file was read");
        }

        /// <summary>
        /// Tests the save method.
        /// </summary>
        [Test]
        public void TestSaveMethod() {
            ModelListFile modelListFile = new ModelListFile();
            modelListFile.Load(TestFile);

            MemoryStream savedStream = new MemoryStream();
            modelListFile.Save(savedStream);

            savedStream.Seek(0, SeekOrigin.Begin);

            ModelListFile savedModelListFile = new ModelListFile();
            savedModelListFile.Load(savedStream);

            savedStream.Close();

            Assert.That(modelListFile.ModelFiles.Count.Equals(savedModelListFile.ModelFiles.Count), "Model file counts do not match");

            for (int i = 0; i < modelListFile.ModelFiles.Count; i++) {
                Assert.That(modelListFile.ModelFiles[i].Equals(savedModelListFile.ModelFiles[i]), "Model file paths do not match");
            }

            Assert.That(modelListFile.TextureFiles.Count.Equals(savedModelListFile.TextureFiles.Count), "Texture file counts do not match");

            for (int i = 0; i < modelListFile.TextureFiles.Count; i++) {
                Assert.That(modelListFile.TextureFiles[i].FilePath.Equals(savedModelListFile.TextureFiles[i].FilePath), "Texture file paths do not match");
                Assert.That(modelListFile.TextureFiles[i].UseSkinShader.Equals(savedModelListFile.TextureFiles[i].UseSkinShader), "Texture use skin shader values do not match");
                Assert.That(modelListFile.TextureFiles[i].AlphaEnabled.Equals(savedModelListFile.TextureFiles[i].AlphaEnabled), "Texture alpha enabled values do not match");
                Assert.That(modelListFile.TextureFiles[i].TwoSided.Equals(savedModelListFile.TextureFiles[i].TwoSided), "Texture two sided values do not match");
                Assert.That(modelListFile.TextureFiles[i].AlphaTestEnabled.Equals(savedModelListFile.TextureFiles[i].AlphaTestEnabled), "Texture alpha test enabled values do not match");
                Assert.That(modelListFile.TextureFiles[i].AlphaReference.Equals(savedModelListFile.TextureFiles[i].AlphaReference), "Texture alpha reference values do not match");
                Assert.That(modelListFile.TextureFiles[i].DepthTestEnabled.Equals(savedModelListFile.TextureFiles[i].DepthTestEnabled), "Texture depth test enabled values do not match");
                Assert.That(modelListFile.TextureFiles[i].DepthWriteEnabled.Equals(savedModelListFile.TextureFiles[i].DepthWriteEnabled), "Texture depth write enabled values do not match");
                Assert.That(modelListFile.TextureFiles[i].BlendType.Equals(savedModelListFile.TextureFiles[i].BlendType), "Texture blend type values do not match");
                Assert.That(modelListFile.TextureFiles[i].UseSpecularShader.Equals(savedModelListFile.TextureFiles[i].UseSpecularShader), "Texture use specular shader values do not match");
                Assert.That(modelListFile.TextureFiles[i].Alpha.Equals(savedModelListFile.TextureFiles[i].Alpha), "Texture alpha values do not match");
                Assert.That(modelListFile.TextureFiles[i].GlowType.Equals(savedModelListFile.TextureFiles[i].GlowType), "Texture glow type values do not match");
                Assert.That(modelListFile.TextureFiles[i].GlowColour.Equals(savedModelListFile.TextureFiles[i].GlowColour), "Texture glow colour do not match");
            }

            Assert.That(modelListFile.EffectFiles.Count.Equals(savedModelListFile.EffectFiles.Count), "Effect file counts do not match");

            for (int j = 0; j < modelListFile.EffectFiles.Count; j++) {
                Assert.That(modelListFile.EffectFiles[j].Equals(savedModelListFile.EffectFiles[j]), "Effect file paths do not match");
            }

            Assert.That(modelListFile.Objects.Count.Equals(savedModelListFile.Objects.Count), "Object counts do not match");

            for (int i = 0; i < modelListFile.Objects.Count; i++) {
                Assert.That(modelListFile.Objects[i].Parts.Count.Equals(savedModelListFile.Objects[i].Parts.Count), "Object part counts do not match");

                for (int j = 0; j < modelListFile.Objects[i].Parts.Count; j++) {
                    Assert.That(modelListFile.Objects[i].Parts[j].Model.Equals(savedModelListFile.Objects[i].Parts[j].Model), "Part model values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].Texture.Equals(savedModelListFile.Objects[i].Parts[j].Texture), "Part texture values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].Position.Equals(savedModelListFile.Objects[i].Parts[j].Position), "Part position values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].Rotation.Equals(savedModelListFile.Objects[i].Parts[j].Rotation), "Part rotation values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].Scale.Equals(savedModelListFile.Objects[i].Parts[j].Scale), "Part scale values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].AxisRotation.Equals(savedModelListFile.Objects[i].Parts[j].AxisRotation), "Part axis rotation values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].Parent.Equals(savedModelListFile.Objects[i].Parts[j].Parent), "Part parent values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].Collision.Equals(savedModelListFile.Objects[i].Parts[j].Collision), "Part collision values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].AnimationFilePath.Equals(savedModelListFile.Objects[i].Parts[j].AnimationFilePath), "Part animation file path values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].VisibleRangeSet.Equals(savedModelListFile.Objects[i].Parts[j].VisibleRangeSet), "Part visible range set values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].UseLightmap.Equals(savedModelListFile.Objects[i].Parts[j].UseLightmap), "Part use lightmap values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].BoneIndex.Equals(savedModelListFile.Objects[i].Parts[j].BoneIndex), "Part bone index values do not match");
                    Assert.That(modelListFile.Objects[i].Parts[j].DummyIndex.Equals(savedModelListFile.Objects[i].Parts[j].DummyIndex), "Part dummy index values do not match");

                    for (int k = 0; k < modelListFile.Objects[i].Parts[j].MonsterAnimations.Length; k++) {
                        Assert.That(modelListFile.Objects[i].Parts[j].MonsterAnimations[k].Equals(savedModelListFile.Objects[i].Parts[j].MonsterAnimations[k]), "Part monster animation file path do not match");
                    }

                    for (int k = 0; k < modelListFile.Objects[i].Parts[j].AvatarAnimations.Length; k++) {
                        Assert.That(modelListFile.Objects[i].Parts[j].AvatarAnimations[k].Equals(savedModelListFile.Objects[i].Parts[j].AvatarAnimations[k]), "Part avatar animation file path do not match");
                    }
                }

                Assert.That(modelListFile.Objects[i].Effects.Count.Equals(savedModelListFile.Objects[i].Effects.Count), "Object effect counts do not match");

                for (int j = 0; j < modelListFile.Objects[i].Effects.Count; j++) {
                    Assert.That(modelListFile.Objects[i].Effects[j].EffectType.Equals(savedModelListFile.Objects[i].Effects[j].EffectType), "Effect type values do not match");
                    Assert.That(modelListFile.Objects[i].Effects[j].Effect.Equals(savedModelListFile.Objects[i].Effects[j].Effect), "Effect values do not match");
                    Assert.That(modelListFile.Objects[i].Effects[j].Position.Equals(savedModelListFile.Objects[i].Effects[j].Position), "Effect position values do not match");
                    Assert.That(modelListFile.Objects[i].Effects[j].Rotation.Equals(savedModelListFile.Objects[i].Effects[j].Rotation), "Effect rotation values do not match");
                    Assert.That(modelListFile.Objects[i].Effects[j].Scale.Equals(savedModelListFile.Objects[i].Effects[j].Scale), "Effect scale values do not match");
                    Assert.That(modelListFile.Objects[i].Effects[j].Parent.Equals(savedModelListFile.Objects[i].Effects[j].Parent), "Effect parent values do not match");
                }
            }
        }
    }
}