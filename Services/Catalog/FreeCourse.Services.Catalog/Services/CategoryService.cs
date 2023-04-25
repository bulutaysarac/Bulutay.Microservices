using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(c => true).ToListAsync();
            if(categories.Any())
            {
                return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
            }
            return Response<List<CategoryDto>>.Fail($"Categories table is empty!", 404);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            await _categoryCollection.InsertOneAsync(_mapper.Map<Category>(categoryCreateDto));
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(categoryCreateDto), 200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(c => c.Id == id).SingleOrDefaultAsync();
            if(category == null)
            {
                return Response<CategoryDto>.Fail($"Category with id {id} is not found!", 404);
            }
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
