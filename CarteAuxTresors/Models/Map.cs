namespace CarteAuxTresors.Models
{
    public class Map
    {
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Height { get; set; }
        public int Width { get; set; }

        public override string ToString()
        {
            return $"C - {Width} - {Height}";
        }
    }
}
