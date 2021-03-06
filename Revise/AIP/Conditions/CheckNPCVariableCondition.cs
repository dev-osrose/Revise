﻿#region License

/**
 * Copyright (C) 2012 Jack Wakefield
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
using Revise.AIP.Interfaces;

namespace Revise.AIP.Conditions
{
    /// <summary>
    /// Represents a condition to check the specified NPC variable.
    /// </summary>
    public class CheckNPCVariableCondition : IArtificialIntelligenceCondition {
        #region Properties

        /// <summary>
        /// Gets the condition type.
        /// </summary>
        public ArtificialIntelligenceCondition Type {
            get {
                return ArtificialIntelligenceCondition.CheckNPCVariable;
            }
        }

        /// <summary>
        /// Gets or sets the variable number.
        /// </summary>
        public short Variable {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public int Value {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the comparison operator to use when comparing <see cref="Variable"/> to <see cref="Value"/>.
        /// </summary>
        public ComparisonOperator Operator {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Reads the condition data from the underlying stream.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void Read(BinaryReader reader) {
            Variable = reader.ReadInt16();
            Value = reader.ReadInt32();
            Operator = (ComparisonOperator)reader.ReadByte();
        }

        /// <summary>
        /// Writes the condition data to the underlying stream.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Write(BinaryWriter writer) {
            writer.Write(Variable);
            writer.Write(Value);
            writer.Write((byte)Operator);
        }
    }
}