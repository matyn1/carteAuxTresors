using System;
using System.Collections.Generic;

namespace CarteAuxTresors.Models
{
    public class Adventurer: ModelWithPosition
    {
        /// <summary>Default constructor.</summary>
        public Adventurer()
        {
        }

        /// <summary>Constructor.</summary>
        /// <param name="x">Position on X axis.</param>
        /// <param name="y">Position on Y axis.</param>
        /// <param name="name">Name of the adventurer.</param>
        /// <param name="orientation">Orientation of the adventurer.</param>
        /// <param name="sequence">Lists of char representing the sequence of movements.</param>
        public Adventurer(int x, int y, string name, OrientationEnum orientation, List<char> sequence)
        {
            Position = new(x, y);
            Name = name;
            Orientation = orientation;
            MovementsSequence = sequence;
        }

        /// <summary>Name of the adventurer.</summary>
        public string Name { get; set; }

        /// <summary>Number of treasure retrieved.</summary>
        public int NbTreasures { get; set; }

        /// <summary>Orientation of the adventurer.</summary>
        public OrientationEnum Orientation { get; set; }

        /// <summary>Lists of char representing the sequence of movements.</summary>
        public IList<char> MovementsSequence { get; set; }

        /// <summary>Moves the adventurer position.</summary>
        /// <param name="position">The new position.</param>
        public void MoveTo((int _, int __) position)
        {
            Position = position;
        }

        /// <summary>Rotates the orientation of the adventurer to the left.</summary>
        /// <param name="position">The new position.</param>
        public void MoveLeft()
        {
            Orientation = (OrientationEnum)(((int)Orientation + 1) % Enum.GetNames(typeof(OrientationEnum)).Length);
        }


        /// <summary>Rotates the orientation of the adventurer to the right.</summary>
        public void MoveRight()
        {
            Orientation--;
            if (Orientation < 0)
                Orientation = (OrientationEnum)(Enum.GetNames(typeof(OrientationEnum)).Length -1);
        }

        /// <summary>Prints the model for the output format.</summary>
        public override string ToString()
        {
            return $"A - {Name} - {Position.X} - {Position.Y} - {Orientation} - {NbTreasures}";
        }
    }

    /// <summary>Possible values of Orientation.</summary>
    public enum OrientationEnum
    {
        N,
        W,
        S,
        E
    }
}
