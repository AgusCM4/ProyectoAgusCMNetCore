using ProyectoAgusCMNetCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Repositories
{
    public class RepositoryApp
    {
        private MainContext context;

        public RepositoryApp(MainContext context)
        {
            this.context = context;
        }
    }
}
