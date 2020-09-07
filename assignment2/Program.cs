using System;
using System.Collections.Generic;
using System.Linq;

namespace assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Guid> Ids = new List<Guid>();
            List<IPlayer> players = new List<IPlayer>();
            List<IPlayer> players2 = new List<IPlayer>();
            try
            {
                //Create players vastaanottaa IPlayereitä, voi tehdä kumpiakin boolista riippuen
                CreatePlayers(players, Ids, true);
                CreatePlayers(players2, Ids, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Duplicates");
            }

            // Generate test player and 50 items with random stats

            Player testPlayer = new Player();
            testPlayer.Items = new List<Item>();
            Random rnd = new Random();




            for (int i = 0; i < 10; i++)
            {
                Item item = new Item();
                item.Id = Guid.NewGuid();
                item.Level = rnd.Next(1, 51);
                testPlayer.Items.Add(item);
            }

            Console.WriteLine("Highest: " + testPlayer.GetHighestLevelItem().Level);
            Console.WriteLine(GetFirstItem(testPlayer));

            //Delegate juttu
            Del handler = ProcessEachItem;
            handler(testPlayer, PrintItem);

            //Lambda juttu
            Action<Item> delegateInstance = (Item item) => Console.WriteLine("ID: " + item.Id.ToString() + " Level: " + item.Level.ToString());
            handler(testPlayer, delegateInstance);

            var game = new Game<IPlayer>(players);
            IPlayer[] topPlayers = game.GetTop10Players();

            for (int i = 0; i < topPlayers.Length; i++)
            {
                Console.WriteLine((i + 1) + ": " + topPlayers[i].Id);
            }

            var game2 = new Game<IPlayer>(players2);
            IPlayer[] topPlayers2 = game2.GetTop10Players();

            for (int i = 0; i < topPlayers2.Length; i++)
            {
                Console.WriteLine((i + 1) + ": " + topPlayers2[i].Id);
            }

        }

        static void CreatePlayers(List<IPlayer> players, List<Guid> Ids, bool playerType)
        {
            Random random = new Random();
            if (playerType)
            {
                for (int i = 0; i < 10; i++)
                {
                    Player player = new Player();
                    //player.Id = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00");
                    player.Id = Guid.NewGuid();
                    players.Add(player);
                    Ids.Add(player.Id);
                    player.Score = random.Next(1, 10000);
                    Console.WriteLine(player.Id);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    PlayerForAnotherGame player = new PlayerForAnotherGame();
                    //player.Id = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00");
                    player.Id = Guid.NewGuid();
                    players.Add(player);
                    Ids.Add(player.Id);
                    player.Score = random.Next(1, 10000);
                    Console.WriteLine(player.Id);
                }
            }

            //Check for duplicates
            if (Ids.Count != Ids.Distinct().Count())
            {
                throw new Exception();
            }

        }
        static void CreatePlayersForAnotherGame(List<PlayerForAnotherGame> players, List<Guid> Ids)
        {
            for (int i = 0; i < 10; i++)
            {
                PlayerForAnotherGame player = new PlayerForAnotherGame();
                //player.Id = Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00");
                player.Id = Guid.NewGuid();
                players.Add(player);
                Ids.Add(player.Id);
                Console.WriteLine(player.Id);
            }
            //Check for duplicates
            if (Ids.Count != Ids.Distinct().Count())
            {
                throw new Exception();
            }

        }


        public static Item[] GetItems(List<Item> items)
        {
            /* if (items == null)
            {
                return false;
            } */
            Console.WriteLine(items.Count);
            Item[] itemsArray = new Item[items.Count];

            for (int i = 0; i < itemsArray.Length; i++)
            {
                itemsArray[i] = items.ElementAt(i);
            }

            return itemsArray;
        }

        public static Item[] GetItemsWithLinq(List<Item> items)
        {
            return items.ToArray();
        }
        public static Item GetFirstItem(Player player)
        {
            if (player.Items.Any())
            {
                return player.Items[0];
            }
            else
            {
                return null;
            }
        }
        public static Item GetFirstItemLinq(Player player)
        {
            return player.Items.FirstOrDefault();
        }
        public static void ProcessEachItem(Player player, Action<Item> process)
        {
            for (int i = 0; i < player.Items.Count; i++)
            {
                process(player.Items[i]);
            }
        }
        public delegate void Del(Player player, Action<Item> process);
        public static void PrintItem(Item item)
        {
            Console.WriteLine("ID: " + item.Id.ToString() + " Level: " + item.Level.ToString());
        }
    }

    public static class PlayerExtensions
    {
        public static Item GetHighestLevelItem(this Player player)
        {
            Item highestLevelItem = new Item();
            highestLevelItem.Level = 0;

            for (int i = 0; i < player.Items.Count(); i++)
            {
                if (player.Items[i].Level >= highestLevelItem.Level)
                {
                    highestLevelItem = player.Items[i];
                }
            }
            return highestLevelItem;
        }
    }
}
