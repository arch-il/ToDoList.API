using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Interfaces;
using ToDoList.API.Models;
using ToDoList.API.Models.ToDoModels.CreateModel;
using ToDoList.API.Models.ToDoModels.UpdateModel;
using ToDoList.API.Models.ToDoModels.ViewModel;

namespace ToDoList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoListController
    {
        private readonly IToDoListService _toDoListService;

        public ToDoListController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [HttpGet("[action]")]
        public async Task<CustomResponseModel<IEnumerable<ToDoViewModel>>> GetAll()
        {
            return await _toDoListService.GetAll();
        }

        [HttpPost("[action]")]
        public async Task<CustomResponseModel<bool>> Create([FromQuery] ToDoCreateModel toDoCreateModel)
        {
            return await _toDoListService.Create(toDoCreateModel);
        }


        [HttpPut("[action]")]
        public async Task<CustomResponseModel<ToDoViewModel>> Update([FromQuery] ToDoUpdateModel toDoUpdateModel)
        {
            return await _toDoListService.Update(toDoUpdateModel);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<CustomResponseModel<bool>> Delete(int id)
        {
            return await _toDoListService.Delete(id);
        }

        [HttpGet("[action]")]
        public async Task<CustomResponseModel<int>> GetCompletedTaskCount()
        {
            return await _toDoListService.GetCompletedTaskCount();
        }
    }
}
