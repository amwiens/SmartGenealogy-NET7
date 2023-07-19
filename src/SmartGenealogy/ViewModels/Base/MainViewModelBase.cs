using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGenealogy.ViewModels.Base;

public class MainViewModelBase : ViewModelBase
{
    public virtual string? Title { get; set; }
}