using System;
using System.Collections.Generic;

namespace expensetracker2._0.Models;

public partial class Category
{
    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public ICollection<Budget> Budgets { get; set; }

    public ICollection<Expense> Expenses { get; set; }

  

}
//public Budget Budget { get; set; }