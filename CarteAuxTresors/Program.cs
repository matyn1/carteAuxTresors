using CarteAuxTresors.Helpers;

namespace CarteAuxTresors
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1 || !GameHelper.InitializeGame(args[0]))
                return;

            GameHelper.RunGame();

            GameHelper.ExportResults();

            // Sympa l'exercice :)
        }
    }
}
