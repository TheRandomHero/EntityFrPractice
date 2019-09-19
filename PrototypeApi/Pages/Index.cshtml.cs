using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrototypeApi.DbModels;

namespace PrototypeApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiContext _context;

        public List<User> Users { get; set; }
        public List<Post> Posts { get; set; }
        public IndexModel(ApiContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Users = _context.Users.ToList();
            Posts = _context.Posts.ToList();
        }
    }
}
