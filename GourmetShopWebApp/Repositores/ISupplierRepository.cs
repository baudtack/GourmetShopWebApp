﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GourmetShopWebApp.Models;

namespace GourmetShopWebApp.Repositories
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync(); 
        Task<Supplier> GetSupplierByIdAsync(int id);        
        Task AddSupplierAsync(Supplier supplier);           
        Task UpdateSupplierAsync(Supplier supplier);         
        Task DeleteSupplierAsync(int id);
        Task<IEnumerable<Supplier>> SearchAsync(string searchTerm);
    }
}
