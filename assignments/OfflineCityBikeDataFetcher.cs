using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System;

public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
{
    int bikesAvailable;
    bool knownName;

    string[] stationList1;
    public async Task<int> GetBikeCountInStation(string stationName)
    {
        if (stationName.Any(char.IsDigit))
            {
                
                throw new System.ArgumentException();
            }
        
        //BikeRentalStationList stationList;
        //string url = "http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental";
        //HttpClient Client = new HttpClient();
        //stationList = JsonConvert.DeserializeObject<BikeRentalStationList>(await Client.GetStringAsync(url));
        knownName = false;
        string[] stationList = File.ReadAllLines(@"bikedata.txt");
        
        for(int i = 0; i < stationList.Length; i++)
        {
            stationList1 = stationList[i].Split(":");
            if(stationList1[0].Trim() == stationName)
            {
                bikesAvailable =  Int32.Parse(stationList1[1].Trim());
                knownName = true;
            }
        }
        if(!knownName)
        {
            throw new NotFoundException();
        }
        
        return bikesAvailable;
    }
    
}