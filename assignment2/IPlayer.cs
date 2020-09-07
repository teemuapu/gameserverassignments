using System;
public interface IPlayer
{
    public Guid Id { get; set; }
    int Score { get; set; }
}