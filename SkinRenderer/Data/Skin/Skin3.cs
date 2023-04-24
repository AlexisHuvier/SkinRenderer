namespace SkinRenderer.Data.Skin;

public class Skin3: ISkin
{
    public class Metadata
    {
        public string model { get; set; }
    }

    public class SKIN
    {
        public string url { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Textures
    {
        public SKIN SKIN { get; set; }
    }
    
    public long timestamp { get; set; }
    public string profileId { get; set; }
    public string profileName { get; set; }
    public Textures textures { get; set; }


    public string GetSkinUrl() => textures.SKIN.url;

    public void Download()
    {
        using var client = new HttpClient();
        using var s = client.GetStreamAsync(GetSkinUrl());
        using var fs = new FileStream(profileName+".png", FileMode.OpenOrCreate);
        s.Result.CopyTo(fs);
    }

    public override string ToString() =>
        $"Skin (timestamp = {timestamp}, profileId = {profileId}, profileName = {profileId}, textures = {textures}, Skin Url = {GetSkinUrl()})";
}