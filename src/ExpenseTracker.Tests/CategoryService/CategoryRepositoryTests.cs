using CategoryService.Data;
using CategoryService.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Entities;

namespace ExpenseTracker.Tests.CategoryService
{
    public class CategoryRepositoryTests : IAsyncLifetime
    {
        private CategoryRepository _categoryRepository;

        public async Task InitializeAsync()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Tests.json")
                .AddEnvironmentVariables() 
                .Build();

            await DbInitializer.InitDb(config, "CategoryDbTests");
            await DB.DropCollectionAsync<Category>();
            _categoryRepository = new CategoryRepository();
        }

        public async Task DisposeAsync()
        {
            await DB.DropCollectionAsync<Category>();
        }

        [Fact]
        public async Task GetAllCategories_ShouldReturnCategories_WhenCategoriesExist()
        {
            // Arrange
            var category1 = new Category { ID = "1", Name = "Groceries" };
            var category2 = new Category { ID = "2", Name = "Electronics" };
            await category1.SaveAsync();
            await category2.SaveAsync();

            // Act
            var categories = await _categoryRepository.GetAllCategories();

            // Assert
            Assert.Equal(2, categories.Count());
            Assert.Contains(categories, c => c.Name == "Groceries");
            Assert.Contains(categories, c => c.Name == "Electronics");
        }

        [Fact]
        public async Task GetAllCategories_ShouldReturnEmptyList_WhenNoCategoriesExist()
        {
            // Act
            var categories = await _categoryRepository.GetAllCategories();

            // Assert
            Assert.Empty(categories);
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { ID = "1", Name = "Groceries" };
            await category.SaveAsync();

            // Act
            var result = await _categoryRepository.GetCategoryById("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Groceries", result.Name);
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Act
            var result = await _categoryRepository.GetCategoryById("nonexistent-id");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateCategory_ShouldSaveCategorySuccessfully()
        {
            // Arrange
            var category = new Category { ID = "1", Name = "Groceries" };

            // Act
            await _categoryRepository.CreateCategory(category);

            // Assert
            var savedCategory = await DB.Find<Category>().OneAsync("1");
            Assert.NotNull(savedCategory);
            Assert.Equal("Groceries", savedCategory.Name);
        }

        [Fact]
        public async Task CreateCategory_ShouldThrowException_WhenCategoryHasInvalidData()
        {
            // Arrange
            var category = new Category { ID = "1", Name = null }; // Invalid because Name is null

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _categoryRepository.CreateCategory(category));
        }
        
        [Fact]
        public async Task CreateCategory_ShouldNotCreateDuplicateCategory_WhenIDAlreadyExists()
        {
            // Arrange
            var category1 = new Category { ID = "1", Name = "Groceries" };
            await category1.SaveAsync();
            var category2 = new Category { ID = "1", Name = "Electronics" };

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _categoryRepository.CreateCategory(category2));

            // Assert
            Assert.Equal("Category with the same ID already exists.", exception.Message);
        }
    }
}