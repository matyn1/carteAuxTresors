using CarteAuxTresors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarteAuxTresors.Helpers
{
    /// <summary>A class that handle the game.</summary>
    public static class GameHelper
    {
        /// <summary>The map.</summary>
        public static Map Map { get; set; }

        /// <summary>A list of Mountain.</summary>
        public static IList<Mountain> Mountains { get; set; } = new List<Mountain>();

        /// <summary>A list of Treasure.</summary>
        public static IList<Treasure> Treasures { get; set; } = new List<Treasure>();

        /// <summary>A list of Adventurer.</summary>
        public static IList<Adventurer> Adventurers { get; set; } = new List<Adventurer>();

        /// <summary>Clears all models.</summary>
        public static void ResetGame()
        {
            Map = null;
            Mountains.Clear();
            Treasures.Clear();
            Adventurers.Clear();
        }

        /// <summary>Initializes all models necessary for "La carte aux trésors".</summary>
        /// <param name="filePath">Path of the entry file.</param>
        /// <returns>False if there is no Map in the file and no adventurer, otherwise true.</returns>
        public static bool InitializeGame(string filePath)
        {
            var lines = FileHelper.ParseFileEntry(filePath);

            if (lines == null)
                return false;

            foreach (var line in lines)
            {
                var lineSplitted = line.Replace(" ","").Split("-");
                switch (lineSplitted[0])
                {
                    case "C":
                        if (int.TryParse(lineSplitted[1], out int width) && int.TryParse(lineSplitted[2], out int height))
                            Map = new Map(width, height);
                        break;
                    case "M":
                        if (int.TryParse(lineSplitted[1], out int mountainX) && int.TryParse(lineSplitted[2], out int mountainY) &&
                            IsPositionAuthorized((mountainX, mountainY)))
                            Mountains.Add(new Mountain(mountainX, mountainY));
                        break;
                    case "T":
                        if (int.TryParse(lineSplitted[1], out int treasureX) && int.TryParse(lineSplitted[2], out int treasureY) && int.TryParse(lineSplitted[3], out int nbTreasure))
                        {
                            if (Treasures.Any(treasure => treasure.Position == (treasureX, treasureY)))
                                Treasures.First(treasure => treasure.Position == (treasureX, treasureY)).Count += nbTreasure;
                            else
                                Treasures.Add(new Treasure(treasureX, treasureY, nbTreasure));
                        }
                        break;
                    case "A":
                        if (int.TryParse(lineSplitted[2], out int adventurerX) && int.TryParse(lineSplitted[3], out int adventurerY) && 
                            Enum.TryParse(lineSplitted[4], out OrientationEnum orientation) &&
                            IsPositionAuthorized((adventurerX, adventurerY)))
                        {
                            var name = lineSplitted[1];
                            var sequence = lineSplitted[5].ToCharArray().ToList();
                            var adventurer = new Adventurer(adventurerX, adventurerY, name, orientation, sequence);
                            Adventurers.Add(adventurer);
                        }
                        break;
                    default:
                        break;
                }
            }

            return Map != null && Adventurers.Any();
        }

        /// <summary>Runs the sequences off all adventurers.</summary>
        public static void RunGame()
        {
            while (Adventurers.Any(adventurer => adventurer.MovementsSequence.Count > 0))
            {
                foreach (var adventurer in Adventurers.Where(adv => adv.MovementsSequence.Count > 0))
                {
                    (int X, int Y) nextPosition = (-1, -1);

                    var nextMovement = adventurer.MovementsSequence[0];
                    // Remove this movement from his sequence.
                    adventurer.MovementsSequence.Remove(adventurer.MovementsSequence[0]);

                    switch (nextMovement)
                    {
                        case 'A':
                            nextPosition = CalculateNextPosition(adventurer);
                            break;
                        case 'G':
                            adventurer.MoveLeft();
                            continue;
                        case 'D':
                            adventurer.MoveRight();
                            continue;
                    }

                    if (!IsPositionAuthorized(nextPosition))
                        continue;

                    adventurer.MoveTo(nextPosition);

                    RetrieveTreasure(adventurer);
                }
            }
        }

        /// <summary>Checks if the position is authorized.</summary>
        /// <param name="position">The position to test.</param>
        /// <returns>True if the position is in the Map and if there is no mountains or no player; otherwise false.</returns>
        public static bool IsPositionAuthorized((int X, int Y) position)
        {
            return !IsOutOfMap(position) && IsPositionAvailable(position);
        }

        /// <summary>Checks if the position is out of the map.</summary>
        /// <param name="position">The position to test.</param>
        /// <returns>True if the position is out of the map; otherwise false.</returns>
        public static bool IsOutOfMap((int X, int Y) position)
        {
            return position.X > Map?.Width - 1 || position.X < 0 || position.Y > Map?.Height - 1 || position.Y < 0;
        }

        /// <summary>Checks if the position is available. No adventurer or no mountain.</summary>
        /// <param name="position">The position to test.</param>
        /// <returns>True if the position is available; otherwise false</returns>
        public static bool IsPositionAvailable((int X, int Y) position)
        {
            return !Mountains.Any(mountain => mountain.Position == position) && !Adventurers.Any(adventurer => adventurer.Position == position);
        }

        /// <summary>Calculates the next position of an adventurer.</summary>
        /// <param name="adventurer">The adventurer.</param>
        /// <returns>The next position.</returns>
        public static (int x, int y) CalculateNextPosition(Adventurer adventurer)
        {
            var nextPosition = adventurer.Position;
            switch (adventurer.Orientation)
            {
                case OrientationEnum.N:
                    nextPosition.Y--;
                    break;
                case OrientationEnum.S:
                    nextPosition.Y++;
                    break;
                case OrientationEnum.E:
                    nextPosition.X++;
                    break;
                case OrientationEnum.W:
                    nextPosition.X--;
                    break;
            }
            return nextPosition;
        }

        /// <summary>Retrieves a treasure if any.</summary>
        /// <param name="adventurer">The adventurer.</param>
        public static void RetrieveTreasure(Adventurer adventurer)
        {
            if (Treasures.Any(treasure => treasure.Position == adventurer.Position && treasure.Count > 0))
            {
                Treasures.First(treasure => treasure.Position == adventurer.Position).Count--;
                adventurer.NbTreasures++;
            }
        }

        /// <summary>Exports the results in a file.</summary>
        public static void ExportResults()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Map.ToString());
            foreach (var mountain in Mountains)
                sb.AppendLine(mountain.ToString());
            foreach (var treasure in Treasures.Where(t => t.Count > 0))
                sb.AppendLine(treasure.ToString());
            foreach (var adventurer in Adventurers)
                sb.AppendLine(adventurer.ToString());
            FileHelper.WriteFileOutput(sb.ToString());
        }
    }
}
