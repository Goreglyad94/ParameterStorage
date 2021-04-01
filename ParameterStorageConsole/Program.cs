using ParameterStorageConsole.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterStorageConsole
{
    class Program
    {
        static ParameterStorageDbContext parameterStorageDbContext = new ParameterStorageDbContext();
        static void Main(string[] args)
        {
            List<Group> groups = new List<Group>();
            groups.Add(new Group() 
            {
                Name = "Ранетки",
                Year = 1101
            });
            groups.Add(new Group()
            {
                Name = "Токиохотел",
                Year = 144
            });

            foreach (var item in AddGrops(groups))
            {

                Console.WriteLine(item.Name + " " + item.Id);
            }

        }
        public static List<Group> AddGrops(List<Group> groups)
        {
            parameterStorageDbContext.Groups.AddRange(groups);
            parameterStorageDbContext.SaveChanges();
            return groups;
        }
    }
}
