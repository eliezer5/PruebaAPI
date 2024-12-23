﻿using Shared.Models;

namespace PruebaAPI.Interfaces;

public interface IWarrantyService
{
    public Task<List<Garantias>> GetAsync();
    public Task<bool> AddAsync(Garantias garantia);
    public Task<Garantias> PutAsync(Garantias garantia);
    public Task<Garantias> DeleteAsync(int id);


}
