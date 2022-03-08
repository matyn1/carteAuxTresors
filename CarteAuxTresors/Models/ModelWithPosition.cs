namespace CarteAuxTresors.Models
{
    /// <summary>Base class with position.</summary>
    public class ModelWithPosition
    {
        /// <summary>Tuple with position on X axis and Y axis<./summary>
        public (int X, int Y) Position { get; set; }
    }
}
