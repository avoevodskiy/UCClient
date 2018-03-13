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
            int userid;
            string fName, mName, lName, bDate;
            Console.WriteLine("Hello World!");
           
            GetUsers();
            Console.WriteLine("Введите ID пользователя ");
            userid = Int32.Parse(Console.ReadLine());
            GetUserCities(userid);

            Console.WriteLine("Введите фамилию пользователя ");
            lName = Console.ReadLine();
            Console.WriteLine("Введите имя пользователя ");
            fName = Console.ReadLine();
            Console.WriteLine("Введите отчество пользователя ");
            mName = Console.ReadLine();
            Console.WriteLine("Введите дату рождения пользователя ");
            bDate = Console.ReadLine();
            PostUser(fName, mName, lName, bDate);
            Console.Read();

        }

        static void GetUsers()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:14749/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            Console.WriteLine("GET Users");
            HttpResponseMessage result = client.GetAsync("api/values").Result;
            //result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                /*возвращает количество объектов, но сами объекты в список не попадают.
                В тырнетах пишут что в .NET Core 2 метод покромсали
                //List<User> users = await result.Content.ReadAsAsync<List<User>>();  */
                List<User> users = result.Content.ReadAsJsonAsync<List<User>>().Result;
                /* Вынес в отдельный метод класаа json
                string data = await result.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(data);*/


                foreach (User user in users)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", user.id, user.FirstName, user.middleName, user.LastName, user.birthDate);
                }

            }//if

        }//getusers

        static void GetUserCities(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:14749/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("GET Cities for User");
            var result = client.GetAsync("api/values/" + id).Result;
            //result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                /*возвращает количество объектов, но сами объекты в список не попадают.
                В тырнетах пишут что в .NET Core 2 метод покромсали
                //List<City> cities = await result.Content.ReadAsAsync<List<City>>(); */
                List<City> cities = result.Content.ReadAsJsonAsync<List<City>>().Result;
                /* Вынес в отдельный метод класаа json
                //string data = await result.Content.ReadAsStringAsync();
                //var cities = JsonConvert.DeserializeObject<IEnumerable<City>>(data);*/
                foreach (City city in cities)
                {
                    Console.WriteLine("{0}\t{1}", city.id, city.name);
                }

            }//if

        }//GetUserCities 


        static void PostUser(string fName, string mName, string lName, string bDate)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:14749/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Console.WriteLine("POST User");

            User newUser = new User();
            newUser.birthDate = DateTime.Parse(bDate);//Convert.ToDateTime("02/03/1986");
            newUser.FirstName = fName;
            newUser.middleName = mName;
            newUser.LastName = lName;

            var result = client.PostAsJsonAsync("api/values", newUser).Result;
            if (result.IsSuccessStatusCode)
            {

                Console.WriteLine("User added");
            }

        }//PostUser
    }

}



    
