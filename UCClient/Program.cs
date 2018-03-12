using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UCClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:14749/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage result;
                
                Console.WriteLine("GET Users");
                result = await client.GetAsync("api/values");
                //result.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                {
                    /*возвращает количество объектов, но сами объекты в список не попадают.
                    В тырнетах пишут что в .NET Core 2 метод покромсали
                    //List<User> users = await result.Content.ReadAsAsync<List<User>>();  */
                    List<User> users = await result.Content.ReadAsJsonAsync<List<User>>();
                    /* Вынес в отдельный метод класаа json
                    string data = await result.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<IEnumerable<User>>(data);*/


                    foreach (User user in users)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", user.id, user.FirstName, user.middleName, user.LastName, user.birthDate);
                    }

                }


                Console.WriteLine("GET Cities for User");
                 result = await client.GetAsync("api/values/1");
                //result.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                {
                    /*возвращает количество объектов, но сами объекты в список не попадают.
                    В тырнетах пишут что в .NET Core 2 метод покромсали
                    //List<City> cities = await result.Content.ReadAsAsync<List<City>>(); */
                    List<City> cities = await result.Content.ReadAsJsonAsync<List<City>>();
                    /* Вынес в отдельный метод класаа json
                    //string data = await result.Content.ReadAsStringAsync();
                    //var cities = JsonConvert.DeserializeObject<IEnumerable<City>>(data);*/
                    foreach (City city in cities)
                    {
                        Console.WriteLine("{0}\t{1}", city.id, city.name);
                    }
                   
                }

                Console.WriteLine("POST User");

                User newUser = new User();
                newUser.birthDate = DateTime.Parse("02/03/1986");//Convert.ToDateTime("02/03/1986");
                newUser.FirstName = "Александр";
                newUser.middleName = "Сергеевич";
                newUser.LastName = "Постовый";

                result = await client.PostAsJsonAsync("api/values", newUser);
                if (result.IsSuccessStatusCode)
                {
                    \
                    Console.WriteLine("User added");
                }



                Console.Read();
                
            }


            }
    }
}
