using SkinRenderer.Data;

namespace SkinRenderer.Renderer;

public class FullRenderer
{
    private static readonly List<List<int>> Parts = new()
    {
        new List<int> { 8, 8, 8, 8, 4, 0 }, // HEAD
        new List<int> { 20, 20, 8, 12, 4, 8 }, // CHEST
        new List<int> { 20, 52, 4, 12, 8, 20 }, // LEFT LEG
        new List<int> { 4, 20, 4, 12, 4, 20 }, // RIGHT LEG
        
        new List<int> { 36, 52, 4, 12, 12, 8 }, // LEFT ARM 4
        new List<int> { 44, 20, 4, 12, 0, 8 }, // RIGHT ARM 4
        
        new List<int> { 36, 52, 3, 12, 12, 8 }, // LEFT ARM 3
        new List<int> { 44, 20, 3, 12, 1, 8 }, // RIGHT ARM 3

        new List<int> { 40, 8, 8, 8, 4, 0 }, // HEAD OVERLAY
        new List<int> { 20, 36, 8, 12, 4, 8 }, // CHEST OVERLAY
        new List<int> { 4, 52, 4, 12, 8, 20 }, // LEFT LEG OVERLAY
        new List<int> { 4, 36, 4, 12, 4, 20 }, // RIGHT LEG OVERLAY
        
        new List<int> { 52, 52, 4, 12, 12, 8 }, // LEFT ARM 4 OVERLAY
        new List<int> { 44, 36, 4, 12, 0, 8 }, // RIGHT ARM 4 OVERLAY
        
        new List<int> { 52, 52, 3, 12, 12, 8 }, // LEFT ARM 3 OVERLAY
        new List<int> { 44, 36, 3, 12, 1, 8 } // RIGHT ARM 3 OVERYLAY
    };
    
    public static void Render(Player player, int scale = 1, bool overlay = true, bool slim = false)
    {
        using var result = new Image<Rgba32>(16, 32);

        result.Mutate(context =>
        {
            using var skin = Image.Load($"{player.Username}.png");

            var images = Parts.Select(part => skin.Clone(processingContext => processingContext.Crop(new Rectangle(part[0], part[1], part[2], part[3])))).ToList();

            for(var i = 0; i < 4; i++)
                context.DrawImage(images[i], new Point(Parts[i][4], Parts[i][5]), 1f);

            if (slim)
                for(var i = 6; i < 8; i++) 
                    context.DrawImage(images[i], new Point(Parts[i][4], Parts[i][5]), 1f);
            else
                for(var i = 4; i < 6; i++) 
                    context.DrawImage(images[i], new Point(Parts[i][4], Parts[i][5]), 1f);

            if (overlay)
            {
                for(var i = 8; i < 12; i++) 
                    context.DrawImage(images[i], new Point(Parts[i][4], Parts[i][5]), 1f);

                if (slim)
                    for(var i = 14; i < 16; i++) 
                        context.DrawImage(images[i], new Point(Parts[i][4], Parts[i][5]), 1f);
                else
                    for(var i = 12; i < 14; i++) 
                        context.DrawImage(images[i], new Point(Parts[i][4], Parts[i][5]), 1f);
            }

            context.Resize(16 * scale, 32 * scale, KnownResamplers.NearestNeighbor);
        });

        result.Save($"{player.Username}-full.png");
    }
}