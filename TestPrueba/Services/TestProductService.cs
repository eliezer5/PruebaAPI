﻿using Microsoft.EntityFrameworkCore;
using PruebaAPI.Dal;
using PruebaAPI.Services;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPrueba.Services
{
    public class TestProductService
    {
        private readonly Context _context;
        private readonly ProductServices _service;

        public TestProductService()
        {
            // Configuración de la base de datos en memoria
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new Context(options);
            _service = new ProductServices(_context);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnAllProducts()
        {
            // Arrange
            _context.Productos.Add(new Productos { ProductoId = 1, Descripcion = "Producto 1" });
            _context.Productos.Add(new Productos { ProductoId = 2, Descripcion = "Producto 2" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddAsync_ShouldAddProduct()
        {
            // Arrange
            var producto = new Productos { ProductoId = 1, Descripcion = "Producto Nuevo" };

            // Act
            var result = await _service.AddAsync(producto);

            // Assert
            Assert.True(result);
            Assert.Equal(1, await _context.Productos.CountAsync());
        }

        [Fact]
        public async Task PutAsync_ShouldUpdateProduct()
        {
            // Arrange
            var producto = new Productos { ProductoId = 1, Descripcion = "Producto Original" };
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            var updatedProduct = new Productos { ProductoId = 1, Descripcion = "Producto Actualizado" };

            // Act
            var result = await _service.PutAsync(updatedProduct);

            // Assert
            var dbPurchase = await _context.Productos.FindAsync(1);
            Assert.NotNull(dbPurchase);
            Assert.Equal(updatedProduct.Descripcion, dbPurchase.Descripcion);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveProduct()
        {
            // Arrange
            var producto = new Productos { ProductoId = 1, Descripcion = "Producto a Eliminar" };
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.Equal(producto.Descripcion, result.Descripcion);
            Assert.Equal(0, await _context.Productos.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowKeyNotFoundException_WhenProductNotFound()
        {
            // Arrange
            int invalidId = 999; // ID no existente

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _service.DeleteAsync(invalidId);
            });
        }
    }
}
