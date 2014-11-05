using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models.Args
{
    public class InfoStylisteArgs
    {
        public int IdMoment { get; set; }
        public int IdStyle { get; set; }
        public int IdActivite { get; set; }

        public InfoStylisteArgs()
        {
            IdActivite = 0;
            IdMoment = 0;
            IdStyle = 0;
        }
    }
}
