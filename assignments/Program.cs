using System;
using System.Linq;
using System.Threading.Tasks;

namespace assignments
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int bikeAmount = 0;
            string haku = args[0];

            if(args.Length > 2){
                for(int i = 1; i < args.Length - 1; i++){
                    haku += " " + args[i];
                }
            }
            
            //Console.WriteLine(args[0]);

            OfflineCityBikeDataFetcher offlinehaku = new OfflineCityBikeDataFetcher();
            RealTimeCityBikeDataFetcher nettihaku = new RealTimeCityBikeDataFetcher();
            if(args[args.Length - 1] == "offline"){
                try{
                    bikeAmount = await offlinehaku.GetBikeCountInStation(haku);
                    Console.WriteLine("Täällä pyöriä onpi: " + bikeAmount);
                    }
                    catch(ArgumentException ex){
                        Console.WriteLine("Invalid argument \n" + ex.Message);
                    }
                    catch(NotFoundException ex){
                        Console.WriteLine("Not found: \n" + ex.Message);
                    }
            }
            if(args[args.Length - 1] == "online"){
                    try{
                    bikeAmount = await nettihaku.GetBikeCountInStation(haku);
                    Console.WriteLine("Täällä pyöriä onpi: " + bikeAmount);
                    }
                    catch(ArgumentException ex){
                        Console.WriteLine("Invalid argument \n" + ex.Message);
                    }
                    catch(NotFoundException ex){
                        Console.WriteLine("Not found: \n" + ex.Message);
                    }
            }
            
            
            
            

            

            
        }
    }
}
