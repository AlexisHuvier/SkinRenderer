using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SkinRenderer.Data;

public class Player
{
    public string Username = "";
    public string Uuid = "";
    public Profile Profile = new();
    public Skin Skin = new();
    
    public Player(string username)
    {
        GetUuid(username);
        GetProfile();
    }

    private void GetUuid(string username)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://api.mojang.com/");
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = httpClient.GetAsync($"users/profiles/minecraft/{username}").Result;
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var dic = JsonSerializer.Deserialize<Dictionary<string, string>>(
                response.Content.ReadAsStringAsync().Result)!;
            
            if (dic.TryGetValue("name", out var name))
                Username = name;
            else {
                Console.WriteLine("Username not found");    
                throw new Exception();
            }
            
            if (dic.TryGetValue("id", out var id))
                Uuid = id;
            else
            {
                Console.WriteLine("Id not found");
                throw new Exception();
            }
        }
        else {
            Console.WriteLine("Player not found");
            throw new Exception();
        }
    }

    private void GetProfile()
    {
        var httpClient = new HttpClient();

        httpClient.BaseAddress = new Uri("https://sessionserver.mojang.com");
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = httpClient.GetAsync($"session/minecraft/profile/{Uuid}").Result;
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Profile = JsonSerializer.Deserialize<Profile>(response.Content.ReadAsStringAsync().Result)!;
            Skin = JsonSerializer.Deserialize<Skin>(
                Encoding.UTF8.GetString(Convert.FromBase64String(Profile.GetSkinBase64())))!;
        }
        else
        {
            Console.WriteLine("Profile not found");
            throw new Exception();
        }
    }

    public override string ToString() => $"Player (Username = {Username}, Uuid = {Uuid}, Profile = {Profile}, Skin = {Skin})";
}