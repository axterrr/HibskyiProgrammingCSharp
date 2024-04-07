using KMA.ProgrammingCSharp.HibskyiPractice4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.Repositories
{
    internal class FileRepository
    {
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CSKMAStorage");

        public FileRepository() 
        {
            if (!Directory.Exists(BaseFolder))
            {
                Directory.CreateDirectory(BaseFolder);

                string[] firstNames = ["Oleksandr", "Anastasia", "Maxym", "Victoria", "Dmytro", "Sophia", "John", "Catherine", 
                    "Oleh", "Mary", "Sergii", "Julia", "Artem", "Tetiana", "Volodymyr", "Iryna", "Helen", "Andrii", "Vitalii", "Natalia", 
                    "Paul", "Olga", "Victor", "Liudmyla", "Valentyn", "Ella", "Roman", "Anna", "Oksana", "Michael", "Larysa", "Anton",
                    "Yevhenia", "Oleksii", "Lily", "Eugene", "Tamara", "Arthur", "Hannah", "Ihor", "Tamila", "Vasyl", "Jane", 
                    "Petro", "Kamila", "Denys", "Ksenia", "Valentyna", "Vladyslav", "Martha"];

                string[] lastNames = ["Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", 
                    "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", "Clark", 
                    "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King", "Wright", "Lopez", "Hill", 
                    "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter", "Mitchell", "Perez", "Roberts", "Turner", 
                    "Phillips", "Campbell", "Parker", "Evans",  "Hibskyi", "Edwards"];

                Random ran = new Random();
                for(int i = 0; i<50; i++)
                    _ = AddOrUpdate(new DBPerson(Guid.NewGuid(), firstNames[i], lastNames[i], 
                        $"{char.ToLower(firstNames[i][0])}.{lastNames[i].ToLower()}@gmail.com", 
                        new DateTime(ran.Next(1900, 2023), ran.Next(1,13), ran.Next(1,29))));
            }
        }

        public async Task AddOrUpdate(DBPerson person)
        {
            string stringObj = JsonSerializer.Serialize(person);

            using(StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, person.Guid.ToString()), false)) 
            {
                await sw.WriteAsync(stringObj);
            }
        }

        public List<DBPerson> GetAll()
        {
            var res = new List<DBPerson>();

            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = sw.ReadToEnd();
                }

                res.Add(JsonSerializer.Deserialize<DBPerson>(stringObj));
            }

            return res;
        }

        public void Delete(Guid guid)
        {
            string filePath = Path.Combine(BaseFolder, guid.ToString());
            if (!File.Exists(filePath))
                return;
            File.Delete(filePath);
        }
    }
}
