using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrototypeApi;
using PrototypeApi.DbModels;

namespace PrototypeApi.Pages
{
    public class MostPostModel : PageModel
    {
        private readonly PrototypeApi.ApiContext _context;

        public MostPostModel(PrototypeApi.ApiContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; }

        public async Task OnGetAsync()
        {
            Post = await _context.Posts
                .Include(p => p.User).ToListAsync();
        }
    }
}
