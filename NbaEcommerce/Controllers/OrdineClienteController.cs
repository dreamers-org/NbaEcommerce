using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NbaEcommerce.Models;
//using NbaEcommerce.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NbaEcommerce.Controllers
{
    public class OrdineClienteController : Controller
    {
        private readonly NbaStoreContext _context;

        public OrdineClienteController(NbaStoreContext context)
        {
            _context = context;
        }



        
    }
}
