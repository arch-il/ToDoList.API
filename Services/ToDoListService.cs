using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToDoList.API.Database.Context;
using ToDoList.API.Database.Entities;
using ToDoList.API.Interfaces;
using ToDoList.API.Models;
using ToDoList.API.Models.ToDoModels.CreateModel;
using ToDoList.API.Models.ToDoModels.UpdateModel;
using ToDoList.API.Models.ToDoModels.ViewModel;

namespace ToDoList.API.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly ILogger<ToDoListService> _logger;
        private readonly ToDoListContext db;

        public ToDoListService(ILogger<ToDoListService> logger, ToDoListContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public async Task<CustomResponseModel<bool>> Create(ToDoCreateModel toDoCreateModel)
        {
            try
            {
                var toDo = new ToDo()
                {
                    Name = toDoCreateModel.Name,
                    Description = toDoCreateModel.Description,
                    IsCompleted = toDoCreateModel.IsCompleted
                };

                await db.ToDos.AddAsync(toDo);
                await db.SaveChangesAsync();

                return new CustomResponseModel<bool>()
                {
                    StatusCode = 200,
                    Result = true
                };
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<bool>()
                {
                    StatusCode = 400,
                    ErrorMessage = ex.Message,
                    Result = false
                };
            }
        }

        public async Task<CustomResponseModel<bool>> Delete(int id)
        {
            try
            {
                if (id < 0)
                    return new CustomResponseModel<bool>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Invalid id",
                        Result = false
                    };

                var toDo = await db.ToDos.FirstOrDefaultAsync(x => x.ID == id);

                if (toDo == null)
                    return new CustomResponseModel<bool>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Not found in database",
                        Result = false
                    };

                db.ToDos.Remove(toDo);
                await db.SaveChangesAsync();

                return new CustomResponseModel<bool>()
                {
                    StatusCode = 200,
                    Result = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<bool>()
                {
                    StatusCode = 400,
                    ErrorMessage = ex.Message,
                    Result = false
                };
            }
        }

        public async Task<CustomResponseModel<IEnumerable<ToDoViewModel>>> GetAll()
        {
            try
            {
                var toDos = await db.ToDos.ToListAsync();

                var toDoViewModels = new List<ToDoViewModel>();

                foreach (var toDo in toDos)
                    toDoViewModels.Add(new ToDoViewModel()
                    {
                        ID = toDo.ID,
                        Name = toDo.Name,
                        Description = toDo.Description,
                        IsCompleted = toDo.IsCompleted
                    });
                
                return new CustomResponseModel<IEnumerable<ToDoViewModel>>()
                {
                    StatusCode = 200,
                    Result = toDoViewModels
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<IEnumerable<ToDoViewModel>>()
                {
                    StatusCode = 400,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<CustomResponseModel<int>> GetCompletedTaskCount()
        {
            try
            {
                // _logger.LogCritical($"{db.ToDos.FromSqlRaw("EXEC GetCompletedTaskCount").ToList()}");
                // couldnt figure out how to call procedure.
                return new CustomResponseModel<int>()
                {
                    StatusCode = 200,
                    Result = 0
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<int>()
                {
                    StatusCode = 400,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<CustomResponseModel<ToDoViewModel>> Update(ToDoUpdateModel toDoUpdateModel)
        {
            try
            {
                if (toDoUpdateModel.ID < 0)
                    return new CustomResponseModel<ToDoViewModel>()
                    {
                        StatusCode = 400,
                        ErrorMessage = "Invalid id"
                    };
                
                var toDo = new ToDo()
                {
                    ID = toDoUpdateModel.ID,
                    Name = toDoUpdateModel.Name,
                    Description = toDoUpdateModel.Description,
                    IsCompleted = toDoUpdateModel.IsCompleted
                };

                db.ToDos.Update(toDo);
                await db.SaveChangesAsync();

                var toDoViewModel = new ToDoViewModel()
                {
                    ID = toDoUpdateModel.ID,
                    Name = toDoUpdateModel.Name,
                    Description = toDoUpdateModel.Description,
                    IsCompleted = toDoUpdateModel.IsCompleted
                };

                return new CustomResponseModel<ToDoViewModel>()
                {
                    StatusCode = 200,
                    Result = toDoViewModel
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                return new CustomResponseModel<ToDoViewModel>()
                {
                    StatusCode = 400,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
