using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication1.Models;

namespace WebApplication1.Application
{
    public class DataStoreHelper
    {
        public void SaveFavourite(HttpRequestBase request,HttpResponseBase response,CharacterDetails fev)
        {
           

            HttpCookie fevouriteCookie = request.Cookies["FavouritesCookie"];
            if (fevouriteCookie == null)return;
                var favourite = fevouriteCookie?.Value;
            if (!string.IsNullOrEmpty(favourite))
            {
                var favIds = JsonConvert.DeserializeObject<List<int>>(favourite);
                if (favIds.Count < 5 )
                {
                    if(fev.IsVavourite&&favIds.Contains(fev.Id))return;
                    if (fev.IsVavourite && !favIds.Contains(fev.Id))
                    {
                        favIds.Add(fev.Id );
                    }
                    if(!fev.IsVavourite && favIds.Contains(fev.Id))
                        favIds.Remove(fev.Id);
                }
                else if ((favIds.Count == 5 && favIds.Contains(fev.Id) && !fev.IsVavourite))
                {
                    favIds.Remove(fev.Id);
                }
                var favIdsEdited = JsonConvert.SerializeObject(favIds);
                fevouriteCookie.Value = favIdsEdited;
                response.SetCookie(fevouriteCookie);

            }
            else 
            {
                fevouriteCookie.Value = JsonConvert.SerializeObject(new List<int>() {fev.Id});
                response.Cookies.Add(fevouriteCookie);
            }
           
        }
        public List<int> GetFavourite(HttpRequestBase request)
        {
            HttpCookie fevouriteCookie = request.Cookies["FavouritesCookie"];
            var favourite = fevouriteCookie?.Value;
            if (string.IsNullOrEmpty(favourite)) return new List<int>();

            var favIds = JsonConvert.DeserializeObject<List<int>>(favourite);
            return favIds;
        }

        public List<Character> FetchCharacters(HttpRequestBase request)
        {
            List<Character> items;
            var favourites = GetFavourite(request);
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri("https://swapi.co/api/");
                var data = http.GetStringAsync("people").Result;
                items = JObject.Parse(data)["results"].ToList().Select(t =>
                {   var item= new Character()
                    {
                        Url = t["url"].ToString(),
                        Gender = t["gender"].ToString(),
                        Name = t["name"].ToString(),
                        Height = Convert.ToInt32(t["height"].ToString()),
                        
                    };
                    item.IsVavourite = favourites.Contains(item.Id);
                    return item;
                }).ToList();

            }
            return items;
        }

        public CharacterDetails FetCharacterDetails(string id)
        {
            CharacterDetails details = new CharacterDetails();

            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri("https://swapi.co/api/");
                var data = http.GetStringAsync($"people/{id}").Result;
                details = JsonConvert.DeserializeObject<CharacterDetails>(data);
                ;
            }
            return details;

        }

        public void CreateCookie(HttpRequestBase request, HttpResponseBase response)
        {
            HttpCookie myCookie = request.Cookies["FavouritesCookie"];

            if (myCookie != null) return;
            myCookie = new HttpCookie("FavouritesCookie");
            myCookie.Expires = DateTime.Now.AddYears(50);
            response.Cookies.Add(myCookie);

        }
    }
}