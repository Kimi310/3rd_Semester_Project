﻿using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class WinnersRepository(LotteryContext context) : IWinnersRepository
{
    public void AddWinners(List<Winner> winners)
    {
        context.Winners.AddRange(winners);
        context.SaveChanges();
    }
}