using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestAPILearning.Models;

namespace RestAPILearning.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var Commands = new List<Command>
            {
                new Command(){ID = 0, HowTo = "Method 1", Line = "do as Method 1", Platform = "Platform 1"},
                new Command(){ID = 1, HowTo = "Method 2", Line = "do as Method 2", Platform = "Platform 2"},
                new Command(){ID = 2, HowTo = "Method 3", Line = "do as Method 3", Platform = "Platform 3"},
                new Command(){ID = 3, HowTo = "Method 4", Line = "do as Method 4", Platform = "Platform 4"}
            };

            return Commands; 
        }

        public Command GetCommandById(int Id)
        {
            return new Command()
            {
               ID = 0, HowTo = "Method 1", Line = "do as Method 1", Platform = "Platform 1"
            };
        }
    }
}
