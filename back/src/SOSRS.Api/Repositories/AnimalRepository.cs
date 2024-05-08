using Microsoft.EntityFrameworkCore;
using SOSRS.Api.Data;
using SOSRS.Api.Entities;
using SOSRS.Api.Enums;

namespace SOSRS.Api.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AppDbContext db;

        public AnimalRepository(AppDbContext appDbContext)
        {
            db = 
                appDbContext 
                ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<Animal> CreateAsync(Animal entity)
        {
            var _entity = await db.Animais.AddAsync(entity);

            await db.SaveChangesAsync();

            return _entity.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var animal = await db.Animais.FirstOrDefaultAsync(s => s.Id == id);

            if(animal != null)
            {
                var result = db.Animais.Remove(animal);

                await db.SaveChangesAsync();

                return result.State == EntityState.Detached;
            }

            return false;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Animal> EditAsync(Animal entity)
        {
            var animal = await db.Animais.FirstOrDefaultAsync(s => s.Id == entity.Id);

            if (animal != null)
            {
                animal.Nome = entity.Nome;
                animal.Tipo = entity.Tipo;
                animal.IdadeAproximada = entity.IdadeAproximada;
                animal.Raca = entity.Raca;
                animal.Cor = entity.Cor;
                animal.Peso = entity.Peso;
                animal.Genero = entity.Genero;
                animal.DataDeEntrada = entity.DataDeEntrada;

                await db.SaveChangesAsync();

                return animal;
            }

            return new Animal();
        }

        public Task<IReadOnlyCollection<Animal>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Animal>> GetAnimalPorAbrigo(int abrigoId)
        {
            var animais = await db.Animais.Where(s => s.AbrigoId == abrigoId).ToListAsync();

            return animais;
        }

        public Task<Animal> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Animal> GetAsync(int id)
        {
            var animal = await db.Animais.FirstAsync(s => s.Id == id);

            return animal;
        }
    }
}
