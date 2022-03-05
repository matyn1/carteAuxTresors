using System;
using System.Collections.Generic;

namespace CarteAuxTresors.Models
{
    public class Adventurer: ModelWithPosition
    {
        public Adventurer()
        {
        }

        public Adventurer(int x, int y, string name, OrientationEnum orientation, List<char> sequence)
        {
            Position = new(x, y);
            Name = name;
            Orientation = orientation;
            MovementsSequence = sequence;
        }

        public string Name { get; set; }
        public int NbTreasures { get; set; }
        public OrientationEnum Orientation { get; set; }
        public IList<char> MovementsSequence { get; set; }

        public override string ToString()
        {
            return $"A - {Name} - {Position.X} - {Position.Y} - {Orientation} - {NbTreasures}";
        }

        public void MoveTo((int _, int __) position)
        {
            Position = position;
        }

        public void MoveLeft()
        {
            Orientation = (OrientationEnum)(((int)Orientation + 1) % Enum.GetNames(typeof(OrientationEnum)).Length);
        }

        public void MoveRight()
        {
            Orientation--;
            if (Orientation < 0)
                Orientation = (OrientationEnum)(Enum.GetNames(typeof(OrientationEnum)).Length -1);
        }
    }

    public enum OrientationEnum
    {
        N,
        W,
        S,
        E
    }
}
