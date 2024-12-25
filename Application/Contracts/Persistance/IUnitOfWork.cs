﻿namespace Application.Contracts.Persistance
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
