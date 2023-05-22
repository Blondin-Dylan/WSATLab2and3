using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [FormatFilter]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        


        [HttpGet("{format:alpha?}")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            return await _context.TodoItems.ToListAsync();
        }

 
        [HttpGet("{id:min(1)}/{format:alpha?}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(uint id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<TodoItem>> PatchToDoItem(uint id, TodoItem updatedData)
        {

            if (id != updatedData.TodoItemId)
            {
                return BadRequest();
            }

            _context.Entry(updatedData).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return updatedData;

        }

        [HttpPatch("{id:int}/{complete:bool}")]
        public async Task<ActionResult<TodoItem>> CompleteTodoItem(uint id, bool complete)
        {

            var todoObj = await _context.TodoItems.FindAsync(id); 

            if (todoObj == null) 
            {
                return NotFound();
            }
            
            if (todoObj.IsComplete == complete)
            {
                return NoContent();
            }

            todoObj.IsComplete = complete; 
            _context.Update(todoObj); 
            await _context.SaveChangesAsync(); 

            return todoObj;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(uint id, TodoItem todoItem)
        {
            if (id != todoItem.TodoItemId)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
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


        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            if (_context.TodoItems == null)
            {
                return Problem("Entity set 'TodoContext.TodoItems'  is null.");
            }
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { id = todoItem.TodoItemId }, todoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(uint id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(uint id)
        {
            return (_context.TodoItems?.Any(e => e.TodoItemId == id)).GetValueOrDefault();
        }
    }
}
