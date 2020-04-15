using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ToDo.Domain;
using ToDo.Domain.Models;
using ToDo.Services.Interfaces;
using ToDoApi.DTOs;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoItemController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ToDoItem>> GetToDoItemById(string id)
        {
            try
            {
                ObjectId toDoItemId = new ObjectId(id);

                var toDoItem = await _toDoService.GetToDoItem(toDoItemId);

                return Ok(toDoItem);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ToDoItem>>> GetToDoItem()
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserId").Value;

                var toDoItems = await _toDoService.GetToDoItems(userId);

                return Ok(toDoItems);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]ToDoItemCreateDto toDoItem)
        {
            try
            {
                string userIdFromUserManager = User.Claims.First(c => c.Type == "UserId").Value;

                var toDoItemToCreate = new ToDoItem
                {
                    Id = ObjectId.GenerateNewId(),
                    Text = toDoItem.Text,
                    Title = toDoItem.Title,
                    IsDone = toDoItem.IsDone,
                    UserId = userIdFromUserManager
                };

                _toDoService.AddToDoItem(toDoItemToCreate);

                return CreatedAtRoute(new
                    {
                        id = toDoItemToCreate.Id
                    }, 
                    toDoItemToCreate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
            
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<bool>> Put(string id, [FromBody]ToDoItemCreateDto toDo)
        {
            try
            {
                ObjectId toDoItemId = new ObjectId(id);

                var toDoItemFromRepo = await _toDoService.GetToDoItem(toDoItemId);

                var toDoToPut = new ToDoItem
                {
                    Id = toDoItemFromRepo.Id,
                    Text = toDo.Text,
                    Title = toDo.Title,
                    IsDone = toDo.IsDone,
                    UserId = toDoItemFromRepo.UserId
                };

                return await _toDoService.UpdateToDoItem(toDoItemId, toDoToPut);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            try
            {
                ObjectId toDoItemId = new ObjectId(id);

                return await _toDoService.DeleteToDoItem(toDoItemId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}