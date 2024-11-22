﻿using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class BoardRepository(GameContext context) : IBoardRepository
{
    public Board PlayBoard(Board board)
    {
        context.Boards.Add(board);
        context.SaveChanges();
        return board;
    }
}