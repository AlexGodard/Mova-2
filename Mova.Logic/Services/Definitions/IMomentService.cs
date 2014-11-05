using Mova.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Services.Definitions
{
    public interface IMomentService
    {

        IList<Moment> RetrieveAll();

    }
}
