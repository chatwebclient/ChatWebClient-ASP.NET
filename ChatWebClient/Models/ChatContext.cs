using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChatWebClient.Models
{
    public class ChatContext : DbContext
    {
        public ChatContext() : base("MainDB")
        {
        }

        public static ChatContext Create()
        {
            return new ChatContext();
        }

        public DbSet<User> Users { get; set; }
    }
}