using SkinRenderer.Data;
using SkinRenderer.Renderer;
using SkinRenderer.Utils;

namespace SkinRenderer;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length >= 2)
        {
            string file;
            if (args[0].Contains('.'))
            {
                var fileParts = args[0].Split(".");
                file = string.Join(".", fileParts.SubArray(0, fileParts.Length - 1));
            }
            else
            {
                var player = new Player(args[0]);
                if (player.Username == "") return;

                player.Skin.Download();
                file = player.Username;
            }

            switch (args[1])
            {
                case "full":
                {
                    FullRenderer.Render(file, args.Length >= 3 && int.TryParse(args[2], out var scale) ? scale : 5, true, args is [_, _, _, "slim", ..]);
                    Console.WriteLine("Rendu fait.");
                    break;
                }
                case "head":
                {
                    HeadRenderer.Render(file, args.Length >= 3 && int.TryParse(args[2], out var scale) ? scale : 5);
                    Console.WriteLine("Rendu fait.");
                    break;
                }
                default:
                    Console.WriteLine("Usage : SkinRenderer.exe <player> <full|head> [slim] [<scale>]");
                    break;
            }
        }
        else
            Console.WriteLine("Usage : SkinRenderer.exe <player> <full|head> [slim] [<scale>]" );
        
    }
}