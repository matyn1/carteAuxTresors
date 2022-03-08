namespace CarteAuxTresors.Models
{
    public class Map
    {
        /// <summary>Constructor.</summary>
        /// <param name="width">Width of the map.</param>
        /// <param name="height">Height of the map.</param>
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>Height (Y axis).</summary>
        public int Height { get; set; }

        /// <summary>Width (X axis).</summary>
        public int Width { get; set; }

        /// <summary>Prints the model for the output format.</summary>
        public override string ToString()
        {
            return $"C - {Width} - {Height}";
        }
    }
}
