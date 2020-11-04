using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPILearning.Data;
using RestAPILearning.DTOs;
using RestAPILearning.Models;

namespace RestAPILearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly CommanderContext _context;
        private readonly IMapper _mapper;

        public CommandsController(CommanderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: api/Commands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDTO>>> GetCommands()
        {
            var Commands = await _context.Commands.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(Commands));
        }

        // GET: api/Commands/5
        [HttpGet("{id}", Name = nameof(GetCommand))]
        public async Task<ActionResult<CommandReadDTO>> GetCommand(int id)
        {
            var command = await _context.Commands.FindAsync(id);

            if (command == null)
            {
                return NotFound();
            }

            var DtoObject = _mapper.Map<CommandReadDTO>(command);

            return DtoObject;
        }

        // PUT: api/Commands/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommand(int id, CommandUpdateDTO commandUpdateDTO)
        {

            //if (id != command.ID)
            //{
            //    return BadRequest();
            //}

            var command = _context.Commands.FirstOrDefault(c => c.ID == id);
            
            if (command == null )
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDTO, command);

            _context.Entry(command).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetCommand), new { command.ID }, command);
        }


        //Not scaffoled

         // Content body should be, 
         /*
          [
            {
                "op": "replace",
                "path": "/howto",
                "value": "Some value to be filled"
            }
          ]
             
          */


        //Patch: api/Commands/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialPutCommand(int Id, JsonPatchDocument<CommandUpdateDTO> patchDocument)
        {
            var command = _context.Commands.FirstOrDefault(c => c.ID == Id);
            
            if (command == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDTO>(command);

            patchDocument.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, command);

            _context.Entry(command).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // POST: api/Commands
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CommandCreateDTO>> PostCommand(CommandCreateDTO commandCreateDTO)
        {
            var command = _mapper.Map<Command>(commandCreateDTO);
            _context.Commands.Add(command);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCommand", new { id = command.ID }, command);
            return CreatedAtRoute(nameof(GetCommand), new { command.ID }, command);
        }

        // DELETE: api/Commands/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Command>> DeleteCommand(int id)
        {
            var command = await _context.Commands.FindAsync(id);
            if (command == null)
            {
                return NotFound();
            }

            _context.Commands.Remove(command);
            await _context.SaveChangesAsync();

            return command;
        }

        private bool CommandExists(int id)
        {
            return _context.Commands.Any(e => e.ID == id);
        }
    }
}
