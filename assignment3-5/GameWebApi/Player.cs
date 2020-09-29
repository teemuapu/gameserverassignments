using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameWebApi;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public int Level { get; set; }
    public bool IsBanned { get; set; }
    public DateTime CreationTime { get; set; }
    public List<Item> PlayerItems { get; set; }
}