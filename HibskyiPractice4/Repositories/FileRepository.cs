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

                Random ran = new Random();
                for(int i = 1; i<=50; i++)
                    _ = AddOrUpdate(new DBPerson(Guid.NewGuid(), $"FirstName{i}", $"LastName{i}", $"email{i}@gmail.com", 
                        new DateTime(ran.Next(1960, 2010), ran.Next(1,13), ran.Next(1,29))));
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

        public async Task<DBPerson> Get(Guid guid)
        {
            string stringObj = null;
            string filePath = Path.Combine(BaseFolder, guid.ToString());

            if (!File.Exists(filePath))
                return null;

            using (StreamReader sw = new StreamReader(filePath))
            {
                stringObj = await sw.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<DBPerson>(stringObj);
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
