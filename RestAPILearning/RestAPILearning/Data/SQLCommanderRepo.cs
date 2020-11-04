using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestAPILearning.Models;

namespace RestAPILearning.Data
{
    public class SQLCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public SQLCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int Id)
        {
            return _context.Commands.FirstOrDefault(p => p.ID == Id);
        }
    }
}
