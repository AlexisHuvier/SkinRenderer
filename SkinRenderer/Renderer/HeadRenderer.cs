namespace SkinRenderer.Renderer;

public static class HeadRenderer
{
    private static readonly List<List<int>> Parts = new()
    {
        new List<int> { 8, 8, 8, 8, 0, 0 }, // HEAD
        new List<int> { 40, 8, 8, 8, 0, 0 } // HEAD OVERLAY
    };
    
    public static void Render(string fileName, int scale = 1, bool overlay = true)
    {
        using var result = new Image<Rgba32>(8, 8);

        result.Mutate(context =>
        {
            using var skin = Image.Load($"{fileName}.png");

            var images = Parts.Select(part => skin.Clone(processingContext => processingContext.Crop(new Rectangle(part[0], part[1], part[2], part[3])))).ToList();

            context.DrawImage(images[0], new Point(Parts[0][4], Parts[0][5]), 1f);
            if(overlay)
                context.DrawImage(images[1], new Point(Parts[1][4], Parts[1][5]), 1f);

            context.Resize(8 * scale, 8 * scale, KnownResamplers.NearestNeighbor);
        });

        result.Save($"{fileName}-head.png");
    }
}