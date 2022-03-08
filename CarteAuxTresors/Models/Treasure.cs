namespace CarteAuxTresors.Models
{
    public class Treasure : ModelWithPosition
    {
        /// <summary>Construtor.</summary>
        /// <param name="x">Position on X axis.</param>
        /// <param name="y">Position on Y axis.</param>
        /// <param name="nb">Number of treasure.</param>
        public Treasure(int x, int y, int nb)
        {
            Position = new(x, y);
            Count = nb;
        }

        /// <summary>Number of treasure.</summary>
        public int Count { get; set; }

        /// <summary>Prints the model for the output format.</summary>
        public override string ToString()
        {
            return $"T - {Position.X} - {Position.Y} - {Count}";
        }
    }
}
