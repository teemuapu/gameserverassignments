using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;


public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
{
    int bikesAvailable;
    bool knownName;
    public async Task<int> GetBikeCountInStation(string stationName)
    {
        if (stationName.Any(char.IsDigit))
            {
                
                throw new System.ArgumentException();
            }
        
        BikeRentalStationList stationList;
        string url = "http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental";
        HttpClient Client = new HttpClient();
        stationList = JsonConvert.DeserializeObject<BikeRentalStationList>(await Client.GetStringAsync(url));
        knownName = false;
        for(int i = 0; i < stationList.stations.Count; i++)
        {
            if(stationList.stations[i].name == stationName)
            {
                bikesAvailable =  stationList.stations[i].bikesAvailable;
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