﻿using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IBoardRepository
{
    public Board PlayBoard(Board board);
}