﻿using DataAccess.Types.Enums;

namespace DataAccess.Models;

public partial class Transaction
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public string Transactionnumber { get; set; } = null!;
    
    public TransactionStatusA Transactionstatus { get; set; }

    public virtual User User { get; set; } = null!;
}
