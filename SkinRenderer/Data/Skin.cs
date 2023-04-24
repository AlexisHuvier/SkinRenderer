namespace SkinRenderer.Data;

public class Skin
{
    public long timestamp { get; set; }
    public string profileId { get; set; }
    public string profileName { get; set; }
    public Dictionary<string, Dictionary<string, string>> textures { get; set; }

    public string GetSkinUrl() => textures["SKIN"]["url"];

    public void Download()
    {
        using var client = new HttpClient();
        using var s = client.GetStreamAsync(GetSkinUrl());
        using var fs = new FileStream(profileName+".png", FileMode.OpenOrCreate);
        s.Result.CopyTo(fs);
    }

    public override string ToString() =>
        $"Skin (timestamp = {timestamp}, profileId = {profileId}, profileName = {profileName}, textures = {textures}, Skin Url = {GetSkinUrl()})";
}