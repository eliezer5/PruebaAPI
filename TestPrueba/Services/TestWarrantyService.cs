﻿using Microsoft.EntityFrameworkCore;
using Shared.Models;
using PruebaAPI.Dal;
using PruebaAPI.Services;
using System.Threading.Tasks;
using Xunit;

namespace TestServices.Services
{
    public class TestWarrantyService : IDisposable
    {
        private readonly Context _context;
        private readonly WarrantyService _service;

        public TestWarrantyService()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("TestWarrantyDatabase")
                .Options;

            _context = new Context(options);
            _service = new WarrantyService(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnAllWarranties()
        {
            // Arrange
            _context.Garantias.Add(new Garantias { GarantiaId = 1, Descripcion = "Garantía 1" });
            _context.Garantias.Add(new Garantias { GarantiaId = 2, Descripcion = "Garantía 2" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddAsync_ShouldAddWarranty()
        {
            // Arrange
            var warranty = new Garantias { GarantiaId = 1, Descripcion = "Garantía Nueva" };

            // Act
            var result = await _service.AddAsync(warranty);

            // Assert
            Assert.True(result);
            Assert.Equal(1, await _context.Garantias.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveWarranty()
        {
            // Arrange
            var warranty = new Garantias { GarantiaId = 1, Descripcion = "Garantía para eliminar" };
            _context.Garantias.Add(warranty);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.Equal(0, await _context.Garantias.CountAsync());
            Assert.Equal(warranty, result);
        }

        [Fact]
        public async Task PutAsync_ShouldUpdateWarranty()
        {
            // Arrange
            var warranty = new Garantias { GarantiaId = 1, Descripcion = "Garantía Original" };
            _context.Garantias.Add(warranty);
            await _context.SaveChangesAsync();

            var updatedWarranty = new Garantias { GarantiaId = 1, Descripcion = "Garantía Actualizada" };

            // Act
            var result = await _service.PutAsync(updatedWarranty);

            // Assert
            var dbWarranty = await _context.Garantias.FindAsync(1);
            Assert.NotNull(dbWarranty);
            Assert.Equal(updatedWarranty.Descripcion, dbWarranty.Descripcion);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowKeyNotFoundException_WhenWarrantyNotFound()
        {
            // Arrange
            int invalidId = 999; // ID que no existe

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _service.DeleteAsync(invalidId);
            });
        }
    }
}
