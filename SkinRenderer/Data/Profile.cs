namespace SkinRenderer.Data;

public class Profile
{
    public string id { get; set; }
    public string name { get; set; }
    public List<Dictionary<string, string>> properties { get; set; }

    public string GetSkinBase64() => properties[0]["value"];

    public override string ToString() =>
        $"Profile (Id = {id}, Name = {name}, Properties = {properties}, SkinBase64 = {GetSkinBase64()})";
}