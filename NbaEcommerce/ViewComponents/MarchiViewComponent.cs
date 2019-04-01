using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NbaEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaEcommerce.ViewComponents
{



    public class MarchiViewComponent:ViewComponent
    {

        private readonly NbaStoreContext _context;

        public MarchiViewComponent(NbaStoreContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Marchio.ToListAsync());
        }

        // GET

    }


}
