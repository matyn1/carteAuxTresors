namespace CarteAuxTresors.Models
{
    public class Mountain: ModelWithPosition
    {
        /// <summary>Constructor.</summary>
        /// <param name="x">Position on X axis.</param>
        /// <param name="y">Position on Y axis.</param>
        public Mountain(int x, int y)
        {
            Position = new (x, y);
        }

        /// <summary>Prints the model for the output format.</summary>
        public override string ToString()
        {
            return $"M - {Position.X} - {Position.Y}";
        }
    }
}
