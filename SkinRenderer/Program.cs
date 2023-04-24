using SkinRenderer.Data;
using SkinRenderer.Renderer;

namespace SkinRenderer;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length >= 2)
        {
            var player = new Player("LavaPowerMC");
            if (player.Username == "") return;
            
            player.Skin.Download();
            switch (args[1])
            {
                case "full":
                {
                    FullRenderer.Render(player, args.Length >= 3 && int.TryParse(args[2], out var scale) ? scale : 5, true, args is [_, _, _, "slim", ..]);
                    Console.WriteLine("Rendu fait.");
                    break;
                }
                case "head":
                {
                    HeadRenderer.Render(player, args.Length >= 3 && int.TryParse(args[2], out var scale) ? scale : 5);
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