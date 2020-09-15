using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

public class FileRepository : IRepository
{
    public Task<Player> Get(Guid id)
    {
        string[] lines = File.ReadAllLines(@"game-dev.txt");
        for (int i = 0; i < lines.Length; i++)
        {
            Player player = JsonConvert.DeserializeObject<Player>(lines[i]);

            if (player.Id == id)
            {
                Console.WriteLine("löytyl");
                return Task.FromResult(player);

            }
        }
        Console.WriteLine("ei");
        return null;
    }
    public Task<Player[]> GetAll()
    {
        string[] lines = File.ReadAllLines(@"game-dev.txt");
        Player[] players = new Player[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            Player player = JsonConvert.DeserializeObject<Player>(lines[i]);


            players[i] = player;
        }
        return Task.FromResult<Player[]>(players);
    }
    public Task<Player> Create(Player player)
    {
        using (StreamWriter sw = File.AppendText("game-dev.txt"))
        {
            string json = JsonConvert.SerializeObject(player);
            sw.WriteLine(json);
        }

        return Task.FromResult<Player>(player);
    }
    public Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        string[] lines = File.ReadAllLines(@"game-dev.txt");
        Player[] players = new Player[lines.Length];
        Player oldPlayer = new Player();

        for (int i = 0; i < lines.Length; i++)
        {

            oldPlayer = JsonConvert.DeserializeObject<Player>(lines[i]);
            if (oldPlayer.Id == id)
            {
                oldPlayer.Score = player.Score;
                break;

            }
        }
        return Task.FromResult(oldPlayer);
    }
    public Task<Player> Delete(Guid id)
    {
        string[] lines = File.ReadAllLines(@"game-dev.txt");
        List<string> newLines = new List<string>();
        Player[] players = new Player[lines.Length];
        Player oldPlayer = new Player();
        for (int i = 0; i < lines.Length; i++)
        {

            oldPlayer = JsonConvert.DeserializeObject<Player>(lines[i]);
            if (oldPlayer.Id == id)
            {
                continue;
            }
            else
            {
                newLines.Add(lines[i]);
            }


        }
        File.WriteAllLines("game-dev.txt", newLines.ToArray());
        return Task.FromResult(oldPlayer);
    }


}