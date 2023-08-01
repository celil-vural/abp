using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TodoApp
{
    public class TodoAppService : ApplicationService, ITodoAppService
    {
        private readonly IRepository<TodoItem, Guid> _todoItemRepository;

        public TodoAppService(IRepository<TodoItem, Guid> todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _todoItemRepository.DeleteAsync(id);
        }


        public async Task<List<TodoItemDto>> GetListAsync()
        {
            var items = await _todoItemRepository.GetListAsync();
            return ObjectMapper.Map<List<TodoItem>, List<TodoItemDto>>(items);
        }

        public async Task<TodoItemDto> CreateAsync(string text)
        {
            var todoItem = new TodoItem { Text = text };
            await _todoItemRepository.InsertAsync(todoItem);

            return ObjectMapper.Map<TodoItem, TodoItemDto>(todoItem);
        }

    }
}
